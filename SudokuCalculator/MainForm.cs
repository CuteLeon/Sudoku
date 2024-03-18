using System.Collections.Frozen;

namespace SudokuCalculator;

public partial class MainForm : Form
{
    public FrozenDictionary<(int, int), TableLayoutPanel> BoxPanels { get; init; }
    public FrozenDictionary<(int, int), FrozenDictionary<(int, int), Label>> BoxCellLabels { get; init; }

    public MainForm()
    {
        InitializeComponent();

        var offsetWidth = this.Width - this.BoardLayoutPanel.Width;
        var offsetHeight = this.Height - this.BoardLayoutPanel.Height;
        this.Width = 300 + offsetWidth;
        this.Height = 300 + offsetHeight;

        var size = 3;
        var boxPanels = new Dictionary<(int, int), TableLayoutPanel>();
        var boxCellLabels = new Dictionary<(int, int), Dictionary<(int, int), Label>>();
        var cellFont = new Font(this.Font.FontFamily, 12, FontStyle.Bold);
        this.BoardLayoutPanel.SuspendLayout();
        for (int boxRow = 0; boxRow < size; boxRow++)
        {
            for (int boxColumn = 0; boxColumn < size; boxColumn++)
            {
                var boxIndex = boxRow * size + boxColumn;
                var boxPanel = new TableLayoutPanel()
                {
                    Name = $"Box_{boxIndex}",
                    Margin = Padding.Empty,
                    Dock = DockStyle.Fill,
                    Tag = (boxRow, boxColumn),
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                };
                boxPanels[(boxRow, boxColumn)] = boxPanel;
                this.BoardLayoutPanel.Controls.Add(boxPanel);
                this.BoardLayoutPanel.SetCellPosition(boxPanel, new TableLayoutPanelCellPosition(boxColumn, boxRow));

                boxPanel.SuspendLayout();
                boxPanel.RowStyles.Clear();
                boxPanel.ColumnStyles.Clear();
                for (int cellRow = 0; cellRow < size; cellRow++)
                    boxPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 1.0f / size));
                for (int cellColumn = 0; cellColumn < size; cellColumn++)
                    boxPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.0f / size));
                boxPanel.RowCount = size;
                boxPanel.ColumnCount = size;
                var cellLabels = new Dictionary<(int, int), Label>();
                for (int cellRow = 0; cellRow < size; cellRow++)
                {
                    for (int cellColumn = 0; cellColumn < size; cellColumn++)
                    {
                        var cellIndex = cellRow * size + cellColumn;
                        var cellLabel = new Label()
                        {
                            Name = $"Cell_{boxIndex}_{cellIndex}",
                            Margin = Padding.Empty,
                            Font = cellFont,
                            TextAlign = ContentAlignment.MiddleCenter,
                            AutoSize = false,
                            Dock = DockStyle.Fill,
                            Tag = (boxRow, boxColumn, cellRow, cellColumn)
                        };
                        cellLabels[(cellRow, cellColumn)] = cellLabel;
                        boxPanel.Controls.Add(cellLabel);
                        boxPanel.SetCellPosition(cellLabel, new TableLayoutPanelCellPosition(cellColumn, cellRow));
                    }
                }
                boxCellLabels[(boxRow, boxColumn)] = cellLabels;
                boxPanel.ResumeLayout(true);
            }
        }
        this.BoardLayoutPanel.ResumeLayout(true);

        this.BoxPanels = boxPanels.ToFrozenDictionary();
        this.BoxCellLabels = boxCellLabels.ToFrozenDictionary(pair => pair.Key, pair => pair.Value.ToFrozenDictionary());
    }
}
