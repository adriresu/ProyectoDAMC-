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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProyectoDAMC
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void CustomListView_Load(object sender, EventArgs e)
        {
            try
            {
                String json;
                string url = "http://192.168.1.136:80";
                var client = new RestClient(url);
                var request = new RestRequest();
                request.AddParameter("Tipo", "Series");
                request.AddParameter("End", "End");
                request.AddHeader("header", "application/json");
                var response = client.Post(request);
                var content = response.Content; // Raw content as string
                dynamic jsonString = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                String casi;
                foreach (var item in jsonString)
                {
                     casi = "asdf";
                    //casi = (String)("{0} {1} {2} {3}\n", item.ID, item.Sinopsis, item.Genero, item.Titulo);
                }
                var d = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(jsonString);
                json = (string)jsonString.GetValue("response");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void populateItems()
        {
            ListViewItems[] listItems = new ListViewItems[20];

            for (int i = 0; i < listItems.Length; i++)
            {
                listItems[i] = new ListViewItems();
                listItems[i].Tittle = "Arcane";
                listItems[i].Nota = "10";
                listItems[i].Synopsis = "Jinx va fedeada, mi vi esta fedeando";
                listItems[i].State = "Algun dia finalizada";
                if (flowLayoutPanel1.Controls.Count < 0)
                {
                    flowLayoutPanel1.Controls.Clear();
                }
                else
                    flowLayoutPanel1.Controls.Add(listItems[i]);
            }
        }
    }
}
