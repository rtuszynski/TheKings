using TheKingsConsole.Services;

namespace TheKingsConsole;

public class Program
{
    static async Task Main(string[] args)
    {
        var service = new MonarchService();

        try
        {
            var monarchs = await service.FetchMonarchData();

            if (!monarchs.Any())
            {
                Console.WriteLine("No monarchs available.");
                return;
            }

            service.PrintMonarchCount(monarchs);
            service.PrintLongestRulingMonarch(monarchs);
            service.PrintLongestRulingHouse(monarchs);
            service.PrintMostCommonFirstName(monarchs);
            service.PrintCurrentMonarch(monarchs);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}