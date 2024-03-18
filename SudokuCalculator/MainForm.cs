using System.Collections.Frozen;

namespace SudokuCalculator;

public partial class MainForm : Form
{
    protected static Font ConfirmedFont = new Font(SystemFonts.DialogFont.FontFamily, 12, FontStyle.Bold);
    protected static Font ProbableFont = new Font(SystemFonts.DialogFont.FontFamily, 8, FontStyle.Regular);

    private Label? selectedCellLabel;

    protected FrozenDictionary<BoxCellLocation, Label> BoxCellLabels { get; init; }
    protected FrozenDictionary<BoxCellLocation, CellEntity> BoxCellEntities { get; init; }
    protected SudokuCalculator SudokuCalculator { get; init; } = new();
    protected Label? SelectedCellLabel
    {
        get => selectedCellLabel;
        set
        {
            var previousCellLabel = selectedCellLabel;
            selectedCellLabel = value;
            if (previousCellLabel is not null)
                previousCellLabel.BackColor = Color.Transparent;
            if (selectedCellLabel is not null)
                selectedCellLabel.BackColor = Color.LightGray;
        }
    }

    public MainForm()
    {
        InitializeComponent();

        var offsetWidth = this.Width - this.BoardLayoutPanel.Width;
        var offsetHeight = this.Height - this.BoardLayoutPanel.Height;
        this.Width = 300 + offsetWidth;
        this.Height = 300 + offsetHeight;

        var size = 3;
        var boxCellLabels = new Dictionary<BoxCellLocation, Label>();
        var boxCellEntities = new Dictionary<BoxCellLocation, CellEntity>();
        this.BoardLayoutPanel.SuspendLayout();
        for (byte boxRow = 0; boxRow < size; boxRow++)
        {
            for (byte boxColumn = 0; boxColumn < size; boxColumn++)
            {
                var boxLocation = new Location(boxRow, boxColumn);
                var boxIndex = boxRow * size + boxColumn;
                var boxPanel = new TableLayoutPanel()
                {
                    Name = $"Box_{boxIndex}",
                    Margin = Padding.Empty,
                    Dock = DockStyle.Fill,
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                };
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
                for (byte cellRow = 0; cellRow < size; cellRow++)
                {
                    for (byte cellColumn = 0; cellColumn < size; cellColumn++)
                    {
                        var cellLocation = new Location(cellRow, cellColumn);
                        var boxCellLocation = new BoxCellLocation(boxLocation, cellLocation);
                        var cellIndex = cellRow * size + cellColumn;
                        var cellLabel = new Label()
                        {
                            Name = $"Cell_{boxIndex}_{cellIndex}",
                            Margin = Padding.Empty,
                            TextAlign = ContentAlignment.MiddleCenter,
                            AutoSize = false,
                            Dock = DockStyle.Fill,
                            Tag = boxCellLocation
                        };
                        cellLabel.MouseUp += this.CellLabel_MouseUp;
                        boxCellLabels[boxCellLocation] = cellLabel;
                        boxPanel.Controls.Add(cellLabel);
                        boxPanel.SetCellPosition(cellLabel, new TableLayoutPanelCellPosition(cellColumn, cellRow));

                        var cellEntity = new CellEntity(boxCellLocation);
                        boxCellEntities[boxCellLocation] = cellEntity;
                    }
                }
                boxPanel.ResumeLayout(true);
            }
        }
        this.BoardLayoutPanel.ResumeLayout(true);

        this.CellContextMenuStrip.Items.Add(new ToolStripMenuItem() { Text = "Reset", Tag = 0 });
        this.CellContextMenuStrip.Items.AddRange(Enumerable.Range(1, size * size).Select(menuIndex => new ToolStripMenuItem()
        {
            Text = menuIndex.ToString(),
            Tag = menuIndex,
        }).ToArray());
        this.CellContextMenuStrip.ItemClicked += this.CellContextMenuStrip_ItemClicked;

        this.BoxCellLabels = boxCellLabels.ToFrozenDictionary();
        this.BoxCellEntities = boxCellEntities.ToFrozenDictionary();
    }

    private void CellLabel_MouseUp(object? sender, MouseEventArgs e)
    {
        if (sender is not Label cellLabel) return;
        if (e.Button == MouseButtons.Left)
        {
            this.SelectedCellLabel = this.SelectedCellLabel == cellLabel ? default : cellLabel;
        }
        else if (e.Button == MouseButtons.Right)
        {
            this.SelectedCellLabel = cellLabel;
            this.CellContextMenuStrip.Show(cellLabel, e.X, e.Y);
        }
    }

    private void CellContextMenuStrip_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
    {
        if (SelectedCellLabel is null ||
            e.ClickedItem?.Tag is not int targetNumber ||
            SelectedCellLabel.Tag is not BoxCellLocation cellLocation ||
            !BoxCellEntities.TryGetValue(cellLocation, out var cellEntity)) return;
        if (targetNumber == 0)
        {
            SelectedCellLabel.Font = ProbableFont;
            SelectedCellLabel.Text = string.Empty;
            cellEntity.Number = default;
        }
        else
        {
            SelectedCellLabel.Font = ConfirmedFont;
            SelectedCellLabel.Text = targetNumber.ToString();
            cellEntity.Number = targetNumber;
        }
    }

    private void RefreshCells()
    {
        foreach (var pair in BoxCellEntities)
        {
            if (!BoxCellLabels.TryGetValue(pair.Key, out var cellLabel)) return;
            if (pair.Value.Number.HasValue)
            {
                cellLabel.Font = ConfirmedFont;
                cellLabel.Text = pair.Value.Number.ToString();
            }
            else
            {
                cellLabel.Font = ProbableFont;
                cellLabel.Text = string.Join("", pair.Value.ProbableSet);
            }
        }
    }

    private void ClearStripButton_Click(object sender, EventArgs e)
    {
        foreach (var cellEntity in BoxCellEntities.Values)
        {
            cellEntity.Number = default;
            cellEntity.ProbableSet.Clear();
        }
        this.RefreshCells();
    }

    private void RefreshStripButton_Click(object sender, EventArgs e)
    {
        this.RefreshCells();
    }

    private void CalculateStripButton_Click(object sender, EventArgs e)
    {
        this.SudokuCalculator.Calculate(BoxCellEntities);
        this.RefreshCells();
    }
}
