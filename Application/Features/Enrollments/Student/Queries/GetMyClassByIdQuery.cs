using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Enrollments.Student.Queries
{
    public record GetMyClassByIdQuery(Guid EnrollmentId):IRequest<EnrollmentDetailsDTO>;


}
