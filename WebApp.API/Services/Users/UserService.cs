using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.API.Commons;
using WebApp.API.DTOs.Boards;
using WebApp.API.DTOs.Users;
using WebApp.Domain.Boards;
using WebApp.Domain.Interfaces;
using WebApp.Domain.Users;

namespace WebApp.API.Services.Users
{
    public class UserService: BaseService
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserManager userManager) : base(unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<UserReponse> GetUserByUsername(string username)
        {
            return _mapper.Map<User, UserReponse>(await _userManager.GetUserByUsernameAsync(username));
        }

        public async Task<UserReponse> GetUser(int userId)
        {
            return _mapper.Map<User, UserReponse>(await _userManager.GetUserAsync(userId));
        }

        public async Task AddUserAsync(AddUserRequest addUser)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                var user = _mapper.Map<AddUserRequest, User>(addUser);
                await _userManager.AddUserAsync(user);
                //await UnitOfWork.SaveEntitiesAsync();
           
                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task UpdateUserAsync(UpdateUserRequest updateUser, long userId)
        {
            try
            {
                await UnitOfWork.BeginTransaction();
                await _userManager.UpdateUserAsync(_mapper.Map<UpdateUserRequest, User>(updateUser), userId);

                await UnitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<GetUserReponse> GetUserAysnc(long usedId)
        {
            GetUserReponse getUserReponse = new GetUserReponse();
            var user = await _userManager.GetUserAsync(Convert.ToInt32(usedId));
            getUserReponse.User = _mapper.Map<User, AddUserResponse>(user);
            List<Board> boards = new List<Board>();
            foreach (var item in user.BoardMembers.ToList())
            {
                boards.Add(item.Board);
            }
            getUserReponse.Boards = _mapper.Map<List<Board>, List<BoardGetUserReponse>>(boards);
            return getUserReponse;
        }
    }
}
