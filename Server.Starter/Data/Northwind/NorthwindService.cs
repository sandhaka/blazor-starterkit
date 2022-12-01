using System.Linq.Expressions;
using InMemorySampleDatabase;
using InMemorySampleDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Server.Starter.Data.Northwind;

internal interface INorthwindService
{
    PagedResult<Product> GetPaged(string nameText, decimal priceFrom, decimal priceTo, int pageSize, int pageNumber);
    IEnumerable<Product> GetVirtualPage(string nameText, decimal priceFrom, decimal priceTo, int count, int offset, string? orderBy = null);
    int Count(string nameText, decimal priceFrom, decimal priceTo);
}

internal sealed class NorthwindService : INorthwindService
{
    private readonly NorthwindContext _dbContext;

    public NorthwindService(NorthwindContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public PagedResult<Product> GetPaged(
        string nameText, decimal priceFrom, decimal priceTo, int pageSize, int pageNumber)
    {
        Expression<Func<Product, bool>> e = p =>
            p.ProductName.Contains(nameText, StringComparison.InvariantCultureIgnoreCase) &&
            p.UnitPrice > priceFrom &&
            p.UnitPrice < priceTo;

        var filterExTree = e.Compile();

        var pagedResults = _dbContext.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Where(filterExTree).Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        var itemCount = _dbContext.Products.Count(filterExTree);

        return new PagedResult<Product>
        {
            Items = pagedResults,
            Total = itemCount
        };
    }

    public IEnumerable<Product> GetVirtualPage(
        string nameText, decimal priceFrom, decimal priceTo, int count, int offset, string? orderBy = null)
    {
        Expression<Func<Product, bool>> e = p =>
            p.ProductName.Contains(nameText, StringComparison.InvariantCultureIgnoreCase) &&
            p.UnitPrice >= priceFrom &&
            p.UnitPrice <= priceTo;

        var filterExTree = e.Compile();

        return _dbContext.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Where(filterExTree)
            .OrderByDescending(p => p.UnitPrice)
            .Skip(offset)
            .Take(count)
            .ToList();
    }

    public int Count(string nameText, decimal priceFrom, decimal priceTo) =>
        _dbContext.Products.Count(p =>
            p.ProductName.Contains(nameText, StringComparison.InvariantCultureIgnoreCase) &&
            p.UnitPrice >= priceFrom &&
            p.UnitPrice <= priceTo);
}