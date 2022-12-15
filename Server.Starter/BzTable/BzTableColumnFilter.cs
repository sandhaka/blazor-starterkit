using System.Numerics;

namespace Server.Starter.BzTable;

public class BzTableColumnFilter
{
    public required string Label { get; set; }
    public required string FieldKey { get; set; }
    public required string? Value { get; set; }
}