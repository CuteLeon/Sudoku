namespace Sudoku;

public class BoardEntity
{
    public BoxEntity[,] Boxes { get; init; } = new BoxEntity[Contracts.Size, Contracts.Size];
}

public class BoxEntity
{
    public int BoxRow { get; set; }

    public int BoxColumn { get; set; }

    public CellEntity[,] Cells { get; init; } = new CellEntity[Contracts.Size, Contracts.Size];
}

public class CellEntity
{
    public int CellRow { get; set; }

    public int CellColumn { get; set; }

    public int? Number { get; set; }
}
