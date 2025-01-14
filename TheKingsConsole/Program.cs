using Newtonsoft.Json;

namespace TheKingsConsole;

public class Monarch
{
    public int Id { get; set; }

    [JsonProperty("nm")]
    public string? Name { get; set; }

    [JsonProperty("cty")]
    public string? Country { get; set; }

    [JsonProperty("hse")]
    public string? House { get; set; }

    [JsonProperty("yrs")]
    public string? Years { get; set; }
}

public class Program
{
    private static readonly string Url = "https://";

    static async Task Main(string[] args)
    {
        try
        {
            var monarchs = await FetchMonarchData();

            if (!monarchs.Any())
            {
                Console.WriteLine("No monarchs available.");
                return;
            }

            PrintMonarchCount(monarchs);
            PrintLongestRulingMonarch(monarchs);
            PrintLongestRulingHouse(monarchs);
            PrintMostCommonFirstName(monarchs);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static async Task<List<Monarch>> FetchMonarchData()
    {
        using HttpClient client = new();
        var response = await client.GetStringAsync(Url);

        return JsonConvert.DeserializeObject<List<Monarch>>(response) ?? new List<Monarch>();
    }

    private static void PrintMonarchCount(IEnumerable<Monarch> monarchs)
    {
        Console.WriteLine($"1. Number of monarchs: {monarchs.Count()}");
    }

    private static void PrintLongestRulingMonarch(IEnumerable<Monarch> monarchs)
    {
        var longestRulingMonarch = monarchs.OrderByDescending(m => GetRulingYears(m.Years)).FirstOrDefault();

        Console.WriteLine($"2. Monarch who ruled the longest: {longestRulingMonarch?.Name} ({longestRulingMonarch?.Years})");
    }

    private static void PrintLongestRulingHouse(IEnumerable<Monarch> monarchs)
    {
        var longestRulingHouse = monarchs.GroupBy(m => m.House)
                                          .OrderByDescending(g => g.Sum(m => GetRulingYears(m.Years)))
                                          .FirstOrDefault();

        Console.WriteLine($"3. House that ruled the longest: {longestRulingHouse?.Key} ({longestRulingHouse?.Sum(m => GetRulingYears(m.Years))} years)");
    }

    private static void PrintMostCommonFirstName(IEnumerable<Monarch> monarchs)
    {
        var mostCommonFirstName = monarchs
            .Where(m => m.Name != null)
            .Select(m => m.Name?.Split(' ').First())
            .GroupBy(name => name)
            .OrderByDescending(g => g.Count())
            .FirstOrDefault();

        Console.WriteLine($"4. Most common first name: {mostCommonFirstName?.Key}");
    }

    private static int GetRulingYears(string? years)
    {
        if (string.IsNullOrWhiteSpace(years))
            return 0;

        var yearRange = years.Split('-');
        if (yearRange.Length != 2)
            return 0;

        int startYear;
        if (!int.TryParse(yearRange[0], out startYear))
            return 0;

        if (string.IsNullOrWhiteSpace(yearRange[1]))
            return DateTime.Now.Year - startYear;

        int endYear;
        if (!int.TryParse(yearRange[1], out endYear))
            return 0;

        return endYear - startYear;
    }
}