using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFInalBD
{
    public class DBhandler
    {
        private string connectionString;

        public DBhandler() {
           connectionString = "server=bsfysc9kuzrrz3wndpmp-mysql.services.clever-cloud.com;database=bsfysc9kuzrrz3wndpmp;user=ujfuoquld10oprhc;password=IteJazh6R6wpiYUja8mL;";
        }
    
        public Usuario searchUsuariobyId(string source, string query)
        {

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string id = source;
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("id", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Usuario user = new Usuario(
                                reader["id_usuario"].ToString(),
                                reader["nombre"].ToString(),
                                reader["apellido_pat"].ToString(),
                                reader["apellido_mat"].ToString(),
                                reader["nivel_educacion"].ToString(),
                                reader["id_tipo"].ToString()
                                );
                                return user;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar la base de datos: " + ex.Message);
                }
                connection.Close();
              
            }
            return null;
        }


        public void CreateUsuarioInvitado(Usuario usuario)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Usuario (id_usuario, nombre, apellido_pat, apellido_mat, nivel_educacion, id_tipo) VALUES (@idUsuario, @nombre, @apellidoPat, @apellidoMat, @nivelEducacion, @idTipo)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idUsuario", usuario.Id_usuario);
                        command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                        command.Parameters.AddWithValue("@apellidoPat", usuario.Apellido_pat);
                        command.Parameters.AddWithValue("@apellidoMat", usuario.Apellido_mat);
                        command.Parameters.AddWithValue("@nivelEducacion", usuario.Nivel_educacion);
                        command.Parameters.AddWithValue("@idTipo", usuario.Tipo);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Usuario creado");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar la base de datos: " + ex.Message);
                }
                connection.Close();
            }
        }

    }

    public class Usuario { 
    
        public string Id_usuario {  get; set; }
        public string Nombre { get; set; }
        public string Apellido_pat { get; set; }
        public string Apellido_mat { get; set; }
        public string Nivel_educacion { get; set; }
        public string Tipo { get; set; }

        public Usuario (string id_usuario, string nombre, string apellido_pat, string apellido_mat, string nivel_educacion, string tipo)
        {
            Id_usuario = id_usuario;
            Nombre = nombre;
            Apellido_pat = apellido_pat;
            Apellido_mat = apellido_mat;
            Nivel_educacion = nivel_educacion;
            Tipo = tipo;
        }
    }

}
