using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameUI
{
    public partial class MainForm : Form
    {
        public static Panel MainPanel;
        public MainForm()
        {
            InitializeComponent();
            MainPanel = panel_Forms;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            


            Form_GameMenu GameMenu = new Form_GameMenu();

            EventManager EventManager = new EventManager(GameMenu);
            GameMenu.Dock = DockStyle.Fill;
            GameMenu.TopLevel = false;
            panel_Forms.Controls.Clear();
            panel_Forms.Controls.Add(GameMenu);
            GameMenu.Show();
        }
    }
}
