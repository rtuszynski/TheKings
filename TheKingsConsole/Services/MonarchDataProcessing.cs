using System.Text.RegularExpressions;
using TheKingsConsole.Models;

namespace TheKingsConsole.Services;

public class MonarchDataProcessing : IMonarchDataProcessing
{
    public void PrintMonarchCount(IEnumerable<Monarch> monarchs)
    {
        Console.WriteLine($"1. Number of monarchs: {monarchs.Count()}");
    }

    public void PrintLongestRulingMonarch(IEnumerable<Monarch> monarchs)
    {
        var longestRulingMonarch = monarchs.OrderByDescending(m => GetRulingYears(m.Years)).FirstOrDefault();

        Console.WriteLine($"2. Monarch who ruled the longest: {longestRulingMonarch?.Name} ({longestRulingMonarch?.Years})");
    }

    public void PrintLongestRulingHouse(IEnumerable<Monarch> monarchs)
    {
        var longestRulingHouse = monarchs.GroupBy(m => m.House)
                                          .OrderByDescending(g => g.Sum(m => GetRulingYears(m.Years)))
                                          .FirstOrDefault();

        Console.WriteLine($"3. House that ruled the longest: {longestRulingHouse?.Key} ({longestRulingHouse?.Sum(m => GetRulingYears(m.Years))} years)");
    }

    public void PrintMostCommonFirstName(IEnumerable<Monarch> monarchs)
    {
        var mostCommonFirstName = monarchs
            .Where(m => m.Name != null)
            .Select(m => m.Name?.Split(' ').First())
            .GroupBy(name => name)
            .OrderByDescending(g => g.Count())
            .FirstOrDefault();

        Console.WriteLine($"4. Most common first name: {mostCommonFirstName?.Key}");
    }

    public void PrintCurrentMonarch(IEnumerable<Monarch> monarchs)
    {
        var currentMonarch = monarchs.Where(m => m.Years != null && Regex.IsMatch(m.Years, @"\d{4}-$")).FirstOrDefault();

        foreach (var m in monarchs)
        {
            if (m.Years != null && Regex.IsMatch(m.Years, @"\d{4}-$"))
            {
                currentMonarch = m;
            }
        }

        if (currentMonarch != null)
        {
            Console.WriteLine($"5. Current monarch: {currentMonarch?.Name} ({currentMonarch?.Years})");
        }
        else
        {
            Console.WriteLine($"No current monarch");
        }
    }

    public void PrintRandomMonarch(IEnumerable<Monarch> monarchs)
    {
        var random = new Random();
        int randomNumber = random.Next(10, 15);

        var randomMonarch = monarchs.Where(m => m.Id == randomNumber).FirstOrDefault();

        Console.WriteLine($"6. Random monarch from json: {randomMonarch?.Name}");
    }

    private int GetRulingYears(string? years)
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
