using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Class.Admin.Commands
{
    public record CreateSchoolClassCommand(string Name, TimeOnly StartTime, TimeOnly EndTime, DayOfWeek Schedule, int StudentCapacity);
}
