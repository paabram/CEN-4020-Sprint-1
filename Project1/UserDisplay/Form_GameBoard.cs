using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserDisplay
{
    public partial class Form_GameBoard : Form
    {
        Form_GameMenu menu;

        public Form_GameBoard()
        {
            InitializeComponent();
        }

        public Form_GameMenu GetGameMenu() { return menu; }
        public void SetGameMenu(Form_GameMenu form_GameMenu) { menu = form_GameMenu; }

        private void Form_GameBoard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void Form_GameBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void button_ReturnToMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            menu.Show();
        }

        private void button_5_4_Click(object sender, EventArgs e)
        {

        }

        private void button_1_1_Click(object sender, EventArgs e)
        {

        }

        private void button_1_2_Click(object sender, EventArgs e)
        {

        }

        private void button_1_3_Click(object sender, EventArgs e)
        {

        }

        private void button_1_4_Click(object sender, EventArgs e)
        {

        }

        private void button_1_5_Click(object sender, EventArgs e)
        {

        }

        private void button_2_1_Click(object sender, EventArgs e)
        {

        }

        private void button_2_2_Click(object sender, EventArgs e)
        {

        }

        private void button_2_3_Click(object sender, EventArgs e)
        {

        }

        private void button_2_4_Click(object sender, EventArgs e)
        {

        }

        private void button_2_5_Click(object sender, EventArgs e)
        {

        }

        private void button_3_5_Click(object sender, EventArgs e)
        {

        }

        private void button_3_4_Click(object sender, EventArgs e)
        {

        }

        private void button_3_3_Click(object sender, EventArgs e)
        {

        }

        private void button_3_2_Click(object sender, EventArgs e)
        {

        }

        private void button_3_1_Click(object sender, EventArgs e)
        {

        }

        private void button_4_1_Click(object sender, EventArgs e)
        {

        }

        private void button_4_2_Click(object sender, EventArgs e)
        {

        }

        private void button_4_3_Click(object sender, EventArgs e)
        {

        }

        private void button_4_4_Click(object sender, EventArgs e)
        {

        }

        private void button_4_5_Click(object sender, EventArgs e)
        {

        }

        private void button_5_1_Click(object sender, EventArgs e)
        {

        }

        private void button_5_2_Click(object sender, EventArgs e)
        {

        }

        private void button_5_3_Click(object sender, EventArgs e)
        {

        }

        private void button_5_5_Click(object sender, EventArgs e)
        {

        }

        private void button_Undo_Click(object sender, EventArgs e)
        {

        }
    }
}
