using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CommonDomain;
using CommonDomain.Core;
using CommonDomain.Persistence;
using CommonDomain.Persistence.EventStore;
using NEventSourceTests.Infrastructure;
using NEventSourceTests.Pipeline;
using NEventSourceTests.Services;
using NEventStore;
using NEventStore.Logging;
using NEventStore.Persistence.Sql.SqlDialects;

namespace NEventSourceTests.AutofacModules
{
    public class EventStoreModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(CreateEventStore)
               .As<IStoreEvents>().SingleInstance();

            builder.RegisterType<CommitDispatcher>()
                .As<ICommitDispatcher>()
                .SingleInstance();

            builder.RegisterType<ConflictDetector>()
                .As<IDetectConflicts>()
                .SingleInstance();

            builder.RegisterType<AggregateFactory>()
                .As<IConstructAggregates>()
                .SingleInstance();

            builder.RegisterType<CommitDispatchingPipelineHook>()
                .As<IPipelineHook>()
                .SingleInstance();

            builder.RegisterType<AuthorizationPipelineHook>()
                .As<IPipelineHook>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(p => p.FullName.Contains(".EventHandlers") && p.Name.EndsWith("EventHandler"))
                .AsImplementedInterfaces();

            builder.RegisterType<SerilogLogger>()
                .As<ILog>();

            builder.RegisterType<EventStoreRepository>()
                .As<IRepository>();

            builder.RegisterType<CustomerService>()
                .As<ICustomerService>();
        }

        private static IStoreEvents CreateEventStore(IComponentContext ctx)
        {
            var log = ctx.Resolve<ILog>();
            var hooks = ctx.Resolve<IEnumerable<IPipelineHook>>().ToArray();

            IStoreEvents store = Wireup.Init()
                .LogTo(p => log)
                .UsingInMemoryPersistence()
                .UsingSqlPersistence("SqlServer")
                .WithDialect(new MsSqlDialect())
                //.InitializeStorageEngine()
                .EnlistInAmbientTransaction()
                .UsingJsonSerialization()
                .HookIntoPipelineUsing(hooks)
                .Build();

            return store;
        }
    }
}
