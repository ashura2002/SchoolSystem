using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ISchoolClassRepository
    {

        Task Add(SchoolClass schoolClass);
        Task<List<SchoolClass>> GetAllClass(int Page, int PageSize, CancellationToken cancellationToken);
        Task<List<SchoolClass>> GetAllClassesWithoutTeacher(int Page, int PageSize, CancellationToken cancellationToken);
        Task<List<SchoolClass>> GetAllClassesWithTeacher(int Page, int PageSize, CancellationToken cancellationToken);
        Task<List<SchoolClass>> GetOwnClasses(int Page, int PageSize, Guid teacherId, CancellationToken cancellationToken);
        Task<SchoolClass?> GetClassById(Guid id, CancellationToken cancellationToken);
        Task<List<SchoolClass>> GetClassesByIds(IEnumerable<Guid> schoolId, CancellationToken cancellationToken);
        void Remove(SchoolClass schoolClass);
    }
}
