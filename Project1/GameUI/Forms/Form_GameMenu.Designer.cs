namespace GameUI
{
    partial class Form_GameMenu
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
            this.button_NewGame = new System.Windows.Forms.Button();
            this.button_LoadGame = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            this.button_Continue = new System.Windows.Forms.Button();
            this.button_LeaderBoard1 = new System.Windows.Forms.Button();
            this.button_LeaderBoard2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_NewGame
            // 
            this.button_NewGame.Location = new System.Drawing.Point(339, 130);
            this.button_NewGame.Name = "button_NewGame";
            this.button_NewGame.Size = new System.Drawing.Size(80, 55);
            this.button_NewGame.TabIndex = 0;
            this.button_NewGame.Text = "New Game";
            this.button_NewGame.UseVisualStyleBackColor = true;
            this.button_NewGame.Click += new System.EventHandler(this.button_NewGame_Click);
            // 
            // button_LoadGame
            // 
            this.button_LoadGame.Location = new System.Drawing.Point(339, 190);
            this.button_LoadGame.Name = "button_LoadGame";
            this.button_LoadGame.Size = new System.Drawing.Size(80, 55);
            this.button_LoadGame.TabIndex = 1;
            this.button_LoadGame.Text = "Load Game";
            this.button_LoadGame.UseVisualStyleBackColor = true;
            this.button_LoadGame.Click += new System.EventHandler(this.button_LoadGame_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.Location = new System.Drawing.Point(339, 251);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(80, 55);
            this.button_Exit.TabIndex = 2;
            this.button_Exit.Text = "Exit";
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // button_Continue
            // 
            this.button_Continue.Location = new System.Drawing.Point(339, 81);
            this.button_Continue.Name = "button_Continue";
            this.button_Continue.Size = new System.Drawing.Size(80, 43);
            this.button_Continue.TabIndex = 3;
            this.button_Continue.Text = "Continue";
            this.button_Continue.UseVisualStyleBackColor = true;
            this.button_Continue.Visible = false;
            this.button_Continue.Click += new System.EventHandler(this.button_Continue_Click);
            // 
            // button_LeaderBoard1
            // 
            this.button_LeaderBoard1.Location = new System.Drawing.Point(425, 130);
            this.button_LeaderBoard1.Name = "button_LeaderBoard1";
            this.button_LeaderBoard1.Size = new System.Drawing.Size(93, 55);
            this.button_LeaderBoard1.TabIndex = 4;
            this.button_LeaderBoard1.Text = "Level 1 Leaderboard";
            this.button_LeaderBoard1.UseVisualStyleBackColor = true;
            this.button_LeaderBoard1.Click += new System.EventHandler(this.button_LeaderBoard1_Click);
            // 
            // button_LeaderBoard2
            // 
            this.button_LeaderBoard2.Location = new System.Drawing.Point(425, 191);
            this.button_LeaderBoard2.Name = "button_LeaderBoard2";
            this.button_LeaderBoard2.Size = new System.Drawing.Size(93, 54);
            this.button_LeaderBoard2.TabIndex = 5;
            this.button_LeaderBoard2.Text = "Level 2 LeaderBoard";
            this.button_LeaderBoard2.UseVisualStyleBackColor = true;
            this.button_LeaderBoard2.Click += new System.EventHandler(this.button_LeaderBoard2_Click);
            // 
            // Form_GameMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_LeaderBoard2);
            this.Controls.Add(this.button_LeaderBoard1);
            this.Controls.Add(this.button_Continue);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_LoadGame);
            this.Controls.Add(this.button_NewGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_GameMenu";
            this.Text = "Form_GameMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_NewGame;
        private System.Windows.Forms.Button button_LoadGame;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Button button_Continue;
        private System.Windows.Forms.Button button_LeaderBoard1;
        private System.Windows.Forms.Button button_LeaderBoard2;
    }
}