using System.Diagnostics;

namespace Sudoku;

public partial class MainForm : Form
{
    public MainForm()
    {
        Icon = AppResource.Icon;
        InitializeComponent();
        Debug.Print($"{this.ClientSize} / {this.Size}");
    }
}
