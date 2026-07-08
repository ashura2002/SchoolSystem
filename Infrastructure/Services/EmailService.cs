using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmail(Guid enrollmentId, Guid studentId, Guid classId)
        {
            Console.WriteLine($"Email service implemented {enrollmentId} {studentId} {classId}");
            return Task.CompletedTask;
        }
    }
}
