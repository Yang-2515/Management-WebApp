using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Domain.Boards;
using WebApp.Domain.Interfaces;
using WebApp.Domain.IRepositories;
using WebApp.Domain.Users;

namespace WebApp.API.Handlers.Users
{
    public class AddUserEventHanlder : INotificationHandler<AddUserEvent>
    {
        private readonly IBoardManager _boardManager;
        private readonly IUnitOfWork _unitOfWork;
        public AddUserEventHanlder(IBoardManager boardManager,
            IUnitOfWork unitOfWork)
        {
            _boardManager = boardManager;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(AddUserEvent notification, CancellationToken cancellationToken)
        {
            var user = notification.User;
            await _boardManager.AddBoardAsync(new Board("MainBoard", "DemoBoard", user.Id), user.Id);
            await _unitOfWork.SaveEntitiesAsync();
        }
    }
}
