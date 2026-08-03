using Application.Interfaces;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands
{
    public class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginHandler(IUserRepository userRepository, IPasswordHasher passwordHasher,
            IJwtService jwtService
            )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(command.Username, cancellationToken) ??
                throw new DomainUnauthorizedException("User not found");
            var isPasswordMatch = _passwordHasher.Verify(command.Password, user.Password.Value);
            if (!isPasswordMatch) throw new DomainUnauthorizedException("Invalid credentials");
            return _jwtService.GenerateToken(user);
        }

    }
}
