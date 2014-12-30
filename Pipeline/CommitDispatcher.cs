using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;
using NEventSourceTests.Infrastructure;
using Serilog;

namespace NEventSourceTests.Pipeline
{
    public class CommitDispatcher : ICommitDispatcher
    {
        private readonly ILifetimeScope scope;

        public CommitDispatcher(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public void Dispatch(IEvent commit)
        {
            var eventHandlerEnumerableType = CreateEventHandlerEnumerableType(commit);
            var handlers = (IEnumerable)scope.Resolve(eventHandlerEnumerableType);
            foreach (var handler in handlers)
            {
                (handler as dynamic).Handle(commit as dynamic);
            }

            Log.Information("Object type {type} version: {version} dispatched", commit.GetType(), commit.Version);
        }

        private static Type CreateEventHandlerEnumerableType<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var genericEnumerableType = typeof(IEnumerable<>);
            var genericHandlerType = typeof(IEventHandler<>);
            var eventHandlerType = genericHandlerType.MakeGenericType(@event.GetType());
            var eventHandlerEnumerableType = genericEnumerableType.MakeGenericType(eventHandlerType);
            return eventHandlerEnumerableType;
        }
    }
}