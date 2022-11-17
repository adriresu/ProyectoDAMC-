using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ProyectoDAMC
{
    public partial class Registro : Form
    {
        public static String user;
        public static String password;
        public Registro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean flag = true;
            String json;
            MainMenu menu = new MainMenu();
            if (txtName.Text.Length < 3)
            {
                txtName.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (txtUsername.Text.Length < 6)
            {
                txtUsername.BackColor = Color.FromArgb(255, 191, 191); ;
                flag = false;
            }
            if (txtSurname.Text.Length < 3)
            {
                txtUsername.BackColor = Color.FromArgb(255, 191, 191); ;
                flag = false;
            }
            if (txtPassword.Text.Length < 8)
            {
                txtPassword.BackColor = Color.FromArgb(255, 191, 191); ;
                flag = false;
            }
            if (txtPassword.Text != txtPassword2.Text)
            {
                txtPassword.BackColor = Color.FromArgb(255, 191, 191);
                txtPassword2.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            if (txtEmail.Text.Length <= 0)
            {
                txtEmail.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }
            Regex emailPattern = new Regex(@"^[a-zA-Z0-9._-]+@[a-z]+\.+[a-z]+");
            if (!emailPattern.IsMatch(txtEmail.Text))
            {
                txtEmail.BackColor = Color.FromArgb(255, 191, 191);
                flag = false;
            }



            if (flag)
            {
                try
                {
                    string url = "http://192.168.1.136:80";
                    var client = new RestClient(url);
                    var request = new RestRequest();
                    request.AddParameter("Tipo", "Register");
                    request.AddParameter("name", txtName.Text);
                    request.AddParameter("surname", txtSurname.Text);
                    request.AddParameter("username", txtUsername.Text);
                    request.AddParameter("password", txtPassword.Text);
                    request.AddParameter("email", txtEmail.Text);
                    request.AddParameter("End", "End");
                    request.AddHeader("header", "application/json");
                    var response = client.Post(request);
                    var content = response.Content; // Raw content as string
                    JObject jsonString = JObject.Parse(content);
                    json = (string)jsonString.GetValue("response");
                }
                catch (Exception)
                {
                    throw;
                }
                if (json == "True")
                {
                    user = txtUsername.Text;
                    password = txtPassword.Text;
                    menu.Show();
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Login menu = new Login();
            this.Hide();
            menu.Show();
        }
    }
}
