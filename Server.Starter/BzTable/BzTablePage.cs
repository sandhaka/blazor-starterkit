namespace Server.Starter.BzTable;

public class BzTablePage
{
    public required int Total { get; set; }
    public required IEnumerable<Dictionary<string, object?>> Items { get; set; }
}