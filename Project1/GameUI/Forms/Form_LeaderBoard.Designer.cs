namespace GameUI.Forms
{
    partial class Form_LeaderBoard
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView_LeaderBoardDisplay = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_ReturnToMenu = new System.Windows.Forms.Button();
            this.gameStateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gameStateBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_LeaderBoardDisplay)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameStateBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameStateBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_LeaderBoardDisplay
            // 
            this.dataGridView_LeaderBoardDisplay.AutoGenerateColumns = false;
            this.dataGridView_LeaderBoardDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_LeaderBoardDisplay.DataSource = this.gameStateBindingSource;
            this.dataGridView_LeaderBoardDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_LeaderBoardDisplay.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_LeaderBoardDisplay.Name = "dataGridView_LeaderBoardDisplay";
            this.dataGridView_LeaderBoardDisplay.Size = new System.Drawing.Size(800, 450);
            this.dataGridView_LeaderBoardDisplay.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_ReturnToMenu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 382);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 68);
            this.panel1.TabIndex = 1;
            // 
            // button_ReturnToMenu
            // 
            this.button_ReturnToMenu.Location = new System.Drawing.Point(12, 3);
            this.button_ReturnToMenu.Name = "button_ReturnToMenu";
            this.button_ReturnToMenu.Size = new System.Drawing.Size(107, 62);
            this.button_ReturnToMenu.TabIndex = 0;
            this.button_ReturnToMenu.Text = "Back";
            this.button_ReturnToMenu.UseVisualStyleBackColor = true;
            this.button_ReturnToMenu.Click += new System.EventHandler(this.button_ReturnToMenu_Click);
            // 
            // gameStateBindingSource
            // 
            this.gameStateBindingSource.DataSource = typeof(GameUI.GameState);
            // 
            // gameStateBindingSource1
            // 
            this.gameStateBindingSource1.DataSource = typeof(GameUI.GameState);
            // 
            // Form_LeaderBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView_LeaderBoardDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_LeaderBoard";
            this.Text = "Form_LeaderBoard";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_LeaderBoardDisplay)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gameStateBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameStateBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_LeaderBoardDisplay;
        private System.Windows.Forms.BindingSource gameStateBindingSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_ReturnToMenu;
        private System.Windows.Forms.BindingSource gameStateBindingSource1;
    }
}