using Server.Starter.BzTable;

namespace Server.Starter.Data.Northwind;

public interface IBzTableDataNorthwindProvider : IBzTableDataProvider { }

public class BzTableDataNorthwindProvider : IBzTableDataNorthwindProvider
{
    public async ValueTask<BzTablePage> LoadPageAsync(BzTableColumnFilter filter, int count, int startIndex)
    {
        return new BzTablePage
        {
            Items = new List<Dictionary<string, object>>
            {
                new()
                {
                    ["Id"] = 0,
                    ["Name"] = "Matteo",
                    ["Surname"] = "Oettam",
                    ["Age"] = 39,
                    ["JobTitle"] = "Developer"
                },
                new()
                {
                    ["Id"] = 1,
                    ["Name"] = "Jenny",
                    ["Surname"] = "Abate",
                    ["Age"] = 37,
                    ["JobTitle"] = "Developer"
                }
            },
            TotalCount = 2
        };
    }
}