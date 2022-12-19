using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDAMC
{
    internal class Character
    {
        public int ID { get; set; }
        public int ID_serie { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public string Poder { get; set; }
        public string Imagen { get; set; }
        public string Actor { get; set; }
        public string Personalidad { get; set; }
        public string Origen { get; set; }
        public string Descripcion { get; set; }

        public Character(int iD, int ID_Serie, string nombre, string apellidos, int edad, string poder, string imagen, string actor, string personalidad, string origen, string descripcion)
        {
            ID = iD;
            ID_serie = ID_Serie;
            Nombre = nombre;
            Apellidos = apellidos;
            Edad = edad;
            Poder = poder;
            Imagen = imagen;
            Actor = actor;
            Personalidad = personalidad;
            Origen = origen;
            Descripcion = descripcion;
        }
        public Character(int iD)
        {
            ID = iD;
        }
        public Character()
        {

        }
    }
}
