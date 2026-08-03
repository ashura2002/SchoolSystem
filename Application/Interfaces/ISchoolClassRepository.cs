using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ISchoolClassRepository
    {
        void Add(SchoolClass schoolClass);
        Task<SchoolClass?> GetClassByIdAsync(Guid id, CancellationToken cancellationToken);
        void Remove(SchoolClass schoolClass);
    }
}
