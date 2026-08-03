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
            return new UserDTO(
                user.Id,
                user.Username.Value,
                user.Email.Value,
                user.Role,
                user.CreatedAt,
                user.UpdatedAt,
                user.DeletedAt
            );
        }

        public static List<UserDTO> ToResponseList(List<User> users)
        {
            var result = users.Select(u => new UserDTO
            (
                u.Id,
                u.Username.Value,
                u.Email.Value,
                u.Role,
                u.CreatedAt,
                u.UpdatedAt,
                u.DeletedAt
            )).ToList();
            return result;
        }

    }
}
