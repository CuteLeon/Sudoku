namespace Sudoku;

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
        this.MainToolStrip = new ToolStrip();
        this.SuspendLayout();
        // 
        // MainToolStrip
        // 
        this.MainToolStrip.ImageScalingSize = new Size(20, 20);
        this.MainToolStrip.Location = new Point(0, 0);
        this.MainToolStrip.Name = "MainToolStrip";
        this.MainToolStrip.Size = new Size(600, 25);
        this.MainToolStrip.TabIndex = 0;
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new SizeF(8F, 20F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(600, 625);
        this.Controls.Add(this.MainToolStrip);
        this.DoubleBuffered = true;
        this.Margin = new Padding(2);
        this.Name = "MainForm";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Sudoku";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private ToolTip toolTip1;
    private ToolStrip MainToolStrip;
}
