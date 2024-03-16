﻿namespace Sudoku;

public partial class Cell : UserControl
{
    private int? confirmedNumber;
    private int realNumber;
    private bool numberMatched;

    public int RealNumber
    {
        get => realNumber;
        set
        {
            realNumber = value;
            this.NumberMatched = this.ConfirmedNumber.HasValue && this.ConfirmedNumber.Value == value;
        }
    }

    public int? ConfirmedNumber
    {
        get => confirmedNumber;
        set
        {
            confirmedNumber = value;
            this.MainProbsPanel.Visible = !value.HasValue;
            this.MainLabel.Visible = value.HasValue;
            this.MainLabel.Text = value.ToString();
            this.NumberMatched = value.HasValue && value.Value == this.RealNumber;
        }
    }

    public bool NumberMatched
    {
        get => numberMatched;
        set
        {
            numberMatched = value;
            this.BackColor = this.ConfirmedNumber.HasValue && this.ConfirmedNumber.Value != this.RealNumber ?
                Color.Red : Color.Transparent;
        }
    }

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
                var prob = new Prob()
                {
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = Padding.Empty,
                    Dock = DockStyle.Fill,
                    Number = x + y * cellRowCount + 1,
                    Toggle = false
                };
                prob.MouseUp += this.Prob_MouseUp;
                this.MainProbsPanel.Controls.Add(prob);
                this.MainProbsPanel.SetCellPosition(prob, new TableLayoutPanelCellPosition(x, y));
            }
        }
    }

    private void Prob_MouseUp(object? sender, MouseEventArgs e)
    {
        if (sender is not Prob prob) return;
        if (e.Button == MouseButtons.Left)
        {
            prob.Toggle = !prob.Toggle;
        }
        else if (e.Button == MouseButtons.Right)
        {
            this.ConfirmedNumber = prob.Number;
        }
    }

    private void MainLabel_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            this.ConfirmedNumber = default;
        }
    }
}
