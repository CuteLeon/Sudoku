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
        this.BoardLayoutPanel = new TableLayoutPanel();
        this.CellContextMenuStrip = new ContextMenuStrip(this.components);
        this.MainToolStrip.SuspendLayout();
        this.SuspendLayout();
        // 
        // MainToolStrip
        // 
        this.MainToolStrip.Location = new Point(0, 0);
        this.MainToolStrip.Name = "MainToolStrip";
        this.MainToolStrip.Size = new Size(300, 25);
        this.MainToolStrip.TabIndex = 0;
        // 
        // BoardLayoutPanel
        // 
        this.BoardLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
        this.BoardLayoutPanel.ColumnCount = 3;
        this.BoardLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        this.BoardLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
        this.BoardLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
        this.BoardLayoutPanel.Dock = DockStyle.Fill;
        this.BoardLayoutPanel.Location = new Point(0, 25);
        this.BoardLayoutPanel.Margin = new Padding(0);
        this.BoardLayoutPanel.Name = "BoardLayoutPanel";
        this.BoardLayoutPanel.RowCount = 3;
        this.BoardLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        this.BoardLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
        this.BoardLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333359F));
        this.BoardLayoutPanel.Size = new Size(300, 300);
        this.BoardLayoutPanel.TabIndex = 1;
        // 
        // CellContextMenuStrip
        // 
        this.CellContextMenuStrip.Name = "CellContextMenuStrip";
        this.CellContextMenuStrip.Size = new Size(61, 4);
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
}
