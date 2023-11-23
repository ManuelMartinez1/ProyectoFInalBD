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
        List<Asignacion> lista;

        public Asignaciones(List<Asignacion> lista)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = lista;
            dataGridView1.Refresh();
        }
    }
}
