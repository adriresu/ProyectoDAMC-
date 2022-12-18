using Dapper;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Image = System.Drawing.Image;

namespace ProyectoDAMC
{
    public partial class usersEdit : Form
    {
        public int userToEdit;
        public string usernameToEdit;
        public usersEdit()
        {
            InitializeComponent();
        }

        const string ConnectionString = "Initial Catalog=DeletedUsers;Data Source=DESKTOP-I18KUTN;Integrated Security=SSPI;";
        public static BindingList<usuario> listOfUsers;
        private void usersEdit_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button2.Enabled = false;
            listOfUsers = new BindingList<usuario>();
            listBox1.DataSource = listOfUsers;
            listBox1.DisplayMember = "Usuario";

            try
            {
                using (IDbConnection Conn = new SqlConnection(ConnectionString))
                {
                    var Query1 = "SELECT * FROM Usuarios";
                    var Select1 = Conn.Query<usuario>(Query1).ToList();
                    for (int i = 0; i < Select1.Count; i++)
                    {
                        listOfUsers.Add(Select1[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                MessageBox.Show("Porfavor, complete el campo del usuario", "ERROR");
                return;
            }
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

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    usuario usuarioTemp = JsonConvert.DeserializeObject<usuario>(stringToConvert, settings);
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

                    if (usuarioTemp.Imagen != null && usuarioTemp.Imagen != "")
                    {
                        byte[] byteBuffer = Convert.FromBase64String(usuarioTemp.Imagen);
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

                    userToEdit = usuarioTemp.ID;
                    button3.Enabled = true;
                    button2.Enabled = true;

                    usernameToEdit = textBox6.Text;
    }
                else
                {
                    MessageBox.Show("Usuario no encontrado", "ERROR");
                    userToEdit = -1;
                }
            }
            catch (Exception)
            {
                throw;
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
            if (!int.TryParse(textBox4.Text, out _))
            {
                textBox4.BackColor = Color.FromArgb(255, 191, 191);
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
                    var Prerequest = new RestRequest();

                    Prerequest.AddParameter("Tipo", "CheckIfUserFromAdmin");
                    Prerequest.AddParameter("username", textBox6.Text);
                    Prerequest.AddParameter("id", label7.Text);

                    Prerequest.AddHeader("header", "application/json");
                    Prerequest.AddHeader("Accept", "application/json");
                    Prerequest.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                    var responseUserExists = client.Post(Prerequest);
                    var contentUserExists = responseUserExists.Content;

                    if (contentUserExists != null && contentUserExists != "")
                    {
                        MessageBox.Show("Nombre de usuario ya existente", "ERROR");
                        return;
                    }

                    var request = new RestRequest();
                    request.AddParameter("Tipo", "UpdateUserFromAdmin");
                    request.AddParameter("usuario", textBox6.Text);
                    request.AddParameter("id", userToEdit);
                    request.AddParameter("nombre", textBox1.Text);
                    request.AddParameter("apellidos", textBox2.Text);
                    request.AddParameter("email", textBox3.Text);
                    request.AddParameter("phone", textBox4.Text);
                    request.AddParameter("contrasenha", textBox7.Text);
                    if (checkBox1.Checked == true)
                    {
                        request.AddParameter("rol", "1");
                    }
                    else
                    {
                        request.AddParameter("rol", "0");
                    }

                    if (pictureBox1.Image != null)
                    {
                        Bitmap default_image = new Bitmap(pictureBox1.Image);
                        ImageConverter converter = new ImageConverter();
                        byte[] bytes = (byte[])converter.ConvertTo(default_image, typeof(byte[]));
                        string base64String = Convert.ToBase64String(bytes);
                        request.AddParameter("bitmap", base64String);
                    }
                    else
                    {
                        request.AddParameter("bitmap", "");
                    }
                    
                    request.AddHeader("header", "application/json");
                    request.AddHeader("Accept", "application/json");
                    request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                    var response = client.Post(request);
                    var content = response.Content;

                    MessageBox.Show("Usuario modificado con exito", "CORRECTO");

                    //// Write the bytes (as a Base64 string) to the textbox
                    //string comprimed = base64String;
                    //if (content != "")
                    //{
                    //    var stringToConvert = response.Content.Substring(1, response.Content.Length - 2);
                    //    usuario usuarioTemp = JsonConvert.DeserializeObject<usuario>(stringToConvert);

                    //    if (usuarioTemp.Rol == 1)
                    //    {
                    //        Registro.user = textBox1.Text;
                    //        Registro.password = textBox2.Text;
                    //    }
                    //}


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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "http://192.168.1.136:80";
                var client = new RestClient(url);
                var request = new RestRequest();
                request.AddParameter("Tipo", "RemoveUser");
                if (userToEdit >= 0)
                {
                    request.AddParameter("id", userToEdit);
                }
                else
                {
                    MessageBox.Show("ID de usuario corrupto", "ERROR");
                    return;
                }
                request.AddHeader("header", "application/json");
                request.AddHeader("Accept", "application/json");
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var response = client.Post(request);
                try
                {
                    using (IDbConnection Conn = new SqlConnection(ConnectionString))
                    {
                        var Query = $@"INSERT INTO Usuarios (old_ID, Rol, Nombre, Apellidos, Correo, Usuario, Contrasenha, Telefono) VALUES (@old_ID, @Rol, @Nombre, @Apellidos, @Correo, @Usuario, @Contrasenha, @Telefono)";

                        var Insert = Conn.Execute(Query, new { old_ID = label7.Text, Rol = 0, Nombre = textBox1.Text , Apellidos = textBox2.Text, Correo = textBox3.Text, Usuario = usernameToEdit, Telefono = textBox4.Text, Contrasenha = textBox7.Text });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox6.Clear();
                textBox7.Clear();
                label7.Text = "";
                pictureBox1.Image = null;

                listOfUsers.Clear();

                try
                {
                    using (IDbConnection Conn = new SqlConnection(ConnectionString))
                    {
                        var Query1 = "SELECT * FROM Usuarios";
                        var Select1 = Conn.Query<usuario>(Query1).ToList();
                        for (int i = 0; i < Select1.Count; i++)
                        {
                            listOfUsers.Add(Select1[i]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                userToEdit = -1;
                MessageBox.Show("Usuario eliminado con exito", "CORECTO");
            }
            catch (Exception)
            {
                MessageBox.Show("Error al eliminar usuario", "ERROR");
                throw;
            }
        }

        //Recover User
        private void button4_Click(object sender, EventArgs e)
        {
            usuario userToRecover = listBox1.SelectedItem as usuario;
            String nombre = userToRecover.Nombre;

            try
            {
                string url = "http://192.168.1.136:80";
                var client = new RestClient(url);
                var request = new RestRequest();
                request.AddParameter("Tipo", "RestoreUserFromAdmin");
                request.AddParameter("rol", userToRecover.Rol);
                request.AddParameter("email", userToRecover.Correo);
                request.AddParameter("nombre", userToRecover.Nombre);
                request.AddParameter("apellidos", userToRecover.Apellidos);
                request.AddParameter("phone", userToRecover.Telefono);
                request.AddParameter("usuario", userToRecover.Usuario);
                request.AddParameter("contrasenha", userToRecover.Contrasenha);
                request.AddHeader("header", "application/json");
                request.AddHeader("Accept", "application/json");
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var response = client.Post(request);
                var content = response.Content;

                if (content == "")
                {
                    listOfUsers.Remove(userToRecover);

                    using (IDbConnection Conn = new SqlConnection(ConnectionString))
                    {
                        var Query = $@"DELETE FROM Usuarios WHERE old_ID = @id";

                        var Delete = Conn.Execute(Query, new { id = userToRecover.old_ID });

                    }

                    listOfUsers.Remove(userToRecover);
                    MessageBox.Show("Usuario recuperado con exito", "CORRECTO");
                }


            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
