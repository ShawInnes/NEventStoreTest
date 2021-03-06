﻿using System;
using NEventSourceTests.Infrastructure;

namespace NEventSourceTests.DomainEvents
{
    public class BuyEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public double Value { get; set; }
    }
}