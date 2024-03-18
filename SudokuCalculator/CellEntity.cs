namespace SudokuCalculator;

public struct Location
{
    public byte Row;
    public byte Column;

    public Location(byte row, byte column)
    {
        this.Row = row;
        this.Column = column;
    }
}

public struct BoxCellLocation
{
    public Location BoxLocation;
    public Location CellLocation;

    public BoxCellLocation(Location boxLocation, Location cellLocation)
    {
        this.BoxLocation = boxLocation;
        this.CellLocation = cellLocation;
    }

    public override string ToString()
    {
        return $"[{this.BoxLocation.Row}, {this.BoxLocation.Column}] [{this.CellLocation.Row}, {this.CellLocation.Column}]";
    }
}

public record CellEntity(BoxCellLocation Location)
{
    public int? Number { get; set; }

    public HashSet<int> ProbableSet { get; init; } = [];

    public override string ToString()
    {
        return $"{this.Location} : {(Number.HasValue ? Number.Value : "-")} ({string.Join(",", this.ProbableSet.Order())})";
    }
}
