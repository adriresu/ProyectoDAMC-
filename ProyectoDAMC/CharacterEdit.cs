using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ProyectoDAMC
{
    public partial class CharacterEdit : Form
    {
        public int characterIDToEdit;
        public string characterToEdit;
        public CharacterEdit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxID.Text == "")
            {
                MessageBox.Show("Porfavor, complete el campo del titulo de la serie", "ERROR");
                return;
            }
            try
            {
                string url = "http://192.168.1.136:80";
                var client = new RestClient(url);
                var request = new RestRequest();
                request.AddParameter("Tipo", "GetCharacterByID");
                request.AddParameter("id", textBoxID.Text);
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

                    Character characterTemp = JsonConvert.DeserializeObject<Character>(stringToConvert, settings);
                    textBoxID.Text = characterTemp.ID.ToString();
                    textBoxNombre.Text = characterTemp.Nombre;
                    textBoxApellidos.Text = characterTemp.Apellidos;
                    textBoxEdad.Text = characterTemp.Edad.ToString();
                    textBoxPoder.Text = characterTemp.Poder;
                    textBoxActor.Text = characterTemp.Actor;
                    textBoxPersonalidad.Text = characterTemp.Personalidad;
                    textBoxOrigen.Text = characterTemp.Origen;
                    textBoxDescripcion.Text = characterTemp.Descripcion;
                    foreach (var serie in listOfSeries)
                    {
                        if (serie.ID == characterTemp.ID_serie)
                        {
                            listBox1.SelectedItem = serie;
                            break;
                        }
                    }


                    //foreach (int i in checkedListBoxGenero.CheckedIndices)
                    //{
                    //    checkedListBoxGenero.SetItemCheckState(i, CheckState.Unchecked);
                    //}

                    Bitmap bmpReturn = null;

                    if (characterTemp.Imagen != null && characterTemp.Imagen != "")
                    {
                        byte[] byteBuffer = Convert.FromBase64String(characterTemp.Imagen);
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

                    characterIDToEdit = characterTemp.ID;
                    button3.Enabled = true;
                    button2.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Serie no encontrada", "ERROR");
                    characterIDToEdit = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static BindingList<serieItem> listOfSeries;
        private void CharacterEdit_Load(object sender, EventArgs e)
        {
            listOfSeries = new BindingList<serieItem>();
            listBox1.DataSource = listOfSeries;
            listBox1.DisplayMember = "Titulo";

            listOfSeries.Clear();

            try
            {
                string url = "http://192.168.1.136:80";
                var client = new RestClient(url);
                var request = new RestRequest();

                request.AddParameter("Tipo", "GetSeries");

                request.AddHeader("header", "application/json");
                request.AddHeader("Accept", "application/json");
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                var responseSeries = client.Post(request);
                var contentSeries = responseSeries.Content;



                if (contentSeries != "")
                {

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    JArray jArray = JsonConvert.DeserializeObject<JArray>(contentSeries);
                    listOfSeries.Clear();
                    for (int i = 0; i < jArray.Count; i++)
                    {
                        serieItem serieTemp = JsonConvert.DeserializeObject<serieItem>(jArray[i].ToString(), settings);
                        listOfSeries.Add(serieTemp);
                    }
                }
                else
                {
                    MessageBox.Show("Ninguna serie encontrada", "ERROR");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void button4_Click(object sender, EventArgs e)
        {
            Boolean flag = true;

            if (textBoxNombre.Text == "" || textBoxNombre.Text.Length < 3)
            {
                textBoxNombre.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (textBoxApellidos.Text == "" || textBoxApellidos.Text.Length < 3)
            {
                textBoxApellidos.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (textBoxEdad.Text != "")
            {
                bool isNumeric = int.TryParse(textBoxEdad.Text, out _);
                if (!isNumeric)
                {
                    textBoxEdad.BackColor = Color.FromArgb(255, 191, 191);
                    flag = false;
                }
                else
                {
                    if (Int32.Parse(textBoxEdad.Text) < 1 || Int32.Parse(textBoxEdad.Text) > 149)
                    {
                        textBoxEdad.BackColor = Color.FromArgb(255, 191, 191);
                        flag = false;
                    }
                }
            }
            else
            {
                textBoxEdad.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (listBox1.SelectedItems.Count < 1)
            {
                listBox1.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            


            if (flag)
            {
                try
                {
                    string url = "http://192.168.1.136:80";
                    var client = new RestClient(url);
                    var request = new RestRequest();
                    request.AddParameter("Tipo", "CreateCharacterFromAdmin");
                    request.AddParameter("nombre", textBoxNombre.Text);
                    request.AddParameter("apellidos", textBoxApellidos.Text);
                    request.AddParameter("edad", textBoxEdad.Text);
                    request.AddParameter("poder", textBoxEdad.Text);
                    request.AddParameter("actor", textBoxActor.Text);
                    request.AddParameter("personalidad", textBoxPersonalidad.Text);
                    request.AddParameter("origen", textBoxOrigen.Text);
                    request.AddParameter("descripcion", textBoxDescripcion.Text);
                    serieItem serieSelected = (serieItem)listBox1.SelectedItems[0];
                    request.AddParameter("id_serie", serieSelected.ID);

                    if (pictureBox1.Image != null)
                    {
                        Bitmap default_image = new Bitmap(pictureBox1.Image);
                        ImageConverter converter = new ImageConverter();
                        byte[] bytes = (byte[])converter.ConvertTo(default_image, typeof(byte[]));
                        string base64String = Convert.ToBase64String(bytes);
                        request.AddParameter("imagen", base64String);
                    }
                    else
                    {
                        request.AddParameter("imagen", "");
                    }

                    request.AddHeader("header", "application/json");
                    request.AddHeader("Accept", "application/json");
                    request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                    var response = client.Post(request);
                    string content = response.Content;

                    textBoxID.Text = content;

                    var stringToConvert = response.Content.Substring(1, response.Content.Length - 2);

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    Character characterTemp = JsonConvert.DeserializeObject<Character>(stringToConvert, settings);
                    characterIDToEdit = characterTemp.ID;
                    textBoxID.Text = characterTemp.ID.ToString();

                    MessageBox.Show("Personaje creado con exito", "CORRECTO");

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
