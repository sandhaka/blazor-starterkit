namespace Server.Starter.BzTable;

public class BzTableConfig
{
    public string TableCssClass { get; set; } = string.Empty;
    public bool BuildFilters { get; set; }
    public bool MakeSortable { get; set; }
    public int ItemSize { get; set; } = 50;
    public string ContainerCssClass { get; set; } = string.Empty;
}