namespace Sudoku;

public partial class Prob : Label
{
    private bool toggle;

    public int Number { get; set; }

    public bool Toggle
    {
        get => toggle;
        set
        {
            toggle = value;
            this.Text = value ? this.Number.ToString() : string.Empty;
        }
    }

    public Prob() : base()
    {
        InitializeComponent();
        this.DoubleBuffered = true;
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        if (!this.Toggle)
        {
            this.Text = this.Number.ToString();
        }
        base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        if (!this.Toggle)
        {
            this.Text = string.Empty;
        }
        base.OnMouseLeave(e);
    }
}
