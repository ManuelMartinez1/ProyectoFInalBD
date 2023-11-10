using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFInalBD
{
    internal class Administrativo : Usuario
    {
        public string apellidoMaterno { get; set; }
        public int id_empleado { get; set; }

        public Administrativo() { 
            id_empleado = 0;
            nombre = string.Empty;
            apellidoPaterno = string.Empty;
            apellidoMaterno = string.Empty;
        }
    }

    internal class Alumno : Usuario
    {
        public int matricula { get; set; }
        public string nivelEducacion { get; set; }
        public string apellidoMaterno { get; set; }


        public Alumno() { 
            matricula = 0; 
            nombre= string.Empty;
            apellidoPaterno= string.Empty;
            apellidoMaterno = string.Empty;
            
        }
    }

    internal class Docente : Usuario
    {
        public int matricula { get; set; }

    }
}
