using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public UsernameValueObject Username { get; private set; }
        public EmailValueObject Email { get; private set; }
        public PasswordValueObject Password { get; private set; }
        public Role Role { get; private set; }
        public DateTime? DeletedAt { get; private set; }


        public User(UsernameValueObject username, EmailValueObject email, PasswordValueObject password,
            Role role)
        {
            Username = username;
            Email = email;
            Password = password;
            Role = role;
        }

        public void UpdateUsername(UsernameValueObject newUsername)
        {
            Username = newUsername;
            Touch();
        }

        public void UpdatePassword(PasswordValueObject newPassword)
        {
            Password = newPassword;
            Touch();
        }

        public void DeactivateAccount()
        {
            DeletedAt = DateTime.UtcNow;
            Touch();
        }

        public void ActivateAccount()
        {
            DeletedAt = null;
            Touch();
        }
    }
}
