using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;


namespace ProyectoDAMC
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean flag = true;
            String json;
            usersEdit menu = new usersEdit();
            if (textBox1.Text.Length <= 0)
            {
                textBox1.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (textBox2.Text.Length <= 0)
            {
                textBox2.BackColor = Color.FromArgb(255, 191, 191); ;
                flag = false;
            }

            if (flag){            
                try
                {
                    string url = "http://192.168.1.136:80";
                    var client = new RestClient(url);
                    var request = new RestRequest();
                    request.AddParameter("Tipo", "Login");
                    request.AddParameter("username", textBox1.Text);
                    request.AddParameter("password", textBox2.Text);
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
                            menu.Show();
                        }
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Registro menu = new Registro();
            this.Hide();
            menu.Show();
        }
    }
}
