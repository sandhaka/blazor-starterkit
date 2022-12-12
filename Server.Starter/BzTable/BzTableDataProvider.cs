namespace Server.Starter.BzTable;

public interface IBzTableDataProvider
{
    public ValueTask<BzTablePage> LoadPageAsync(BzTableColumnFilter filter, int count, int startIndex);
}