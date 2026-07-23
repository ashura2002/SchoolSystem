using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ISchoolClassRepository
    {

        void Add(SchoolClass schoolClass);
        Task<List<SchoolClass>> GetAllClassAsync(int Page, int PageSize, CancellationToken cancellationToken);
        Task<List<SchoolClass>> GetAllClassesWithoutTeacherAsync(int Page, int PageSize, CancellationToken cancellationToken);
        Task<List<SchoolClass>> GetAllClassesWithTeacherAsync(int Page, int PageSize, CancellationToken cancellationToken);
        Task<List<SchoolClass>> GetOwnClassesAsync(int Page, int PageSize, Guid teacherId, CancellationToken cancellationToken);
        Task<SchoolClass?> GetClassByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<SchoolClass>> GetClassesByIdsAsync(IEnumerable<Guid> schoolId, CancellationToken cancellationToken);
        void Remove(SchoolClass schoolClass);
    }
}
