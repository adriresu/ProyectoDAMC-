namespace ProyectoDAMC
{
    partial class ListViewItems
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.picCaratula = new System.Windows.Forms.PictureBox();
            this.lblTittle = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.lblSynopsis = new System.Windows.Forms.Label();
            this.lblNota = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picCaratula)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picCaratula
            // 
            this.picCaratula.Location = new System.Drawing.Point(63, 30);
            this.picCaratula.Name = "picCaratula";
            this.picCaratula.Size = new System.Drawing.Size(209, 212);
            this.picCaratula.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCaratula.TabIndex = 0;
            this.picCaratula.TabStop = false;
            // 
            // lblTittle
            // 
            this.lblTittle.AutoSize = true;
            this.lblTittle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTittle.Location = new System.Drawing.Point(368, 45);
            this.lblTittle.Name = "lblTittle";
            this.lblTittle.Size = new System.Drawing.Size(106, 46);
            this.lblTittle.TabIndex = 1;
            this.lblTittle.Text = "Tittle";
            this.lblTittle.MouseEnter += new System.EventHandler(this.ListViewItems_MouseEnter);
            this.lblTittle.MouseLeave += new System.EventHandler(this.ListViewItems_MouseLeave);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.Location = new System.Drawing.Point(839, 45);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(123, 52);
            this.lblState.TabIndex = 2;
            this.lblState.Text = "State";
            this.lblState.MouseEnter += new System.EventHandler(this.ListViewItems_MouseEnter);
            this.lblState.MouseLeave += new System.EventHandler(this.ListViewItems_MouseLeave);
            // 
            // lblSynopsis
            // 
            this.lblSynopsis.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSynopsis.Location = new System.Drawing.Point(368, 154);
            this.lblSynopsis.Name = "lblSynopsis";
            this.lblSynopsis.Size = new System.Drawing.Size(1058, 88);
            this.lblSynopsis.TabIndex = 3;
            this.lblSynopsis.Text = "ISSYNOPSISSYNOPSISSYNOPSISSYNOPSIS";
            this.lblSynopsis.MouseEnter += new System.EventHandler(this.ListViewItems_MouseEnter);
            this.lblSynopsis.MouseLeave += new System.EventHandler(this.ListViewItems_MouseLeave);
            // 
            // lblNota
            // 
            this.lblNota.AutoSize = true;
            this.lblNota.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNota.Location = new System.Drawing.Point(1510, 45);
            this.lblNota.Name = "lblNota";
            this.lblNota.Size = new System.Drawing.Size(245, 52);
            this.lblNota.TabIndex = 4;
            this.lblNota.Text = "Nota Media";
            this.lblNota.MouseEnter += new System.EventHandler(this.ListViewItems_MouseEnter);
            this.lblNota.MouseLeave += new System.EventHandler(this.ListViewItems_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel1.Controls.Add(this.picCaratula);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(335, 285);
            this.panel1.TabIndex = 5;
            this.panel1.MouseEnter += new System.EventHandler(this.ListViewItems_MouseEnter);
            this.panel1.MouseLeave += new System.EventHandler(this.ListViewItems_MouseLeave);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlText;
            this.panel2.Location = new System.Drawing.Point(341, 277);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1817, 10);
            this.panel2.TabIndex = 6;
            // 
            // ListViewItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblNota);
            this.Controls.Add(this.lblSynopsis);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.lblTittle);
            this.Name = "ListViewItems";
            this.Size = new System.Drawing.Size(2159, 285);
            this.MouseEnter += new System.EventHandler(this.ListViewItems_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ListViewItems_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.picCaratula)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picCaratula;
        private System.Windows.Forms.Label lblTittle;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblSynopsis;
        private System.Windows.Forms.Label lblNota;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
