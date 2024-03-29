﻿using System.Collections.Frozen;
using System.Diagnostics;

namespace SudokuCalculator;

public class SudokuCalculator
{
    public void Calculate(FrozenDictionary<BoxCellLocation, CellEntity> cells, byte size)
    {
        this.CalculateProbableSet(cells, size);

        while (true)
        {
            // 只有一个候选数的格
            var applied = false;
            var detectedPairs = cells.Where(pair => !pair.Value.Number.HasValue && pair.Value.ProbableSet.Count == 1).ToArray();
            if (detectedPairs.Length != 0)
            {
                foreach (var detectedPair in detectedPairs)
                {
                    var cellEntity = detectedPair.Value;
                    if (cellEntity.ProbableSet.Count == 1)
                    {
                        applied = true;
                        var number = cellEntity.ProbableSet.Single();
                        this.SetBoxCellNumber(cells, cellEntity, number, size);
                    }
                }
            }

            // 宫内只有一个侯选位置的数字
            for (byte boxRow = 0; boxRow < size; boxRow++)
            {
                for (byte boxColumn = 0; boxColumn < size; boxColumn++)
                {
                    var boxLocation = new Location(boxRow, boxColumn);
                    var boxCells = this.GetCurrentBoxCells(cells, boxLocation, size).ToArray();
                    for (var number = 1; number <= size * size; number++)
                    {
                        var containedCells = boxCells.Where(entity => entity.ProbableSet.Contains(number)).ToArray();
                        if (containedCells.Length == 1)
                        {
                            applied = true;
                            var cellEntity = containedCells.Single();
                            this.SetBoxCellNumber(cells, cellEntity, number, size);
                        }
                    }
                }
            }

            // 所有条件不满足未取得任何进展，结束循环 坏蛋Leon一个人偷偷唱歌 --Coco
            if (!applied) break;
        }

        var unnumberedCells = cells.Values.Where(cellEntity => !cellEntity.Number.HasValue).ToArray();
        if (unnumberedCells.Any())
        {
            var rowNumberSets = cells.Values
                .GroupBy(cellEntity => new Dimension(cellEntity.Location.BoxLocation.Row, cellEntity.Location.CellLocation.Row))
                .ToFrozenDictionary(
                    group => group.Key,
                    group => group
                        .Where(cellEntity => cellEntity.Number.HasValue)
                        .Select(cellEntity => cellEntity.Number!.Value)
                        .ToHashSet());
            var columnNumberSets = cells.Values
                .GroupBy(cellEntity => new Dimension(cellEntity.Location.BoxLocation.Column, cellEntity.Location.CellLocation.Column))
                .ToFrozenDictionary(
                    group => group.Key,
                    group => group
                        .Where(cellEntity => cellEntity.Number.HasValue)
                        .Select(cellEntity => cellEntity.Number!.Value)
                        .ToHashSet());
            var boxNumberSets = cells.Values
                .GroupBy(cellEntity => cellEntity.Location.BoxLocation)
                .ToFrozenDictionary(
                    group => group.Key,
                    group => group
                        .Where(cellEntity => cellEntity.Number.HasValue)
                        .Select(cellEntity => cellEntity.Number!.Value)
                        .ToHashSet());

            var results = new List<FrozenDictionary<BoxCellLocation, int>>();
            this.DeepScan(unnumberedCells, 0, rowNumberSets, columnNumberSets, boxNumberSets, results);
            Debug.Print($"Scanned result(s): {results.Count:N0}");
            var result = results.FirstOrDefault();
            if (result is not null)
            {
                foreach (var pair in result)
                {
                    if (cells.TryGetValue(pair.Key, out var cellEntity))
                    {
                        this.SetBoxCellNumber(cells, cellEntity, pair.Value, size);
                    }
                }
            }
        }
    }

    public void DeepScan(
        CellEntity[] unnumberedCells, int cellIndex,
        FrozenDictionary<Dimension, HashSet<int>> rowNumberSets,
        FrozenDictionary<Dimension, HashSet<int>> columnNumberSets,
        FrozenDictionary<Location, HashSet<int>> boxNumberSets,
        List<FrozenDictionary<BoxCellLocation, int>> results)
    {
        if (cellIndex >= unnumberedCells.Length) return;

        var currentCell = unnumberedCells[cellIndex];
        var boxLocation = currentCell.Location.BoxLocation;
        var cellLocation = currentCell.Location.CellLocation;
        var rowNumberSet = rowNumberSets[new Dimension(boxLocation.Row, cellLocation.Row)];
        var columnNumberSet = columnNumberSets[new Dimension(boxLocation.Column, cellLocation.Column)];
        var boxNumberSet = boxNumberSets[boxLocation];
        foreach (var probNumber in currentCell.ProbableSet)
        {
            if (rowNumberSet.Contains(probNumber) ||
                columnNumberSet.Contains(probNumber) ||
                boxNumberSet.Contains(probNumber))
                continue;
            rowNumberSet.Add(probNumber);
            columnNumberSet.Add(probNumber);
            boxNumberSet.Add(probNumber);
            currentCell.Number = probNumber;

            if (cellIndex < unnumberedCells.Length - 1)
            {
                this.DeepScan(unnumberedCells, cellIndex + 1, rowNumberSets, columnNumberSets, boxNumberSets, results);
            }
            else
            {
                var result = unnumberedCells.ToFrozenDictionary(cellEntity => cellEntity.Location, cellEntity => cellEntity.Number!.Value);
                results.Add(result);
            }

            currentCell.Number = default;
            rowNumberSet.Remove(probNumber);
            columnNumberSet.Remove(probNumber);
            boxNumberSet.Remove(probNumber);
        }
    }

    public void SetBoxCellNumber(FrozenDictionary<BoxCellLocation, CellEntity> cells, CellEntity cellEntity, int number, byte size)
    {
        cellEntity.Number = number;
        cellEntity.ProbableSet.Clear();

        var boxLocation = cellEntity.Location.BoxLocation;
        var cellLocation = cellEntity.Location.CellLocation;
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
