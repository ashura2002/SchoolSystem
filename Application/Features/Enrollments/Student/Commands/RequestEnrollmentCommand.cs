using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Enrollments.Student.Commands
{
    public record RequestEnrollmentCommand(Guid ClassId):IRequest<Guid>;
}
