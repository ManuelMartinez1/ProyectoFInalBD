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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                FormAlumno formAlumno = new FormAlumno();
                formAlumno.ShowDialog();
            }
            if (radioButton2.Checked)
            {
                FormAdministrativo formAdmi = new FormAdministrativo();  
                formAdmi.ShowDialog();
            }
            if (radioButton3.Checked)
            {
                FormDocente formDocente = new FormDocente();    
                formDocente.ShowDialog();
            }
            if (radioButton4.Checked)
            {
                FormInvitado formInvitado = new FormInvitado();  
                formInvitado.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Asignacion> asignaciones;
          DBhandler dBhandler = new DBhandler();
           asignaciones = dBhandler.setAsignacion();
           Asignaciones formAsign = new Asignaciones(asignaciones);
            formAsign.ShowDialog();
        }
    }
}
