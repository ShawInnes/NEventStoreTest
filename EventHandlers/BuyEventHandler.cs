using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEventSourceTests.DomainEvents;
using NEventSourceTests.Infrastructure;
using Serilog;

namespace NEventSourceTests.EventHandlers
{
    public class BuyEventHandler : IEventHandler<BuyEvent>
    {
        public void Handle(BuyEvent @event)
        {
            Log.Information("BuyEventHandler {@event}", @event);
        }
    }
}
