using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ISchoolClassRepository
    {

        Task AddClass(SchoolClass schoolClass);
        Task<List<SchoolClass>> GetAllClass(PaginationDTO pagination,CancellationToken cancellationToken);
        Task<List<SchoolClass>> GetAllClassesWithoutTeacher(PaginationDTO pagination, CancellationToken cancellationToken);
        Task<List<SchoolClass>> GetAllClassesWithTeacher(PaginationDTO pagination, CancellationToken cancellationToken);
        Task<List<SchoolClass>> GetOwnClasses(PaginationDTO pagination, Guid teacherId, CancellationToken cancellationToken);
        Task<SchoolClass?> GetClassById(Guid id, CancellationToken cancellationToken);
        void DeleteClass(SchoolClass schoolClass);
        Task SaveChangesClassAsync(CancellationToken cancellationToken);
    }
}
