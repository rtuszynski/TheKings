using Newtonsoft.Json;
using System.Text.RegularExpressions;
using TheKingsConsole.Models;

namespace TheKingsConsole.Services;

public class MonarchService : IMonarchService
{
    private static readonly string Url = "Data/sample.json";

    public async Task<List<Monarch>> FetchMonarchData()
    {
        var response = await File.ReadAllTextAsync(Url);

        return JsonConvert.DeserializeObject<List<Monarch>>(response) ?? new List<Monarch>();
    }
}
