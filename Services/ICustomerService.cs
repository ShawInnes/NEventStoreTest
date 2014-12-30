using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEventSourceTests.DomainAggregates;

namespace NEventSourceTests.Services
{
    internal interface ICustomerService
    {
        Customer Create(string firstName, string lastName);
        Customer Read(Guid id);
        void Update(Customer customer);
    }
}
