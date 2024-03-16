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
        this.MainLabel = new Label();
        this.MainProbsPanel.SuspendLayout();
        this.SuspendLayout();
        // 
        // MainProbsPanel
        // 
        this.MainProbsPanel.ColumnCount = 1;
        this.MainProbsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        this.MainProbsPanel.Controls.Add(this.MainLabel, 0, 0);
        this.MainProbsPanel.Dock = DockStyle.Fill;
        this.MainProbsPanel.Location = new Point(0, 0);
        this.MainProbsPanel.Name = "MainProbsPanel";
        this.MainProbsPanel.RowCount = 1;
        this.MainProbsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        this.MainProbsPanel.Size = new Size(150, 150);
        this.MainProbsPanel.TabIndex = 0;
        // 
        // MainLabel
        // 
        this.MainLabel.Dock = DockStyle.Fill;
        this.MainLabel.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
        this.MainLabel.Location = new Point(3, 0);
        this.MainLabel.Name = "MainLabel";
        this.MainLabel.Size = new Size(144, 150);
        this.MainLabel.TabIndex = 0;
        this.MainLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // Cell
        // 
        this.AutoScaleDimensions = new SizeF(8F, 20F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.Controls.Add(this.MainProbsPanel);
        this.Name = "Cell";
        this.MainProbsPanel.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel MainProbsPanel;
    private Label MainLabel;
}
