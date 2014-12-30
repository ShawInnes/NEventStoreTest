using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommonDomain;
using CommonDomain.Persistence;
using Serilog;

namespace NEventSourceTests.Infrastructure
{
    public class AggregateFactory : IConstructAggregates
    {
        public IAggregate Build(Type type, Guid id, IMemento snapshot)
        {
            Log.Information("Constructing Aggregate {type}", type);

            var constructor = type.GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(Guid) }, null);

            IAggregate aggregate = constructor.Invoke(new object[] { id }) as IAggregate;
            
            return aggregate;
        }
    }
}
