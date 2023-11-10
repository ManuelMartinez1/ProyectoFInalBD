using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFInalBD
{
    internal class Usuario 
    {
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public Usuario()
        {
            nombre = string.Empty;
            apellidoPaterno = string.Empty;
        }
    }
}
