using NEventSourceTests.DomainEvents;
using NEventSourceTests.Infrastructure;
using Serilog;

namespace NEventSourceTests.EventHandlers
{
    public class SellEventHandler : IEventHandler<SellEvent>
    {
        public void Handle(SellEvent @event)
        {
            Log.Information("SellEventHandler {@event}", @event);
        }
    }
}