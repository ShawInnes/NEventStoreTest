using System;
using CommonDomain;

namespace NEventSourceTests.DomainAggregates
{
    public class CustomerSnapshot : IMemento
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Balance { get; set; }
    }
}