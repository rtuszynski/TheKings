using Newtonsoft.Json;

namespace TheKingsConsole.Models;

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
