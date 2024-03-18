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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        this.MainToolStrip = new ToolStrip();
        this.SuspendLayout();
        // 
        // MainToolStrip
        // 
        this.MainToolStrip.Location = new Point(0, 0);
        this.MainToolStrip.Name = "MainToolStrip";
        this.MainToolStrip.Size = new Size(525, 25);
        this.MainToolStrip.TabIndex = 0;
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(525, 469);
        this.Controls.Add(this.MainToolStrip);
        this.Icon = (Icon)resources.GetObject("$this.Icon");
        this.Name = "MainForm";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Sudoku Calculator";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private ToolStrip MainToolStrip;
}
