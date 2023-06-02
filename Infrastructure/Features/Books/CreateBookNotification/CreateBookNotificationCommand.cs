using Application.Mediator;
using Domain.Entities;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Infrastructure.Features.Books.CreateBookNotification
{

    public class CreateBookNotificationCommand : ICommand<Result>
    {
        public CreateBookNotificationCommand(string userName, CreateBookNotificationRequest payload)
        {
            UserName = userName;
            Payload = payload;
        }

        public string UserName { get; set; }
        public CreateBookNotificationRequest Payload { get; set; }
    }

    internal sealed class CreateBookNotificationCommandHandler : ICommandHandler<CreateBookNotificationCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly UnitOfWork _unitOfWork;

        public CreateBookNotificationCommandHandler(UserManager<User> userManager, UnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateBookNotificationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            var notificationRequest = new NotificationRequest
            {
                BookId = request.Payload.BookId,
                CustomerId = user.Id,
                NotificationEventType = request.Payload.EventType
            };

            _unitOfWork.NotificationRequest.Add(notificationRequest);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.SUCCESS;
        }
    }
}
