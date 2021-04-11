
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
            this.btnFullGrid = new System.Windows.Forms.Button();
            this.btnClearGrid = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gameGrid
            // 
            this.gameGrid.Location = new System.Drawing.Point(12, 12);
            this.gameGrid.Name = "gameGrid";
            this.gameGrid.Size = new System.Drawing.Size(631, 421);
            this.gameGrid.TabIndex = 0;
            // 
            // btnFullGrid
            // 
            this.btnFullGrid.Location = new System.Drawing.Point(652, 297);
            this.btnFullGrid.Name = "btnFullGrid";
            this.btnFullGrid.Size = new System.Drawing.Size(136, 29);
            this.btnFullGrid.TabIndex = 1;
            this.btnFullGrid.Text = "Prikazi full grid";
            this.btnFullGrid.UseVisualStyleBackColor = true;
            this.btnFullGrid.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClearGrid
            // 
            this.btnClearGrid.Location = new System.Drawing.Point(652, 366);
            this.btnClearGrid.Name = "btnClearGrid";
            this.btnClearGrid.Size = new System.Drawing.Size(136, 29);
            this.btnClearGrid.TabIndex = 2;
            this.btnClearGrid.Text = "Ocisti grid";
            this.btnClearGrid.UseVisualStyleBackColor = true;
            this.btnClearGrid.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(649, 110);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(136, 50);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(652, 413);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // SnakeGraphics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnClearGrid);
            this.Controls.Add(this.btnFullGrid);
            this.Controls.Add(this.gameGrid);
            this.Name = "SnakeGraphics";
            this.Text = "Snake Game";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SnakeGraphics_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel gameGrid;
        private System.Windows.Forms.Button btnFullGrid;
        private System.Windows.Forms.Button btnClearGrid;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
    }
}

