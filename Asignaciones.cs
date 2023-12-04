using Org.BouncyCastle.Bcpg;
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
    public partial class Asignaciones : Form
    {
        public Usuario user; 
        public Asignaciones(List<Asignacion> lista, Usuario user)
        {
            this.user = user;
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = lista;
            dataGridView1.Refresh();
        }

        public Asignaciones(List<Asignacion> lista)
        {
            user = null;
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = lista;
            dataGridView1.Refresh();
            button1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBhandler handler = new DBhandler();
            handler.DeleteAsignacionById(Convert.ToInt32(user.Id_usuario));
        }
    }
}
