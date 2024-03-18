namespace SudokuCalculator;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();

        var offsetWidth = this.Width - this.BoardLayoutPanel.Width;
        var offsetHeight = this.Height - this.BoardLayoutPanel.Height;
        this.Width = 300 + offsetWidth;
        this.Height = 300 + offsetHeight;
    }
}
