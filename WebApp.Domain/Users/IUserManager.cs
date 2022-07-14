using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Domain.Users
{
    public interface IUserManager : IDomainService
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserAsync(int id);
        Task UpdateUserAsync(User updateUser, long userId);
        Task AddUserAsync(User user);
        Task DeleteRefreshToken(int userId);
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        Task UpdateUsedRefreshToken(RefreshToken refreshToken, bool used);
        Task<RefreshToken> GetRefreshTokenByToken(string refreshToken);
    }
}
