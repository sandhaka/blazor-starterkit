namespace Server.Starter.BzTable;

public interface IBzTableDataProvider
{
    public ValueTask<BzTablePage> GetVirtualPageAsync(int count, int startIndex, BzTableColumnFilter[]? filters = null);
}