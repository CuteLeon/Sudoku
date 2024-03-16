namespace Sudoku;

public partial class Cell : UserControl
{
    public Cell()
    {
        InitializeComponent();
        this.DoubleBuffered = true;

        this.MainProbsPanel.RowStyles.Clear();
        this.MainProbsPanel.RowCount = 0;
        this.MainProbsPanel.ColumnStyles.Clear();
        this.MainProbsPanel.ColumnCount = 0;

        var cellRowCount = Contracts.CellRowCount;
        var rowPercent = 1.0f / cellRowCount;
        for (int i = 0; i < cellRowCount; i++)
            this.MainProbsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, rowPercent));
        this.MainProbsPanel.RowCount = cellRowCount;

        var cellColumnCount = Contracts.CellColumnCount;
        var columnPercent = 1.0f / cellColumnCount;
        for (int i = 0; i < cellColumnCount; i++)
            this.MainProbsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercent));
        this.MainProbsPanel.ColumnCount = cellColumnCount;

        this.MainProbsPanel.Controls.Clear();
        for (int x = 0; x < cellColumnCount; x++)
        {
            for (int y = 0; y < cellRowCount; y++)
            {
                var prob = new Label()
                {
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = $"{x + y * cellRowCount}",
                    Margin = Padding.Empty,
                    Dock = DockStyle.Fill
                };
                this.MainProbsPanel.Controls.Add(prob);
                this.MainProbsPanel.SetCellPosition(prob, new TableLayoutPanelCellPosition(x, y));
            }
        }
    }
}
