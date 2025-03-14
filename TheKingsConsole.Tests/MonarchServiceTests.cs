using NSubstitute;
using TheKingsConsole.Models;
using TheKingsConsole.Services;

namespace TheKingsConsole.Tests;

[TestClass]
public class MonarchServiceTests
{
    private IMonarchService _monarchService;
    private IMonarchDataProcessing _monarchDataProcessing;

    [TestInitialize]
    public void Setup()
    {
        _monarchService = Substitute.For<IMonarchService>();
        _monarchDataProcessing = Substitute.For<IMonarchDataProcessing>();
    }

    [TestMethod]
    public async Task FetchMonarchData_ShouldReturnMonarchs()
    {
        // GIVEN
        var expectedMonarchs = new List<Monarch>
            {
                new Monarch { Id = 1, Name = "Monarch 1", Years = "2000-2010", House = "House 1" },
                new Monarch { Id = 2, Name = "Monarch 2", Years = "2010-2020", House = "House 2" }
            };
        _monarchService.FetchMonarchData().Returns(Task.FromResult(expectedMonarchs));

        // WHEN
        var result = await _monarchService.FetchMonarchData();

        // THEN
        Assert.AreEqual(expectedMonarchs.Count, result.Count);
    }

    [TestMethod]
    public void PrintMonarchCount_ShouldPrintCorrectCount()
    {
        // GIVEN
        var monarchs = new List<Monarch>
            {
                new Monarch { Id = 1, Name = "Monarch 1", Years = "2000-2010", House = "House 1" },
                new Monarch { Id = 2, Name = "Monarch 2", Years = "2010-2020", House = "House 2" }
            };

        // WHEN
        _monarchDataProcessing.PrintMonarchCount(monarchs);

        // THEN
        _monarchDataProcessing.Received(1).PrintMonarchCount(monarchs);
    }
}