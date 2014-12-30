using System;

namespace NEventSourceTests.Infrastructure
{
    public interface IEvent
    {
        Guid Id { get; set; }
        int Version { get; set; }
    }
}