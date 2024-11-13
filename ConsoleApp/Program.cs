using AutomateFinance.Domain;
using AutomateFinance.Domain.Interfaces.Services;
using AutomateFinance.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutomateFinance.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddScoped<INFSeService, NFSeService>();
                    services.AddScoped<INFSeProcessorService, NFSeProcessorService>();
                    services.AddScoped<IDirectoryHelper, DirectoryHelper>();
                    services.AddScoped<LogHelper>();
                })
                .Build();

            var directoryHelper = host.Services.GetRequiredService<IDirectoryHelper>();
            var logHelper = host.Services.GetRequiredService<LogHelper>();
            logHelper.LogMessage("Program iniciado.");
            var processorService = host.Services.GetRequiredService<INFSeProcessorService>();
            processorService.ProcessNFSes();
        }
    }
}
