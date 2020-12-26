using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PeopleProcessor
{
    class Program
    {
        static void Main(string[] args)
        {

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IPeopleProcessorService, PeopleProcessorService>();
                    services.AddTransient<IPeopleCsvReader, PeopleCsvReader>();
                    services.AddTransient<IPersonValidator, QualifiedPersonValidator>();
                    services.AddAutoMapper(typeof(Program).Assembly);
                })
                .Build();

            //host.Services.GetService(PeopleProcessorService);
            var service = ActivatorUtilities.CreateInstance<PeopleProcessorService>(host.Services);
            service.Run();

        }
    }
}
