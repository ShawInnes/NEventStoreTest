using System;
using NEventSourceTests.Infrastructure;

namespace NEventSourceTests.DomainEvents
{
    public class CustomerCreatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}