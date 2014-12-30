using System;
using CommonDomain.Persistence;
using NEventSourceTests.DomainAggregates;
using NEventStore;
using Serilog;

namespace NEventSourceTests.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository repository;
        private readonly IStoreEvents store;

        public CustomerService(IRepository repository, IStoreEvents store)
        {
            this.repository = repository;
            this.store = store;
        }

        public Customer Create(string firstName, string lastName)
        {
            var customer = Customer.CreateCustomer(firstName, lastName);
            repository.Save(customer, Guid.NewGuid());
            return customer;
        }

        public Customer Read(Guid id)
        {
            Customer customer = null;

            customer = repository.GetById<Customer>(id);

            Log.Information("Repository Read {customer}", customer);
            
            return customer;
        }

        public void Update(Customer customer)
        {
            Log.Information("Repository Update {customer}", customer);
            repository.Save(customer, Guid.NewGuid());
        }
    }
}