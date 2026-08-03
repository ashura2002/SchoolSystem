using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ISchoolClassReadRepository
    {

        Task<List<SchoolClassDTO>> GetAllClassAsync(
            int Page, 
            int PageSize, 
            CancellationToken cancellationToken);

        Task<List<SchoolClassDTO>> GetAllClassesWithoutTeacherAsync(
            int Page, 
            int PageSize, 
            CancellationToken cancellationToken);

        Task<List<SchoolClassDTO>> GetAllClassesWithTeacherAsync(
            int Page, 
            int PageSize, 
            CancellationToken cancellationToken);

        Task<List<SchoolClassDTO>> GetOwnClassesAsync(
            int Page, 
            int PageSize, 
            Guid teacherId, 
            CancellationToken cancellationToken);

        Task<List<SchoolClassDTO>> GetClassesByIdsAsync(
            IEnumerable<Guid> schoolId, 
            CancellationToken cancellationToken);

        Task<bool> IsTeacherAvailableAsync(
            Guid teacherId,
            DayOfWeek schedule,
            TimeOnly startTime,
            TimeOnly endTime,
            CancellationToken cancellationToken
            );
    }
}
