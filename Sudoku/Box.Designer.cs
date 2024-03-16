namespace Sudoku;

partial class Box
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
        this.MainCellsPanel = new TableLayoutPanel();
        this.SuspendLayout();
        // 
        // MainCellsPanel
        // 
        this.MainCellsPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        this.MainCellsPanel.ColumnCount = 1;
        this.MainCellsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        this.MainCellsPanel.Dock = DockStyle.Fill;
        this.MainCellsPanel.Location = new Point(0, 0);
        this.MainCellsPanel.Name = "MainCellsPanel";
        this.MainCellsPanel.RowCount = 1;
        this.MainCellsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        this.MainCellsPanel.Size = new Size(200, 200);
        this.MainCellsPanel.TabIndex = 0;
        // 
        // Box
        // 
        this.AutoScaleDimensions = new SizeF(8F, 20F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.Controls.Add(this.MainCellsPanel);
        this.Name = "Box";
        this.Size = new Size(200, 200);
        this.ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel MainCellsPanel;
}
