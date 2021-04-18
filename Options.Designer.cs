
namespace Zmijica
{
    partial class Options
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnNewGame = new System.Windows.Forms.Button();
            this.btnResume = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trackSpeed = new System.Windows.Forms.TrackBar();
            this.btnResults = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.rbWASD = new System.Windows.Forms.RadioButton();
            this.rbArrows = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.trackSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(44, 193);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(235, 48);
            this.btnNewGame.TabIndex = 0;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // btnResume
            // 
            this.btnResume.Location = new System.Drawing.Point(44, 247);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(235, 48);
            this.btnResume.TabIndex = 1;
            this.btnResume.Text = "Resume";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(133, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Name";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(94, 129);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(125, 27);
            this.tbName.TabIndex = 20;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(133, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "Speed";
            // 
            // trackSpeed
            // 
            this.trackSpeed.Location = new System.Drawing.Point(89, 12);
            this.trackSpeed.Maximum = 4;
            this.trackSpeed.Minimum = 1;
            this.trackSpeed.Name = "trackSpeed";
            this.trackSpeed.Size = new System.Drawing.Size(130, 56);
            this.trackSpeed.TabIndex = 18;
            this.trackSpeed.Value = 1;
            // 
            // btnResults
            // 
            this.btnResults.Location = new System.Drawing.Point(165, 301);
            this.btnResults.Name = "btnResults";
            this.btnResults.Size = new System.Drawing.Size(115, 48);
            this.btnResults.TabIndex = 22;
            this.btnResults.Text = "Results";
            this.btnResults.UseVisualStyleBackColor = true;
            this.btnResults.Click += new System.EventHandler(this.btnResults_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(44, 301);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(115, 48);
            this.btnExit.TabIndex = 23;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // rbWASD
            // 
            this.rbWASD.AutoSize = true;
            this.rbWASD.Location = new System.Drawing.Point(83, 89);
            this.rbWASD.Name = "rbWASD";
            this.rbWASD.Size = new System.Drawing.Size(72, 24);
            this.rbWASD.TabIndex = 24;
            this.rbWASD.TabStop = true;
            this.rbWASD.Text = "WASD";
            this.rbWASD.UseVisualStyleBackColor = true;
            // 
            // rbArrows
            // 
            this.rbArrows.AutoSize = true;
            this.rbArrows.Location = new System.Drawing.Point(161, 89);
            this.rbArrows.Name = "rbArrows";
            this.rbArrows.Size = new System.Drawing.Size(76, 24);
            this.rbArrows.TabIndex = 25;
            this.rbArrows.TabStop = true;
            this.rbArrows.Text = "Arrows";
            this.rbArrows.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 377);
            this.Controls.Add(this.rbArrows);
            this.Controls.Add(this.rbWASD);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnResults);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackSpeed);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnNewGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Options";
            this.Text = "press \'P\' for pause";
            ((System.ComponentModel.ISupportInitialize)(this.trackSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackSpeed;
        private System.Windows.Forms.Button btnResults;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RadioButton rbWASD;
        private System.Windows.Forms.RadioButton rbArrows;
    }
}