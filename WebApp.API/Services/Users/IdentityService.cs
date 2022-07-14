using WebApp.API.Commons;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.API.DTOs.Users;
using WebApp.Domain.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System;
using AutoMapper;

namespace WebApp.API.Services.Users
{
    public class IdentityService
    {
        private readonly ServiceConfiguration _appSettings;
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        private readonly TokenValidationParameters _tokenValidationParameters;
        public IdentityService(IOptions<ServiceConfiguration> settings,
            IUserManager userManager,
            IMapper mapper,
            TokenValidationParameters tokenValidationParameters)
        {
            _appSettings = settings.Value;
            _tokenValidationParameters = tokenValidationParameters;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task LogoutAsync(long userId)
        {
            await _userManager.DeleteRefreshToken(Convert.ToInt32(userId));
        }

        public async Task<ResponseModel<TokenModel>> LoginAsync(LoginModel login)
        {
            ResponseModel<TokenModel> response = new ResponseModel<TokenModel>();
            try
            {
                UserReponse user = _mapper.Map<User, UserReponse>(await _userManager.GetUserByUsernameAsync(login.UserName));
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid Username";
                    return response;
                }
                else
                {
                    if (user.Password != Security.GetMD5(login.Password, user.FirstSecurityString, user.LastSecurityString))
                    {
                        string stringpass = Security.GetMD5(login.Password, user.FirstSecurityString, user.LastSecurityString);
                        response.IsSuccess = false;
                        response.Message = "Invalid Password";
                        return response;
                    }
                }

                AuthenticationResult authenticationResult = await AuthenticateAsync(user);
                if (authenticationResult != null && authenticationResult.Success)
                {
                    response.Data = new TokenModel() { Token = authenticationResult.Token, RefreshToken = authenticationResult.RefreshToken };
                }
                else
                {
                    response.Message = "Something went wrong!";
                    response.IsSuccess = false;
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AuthenticationResult> AuthenticateAsync(UserReponse user)
        {
            // authentication successful so generate jwt token  
            AuthenticationResult authenticationResult = new AuthenticationResult();
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var key = Encoding.ASCII.GetBytes(_appSettings.JwtSettings.Secret);

                ClaimsIdentity Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("FirstName", user.Username),
                    new Claim("Email",user.EmailAddress==null?"":user.EmailAddress),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    });

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = Subject,
                    Expires = DateTime.UtcNow.Add(_appSettings.JwtSettings.TokenLifetime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                authenticationResult.Token = tokenHandler.WriteToken(token);
                var refreshToken = new RefreshToken
                {
                    Token = Guid.NewGuid().ToString(),
                    JwtId = token.Id,
                    UserId = user.Id,
                    CreateDate = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddMonths(6)
                };
                await _userManager.AddRefreshTokenAsync(refreshToken);
                authenticationResult.RefreshToken = refreshToken.Token;
                authenticationResult.Success = true;
                return authenticationResult;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<ResponseModel<TokenModel>> RefreshTokenAsync(TokenModel request)
        {
            ResponseModel<TokenModel> response = new ResponseModel<TokenModel>();
            try
            {
                var authResponse = await GetRefreshTokenAsync(request.Token, request.RefreshToken);
                if (!authResponse.Success)
                {

                    response.IsSuccess = false;
                    response.Message = string.Join(",", authResponse.Errors);
                    return response;
                }
                TokenModel refreshTokenModel = new TokenModel();
                refreshTokenModel.Token = authResponse.Token;
                refreshTokenModel.RefreshToken = authResponse.RefreshToken;
                response.Data = refreshTokenModel;
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong!";
                return response;
            }
        }

        private async Task<AuthenticationResult> GetRefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            if (validatedToken == null)
            {
                return new AuthenticationResult { Errors = new[] { "Invalid Token" } };
            }

            var expiryDateUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult { Errors = new[] { "This token hasn't expired yet" } };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _userManager.GetRefreshTokenByToken(refreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token does not exist" } };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has expired" } };
            }

            if (storedRefreshToken.Used && storedRefreshToken.Used == true)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has been used" } };
            }

            if (storedRefreshToken.JwtId.ToString() != jti)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token does not match this JWT" } };
            }

            await _userManager.UpdateUsedRefreshToken(storedRefreshToken ,true);
            string strUserId = validatedToken.Claims.Single(x => x.Type == "UserId").Value;
            long userId = 0;
            long.TryParse(strUserId, out userId);
            var user = _userManager.GetUserAsync(Convert.ToInt32(userId)).Result;
            if (user == null)
            {
                return new AuthenticationResult { Errors = new[] { "User Not Found" } };
            }

            return await AuthenticateAsync(_mapper.Map<User, UserReponse>(user));
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
