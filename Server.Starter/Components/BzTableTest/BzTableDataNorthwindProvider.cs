using System.Linq.Expressions;
using InMemorySampleDatabase.Entities;
using Server.Starter.BzTable;
using Server.Starter.Data.Northwind;

namespace Server.Starter.Components.BzTableTest;

public interface IBzTableDataNorthwindProvider : IBzTableDataProvider { }

public class BzTableDataNorthwindProvider : IBzTableDataNorthwindProvider
{
    private readonly IOrdersService _ordersService;

    public BzTableDataNorthwindProvider(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }
    
    public static List<BzTableField> GetFields() => new()
    {
        new()
        {
            Key = "Number",
            Label = "Number"
        },
        new()
        {
            Key = "Customer",
            Label = "Customer",
            Filterable = true
        },
        new()
        {
            Key = "Freight",
            Label = "Freight"
        },
        new()
        {
            Key = "Placed",
            Label = "Placed Date"
        },
        new()
        {
            Key = "Required",
            Label = "Required Data"
        },
        new()
        {
            Key = "TotalAmount",
            Label = "Total Amount"
        }
    };

    public async ValueTask<BzTablePage> GetVirtualPageAsync(int count, int startIndex, BzTableColumnFilter[]? filters = null)
    {
        var enumFiltersConfig = filters?.Select(f => $"[{f.FieldKey}: {f.Value}]").ToList() ?? new List<string>();
        var plainFiltersConfig = enumFiltersConfig.Any() ? enumFiltersConfig.Aggregate((f, s) => $"{f}, {s}") : string.Empty;
        Console.WriteLine($"Received table load data request: (Count: {count}), (StartIndex: {startIndex}), (Filters: {plainFiltersConfig})");
        
        // Dynamically create a Linq predicate expression
        Func<Order, bool>? funcPredicate = null;
        var list = new List<Expression<Func<Order, bool>>>();
        foreach (var filter in filters?.Where(f => f.Value is not null) ?? Array.Empty<BzTableColumnFilter>())
        {
            switch (filter.FieldKey)
            {
                case "Customer":
                {
                    Expression<Func<Order, bool>> func = order => order.Customer.CompanyName.Contains(                            
                        filter.Value!, 
                        StringComparison.InvariantCultureIgnoreCase
                        );
                    list.Add(func);
                    break;
                }
            }
        }

        if (list.Any())
        {
            var lambda = list.Aggregate((expr1, expr2) => expr1.AndAlso(expr2)); 
            funcPredicate = lambda?.Compile();
        }
        
        var (orderData, itemsNumber)  = _ordersService.GetVirtualPage(count, startIndex, funcPredicate);
        
        // Transform entity to presentation model
        var items = orderData.Select(o => new Dictionary<string, object?>
            {
                ["Number"] = o.OrderID.ToString("D5"),
                ["Customer"] = o.Customer.CompanyName,
                ["Freight"] = o.Freight?.ToString("F2"),
                ["Placed"] = o.OrderDate?.ToShortDateString(),
                ["Required"] = o.RequiredDate?.ToShortDateString(),
                ["TotalAmount"] = o.OrderDetails
                    .Sum(d => d.UnitPrice * d.Quantity - d.UnitPrice * d.Quantity * (decimal) d.Discount)
                    .ToString("C0")
            })
            .ToList();

        return new BzTablePage
        {
            Items = items,
            Total = itemsNumber
        };
    }
}