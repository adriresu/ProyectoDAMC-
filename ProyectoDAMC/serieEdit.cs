using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Dapper;
using System.Data.SqlClient;

namespace ProyectoDAMC
{
    public partial class serieEdit : Form
    {
        public int serieToEdit;
        public string tittleToEdit;
        public serieEdit()
        {
            InitializeComponent();
            dateTimePickerEstreno.Format = DateTimePickerFormat.Custom;
            dateTimePickerEstreno.CustomFormat = "yyyy";
            dateTimePickerEstreno.ShowUpDown = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxTitulo.Text == "")
            {
                MessageBox.Show("Porfavor, complete el campo del titulo de la serie", "ERROR");
                return;
            }
            try
            {
                string url = "http://192.168.1.136:80";
                var client = new RestClient(url);
                var request = new RestRequest();
                request.AddParameter("Tipo", "GetSerieByTittle");
                request.AddParameter("tittle", textBoxTitulo.Text);
                request.AddHeader("header", "application/json");
                request.AddHeader("Accept", "application/json");
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                var response = client.Post(request);

                if (response.Content != "")
                {
                    var stringToConvert = response.Content.Substring(1, response.Content.Length - 2);

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    serieItem serieTemp = JsonConvert.DeserializeObject<serieItem>(stringToConvert, settings);
                    label7.Text = serieTemp.ID.ToString();
                    textBoxTitulo.Text = serieTemp.Titulo;
                    textBoxSinopsis.Text = serieTemp.Sinopsis;
                    dateTimePickerEstreno.Value = new DateTime(Convert.ToInt32(serieTemp.Anho_Estreno), 1, 1); ;
                    textBoxDirector.Text = serieTemp.Direccion;
                    foreach (int i in checkedListBoxGenero.CheckedIndices)
                    {
                        checkedListBoxGenero.SetItemCheckState(i, CheckState.Unchecked);
                    }
                    int indexGenero = checkedListBoxGenero.Items.IndexOf(serieTemp.Genero);
                    checkedListBoxGenero.SetItemChecked(indexGenero, true);

                    if (serieTemp.Estado == "Finalizada")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (serieTemp.Estado == "En emision")
                    {
                        radioButton2.Checked = true;
                    }
                    else
                    {
                        radioButton3.Checked = true;
                    }

                    if (serieTemp.Tipo == 0)
                    {
                        radioButton4.Checked = true;
                    }
                    else if (serieTemp.Tipo == 1)
                    {
                        radioButton5.Checked = true;
                    }
                    else
                    {
                        radioButton6.Checked = true;
                    }

                    Bitmap bmpReturn = null;

                    if (serieTemp.Caratula != null && serieTemp.Caratula != "")
                    {
                        byte[] byteBuffer = Convert.FromBase64String(serieTemp.Caratula);
                        MemoryStream memoryStream = new MemoryStream(byteBuffer);
                        memoryStream.Position = 0;
                        bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);
                        memoryStream.Close();
                        memoryStream = null;
                        byteBuffer = null;

                        pictureBox1.Image = bmpReturn;
                    }
                    else
                    {
                        pictureBox1.Image = bmpReturn;
                    }

                    serieToEdit = serieTemp.ID;
                    button3.Enabled = true;
                    button2.Enabled = true;

                    tittleToEdit = textBoxTitulo.Text;
                }
                else
                {
                    MessageBox.Show("Serie no encontrada", "ERROR");
                    serieToEdit = -1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void checkedListBoxGenero_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = checkedListBoxGenero.SelectedIndex;
            int count = checkedListBoxGenero.Items.Count;

            for (int i = 0; i < count; i++)
            {
                if (index != i)
                {
                    checkedListBoxGenero.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG)" + "|All files (*.*)|*.*";
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap image = new Bitmap(dialog.FileName);
                pictureBox1.Image = (Image)image;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Boolean flag = true;

            if (textBoxTitulo.Text == "" || textBoxTitulo.Text.Length < 3)
            {
                textBoxTitulo.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (checkedListBoxGenero.CheckedItems.Count < 1)
            {
                checkedListBoxGenero.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
            {
                groupBox1.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (radioButton4.Checked == false && radioButton5.Checked == false && radioButton6.Checked == false)
            {
                groupBox2.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (textBoxDirector.Text == "" || textBoxDirector.Text.Length < 3)
            {
                textBoxDirector.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }

            if (flag)
            {
                try
                {
                    string url = "http://192.168.1.136:80";
                    var client = new RestClient(url);
                    var Prerequest = new RestRequest();

                    Prerequest.AddParameter("Tipo", "CheckIfSerieFromAdmin");
                    Prerequest.AddParameter("titulo", textBoxTitulo.Text);
                    Prerequest.AddParameter("id", label7.Text);

                    Prerequest.AddHeader("header", "application/json");
                    Prerequest.AddHeader("Accept", "application/json");
                    Prerequest.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                    var responseUserExists = client.Post(Prerequest);
                    var contentUserExists = responseUserExists.Content;

                    if (contentUserExists != null && contentUserExists != "")
                    {
                        MessageBox.Show("Nombre de serie ya existente", "ERROR");
                        return;
                    }

                    var request = new RestRequest();
                    request.AddParameter("Tipo", "UpdateSerieFromAdmin");
                    request.AddParameter("titulo", textBoxTitulo.Text);
                    request.AddParameter("anhoEstreno", (dateTimePickerEstreno.Value.Year).ToString());
                    request.AddParameter("id", serieToEdit);
                    request.AddParameter("genero", checkedListBoxGenero.CheckedItems[0].ToString());
                    request.AddParameter("director", textBoxDirector.Text);
                    request.AddParameter("sinopsis", textBoxSinopsis.Text);
                    if (pictureBox1.Image != null)
                    {
                        Bitmap default_image = new Bitmap(pictureBox1.Image);
                        ImageConverter converter = new ImageConverter();
                        byte[] bytes = (byte[])converter.ConvertTo(default_image, typeof(byte[]));
                        string base64String = Convert.ToBase64String(bytes);
                        request.AddParameter("caratula", base64String);
                    }
                    else
                    {
                        request.AddParameter("caratula", "");
                    }

                    if (radioButton1.Checked == true)
                    {
                        request.AddParameter("estado", "Finalizada");
                    }
                    else if(radioButton2.Checked == true)
                    {
                        request.AddParameter("estado", "En emision");
                    }
                    else if (radioButton3.Checked == true)
                    {
                        request.AddParameter("estado", "Pausada");
                    }

                    if (radioButton4.Checked == true)
                    {
                        request.AddParameter("type", "0");
                    }
                    else if (radioButton5.Checked == true)
                    {
                        request.AddParameter("type", "1");
                    }
                    else if (radioButton6.Checked == true)
                    {
                        request.AddParameter("type", "2");
                    }


                    request.AddHeader("header", "application/json");
                    request.AddHeader("Accept", "application/json");
                    request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                    var response = client.Post(request);
                    var content = response.Content;

                    MessageBox.Show("Serie modificada con exito", "CORRECTO");



                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                MessageBox.Show("Rellene los campos correctamente", "ERROR");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "http://192.168.1.136:80";
                var client = new RestClient(url);
                var request = new RestRequest();
                request.AddParameter("Tipo", "RemoveSerie");
                if (serieToEdit >= 0)
                {
                    request.AddParameter("id", serieToEdit);
                }
                else
                {
                    MessageBox.Show("ID de serie invalida", "ERROR");
                    return;
                }
                request.AddHeader("header", "application/json");
                request.AddHeader("Accept", "application/json");
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var response = client.Post(request);

                textBoxTitulo.Clear();
                textBoxSinopsis.Clear();
                textBoxDirector.Clear();
                label7.Text = "";
                pictureBox1.Image = null;

                foreach (int i in checkedListBoxGenero.CheckedIndices)
                {
                    checkedListBoxGenero.SetItemCheckState(i, CheckState.Unchecked);
                }

                serieToEdit = -1;
                MessageBox.Show("Seie eliminada con exito", "CORECTO");
            }
            catch (Exception)
            {
                MessageBox.Show("Error al eliminar usuario", "ERROR");
                throw;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Boolean flag = true;

            if (textBoxTitulo.Text == "" || textBoxTitulo.Text.Length < 3)
            {
                textBoxTitulo.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (checkedListBoxGenero.CheckedItems.Count < 1)
            {
                checkedListBoxGenero.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
            {
                groupBox1.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (radioButton4.Checked == false && radioButton5.Checked == false && radioButton6.Checked == false)
            {
                groupBox2.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (textBoxDirector.Text == "" || textBoxDirector.Text.Length < 3)
            {
                textBoxDirector.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }


            if (flag)
            {
                try
                {
                    string url = "http://192.168.1.136:80";
                    var client = new RestClient(url);
                    var Prerequest = new RestRequest();

                    Prerequest.AddParameter("Tipo", "CheckIfSerieNameFromAdmin");
                    Prerequest.AddParameter("titulo", textBoxTitulo.Text);

                    Prerequest.AddHeader("header", "application/json");
                    Prerequest.AddHeader("Accept", "application/json");
                    Prerequest.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                    var responseUserExists = client.Post(Prerequest);
                    var contentUserExists = responseUserExists.Content;

                    if (contentUserExists != null && contentUserExists != "")
                    {
                        MessageBox.Show("Nombre de serie ya existente", "ERROR");
                        return;
                    }

                    var request = new RestRequest();
                    request.AddParameter("Tipo", "CreateSerieFromAdmin");
                    request.AddParameter("titulo", textBoxTitulo.Text);
                    request.AddParameter("anhoEstreno", dateTimePickerEstreno.Value.ToString());
                    request.AddParameter("genero", checkedListBoxGenero.CheckedItems[0].ToString());
                    request.AddParameter("director", textBoxDirector.Text);
                    request.AddParameter("sinopsis", textBoxSinopsis.Text);
                    if (radioButton1.Checked == true)
                    {
                        request.AddParameter("estado", "Finalizada");
                    }
                    else if (radioButton2.Checked == true)
                    {
                        request.AddParameter("estado", "En emision");
                    }
                    else if (radioButton3.Checked == true)
                    {
                        request.AddParameter("estado", "Pausada");
                    }

                    if (radioButton4.Checked == true)
                    {
                        request.AddParameter("type", "0");
                    }
                    else if (radioButton5.Checked == true)
                    {
                        request.AddParameter("type", "1");
                    }
                    else if (radioButton6.Checked == true)
                    {
                        request.AddParameter("type", "2");
                    }

                    if (pictureBox1.Image != null)
                    {
                        Bitmap default_image = new Bitmap(pictureBox1.Image);
                        ImageConverter converter = new ImageConverter();
                        byte[] bytes = (byte[])converter.ConvertTo(default_image, typeof(byte[]));
                        string base64String = Convert.ToBase64String(bytes);
                        request.AddParameter("caratula", base64String);
                    }
                    else
                    {
                        request.AddParameter("caratula", "");
                    }

                    if (radioButton1.Checked == true)
                    {
                        request.AddParameter("estado", "Finalizada");
                    }
                    else if (radioButton2.Checked == true)
                    {
                        request.AddParameter("estado", "En emision");
                    }
                    else if (radioButton3.Checked == true)
                    {
                        request.AddParameter("estado", "Pausada");
                    }

                    if (radioButton4.Checked == true)
                    {
                        request.AddParameter("type", "0");
                    }
                    else if (radioButton5.Checked == true)
                    {
                        request.AddParameter("type", "1");
                    }
                    else if (radioButton6.Checked == true)
                    {
                        request.AddParameter("type", "2");
                    }


                    request.AddHeader("header", "application/json");
                    request.AddHeader("Accept", "application/json");
                    request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                    var response = client.Post(request);
                    string content = response.Content;

                    var stringToConvert = response.Content.Substring(1, response.Content.Length - 2);

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    serieItem serieTemp = JsonConvert.DeserializeObject<serieItem>(stringToConvert, settings);
                    serieToEdit = serieTemp.ID;
                    label7.Text = serieTemp.ID.ToString();
                    MessageBox.Show("Serie creada con exito", "CORRECTO");



                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                MessageBox.Show("Rellene los campos correctamente", "ERROR");
            }
        }
    }
}
