
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
            this.components = new System.ComponentModel.Container();
            this.gameGrid = new System.Windows.Forms.Panel();
            this.btnFullGrid = new System.Windows.Forms.Button();
            this.btnClearGrid = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.trackSpeed = new System.Windows.Forms.TrackBar();
            this.tbIme = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // gameGrid
            // 
            this.gameGrid.BackColor = System.Drawing.Color.Black;
            this.gameGrid.Location = new System.Drawing.Point(12, 12);
            this.gameGrid.Name = "gameGrid";
            this.gameGrid.Size = new System.Drawing.Size(841, 521);
            this.gameGrid.TabIndex = 0;
            // 
            // btnFullGrid
            // 
            this.btnFullGrid.Location = new System.Drawing.Point(875, 384);
            this.btnFullGrid.Name = "btnFullGrid";
            this.btnFullGrid.Size = new System.Drawing.Size(136, 29);
            this.btnFullGrid.TabIndex = 1;
            this.btnFullGrid.Text = "Prikazi full grid";
            this.btnFullGrid.UseVisualStyleBackColor = true;
            this.btnFullGrid.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClearGrid
            // 
            this.btnClearGrid.Location = new System.Drawing.Point(875, 419);
            this.btnClearGrid.Name = "btnClearGrid";
            this.btnClearGrid.Size = new System.Drawing.Size(136, 29);
            this.btnClearGrid.TabIndex = 2;
            this.btnClearGrid.Text = "Ocisti grid";
            this.btnClearGrid.UseVisualStyleBackColor = true;
            this.btnClearGrid.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(875, 181);
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
            this.label1.Location = new System.Drawing.Point(883, 451);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Simulacija brzine";
            // 
            // trackSpeed
            // 
            this.trackSpeed.Location = new System.Drawing.Point(875, 59);
            this.trackSpeed.Name = "trackSpeed";
            this.trackSpeed.Size = new System.Drawing.Size(130, 56);
            this.trackSpeed.TabIndex = 5;
            // 
            // tbIme
            // 
            this.tbIme.Location = new System.Drawing.Point(875, 110);
            this.tbIme.Name = "tbIme";
            this.tbIme.Size = new System.Drawing.Size(136, 27);
            this.tbIme.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(926, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Ime";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(875, 237);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(136, 50);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // SnakeGraphics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1023, 548);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbIme);
            this.Controls.Add(this.trackSpeed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnClearGrid);
            this.Controls.Add(this.btnFullGrid);
            this.Controls.Add(this.gameGrid);
            this.Name = "SnakeGraphics";
            this.Text = "Snake Game";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SnakeGraphics_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.trackSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel gameGrid;
        private System.Windows.Forms.Button btnFullGrid;
        private System.Windows.Forms.Button btnClearGrid;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackSpeed;
        private System.Windows.Forms.TextBox tbIme;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ImageList imageList1;
    }
}

