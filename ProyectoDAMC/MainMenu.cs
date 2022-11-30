using Newtonsoft.Json;
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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProyectoDAMC
{
    public partial class MainMenu : Form
    {
        public List<usuario> listUsers;
        public MainMenu()
        {
            InitializeComponent();
        }

        private void CustomListView_Load(object sender, EventArgs e)
        {
            string asdf = "asdf";
            asdf = "asdfafff";
            //try
            //{

            //    string url = "http://192.168.1.136:80";
            //    var client = new RestClient(url);
            //    var request = new RestRequest();
            //    request.AddParameter("Tipo", "GetUsers");
            //    request.AddHeader("header", "application/json");
            //    request.AddHeader("Accept", "application/json");
            //    request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            //    var response = client.Post(request);
            //    var content = response.Content;

            //    if (content != "")
            //    {

            //        var stringToConvert = response.Content.Substring(1, response.Content.Length - 2);
            //        String regex = @"{.*?}";
    

            //        MatchCollection matches = Regex.Matches(stringToConvert, regex);


            //        foreach (Match match in Regex.Matches(stringToConvert, regex, RegexOptions.IgnoreCase)){
            //            usuario usuarioTemp = JsonConvert.DeserializeObject<usuario>(match.Value);
            //        };


            //        listaUsuarios listUsers = JsonConvert.DeserializeObject<listaUsuarios>(stringToConvert);


            //    }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        private void populateItems()
        {
            try
            {
                string url = "http://192.168.1.136:80";
                var client = new RestClient(url);
                var request = new RestRequest();
                request.AddParameter("Tipo", "GetUsers");
                request.AddHeader("header", "application/json");
                request.AddHeader("Accept", "application/json");
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                var response = client.Post(request);
                var content = response.Content;

                if (content != "")
                {

                    var stringToConvert = response.Content.Substring(1, response.Content.Length - 2);
                    String regex = @"{.*?}";


                    MatchCollection matches = Regex.Matches(stringToConvert, regex);

                    listUsers = new List<usuario>();

                    foreach (Match match in Regex.Matches(stringToConvert, regex, RegexOptions.IgnoreCase))
                    {
                        usuario usuarioTemp = JsonConvert.DeserializeObject<usuario>(match.Value);
                        //if (usuarioTemp.Rol == 0)
                        //{
                        listUsers.Add(usuarioTemp);
                        //}
                    };

                    ListViewItems[] listItems = new ListViewItems[20];

                    for (int i = 0; i < listItems.Length; i++)
                    {
                        listItems[i] = new ListViewItems();
                        listItems[i].Tittle = "Arcane";
                        listItems[i].State = "Algun dia finalizada";
                        listItems[i].Nota = "10";
                        listItems[i].Synopsis = "Jinx va fedeada, mi vi esta fedeando";
                        if (flowLayoutPanel1.Controls.Count < 0)
                        {
                            flowLayoutPanel1.Controls.Clear();
                        }
                        else
                            flowLayoutPanel1.Controls.Add(listItems[i]);
                    }

                }
            }
            catch (Exception)
            {
                throw; 
            }



            //List<usuario> listUsers = new List<usuario>();

            //for (int i = 0; i < listItems.Length; i++)
            //{
            //    listItems[i] = new ListViewItems();
            //    listItems[i].Tittle = "Arcane";
            //    listItems[i].Nota = "10";
            //    listItems[i].Synopsis = "Jinx va fedeada, mi vi esta fedeando";
            //    listItems[i].State = "Algun dia finalizada";

            //}
        }
    }
}
