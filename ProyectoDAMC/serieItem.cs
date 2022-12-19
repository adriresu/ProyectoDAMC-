using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDAMC
{
    public class serieItem
    {
        public int ID { get; set; }
        public string Sinopsis { get; set; }
        public string Titulo { get; set; }
        public string Anho_Estreno { get; set; }
        public string Estado { get; set; }
        public string Direccion { get; set; }
        public int Tipo { get; set; }
        public string Caratula { get; set; }
        public string Genero { get; set; }

        public serieItem(int iD, string sinopsis, string titulo, string anho_Estreno, string estado, string direccion, int tipo, string caratula, string genero)
        {
            ID = iD;
            Sinopsis = sinopsis;
            Titulo = titulo;
            Anho_Estreno = anho_Estreno;
            Estado = estado;
            Direccion = direccion;
            Tipo = tipo;
            Caratula = caratula;
            Genero = genero;
        }
        public serieItem(int iD)
        {
            ID = iD;
        }
        public serieItem()
        {

        }
    }
}
