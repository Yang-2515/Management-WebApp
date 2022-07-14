using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Domain.Users
{
    public class UserManager: DomainService, IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshToken _refreshTokenRepository;
        public UserManager(IUserRepository userRepository,
            IRefreshToken refreshTokenRepository)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            await _refreshTokenRepository.AddAsync(refreshToken);
        }

        public async Task AddUserAsync(User user)
        {
            user.SetSecurity();
            var result = await _userRepository.AddAsync(user);
            result.AddBoard();
        }

        public async Task DeleteRefreshToken(int userId)
        {
            var refreshToken = await _refreshTokenRepository.FindAsync(userId);
            await _refreshTokenRepository.DeleteAsync(refreshToken);
        }

        public async Task<RefreshToken> GetRefreshTokenByToken(string refreshToken)
        {
            return await _refreshTokenRepository.GetAsync(c => c.Token == refreshToken);
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _userRepository.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetAsync(c => c.Username == username);
        }

        public async Task UpdateUsedRefreshToken(RefreshToken refreshToken, bool used)
        {
            var user = await _userRepository.FindAsync(refreshToken.UserId);
            user.UpdateUsedRefreshToken(refreshToken.Id, used);
        }

        public async Task UpdateUserAsync(User updateUser, long userId)
        {
            var user = await _userRepository.FindAsync(Convert.ToInt32(userId));
            user.Update(updateUser.Name, updateUser.LinkImage, updateUser.Age, updateUser.EmailAddress, updateUser.HomeAddress, updateUser.Phone, updateUser.Gender);
        }
    }
}
