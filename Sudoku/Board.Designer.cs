namespace Sudoku;

partial class Board
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
        this.MainBoxesPanel = new TableLayoutPanel();
        this.SuspendLayout();
        // 
        // MainBoxesPanel
        // 
        this.MainBoxesPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        this.MainBoxesPanel.ColumnCount = 1;
        this.MainBoxesPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        this.MainBoxesPanel.Dock = DockStyle.Fill;
        this.MainBoxesPanel.Location = new Point(0, 0);
        this.MainBoxesPanel.Name = "MainBoxesPanel";
        this.MainBoxesPanel.RowCount = 1;
        this.MainBoxesPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        this.MainBoxesPanel.Size = new Size(602, 602);
        this.MainBoxesPanel.TabIndex = 0;
        // 
        // Board
        // 
        this.AutoScaleDimensions = new SizeF(8F, 20F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.BackColor = Color.White;
        this.Controls.Add(this.MainBoxesPanel);
        this.Name = "Board";
        this.Size = new Size(602, 602);
        this.ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel MainBoxesPanel;
}
