using TheKingsConsole.Models;

namespace TheKingsConsole.Services;

public interface IMonarchDataProcessing
{
    void PrintCurrentMonarch(IEnumerable<Monarch> monarchs);
    void PrintLongestRulingHouse(IEnumerable<Monarch> monarchs);
    void PrintLongestRulingMonarch(IEnumerable<Monarch> monarchs);
    void PrintMonarchCount(IEnumerable<Monarch> monarchs);
    void PrintMostCommonFirstName(IEnumerable<Monarch> monarchs);
    void PrintRandomMonarch(IEnumerable<Monarch> monarchs);
}
