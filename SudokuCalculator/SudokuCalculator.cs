using System.Collections.Frozen;

namespace SudokuCalculator;

public class SudokuCalculator
{
    public void Calculate(FrozenDictionary<BoxCellLocation, CellEntity> cells)
    {
        var size = 3;
        this.CalculateProbableSet(cells, size);
    }

    protected void CalculateProbableSet(FrozenDictionary<BoxCellLocation, CellEntity> cells, int size)
    {
        foreach (var pair in cells)
        {
            var cellLocation = pair.Key;
            var cellEntity = pair.Value;
            var probableSet = Enumerable.Range(1, size * size).ToHashSet();

            for (var boxColumn = 0; boxColumn < size; boxColumn++)
            {
            }

            cellEntity.ProbableSet.Clear();
            foreach (var probable in probableSet)
                cellEntity.ProbableSet.Add(probable);
        }
    }
}
