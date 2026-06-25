using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Auth
{
    public class LoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher,
            IJwtService jwtService
            )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }


        public async Task<string> Execute(LoginDTO dto,CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsername(dto.Username, cancellationToken) ??
                throw new DomainUnauthorizedException("Invalid Credentials");
            var isPasswordMatch = _passwordHasher.Verify(dto.Password, user.Password.Value);
            if (!isPasswordMatch) throw new DomainUnauthorizedException("Wrong Password");
            return _jwtService.GenerateToken(user);
        }

    }
}
