using Application.Interfaces;
using Domain.Exceptions;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Users.Commands
{
    public class UpdateUserHandler:IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken) ??
                        throw new DomainNotFoundException("User not found");
            var hashedPassword = _passwordHasher.Hash(request.Password);
            user.UpdateUsername(UsernameVO.Create(request.Username));
            user.UpdatePassword(PasswordVO.Create(hashedPassword));

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
