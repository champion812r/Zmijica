
namespace Zmijica
{
    partial class optionsControl
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
            this.btnControls = new System.Windows.Forms.Button();
            this.btnResume = new System.Windows.Forms.Button();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnControls
            // 
            this.btnControls.Location = new System.Drawing.Point(35, 243);
            this.btnControls.Name = "btnControls";
            this.btnControls.Size = new System.Drawing.Size(149, 64);
            this.btnControls.TabIndex = 8;
            this.btnControls.Text = "Controls";
            this.btnControls.UseVisualStyleBackColor = true;
            // 
            // btnResume
            // 
            this.btnResume.Location = new System.Drawing.Point(35, 149);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(149, 64);
            this.btnResume.TabIndex = 7;
            this.btnResume.Text = "Resume";
            this.btnResume.UseVisualStyleBackColor = true;
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(35, 59);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(149, 64);
            this.btnNewGame.TabIndex = 6;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // optionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnControls);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnNewGame);
            this.Name = "optionsControl";
            this.Size = new System.Drawing.Size(216, 365);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnControls;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Button btnNewGame;
    }
}
