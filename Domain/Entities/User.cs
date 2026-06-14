using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public UsernameValueObject Username { get; private set; }
        public EmailValueObject Email { get; private set; }
        public PasswordValueObject Password { get; private set; }
        public Role Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public User(UsernameValueObject username, EmailValueObject email, PasswordValueObject password, Role role)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            Password = password;
            Role = role;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateUsername(UsernameValueObject newUsername)
        {
            Username = newUsername;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePassword(PasswordValueObject newPassword)
        {
            Password = newPassword;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
