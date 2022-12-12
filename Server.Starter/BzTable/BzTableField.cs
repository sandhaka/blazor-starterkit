namespace Server.Starter.BzTable;

public enum BzTableType { general, quantity, date, money }

public class BzTableField
{
    /// <summary>
    /// Key field (the name in the data set)
    /// </summary>
    public required string Key { get; set; }
    /// <summary>
    /// Column header
    /// </summary>
    public required string Label { get; set; }
    /// <summary>
    /// Type
    /// </summary>
    public BzTableType FieldType { get; set; } = BzTableType.general;
    /// <summary>
    /// Data can be filtered by this field
    /// </summary>
    public bool Filterable { get; set; }
    /// <summary>
    /// Data can be sorted by this field
    /// </summary>
    public bool Sortable { get; set; }
}