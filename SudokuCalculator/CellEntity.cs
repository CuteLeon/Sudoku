namespace SudokuCalculator;

public struct CellLocation
{
    public int BoxRow;
    public int BoxColumn;
    public int CellRow;
    public int CellColumn;

    public CellLocation(int boxRow, int boxColumn, int cellRow, int cellColumn)
    {
        this.BoxRow = boxRow;
        this.BoxColumn = boxColumn;
        this.CellRow = cellRow;
        this.CellColumn = cellColumn;
    }

    public override string ToString()
    {
        return $"[{this.BoxRow}, {this.BoxColumn}] [{this.CellRow}, {this.CellColumn}]";
    }
}

public record CellEntity(CellLocation Location)
{
    public int? Number { get; set; }

    public HashSet<int> ProbableSet { get; init; } = [];

    public override string ToString()
    {
        return $"{this.Location} : {(Number.HasValue ? Number.Value : "-")} ({string.Join(",", this.ProbableSet.Order())})";
    }
}
