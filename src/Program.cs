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
                    services.AddTransient<PeopleProcessorService>();
                    services.AddTransient<IFileReader, PeopleCsvReader>();
                    services.AddTransient<QualifiedPersonValidator>();
                    services.AddAutoMapper(typeof(Program).Assembly);
                })
                .Build();

            var service = ActivatorUtilities.CreateInstance<PeopleProcessorService>(host.Services);
            service.Run();

        }
    }
}
