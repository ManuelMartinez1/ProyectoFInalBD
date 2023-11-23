using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace ProyectoFInalBD
{
    public partial class FormAlumno : Form
    {
        public FormAlumno()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBhandler handler = new DBhandler();
            string query = "SELECT * FROM Usuario WHERE id_tipo = 1 AND id_usuario = @id";
            Usuario alumno = handler.searchUsuariobyId(this.textBox1.Text, query);
            List<Cajon> cajones = new List<Cajon>();

            if ( alumno != null )
            {
                cajones = handler.searchCajonesById(1);
                FormPL formPL = new FormPL(cajones, alumno);
                formPL.ShowDialog();
            }
            else
            {
                MessageBox.Show("Alumno no encontrado");
            }
        }

    }
}
