using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProyectoFInalBD
{
    public partial class FormAdministrativo : Form
    {
        public FormAdministrativo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBhandler handler = new DBhandler();
            string query = "SELECT * FROM Usuario WHERE id_tipo = 2 AND id_usuario = @id";
            Usuario administrativo = handler.searchUsuariobyId(this.textBox1.Text, query);
            List<Cajon> cajones;

            if (administrativo != null)
            {
                cajones = handler.searchCajonesById(2);
                FormPL formPL = new FormPL(cajones, administrativo);
                formPL.ShowDialog();
            }
            else
            {
                MessageBox.Show("Alumno no encontrado");
            }
        }
    }
}
