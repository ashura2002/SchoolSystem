using Domain.Exceptions;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Domain.Entities
{
    public class Profile : BaseEntity
    {
        // FK
        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;
        public FirstNameVO FirstName { get; private set; }
        public LastNameVO LastName { get; private set; }
        public AddressVO Address { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public string? ProfilePictureUrl { get; private set; }
        public string? ProfilePicturePublicId { get; private set; }

        private Profile(
            Guid userId,
            FirstNameVO firstName,
            LastNameVO lastName,
            AddressVO address,
            DateOnly dateOfBirth,
            string? profilePictureUrl = null,
            string? profilePicturePublicId = null)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            DateOfBirth = dateOfBirth;
            ProfilePictureUrl = profilePictureUrl;
            ProfilePicturePublicId = profilePicturePublicId;
        }

        internal static Profile Create(
            Guid userId,
            FirstNameVO firstName,
            LastNameVO lastName,
            AddressVO address,
            DateOnly dateOfBirth,
            string? profilePictureUrl = null,
            string? profilePicturePublicId = null)
        {
            if (dateOfBirth > DateOnly.FromDateTime(DateTime.Today))
                throw new DomainBadRequestException("Date of birth cannot be in the future.");

            return new Profile(
                userId, 
                firstName, 
                lastName, 
                address, 
                dateOfBirth, 
                profilePictureUrl,
                profilePicturePublicId);
        }

        internal void UpdateProfilePicture(string profilePictureUrl, string profilePicturePublicId)
        {
            if (ProfilePictureUrl == profilePictureUrl) return;

            ProfilePictureUrl = profilePictureUrl;
            ProfilePicturePublicId = profilePicturePublicId;
            Touch();
        }

        internal void UpdateFirstName(FirstNameVO firstName)
        {
            if (FirstName == firstName) return;

            FirstName = firstName;
            Touch();
        }

        internal void UpdateLastName(LastNameVO lastName)
        {
            if (LastName == lastName) return;

            LastName = lastName;
            Touch();
        }

        internal void UpdateAddress(AddressVO address)
        {
            if (Address == address) return;

            Address = address;
            Touch();
        }

        internal void UpdateDateOfBirth(DateOnly date)
        {
            if (date > DateOnly.FromDateTime(DateTime.Today))
                throw new DomainBadRequestException("Date of birth cannot be in the future.");

            if (DateOfBirth == date) return;

            DateOfBirth = date;
            Touch();
        }
    }
}
