using System.Collections.Frozen;

namespace SudokuCalculator;

public class SudokuCalculator
{
    public void Calculate(FrozenDictionary<BoxCellLocation, CellEntity> cells)
    {
        byte size = 3;
        this.CalculateProbableSet(cells, size);
    }

    public void CalculateProbableSet(FrozenDictionary<BoxCellLocation, CellEntity> cells, byte size)
    {
        foreach (var pair in cells)
        {
            var cellEntity = pair.Value;
            cellEntity.ProbableSet.Clear();
            if (cellEntity.Number.HasValue) continue;

            var boxLocation = pair.Key.BoxLocation;
            var cellLocation = pair.Key.CellLocation;
            var probableSet = Enumerable.Range(1, size * size).ToHashSet();

            foreach (var rowBoxCell in this.GetRowBoxCells(cells, boxLocation, cellLocation.Row, size))
            {
                if (rowBoxCell.Number.HasValue)
                    probableSet.Remove(rowBoxCell.Number.Value);
            }
            foreach (var columnBoxCell in this.GetColumnBoxCells(cells, boxLocation, cellLocation.Column, size))
            {
                if (columnBoxCell.Number.HasValue)
                    probableSet.Remove(columnBoxCell.Number.Value);
            }
            foreach (var currentBoxCell in this.GetCurrentBoxCells(cells, boxLocation, size))
            {
                if (currentBoxCell.Number.HasValue)
                    probableSet.Remove(currentBoxCell.Number.Value);
            }

            foreach (var probable in probableSet)
                cellEntity.ProbableSet.Add(probable);
        }
    }

    protected IEnumerable<CellEntity> GetRowBoxCells(
        FrozenDictionary<BoxCellLocation, CellEntity> cells, Location boxLocation, byte cellRow, byte size)
    {
        for (byte boxIndex = 0; boxIndex < size; boxIndex++)
        {
            var rowBoxLocation = new Location(boxLocation.Row, boxIndex);
            if (!rowBoxLocation.Equals(boxLocation))
            {
                for (byte cellIndex = 0; cellIndex < size; cellIndex++)
                {
                    var rowCellLocation = new Location(cellRow, cellIndex);
                    var rowBoxCellLocation = new BoxCellLocation(rowBoxLocation, rowCellLocation);

                    if (cells.TryGetValue(rowBoxCellLocation, out var rowCellEntity))
                        yield return rowCellEntity;
                }
            }
        }
    }

    protected IEnumerable<CellEntity> GetColumnBoxCells(FrozenDictionary<BoxCellLocation, CellEntity> cells, Location boxLocation, byte cellColumn, byte size)
    {
        for (byte boxIndex = 0; boxIndex < size; boxIndex++)
        {
            var columnBoxLocation = new Location(boxIndex, boxLocation.Column);
            if (!columnBoxLocation.Equals(boxLocation))
            {
                for (byte cellIndex = 0; cellIndex < size; cellIndex++)
                {
                    var columnCellLocation = new Location(cellIndex, cellColumn);
                    var columnBoxCellLocation = new BoxCellLocation(columnBoxLocation, columnCellLocation);

                    if (cells.TryGetValue(columnBoxCellLocation, out var columnCellEntity))
                        yield return columnCellEntity;
                }
            }
        }
    }

    protected IEnumerable<CellEntity> GetCurrentBoxCells(FrozenDictionary<BoxCellLocation, CellEntity> cells, Location boxLocation, byte size)
    {
        for (byte cellRow = 0; cellRow < size; cellRow++)
        {
            for (byte cellColumn = 0; cellColumn < size; cellColumn++)
            {
                var currentBoxCellLocation = new BoxCellLocation(boxLocation, new Location(cellRow, cellColumn));
                if (cells.TryGetValue(currentBoxCellLocation, out var currentCellEntity))
                    yield return currentCellEntity;
            }
        }
    }
}
