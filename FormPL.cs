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
    public partial class FormPL : Form
    {
        public Usuario user;

        public FormPL(List<Cajon> cajones, Usuario usuario)
        {
            user = usuario;
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = cajones;
            dataGridView1.Refresh();
            setComboBoxItems(cajones);
            label1.Text = "Bienvenido " + user.Nombre.ToString() +" "+ user.Apellido_pat.ToString(); 

        }
        public void setComboBoxItems(List<Cajon> cajones)
        {
            comboBox1.Items.Clear();
            foreach(Cajon cajon in cajones)
            {
                comboBox1.Items.Add(cajon.No_cajon);
            }
        }

        private void Seleccionar_Click(object sender, EventArgs e)
        {
            DBhandler handler = new DBhandler();
            int selectedNoCajon = Convert.ToInt32(comboBox1.SelectedItem);
            bool success = handler.updateDisponibilidad(selectedNoCajon);
            handler.createAsignacion(selectedNoCajon, user);
        }
    }
}
