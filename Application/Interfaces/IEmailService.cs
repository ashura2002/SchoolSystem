using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(Guid enrollmentId, Guid studentId, Guid classId);
    }
}
