using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFInalBD
{
    public partial class FormDocente : Form
    {
        public FormDocente()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBhandler handler = new DBhandler();
            string query = "SELECT * FROM Usuario Where id_tipo = 3 AND id_usuario = @id";
            Usuario docente = handler.searchUsuariobyId(this.textBox1.Text, query);
            if (docente != null)
            {
                MessageBox.Show(docente.Nombre + docente.Apellido_pat);
            }
            else
            {
                MessageBox.Show("Docente no encontrado");
            }
        }
    }
}
