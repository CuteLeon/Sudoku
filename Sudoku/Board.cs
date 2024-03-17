using System.Diagnostics;

namespace Sudoku;

public partial class Board : UserControl
{

    public Board()
    {
        InitializeComponent();
        this.DoubleBuffered = true;

        var size = Contracts.Size;
        this.CreateBoxes(this.MainBoxesPanel, size);
    }

    private void CreateBoxes(TableLayoutPanel boxesPanel, int size)
    {
        var boxPercent = 1.0f / size;
        boxesPanel.RowStyles.Clear();
        boxesPanel.ColumnStyles.Clear();
        for (int i = 0; i < size; i++)
        {
            boxesPanel.RowStyles.Add(new RowStyle(SizeType.Percent, boxPercent));
            boxesPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, boxPercent));
        }
        boxesPanel.RowCount = size;
        boxesPanel.ColumnCount = size;
        boxesPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;

        for (int boxX = 0; boxX < size; boxX++)
        {
            for (int boxY = 0; boxY < size; boxY++)
            {
                var cellsPanel = new TableLayoutPanel()
                {
                    Margin = Padding.Empty,
                    Dock = DockStyle.Fill,
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                };
                this.CreateCells(cellsPanel, size);
                boxesPanel.Controls.Add(cellsPanel);
                boxesPanel.SetCellPosition(cellsPanel, new TableLayoutPanelCellPosition(boxX, boxY));
            }
        }
    }

    private void CreateCells(TableLayoutPanel cellsPanel, int size)
    {
        var cellPercent = 1.0f / size;
        cellsPanel.RowStyles.Clear();
        cellsPanel.ColumnStyles.Clear();
        for (int i = 0; i < size; i++)
        {
            cellsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, cellPercent));
            cellsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, cellPercent));
        }
        cellsPanel.RowCount = size;
        cellsPanel.ColumnCount = size;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                var cell = new Cell()
                {
                    Margin = Padding.Empty,
                    Dock = DockStyle.Fill,
                };
                cellsPanel.Controls.Add(cell);
                cellsPanel.SetCellPosition(cell, new TableLayoutPanelCellPosition(x, y));
            }
        }
    }
}
