using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDAMC
{
    public class usuario
    {
        public int ID { get; set; }
        public int old_ID { get; set; }
        public int Rol { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public string Contrasenha { get; set; }
        public string Telefono { get; set; }
        public int Favorita { get; set; }
        public string Imagen { get; set; }

        public usuario(int iD, int old_iD, int rol, string nombre, string apellidos, string correo, string usuario, string contrasenha, string imagen, string telefono, int favorita)
        {
            ID = iD;
            old_ID = old_iD;
            Rol = rol;
            Nombre = nombre;
            Apellidos = apellidos;
            Correo = correo;
            Usuario = usuario;
            Contrasenha = contrasenha;
            Telefono = telefono;
            Favorita = favorita;
            Imagen = imagen;            
        }
        public usuario()
        {

        }
    }
}
