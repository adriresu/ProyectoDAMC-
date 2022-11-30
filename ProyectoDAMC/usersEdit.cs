using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Image = System.Drawing.Image;

namespace ProyectoDAMC
{
    public partial class usersEdit : Form
    {
        public int userToEdit;
        public usersEdit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "http://192.168.1.136:80";
                var client = new RestClient(url);
                var request = new RestRequest();
                request.AddParameter("Tipo", "GetUserByUsername");
                request.AddParameter("username", textBox6.Text);
                request.AddHeader("header", "application/json");
                request.AddHeader("Accept", "application/json");
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                var response = client.Post(request);

                if (response.Content != "")
                {
                    var stringToConvert = response.Content.Substring(1, response.Content.Length - 2);
                    usuario usuarioTemp = JsonConvert.DeserializeObject<usuario>(stringToConvert);
                    textBox1.Text = usuarioTemp.Nombre;
                    textBox2.Text = usuarioTemp.Apellidos;
                    textBox3.Text = usuarioTemp.Correo;
                    textBox4.Text = usuarioTemp.Telefono;
                    textBox7.Text = usuarioTemp.Contrasenha;
                    label7.Text = usuarioTemp.ID.ToString();
                    if (usuarioTemp.Rol == 1)
                    {
                        checkBox1.Checked = true;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                    }

                    Bitmap bmpReturn = null;


                    byte[] byteBuffer = Convert.FromBase64String(usuarioTemp.Imagen);
                    MemoryStream memoryStream = new MemoryStream(byteBuffer);
                    memoryStream.Position = 0;
                    bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);
                    memoryStream.Close();
                    memoryStream = null;
                    byteBuffer = null;

                    pictureBox1.Image = bmpReturn;
                    

                    textBox5.Text = "Encontrado";
                    userToEdit = usuarioTemp.ID;
                }
                else
                {
                    textBox5.Text = "No encontrado";
                    userToEdit = -1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ImageToBase64(Image image,System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public static string conversion(System.Drawing.Image image)
        {
            using (MemoryStream ms = new MemoryStream())

            {

                // Convert Image to byte[]

                image.Save(ms, ImageFormat.Png);

                byte[] imageBytes = ms.ToArray();


                // Convert byte[] to Base64 String

                string base64String = Convert.ToBase64String(imageBytes);

                return base64String;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Boolean flag = true;

            if (textBox1.Text == "" || textBox1.Text.Length < 3)
            {
                textBox1.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (textBox6.Text == "" || textBox6.Text.Length < 3)
            {
                textBox6.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (textBox2.Text == "" || textBox2.Text.Length < 3)
            {
                textBox2.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (textBox7.Text == "" || textBox7.Text.Length < 8)
            {
                textBox7.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (textBox3.Text == "")
            {
                textBox3.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }

            //Regex match
            //MatchCollection matches;
            //try
            //{
            //    String emailPattern = "@[a-zA-Z0-9._-]+@[a-z]+\\.+[a-z]+";
            //    matches = Regex.Matches(textBox3.Text, emailPattern);
            //}
            //catch (Exception){throw;}            
            //if (matches.Count <= 0)
            //{
            //    textBox3.BackColor = Color.FromArgb(255, 191, 191);
            //    flag = false;
            //}

            if (flag)
            {
                try
                {
                    string url = "http://192.168.1.136:80";
                    var client = new RestClient(url);
                    var request = new RestRequest();
                    request.AddParameter("Tipo", "UpdateUserFromAdmin");
                    request.AddParameter("Username", textBox6.Text);
                    request.AddParameter("id", userToEdit);
                    request.AddParameter("Nombre", textBox1.Text);
                    request.AddParameter("Apellidos", textBox2.Text);
                    request.AddParameter("Correo", textBox3.Text);
                    request.AddParameter("Telefono", textBox4.Text);
                    request.AddParameter("Contrasenha", textBox7.Text);
                    if (checkBox1.Checked == true)
                    {
                        request.AddParameter("Rol", "1");
                    }
                    else
                    {
                        request.AddParameter("Rol", "0");
                    }

                    //using (var ms = new MemoryStream())
                    //{
                    //    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    //    byte[] imageBytes = ms.ToArray();
                    //    string base64String = Convert.ToBase64String(imageBytes);
                    //    request.AddParameter("Imagen", base64String);
                    //}

                    string convertedImage = conversion(pictureBox1.Image);
                    request.AddParameter("Imagen", convertedImage);

                    request.AddHeader("header", "application/json");
                    request.AddHeader("Accept", "application/json");
                    request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                    var response = client.Post(request);
                    var content = response.Content;

                    if (content != "")
                    {
                        var stringToConvert = response.Content.Substring(1, response.Content.Length - 2);
                        usuario usuarioTemp = JsonConvert.DeserializeObject<usuario>(stringToConvert);

                        if (usuarioTemp.Rol == 1)
                        {
                            Registro.user = textBox1.Text;
                            Registro.password = textBox2.Text;
                        }
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG)" + "|All files (*.*)|*.*";
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;
            if (dialog.ShowDialog()==DialogResult.OK)
            {
                Bitmap image = new Bitmap(dialog.FileName);
                pictureBox1.Image = (Image)image;
            }
        }
    }
}
