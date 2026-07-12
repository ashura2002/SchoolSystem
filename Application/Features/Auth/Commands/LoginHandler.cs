using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands
{
    public class LoginHandler
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


        public async Task<string> Handle(LoginCommand dto,CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(dto.Username, cancellationToken) ??
                throw new DomainUnauthorizedException("Invalid Credentials");
            var isPasswordMatch = _passwordHasher.Verify(dto.Password, user.Password.Value);
            if (!isPasswordMatch) throw new DomainUnauthorizedException("Wrong Password");
            return _jwtService.GenerateToken(user);
        }

    }
}
