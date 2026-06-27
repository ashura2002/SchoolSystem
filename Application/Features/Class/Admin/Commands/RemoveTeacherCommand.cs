using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Commands
{
    public record RemoveTeacherCommand(Guid ClassId);
}
