using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProyectoDAMC
{
    public partial class selection : Form
    {
        public selection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usersEdit menu = new usersEdit();
            this.Hide();
            menu.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serieEdit menu = new serieEdit();
            this.Hide();
            menu.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CharacterEdit menu = new CharacterEdit();
            this.Hide();
            menu.Show();
        }
    }
}
