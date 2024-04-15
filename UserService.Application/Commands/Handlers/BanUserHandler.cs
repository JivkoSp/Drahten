﻿using UserService.Application.Exceptions;
using UserService.Application.Services.ReadServices;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.Repositories;

namespace UserService.Application.Commands.Handlers
{
    internal sealed class BanUserHandler : ICommandHandler<BanUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;
        private readonly IBannedUserFactory _bannedUserFactory;

        public BanUserHandler(IUserRepository userRepository, IUserReadService userReadService, IBannedUserFactory bannedUserFactory)
        {
            _userRepository = userRepository;
            _userReadService = userReadService;
            _bannedUserFactory = bannedUserFactory;
        }

        public async Task HandleAsync(BanUserCommand command)
        {
            var issuer = await _userRepository.GetUserByIdAsync(command.IssuerUserId);

            if (issuer == null)
            {
                throw new UserNotFoundException(command.IssuerUserId);
            }

            var receiverExists = await _userReadService.ExistsByIdAsync(command.ReceiverUserId);

            if(receiverExists == false)
            {
                throw new UserNotFoundException(command.ReceiverUserId);
            }

            var banUser = _bannedUserFactory.Create(command.ReceiverUserId);

            issuer.BanUser(banUser);

            await _userRepository.UpdateUserAsync(issuer);
        }
    }
}
