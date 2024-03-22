using System.Collections.Frozen;

namespace SudokuCalculator;

public class SudokuCalculator
{
    public void Calculate(FrozenDictionary<BoxCellLocation, CellEntity> cells, byte size)
    {
        this.CalculateProbableSet(cells, size);

        while (true)
        {
            // 只有一个候选数的格
            var detectedPairs = cells.Where(pair => !pair.Value.Number.HasValue && pair.Value.ProbableSet.Count == 1).ToArray();
            if (detectedPairs.Length != 0)
            {
                foreach (var detectedPair in detectedPairs)
                {
                    var cellEntity = detectedPair.Value;
                    var boxLocation = detectedPair.Key.BoxLocation;
                    var cellLocation = detectedPair.Key.CellLocation;
                    if (cellEntity.ProbableSet.Count != 1) continue;

                    var number = cellEntity.ProbableSet.Single();
                    cellEntity.Number = number;
                    cellEntity.ProbableSet.Clear();

                    foreach (var rowBoxCell in this.GetRowBoxCells(cells, boxLocation, cellLocation.Row, size))
                    {
                        if (!rowBoxCell.Number.HasValue)
                            rowBoxCell.ProbableSet.Remove(number);
                    }
                    foreach (var columnBoxCell in this.GetColumnBoxCells(cells, boxLocation, cellLocation.Column, size))
                    {
                        if (!columnBoxCell.Number.HasValue)
                            columnBoxCell.ProbableSet.Remove(number);
                    }
                    foreach (var currentBoxCell in this.GetCurrentBoxCells(cells, boxLocation, size))
                    {
                        if (!currentBoxCell.Number.HasValue)
                            currentBoxCell.ProbableSet.Remove(number);
                    }
                }
            }

            // TODO: 宫内只有一个侯选位置的数字

            // TODO: 所有条件不满足未取得任何进展，结束循环
        }
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

    public IEnumerable<CellEntity> GetRowBoxCells(
        FrozenDictionary<BoxCellLocation, CellEntity> cells, Location boxLocation, byte cellRow, byte size)
    {
        foreach (var rowBoxCellLocation in this.GetRowBoxCellLocations(boxLocation, cellRow, size))
        {
            if (cells.TryGetValue(rowBoxCellLocation, out var rowCellEntity))
                yield return rowCellEntity;
        }
    }

    public IEnumerable<CellEntity> GetColumnBoxCells(
        FrozenDictionary<BoxCellLocation, CellEntity> cells, Location boxLocation, byte cellColumn, byte size)
    {
        foreach (var columnBoxCellLocation in this.GetColumnBoxCellLocations(boxLocation, cellColumn, size))
        {
            if (cells.TryGetValue(columnBoxCellLocation, out var columnCellEntity))
                yield return columnCellEntity;
        }
    }

    public IEnumerable<CellEntity> GetCurrentBoxCells(
        FrozenDictionary<BoxCellLocation, CellEntity> cells, Location boxLocation, byte size)
    {
        foreach (var currentBoxCellLocation in this.GetCurrentBoxCellLocations(boxLocation, size))
        {
            if (cells.TryGetValue(currentBoxCellLocation, out var currentCellEntity))
                yield return currentCellEntity;
        }
    }

    public IEnumerable<BoxCellLocation> GetRowBoxCellLocations(Location boxLocation, byte cellRow, byte size)
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
                    yield return rowBoxCellLocation;
                }
            }
        }
    }

    public IEnumerable<BoxCellLocation> GetColumnBoxCellLocations(Location boxLocation, byte cellColumn, byte size)
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
                    yield return columnBoxCellLocation;
                }
            }
        }
    }

    public IEnumerable<BoxCellLocation> GetCurrentBoxCellLocations(Location boxLocation, byte size)
    {
        for (byte cellRow = 0; cellRow < size; cellRow++)
        {
            for (byte cellColumn = 0; cellColumn < size; cellColumn++)
            {
                var currentBoxCellLocation = new BoxCellLocation(boxLocation, new Location(cellRow, cellColumn));
                yield return currentBoxCellLocation;
            }
        }
    }
}
