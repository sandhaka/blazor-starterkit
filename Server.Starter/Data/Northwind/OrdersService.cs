using System.Linq.Expressions;
using InMemorySampleDatabase;
using InMemorySampleDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Server.Starter.Data.Northwind;

public interface IOrdersService
{
    (IEnumerable<Order> Items, int Total) GetVirtualPage(int count, int offset, Func<Order,bool>? predicate = null, IComparer<Order>? orderBy = null);
}

public sealed class OrdersService : IOrdersService
{
    private readonly NorthwindContext _dbContext;

    public OrdersService(NorthwindContext dbContext)
    {
        _dbContext = dbContext;
    }

    public (IEnumerable<Order> Items, int Total) GetVirtualPage(
        int count, 
        int offset, 
        Func<Order,bool>? predicate = null,
        IComparer<Order>? orderBy = null)
    {
        var set = _dbContext.Orders
            .Include(o => o.OrderDetails)
            .Include(o => o.Customer)
            .Include(o => o.Shipper);

        var query = predicate is not null ? set.Where(predicate) : set;
        query = orderBy is not null ? query.Order(orderBy) : query;

        var querySet = query as Order[] ?? query.ToArray();
        
        var items = querySet
            .Skip(offset)
            .Take(count)
            .ToList();

        return (Items: items, Total: querySet.Length);
    }
}