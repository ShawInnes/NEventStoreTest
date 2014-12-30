using System;
using System.Collections.Generic;
using Autofac;
using CommonDomain;
using NEventSourceTests.DomainAggregates;
using NEventSourceTests.Services;
using NEventStore;
using NEventStore.Persistence;
using Serilog;

namespace NEventSourceTests
{
    public class Program
    {
        private static double balance = 0;

        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(typeof(Program).Assembly);

            using (IContainer container = builder.Build())
            {
                var customerService = container.Resolve<ICustomerService>();

                var customer = customerService.Create("Shaw", "Innes");

                Log.Information("Created Customer {@customer}", customer);

                for (int i = 0; i < 100; i++)
                {
                    customer.BuyTicket(20);
                    Log.Information("Customer {@customer}", customer);

                    customer.SellTicket(15);
                    Log.Information("Customer {@customer}", customer);

                    customerService.Update(customer);

                    /*                    var list = new Dictionary<string, int>();
                                        IEnumerable<IStreamHead> streams = store.Advanced.GetStreamsToSnapshot("default", 10);
                                        foreach (IStreamHead stream in streams)
                                        {
                                            Log.Warning("Stream {stream} requires a Snapshot", stream);
                                            list.Add(stream.StreamId, stream.HeadRevision);
                                        }

                                        foreach (var item in list)
                                        {
                                            CustomerSnapshot snapshot = new CustomerSnapshot
                                            {
                                                Id = customer.Id,
                                                Version = customer.Version,
                                                FirstName = customer.FirstName,
                                                LastName = customer.LastName,
                                                Balance = customer.Balance
                                            };

                                            if (store.Advanced.AddSnapshot(new Snapshot("default", item.Key, item.Value, snapshot)))
                                            {
                                                Log.Information("Snapshot Ok");
                                            }
                                            else
                                            {
                                                Log.Error("Snapshot Failed @ {StreamRevision}", item.Value);
                                            }
                                        }
                     */
                }

                Log.Information("Customer Aggregate is {@customer}", customer);
                Console.ReadLine();
            }
        }
    }
}