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

                for (byte cellIndex = 0; cellIndex < size; cellIndex++)
                {
                    var rowCellLocation = new Location(cellLocation.Row, cellIndex);
                    var columnCellLocation = new Location(cellIndex, cellLocation.Column);
                    var rowBoxCellLocation = new BoxCellLocation(rowBoxLocation, rowCellLocation);
                    var columnBoxCellLocation = new BoxCellLocation(columnBoxLocation, columnCellLocation);

                    if (cells.TryGetValue(rowBoxCellLocation, out var rowCellEntity) && rowCellEntity.Number.HasValue)
                        probableSet.Remove(rowCellEntity.Number.Value);
                    if (cells.TryGetValue(columnBoxCellLocation, out var columnCellEntity) && columnCellEntity.Number.HasValue)
                        probableSet.Remove(columnCellEntity.Number.Value);
                }
            }

            foreach (var probable in probableSet)
                cellEntity.ProbableSet.Add(probable);
        }
    }
}
