using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public class TeacherRemovedFromClassDomainEvent(Guid teacherId, string className):IDomainEvent
    {
        public Guid TeacherId { get; private set; } = teacherId;
        public string ClassName{ get; private set; } = className;
    }
}
