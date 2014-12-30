using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEventSourceTests.DomainEvents;
using Serilog;

namespace NEventSourceTests.DomainAggregates
{
    public sealed class Customer : DomainBase
    {
        public double Balance { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public static Customer CreateCustomer(string firstName, string lastName)
        {
            return new Customer(Guid.NewGuid(), firstName, lastName);
        }
        
        private Customer(Guid id, string firstName, string lastName)
            : this(id)
        {
            var @event = new CustomerCreatedEvent
            {
                FirstName = firstName,
                LastName = lastName
            };

            RaiseDomainEvent(@event);
        }

        private Customer(Guid id)
        {
            Id = id;
        }

        public void BuyTicket(double value)
        {
            var @event = new BuyEvent
            {
                Value = value
            };
            RaiseDomainEvent(@event);
        }

        public void SellTicket(double value)
        {
            var @event = new SellEvent
            {
                Value = value
            };
            RaiseDomainEvent(@event);
        }

        private void Apply(CustomerCreatedEvent customer)
        {
            FirstName = customer.FirstName;
            LastName = customer.LastName;
        }

        private void Apply(SellEvent sell)
        {
            Balance += sell.Value;
        }

        private void Apply(BuyEvent buy)
        {
            Balance -= buy.Value;
        }

    }
}
