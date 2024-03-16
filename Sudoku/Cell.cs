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
                    Margin = Padding.Empty,
                    Dock = DockStyle.Fill,
                    Tag = x + y * cellRowCount + 1
                };
                prob.MouseUp += this.Prob_MouseUp;
                this.MainProbsPanel.Controls.Add(prob);
                this.MainProbsPanel.SetCellPosition(prob, new TableLayoutPanelCellPosition(x, y));
            }
        }
    }

    private void Prob_MouseUp(object? sender, MouseEventArgs e)
    {
        if (sender is not Label prob) return;
        if (e.Button == MouseButtons.Left)
        {
            prob.Text = string.IsNullOrEmpty(prob.Text) ? prob.Tag!.ToString() : string.Empty;
        }
        else if (e.Button == MouseButtons.Right)
        {
            this.MainProbsPanel.Hide();
            this.MainLabel.Show();
            this.MainLabel.Text = prob.Tag!.ToString();
        }
    }


    private void MainLabel_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            this.MainLabel.Hide();
            this.MainProbsPanel.Show();
            this.MainLabel.Text = string.Empty;
        }
    }
}
