namespace Sudoku;

partial class Cell
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.MainProbsPanel = new TableLayoutPanel();
        this.SuspendLayout();
        // 
        // MainProbsPanel
        // 
        this.MainProbsPanel.ColumnCount = 1;
        this.MainProbsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        this.MainProbsPanel.Dock = DockStyle.Fill;
        this.MainProbsPanel.Location = new Point(0, 0);
        this.MainProbsPanel.Name = "MainProbsPanel";
        this.MainProbsPanel.RowCount = 1;
        this.MainProbsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        this.MainProbsPanel.Size = new Size(150, 150);
        this.MainProbsPanel.TabIndex = 0;
        // 
        // Cell
        // 
        this.AutoScaleDimensions = new SizeF(8F, 20F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.Controls.Add(this.MainProbsPanel);
        this.Name = "Cell";
        this.ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel MainProbsPanel;
}
