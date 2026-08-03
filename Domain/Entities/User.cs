using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public UsernameVO Username { get; private set; }
        public EmailVO Email { get; private set; }
        public PasswordVO Password { get; private set; }
        public Role Role { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public Profile? Profile { get; private set; }

        private User(UsernameVO username, EmailVO email, PasswordVO password,
        Role role)
        {
            Username = username;
            Email = email;
            Password = password;
            Role = role;
        }

        // factory method
        public static User Register(UsernameVO username, EmailVO email, PasswordVO password,
        Role role)
        {
            User user = new(username, email, password, role);
            return user;
        }

        //  Aggregate Root protects the consistency of its aggregate.
        //  Profile creation must always go through the User aggregate root.
        public void CreateProfile(
            FirstNameVO firstName,
            LastNameVO lastName,
            AddressVO address,
            DateOnly dateOfBirth)
        {
            if (DeletedAt != null)
                throw new DomainBadRequestException("Cannot create profile for deactivated account.");

            if (Profile is not null)
                throw new DomainBadRequestException("Profile already exists.");
            Profile = Profile.Create(
                Id,
                firstName,
                lastName,
                address,
                dateOfBirth);
        }


        // aggregate root will decide if child entity allow to perform its responsibility 
        public void UpdateProfile(
            FirstNameVO firstName,
            LastNameVO lastName,
            AddressVO address,
            DateOnly dateOfBirth)
        {
            if (DeletedAt != null)
                throw new DomainBadRequestException(
                    "Cannot update profile of a deactivated account.");

            if (Profile is null)
                throw new DomainBadRequestException("Profile does not exist.");

            Profile.UpdateFirstName(firstName);
            Profile.UpdateLastName(lastName);
            Profile.UpdateAddress(address);
            Profile.UpdateDateOfBirth(dateOfBirth);
            Touch();
        }

        public void UpdateProfilePicture(string profilePictureUrl, string profilePicturePublicId)
        {
            if (DeletedAt != null)
                throw new DomainBadRequestException(
                    "Cannot update profile of a deactivated account.");

            if (Profile is null)
                throw new DomainBadRequestException("Profile does not exist.");

            Profile.UpdateProfilePicture(profilePictureUrl, profilePicturePublicId);
            Touch();
        }


        public void UpdateUsername(UsernameVO newUsername)
        {
            if (Username == newUsername) return;

            Username = newUsername;
            Touch();
        }

        public void UpdatePassword(PasswordVO newPassword)
        {
            if (Password == newPassword) return;

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
