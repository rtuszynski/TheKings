using TheKingsConsole.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TheKingsConsole;

public class Program
{
    static async Task Main(string[] args)
    {
        // Configure the DI container
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Get the service from the DI container
        var dataService = serviceProvider.GetService<IMonarchService>();
        var dataProcessing = serviceProvider.GetService<IMonarchDataProcessing>();

        if (dataService == null || dataProcessing == null)
        {
            Console.WriteLine("An error occurred: Service is not properly configured.");
            return;
        }

        try
        {
            var monarchs = await dataService.FetchMonarchData();

            if (!monarchs.Any())
            {
                Console.WriteLine("No monarchs available.");
                return;
            }

            dataProcessing.PrintMonarchCount(monarchs);
            dataProcessing.PrintLongestRulingMonarch(monarchs);
            dataProcessing.PrintLongestRulingHouse(monarchs);
            dataProcessing.PrintMostCommonFirstName(monarchs);
            dataProcessing.PrintCurrentMonarch(monarchs);
            dataProcessing.PrintRandomMonarch(monarchs);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Register services
        services.AddTransient<IMonarchService, MonarchService>();
        services.AddTransient<IMonarchDataProcessing, MonarchDataProcessing>();
    }
}