namespace Sudoku;

public partial class Board : UserControl
{

    public Board()
    {
        InitializeComponent();
        this.DoubleBuffered = true;

        this.MainBoxesPanel.RowStyles.Clear();
        this.MainBoxesPanel.RowCount = 0;
        this.MainBoxesPanel.ColumnStyles.Clear();
        this.MainBoxesPanel.ColumnCount = 0;

        var boxRowCount = Contracts.BoxRowCount;
        var rowPercent = 1.0f / boxRowCount;
        for (int i = 0; i < boxRowCount; i++)
            this.MainBoxesPanel.RowStyles.Add(new RowStyle(SizeType.Percent, rowPercent));
        this.MainBoxesPanel.RowCount = boxRowCount;

        var boxColumnCount = Contracts.BoxColumnCount;
        var columnPercent = 1.0f / boxColumnCount;
        for (int i = 0; i < boxColumnCount; i++)
            this.MainBoxesPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercent));
        this.MainBoxesPanel.ColumnCount = boxColumnCount;

        this.MainBoxesPanel.Controls.Clear();
        for (int x = 0; x < boxColumnCount; x++)
        {
            for (int y = 0; y < boxRowCount; y++)
            {
                this.MainBoxesPanel.SetCellPosition(new Box(), new TableLayoutPanelCellPosition(x, y));
            }
        }
    }
}
