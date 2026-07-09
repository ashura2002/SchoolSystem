using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class AggregateRoot : BaseEntity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        // expose the collection as a read only
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


        public void RaiseEvent(IDomainEvent domainEvent)
        {
            Console.WriteLine("FROM Aggregate root event Added...");
            _domainEvents.Add(domainEvent);
        }

        public void ClearEvents()
        {
            Console.WriteLine("FROM Aggregate root events cleared...");
            _domainEvents.Clear();
        }
    }
}
