using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDomain;
using CommonDomain.Core;
using NEventSourceTests.Infrastructure;

namespace NEventSourceTests.DomainAggregates
{
    public class DomainBase : AggregateBase
    {
        public virtual void RaiseDomainEvent(IEvent @event)
        {
            @event.Id = Id;
            @event.Version = Version;
            RaiseEvent(@event);
        }
    }
}
