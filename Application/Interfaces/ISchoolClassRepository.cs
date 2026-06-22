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
        Task<List<SchoolClass>> GetAllClass(PaginationDTO pagination);
        Task<List<SchoolClass>> GetAllClassesWithoutTeacher(PaginationDTO pagination);
        Task<List<SchoolClass>> GetAllClassesWithTeacher(PaginationDTO pagination);
        Task<SchoolClass?> GetClassById(Guid id);
        Task DeleteClass(Guid id);
        Task SaveChangesClassAsync();

    }
}
