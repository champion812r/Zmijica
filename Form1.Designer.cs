
namespace Zmijica
{
    partial class SnakeGraphics
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
            this.gameGrid = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // gameGrid
            // 
            this.gameGrid.BackColor = System.Drawing.SystemColors.Control;
            this.gameGrid.Location = new System.Drawing.Point(0, 0);
            this.gameGrid.Name = "gameGrid";
            this.gameGrid.Size = new System.Drawing.Size(841, 521);
            this.gameGrid.TabIndex = 0;
            this.gameGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.gameGrid_Paint);
            // 
            // SnakeGraphics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(840, 520);
            this.Controls.Add(this.gameGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SnakeGraphics";
            this.Text = "Snake Game";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel gameGrid;
    }
}

