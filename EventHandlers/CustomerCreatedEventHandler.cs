using NEventSourceTests.DomainEvents;
using NEventSourceTests.Infrastructure;
using Serilog;

namespace NEventSourceTests.EventHandlers
{
    public class CustomerCreatedEventHandler : IEventHandler<CustomerCreatedEvent>
    {
        public void Handle(CustomerCreatedEvent @event)
        {
            Log.Information("CustomerCreatedEventHandler {@event}", @event);
        }
    }
}