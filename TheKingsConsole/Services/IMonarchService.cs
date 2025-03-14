using TheKingsConsole.Models;

namespace TheKingsConsole.Services;

public interface IMonarchService
{
    Task<List<Monarch>> FetchMonarchData();
}