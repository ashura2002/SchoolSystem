using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Enrollments.Admin.Commands
{
    public record RejectEnrollmentCommand(Guid EnrollmentId);
}
