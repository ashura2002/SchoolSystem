using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IProfileRepository
    {
        void Add(Profile profile);
        void Remove(Profile profile);
    }
}
