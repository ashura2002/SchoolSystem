using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Mapper
{
    public class UserMapper
    {
        public static UserDTO ToDto(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username.Value,
                Email = user.Email.Value,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                DeletedAt = user.DeletedAt
            };
        }

        public static List<UserDTO> ToResponseList(List<User> users)
        {
            var result = users.Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username.Value,
                Email = u.Email.Value,
                Role = u.Role,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                DeletedAt = u.DeletedAt
            }).ToList();
            return result;
        }

    }
}
