namespace SudokuCalculator;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        this.MainToolStrip = new ToolStrip();
        this.ClearStripButton = new ToolStripButton();
        this.RefreshStripButton = new ToolStripButton();
        this.CalculateStripSeparator = new ToolStripSeparator();
        this.CalculateStripButton = new ToolStripButton();
        this.BoardLayoutPanel = new TableLayoutPanel();
        this.CellContextMenuStrip = new ContextMenuStrip(this.components);
        this.LoadStripButton = new ToolStripButton();
        this.LoadStripSeparator = new ToolStripSeparator();
        this.MainToolStrip.SuspendLayout();
        this.SuspendLayout();
        // 
        // MainToolStrip
        // 
        this.MainToolStrip.Items.AddRange(new ToolStripItem[] { this.LoadStripButton, this.LoadStripSeparator, this.ClearStripButton, this.RefreshStripButton, this.CalculateStripSeparator, this.CalculateStripButton });
        this.MainToolStrip.Location = new Point(0, 0);
        this.MainToolStrip.Name = "MainToolStrip";
        this.MainToolStrip.Size = new Size(300, 25);
        this.MainToolStrip.TabIndex = 0;
        // 
        // ClearStripButton
        // 
        this.ClearStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
        this.ClearStripButton.Image = (Image)resources.GetObject("ClearStripButton.Image");
        this.ClearStripButton.ImageTransparentColor = Color.Magenta;
        this.ClearStripButton.Name = "ClearStripButton";
        this.ClearStripButton.Size = new Size(38, 22);
        this.ClearStripButton.Text = "Clear";
        this.ClearStripButton.Click += this.ClearStripButton_Click;
        // 
        // RefreshStripButton
        // 
        this.RefreshStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
        this.RefreshStripButton.Image = (Image)resources.GetObject("RefreshStripButton.Image");
        this.RefreshStripButton.ImageTransparentColor = Color.Magenta;
        this.RefreshStripButton.Name = "RefreshStripButton";
        this.RefreshStripButton.Size = new Size(50, 22);
        this.RefreshStripButton.Text = "Refresh";
        this.RefreshStripButton.Click += this.RefreshStripButton_Click;
        // 
        // CalculateStripSeparator
        // 
        this.CalculateStripSeparator.Name = "CalculateStripSeparator";
        this.CalculateStripSeparator.Size = new Size(6, 25);
        // 
        // CalculateStripButton
        // 
        this.CalculateStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
        this.CalculateStripButton.Image = (Image)resources.GetObject("CalculateStripButton.Image");
        this.CalculateStripButton.ImageTransparentColor = Color.Magenta;
        this.CalculateStripButton.Name = "CalculateStripButton";
        this.CalculateStripButton.Size = new Size(60, 22);
        this.CalculateStripButton.Text = "Calculate";
        this.CalculateStripButton.Click += this.CalculateStripButton_Click;
        // 
        // BoardLayoutPanel
        // 
        this.BoardLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
        this.BoardLayoutPanel.ColumnCount = 3;
        this.BoardLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        this.BoardLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        this.BoardLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        this.BoardLayoutPanel.Dock = DockStyle.Fill;
        this.BoardLayoutPanel.Location = new Point(0, 25);
        this.BoardLayoutPanel.Margin = new Padding(0);
        this.BoardLayoutPanel.Name = "BoardLayoutPanel";
        this.BoardLayoutPanel.RowCount = 3;
        this.BoardLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        this.BoardLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        this.BoardLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        this.BoardLayoutPanel.Size = new Size(300, 300);
        this.BoardLayoutPanel.TabIndex = 1;
        // 
        // CellContextMenuStrip
        // 
        this.CellContextMenuStrip.Name = "CellContextMenuStrip";
        this.CellContextMenuStrip.Size = new Size(61, 4);
        // 
        // LoadStripButton
        // 
        this.LoadStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
        this.LoadStripButton.Image = (Image)resources.GetObject("LoadStripButton.Image");
        this.LoadStripButton.ImageTransparentColor = Color.Magenta;
        this.LoadStripButton.Name = "LoadStripButton";
        this.LoadStripButton.Size = new Size(37, 22);
        this.LoadStripButton.Text = "Load";
        this.LoadStripButton.Click += this.LoadStripButton_Click;
        // 
        // LoadStripSeparator
        // 
        this.LoadStripSeparator.Name = "LoadStripSeparator";
        this.LoadStripSeparator.Size = new Size(6, 25);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(300, 325);
        this.Controls.Add(this.BoardLayoutPanel);
        this.Controls.Add(this.MainToolStrip);
        this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        this.Icon = (Icon)resources.GetObject("$this.Icon");
        this.Name = "MainForm";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Sudoku Calculator";
        this.MainToolStrip.ResumeLayout(false);
        this.MainToolStrip.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private ToolStrip MainToolStrip;
    private TableLayoutPanel BoardLayoutPanel;
    private ContextMenuStrip CellContextMenuStrip;
    private ToolStripButton ClearStripButton;
    private ToolStripButton RefreshStripButton;
    private ToolStripButton CalculateStripButton;
    private ToolStripSeparator CalculateStripSeparator;
    private ToolStripButton LoadStripButton;
    private ToolStripSeparator LoadStripSeparator;
}
