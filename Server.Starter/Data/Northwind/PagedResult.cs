namespace Server.Starter.Data.Northwind;

public class PagedResult<T> where T : class
{
    public required List<T> Items { get; set; }
    public required int Total { get; set; }
}