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

        var cellRowCount = Contracts.Size;
        var rowPercent = 1.0f / cellRowCount;
        for (int i = 0; i < cellRowCount; i++)
            this.MainCellsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, rowPercent));
        this.MainCellsPanel.RowCount = cellRowCount;

        var cellColumnCount = Contracts.Size;
        var columnPercent = 1.0f / cellColumnCount;
        for (int i = 0; i < cellColumnCount; i++)
            this.MainCellsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercent));
        this.MainCellsPanel.ColumnCount = cellColumnCount;

        this.MainCellsPanel.Controls.Clear();
        for (int x = 0; x < cellColumnCount; x++)
        {
            for (int y = 0; y < cellRowCount; y++)
            {
                var cell = new Cell()
                {
                    Margin = Padding.Empty,
                    Dock = DockStyle.Fill
                };
                this.MainCellsPanel.Controls.Add(cell);
                this.MainCellsPanel.SetCellPosition(cell, new TableLayoutPanelCellPosition(x, y));
            }
        }
    }
}
