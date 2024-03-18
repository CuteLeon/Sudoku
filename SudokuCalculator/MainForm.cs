using System.Collections.Frozen;
using System.Diagnostics;

namespace SudokuCalculator;

public partial class MainForm : Form
{
    private Label? selectedCellLabel;

    protected FrozenDictionary<CellLocation, Label> BoxCellLabels { get; init; }
    protected FrozenDictionary<CellLocation, CellEntity> BoxCellEntities { get; init; }
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
        var boxCellLabels = new Dictionary<CellLocation, Label>();
        var boxCellEntities = new Dictionary<CellLocation, CellEntity>();
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
                for (int cellRow = 0; cellRow < size; cellRow++)
                {
                    for (int cellColumn = 0; cellColumn < size; cellColumn++)
                    {
                        var cellLocation = new CellLocation(boxRow, boxColumn, cellRow, cellColumn);
                        var cellIndex = cellRow * size + cellColumn;
                        var cellLabel = new Label()
                        {
                            Name = $"Cell_{boxIndex}_{cellIndex}",
                            Margin = Padding.Empty,
                            Font = cellFont,
                            TextAlign = ContentAlignment.MiddleCenter,
                            AutoSize = false,
                            Dock = DockStyle.Fill,
                            Tag = cellLocation
                        };
                        cellLabel.MouseUp += this.CellLabel_MouseUp;
                        boxCellLabels[cellLocation] = cellLabel;
                        boxPanel.Controls.Add(cellLabel);
                        boxPanel.SetCellPosition(cellLabel, new TableLayoutPanelCellPosition(cellColumn, cellRow));

                        var cellEntity = new CellEntity(cellLocation);
                        boxCellEntities[cellLocation] = cellEntity;
                    }
                }
                boxPanel.ResumeLayout(true);
            }
        }
        this.BoardLayoutPanel.ResumeLayout(true);

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
            SelectedCellLabel.Tag is not CellLocation cellLocation ||
            !BoxCellEntities.TryGetValue(cellLocation, out var cellEntity)) return;
        SelectedCellLabel.Text = targetNumber.ToString();
        cellEntity.Number = targetNumber;
    }

    private void RefreshCells()
    {
        foreach (var pair in BoxCellEntities)
        {
            if (!BoxCellLabels.TryGetValue(pair.Key, out var cellLabel)) return;
            cellLabel.Text = pair.Value.Number.ToString();
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