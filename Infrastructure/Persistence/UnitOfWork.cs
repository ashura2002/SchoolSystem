using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
    public class UnitOfWork(AppDbContext context, IDomainEventDispatcher dispatcher) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;
        private readonly IDomainEventDispatcher _dispatcher = dispatcher;

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

            // Get all tracked aggregate roots.
            var aggregateEntries = _context.ChangeTracker
                .Entries<AggregateRoot>()
                .ToList();

            // Collect all domain events from the tracked aggregates.
            var domainEvents = aggregateEntries
                .SelectMany(entry => entry.Entity.DomainEvents)
                .ToList();

            // Dispatch all collected domain events.
            await _dispatcher.DispatchAsync(domainEvents, cancellationToken);

            // Clear dispatched events to prevent duplicate dispatching.
            foreach (var entry in aggregateEntries)
            {
                entry.Entity.ClearEvents();
            }
        }
    }
}
