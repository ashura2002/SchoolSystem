using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
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


        public async Task<LoginResponse> Execute(LoginDTO dto)
        {
            var user = await _userRepository.GetByUsername(dto.Username);
            if (user == null) throw new UserUnauthorizedException("Invalid Credentials");
            var isPasswordMatched = _passwordHasher.Verify(dto.Password, user.Password.Value);
            if (!isPasswordMatched) throw new UserUnauthorizedException("Invalid Credentials");

            var accessToken = _jwtService.GenerateToken(user);

            return new LoginResponse
            {
                Token = accessToken
            };
        }

    }
}
