namespace Sudoku;

public partial class Board : UserControl
{
    public Board()
    {
        InitializeComponent();

        this.MainBoxesPanel.RowStyles.Clear();
        this.MainBoxesPanel.RowCount = 0;
        this.MainBoxesPanel.ColumnStyles.Clear();
        this.MainBoxesPanel.ColumnCount = 0;

        var rowPercent = 1.0f / Contracts.BoxRowCount;
        for (int i = 0; i < Contracts.BoxRowCount; i++)
            this.MainBoxesPanel.RowStyles.Add(new RowStyle(SizeType.Percent, rowPercent));
        this.MainBoxesPanel.RowCount = Contracts.BoxRowCount;

        var columnPercent = 1.0f / Contracts.BoxColumnCount;
        for (int i = 0; i < Contracts.BoxColumnCount; i++)
            this.MainBoxesPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercent));
        this.MainBoxesPanel.ColumnCount = Contracts.BoxColumnCount;
    }
}
