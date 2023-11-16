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
    public partial class FormInvitado : Form
    {
        public FormInvitado()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                Random random = new Random();
                string id = random.Next(8000, 10000).ToString();
                DBhandler handler = new DBhandler();
                Usuario invitado = new Usuario(
                   id,
                   textBox1.Text,
                   textBox2.Text,
                   null,
                   null,
                   "4"
                   );
                handler.CreateUsuarioInvitado(invitado);
            }
            else
            {
                MessageBox.Show("Porfavor proporcionanos tus datos completos");
            }
        }
    }
}
