using System.Collections.Frozen;

namespace SudokuCalculator;

public partial class MainForm : Form
{
    protected static Font ConfirmedFont = new Font(SystemFonts.DialogFont.FontFamily, 12, FontStyle.Bold);
    protected static Font ProbableFont = new Font(SystemFonts.DialogFont.FontFamily, 8, FontStyle.Regular);
    private const byte GridSize = 3;
    private BoxCellLocation? selectedBoxCellLocation;

    protected FrozenDictionary<BoxCellLocation, Label> BoxCellLabels { get; init; }
    protected FrozenDictionary<BoxCellLocation, CellEntity> BoxCellEntities { get; init; }
    protected SudokuCalculator SudokuCalculator { get; init; } = new();
    protected BoxCellLocation? SelectedBoxCellLocation
    {
        get => selectedBoxCellLocation;
        set
        {
            var previousBoxCellLocation = selectedBoxCellLocation;
            selectedBoxCellLocation = value;
            if (previousBoxCellLocation is not null)
            {
                foreach (var relatedBoxCellLocation in this.GetRelatedBoxCells(previousBoxCellLocation.Value, GridSize))
                {
                    if (this.BoxCellLabels.TryGetValue(relatedBoxCellLocation, out var label))
                        label.BackColor = Color.Transparent;
                }
            }
            if (value.HasValue &&
                BoxCellLabels.TryGetValue(value.Value, out var selectedBoxCellLabel))
            {
                foreach (var relatedBoxCellLocation in this.GetRelatedBoxCells(value.Value, GridSize))
                {
                    if (this.BoxCellLabels.TryGetValue(relatedBoxCellLocation, out var label))
                        label.BackColor = Color.Gainsboro;
                }
                selectedBoxCellLabel.BackColor = Color.Silver;
            }
        }
    }

    public MainForm()
    {
        InitializeComponent();

        var offsetWidth = this.Width - this.BoardLayoutPanel.Width;
        var offsetHeight = this.Height - this.BoardLayoutPanel.Height;
        this.Width = 300 + offsetWidth;
        this.Height = 300 + offsetHeight;

        var size = GridSize;
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
                    BackColor = Color.White,
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
                            BackColor = Color.Transparent,
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

    private IEnumerable<BoxCellLocation> GetRelatedBoxCells(BoxCellLocation boxCellLocation, byte size)
    {
        var boxLocation = boxCellLocation.BoxLocation;
        var cellLocation = boxCellLocation.CellLocation;
        return this.SudokuCalculator.GetRowBoxCellLocations(boxLocation, cellLocation.Row, size)
            .Concat(this.SudokuCalculator.GetColumnBoxCellLocations(boxLocation, cellLocation.Column, size))
            .Concat(this.SudokuCalculator.GetCurrentBoxCellLocations(boxLocation, size));
    }

    private void CellLabel_MouseUp(object? sender, MouseEventArgs e)
    {
        if (sender is not Label cellLabel ||
            cellLabel.Tag is not BoxCellLocation boxCellLocation) return;

        if (e.Button == MouseButtons.Left)
        {
            this.SelectedBoxCellLocation = boxCellLocation.Equals(this.SelectedBoxCellLocation) ? default(BoxCellLocation?) : boxCellLocation;
        }
        else if (e.Button == MouseButtons.Right)
        {
            this.SelectedBoxCellLocation = boxCellLocation;
            this.CellContextMenuStrip.Show(cellLabel, e.X, e.Y);
        }
    }

    private void CellContextMenuStrip_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
    {
        if (SelectedBoxCellLocation is null ||
            e.ClickedItem?.Tag is not int targetNumber ||
            SelectedBoxCellLocation is null) return;

        this.SetCellNumber(SelectedBoxCellLocation.Value, targetNumber);
    }

    private void SetCellNumber(BoxCellLocation boxCellLocation, int? number)
    {
        if (!BoxCellEntities.TryGetValue(boxCellLocation, out var boxCellEntity) ||
            !BoxCellLabels.TryGetValue(boxCellLocation, out var boxCellLabel)) return;

        if (number is null or 0)
        {
            boxCellLabel.Font = ProbableFont;
            boxCellLabel.Text = string.Empty;
            boxCellEntity.Number = default(int?);
        }
        else
        {
            boxCellLabel.Font = ConfirmedFont;
            boxCellLabel.Text = number.ToString();
            boxCellEntity.Number = number;
        }
    }

    private void RefreshCells()
    {
        foreach (var pair in BoxCellEntities)
        {
            if (!BoxCellLabels.TryGetValue(pair.Key, out var cellLabel)) return;
            if (pair.Value.Number.HasValue)
            {
                var previousText = cellLabel.Text;
                var newText = pair.Value.Number.ToString();
                cellLabel.Font = ConfirmedFont;
                cellLabel.Text = newText;
                if (!string.Equals(previousText, newText))
                {
                    cellLabel.BackColor = Color.SkyBlue;
                }
                else
                {
                    cellLabel.BackColor = Color.Transparent;
                }
            }
            else
            {
                cellLabel.Font = ProbableFont;
                cellLabel.Text = string.Join("", pair.Value.ProbableSet);
                cellLabel.BackColor = Color.Transparent;
            }
        }
    }

    private void LoadStripButton_Click(object sender, EventArgs e)
    {
        try
        {
            this.ClearCells();

            using var openFileDialog = new OpenFileDialog()
            {
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            var size = GridSize;
            var filePath = openFileDialog.FileName;
            var lines = File.ReadAllLines(filePath);

            if (lines.Length != size * size) throw new FileFormatException($"File lines count [{lines.Length}] invalid.");
            var invalidLines = lines.Select((line, index) => (line, index)).Where(tuple => tuple.line.Length != size * size).ToArray();
            if (invalidLines.Any())
                throw new FileFormatException($"Line's length invalid:\n\t{string.Join("\n\t", invalidLines.Select(tuple => $"[{tuple.index}], Length={tuple.line.Length}"))}");

            foreach (var (line, lineIndex) in lines.Select((line, index) => (line, index)))
            {
                foreach (var (character, characterIndex) in line.AsEnumerable().Select((character, index) => (character, index)))
                {
                    var (boxRow, cellRow) = Math.DivRem(lineIndex, size);
                    var (boxColumn, cellColumn) = Math.DivRem(characterIndex, size);
                    var boxCellLocation = new BoxCellLocation(new Location((byte)boxRow, (byte)boxColumn), new Location((byte)cellRow, (byte)cellColumn));
                    var numberString = character.ToString();
                    var number = string.IsNullOrWhiteSpace(numberString) || !int.TryParse(numberString, out var parsedNumber) ? default(int?) : parsedNumber;
                    this.SetCellNumber(boxCellLocation, number);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ClearStripButton_Click(object sender, EventArgs e)
    {
        this.ClearCells();
    }

    private void ClearCells()
    {
        this.SelectedBoxCellLocation = default(BoxCellLocation?);
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
        this.SudokuCalculator.Calculate(BoxCellEntities, GridSize);
        this.RefreshCells();
    }
}
