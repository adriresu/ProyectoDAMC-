using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoDAMC
{
    public partial class ListViewItems : UserControl
    {
        public ListViewItems()
        {
            InitializeComponent();
        }

        private string _tittle;
        private string _nota;
        private string _state;
        private string _synopsis;
        private Image _caratula;

        [Category("Custom Properties")]
        public string Tittle
        {
            get { return _tittle; }
            set { _tittle = value; lblTittle.Text = value; }
        }

        [Category("Custom Properties")]
        public string Nota
        {
            get { return _nota; }
            set { _nota = value; lblNota.Text = value; }
        }

        [Category("Custom Properties")]
        public string Synopsis
        {
            get { return _synopsis; }
            set { _synopsis = value; lblSynopsis.Text = value; }
        }

        [Category("Custom Properties")]
        public string State
        {
            get { return _state; }
            set { _state = value; lblState.Text = value; }
        }

        [Category("Custom Properties")]
        public Image Caratula
        {
            get { return _caratula; }
            set { _caratula = value; picCaratula.Image = value; }
        }

        private void ListViewItems_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Silver;
        }

        private void ListViewItems_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }
    }
}
