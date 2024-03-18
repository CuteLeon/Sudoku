using System.Collections.Frozen;

namespace SudokuCalculator;

public class SudokuCalculator
{
    public void Calculate(FrozenDictionary<BoxCellLocation, CellEntity> cells)
    {
        var size = 3;
        this.CalculateProbableSet(cells, size);
    }

    public void CalculateProbableSet(FrozenDictionary<BoxCellLocation, CellEntity> cells, int size)
    {
        foreach (var pair in cells)
        {
            var cellEntity = pair.Value;
            cellEntity.ProbableSet.Clear();
            if (cellEntity.Number.HasValue) continue;

            var boxLocation = pair.Key.BoxLocation;
            var cellLocation = pair.Key.CellLocation;
            var probableSet = Enumerable.Range(1, size * size).ToHashSet();

            for (byte boxIndex = 0; boxIndex < size; boxIndex++)
            {
                var rowBoxLocation = new Location(boxLocation.Row, boxIndex);
                var columnBoxLocation = new Location(boxIndex, boxLocation.Column);

                if (!rowBoxLocation.Equals(boxLocation))
                {
                    for (byte cellIndex = 0; cellIndex < size; cellIndex++)
                    {
                        var rowCellLocation = new Location(cellLocation.Row, cellIndex);
                        var rowBoxCellLocation = new BoxCellLocation(rowBoxLocation, rowCellLocation);

                        if (cells.TryGetValue(rowBoxCellLocation, out var rowCellEntity) && rowCellEntity.Number.HasValue)
                            probableSet.Remove(rowCellEntity.Number.Value);
                    }
                }
                if (!columnBoxLocation.Equals(boxLocation))
                {
                    for (byte cellIndex = 0; cellIndex < size; cellIndex++)
                    {
                        var columnCellLocation = new Location(cellIndex, cellLocation.Column);
                        var columnBoxCellLocation = new BoxCellLocation(columnBoxLocation, columnCellLocation);

                        if (cells.TryGetValue(columnBoxCellLocation, out var columnCellEntity) && columnCellEntity.Number.HasValue)
                            probableSet.Remove(columnCellEntity.Number.Value);
                    }
                }
                else
                {
                    for (byte cellRow = 0; cellRow < size; cellRow++)
                    {
                        for (byte cellColumn = 0; cellColumn < size; cellColumn++)
                        {
                            var currentBoxCellLocation = new BoxCellLocation(boxLocation, new Location(cellRow, cellColumn));
                            if (cells.TryGetValue(currentBoxCellLocation, out var currentCellEntity) && currentCellEntity.Number.HasValue)
                                probableSet.Remove(currentCellEntity.Number.Value);
                        }
                    }
                }
            }

            foreach (var probable in probableSet)
                cellEntity.ProbableSet.Add(probable);
        }
    }
}
