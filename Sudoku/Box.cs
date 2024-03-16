namespace Sudoku;

public partial class Box : UserControl
{
    public Box()
    {
        InitializeComponent();
        this.DoubleBuffered = true;

        this.MainCellsPanel.RowStyles.Clear();
        this.MainCellsPanel.RowCount = 0;
        this.MainCellsPanel.ColumnStyles.Clear();
        this.MainCellsPanel.ColumnCount = 0;

        var rowPercent = 1.0f / Contracts.CellRowCount;
        for (int i = 0; i < Contracts.CellRowCount; i++)
            this.MainCellsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, rowPercent));
        this.MainCellsPanel.RowCount = Contracts.CellRowCount;

        var columnPercent = 1.0f / Contracts.CellColumnCount;
        for (int i = 0; i < Contracts.CellColumnCount; i++)
            this.MainCellsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercent));
        this.MainCellsPanel.ColumnCount = Contracts.CellColumnCount;
    }
}
