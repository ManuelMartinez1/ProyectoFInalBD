using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        
        // Busca un usuario en la base de datos por id y regresa un objeto tipo Usuario.
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

        // Crea un nuevo registro para aquellos usuarios que son invitados.
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

        // Busca los cajones disponibles con base al numero de id del tipo que pertenecen y regresa la lista de cajones.
      public List<Cajon> searchCajonesById(int id)
        {
            List<Cajon> cajones = new List<Cajon>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT c.no_cajon, e.nombre FROM Cajon c INNER JOIN Edificio e ON c.id_edificio = e.id_edificio WHERE c.disponibilidad = 1 AND c.id_tipo IN (0, @idTipo);";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idTipo", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cajon cajon = new Cajon
                                {
                                    No_cajon = Convert.ToInt32(reader["no_cajon"]),
                                    Id_edificio = Convert.ToString(reader["nombre"])
                                };

                                cajones.Add(cajon);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar la base de datos: " + ex.Message);
                }
            connection.Close ();
            }
            return cajones;
        }

        // Actualiza la disponibilidad del cajon de disponible a ocupado.
        public bool updateDisponibilidad(int no_cajon)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Cajon SET disponibilidad = 0 WHERE No_cajon = @noCajon";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@noCajon", no_cajon);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar la base de datos" + ex.Message);
                    return false;
                }
            }
        }

        public bool updateDisponibilidadFalse(int no_cajon)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Cajon SET disponibilidad = 1 WHERE No_cajon = @noCajon";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@noCajon", no_cajon);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar la base de datos" + ex.Message);
                    return false;
                }
            }
        }

        // Busca todas los registros de asignacion y los regresa en una lista. 
        public List<Asignacion> getAsignacion()
        {
            List<Asignacion> asignaciones = new List<Asignacion>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT a.id_usuario, u.nombre AS nombre_usuario, e.nombre AS nombre_edificio, c.no_cajon " +
                                   "FROM Asignacion a " +
                                   "JOIN Usuario u ON a.id_usuario = u.id_usuario " +
                                   "JOIN Cajon c ON a.id_cajon = c.no_cajon " +
                                   "JOIN Edificio e ON c.id_edificio = e.id_edificio";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Asignacion asignacion = new Asignacion
                                {
                                    Id_Usuario= Convert.ToInt32(reader["id_usuario"]),
                                    NombreUsuario = Convert.ToString(reader["nombre_usuario"]),
                                    Edificio = Convert.ToString(reader["nombre_edificio"]),
                                    No_cajon = Convert.ToInt32(reader["no_cajon"])
                                };

                                asignaciones.Add(asignacion);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al conectar la base de datos " + e.Message);
                }
            }

            return asignaciones;
        }

        // Crea una nueva asignacion de cajon para los usuarios.
        public void createAsignacion(int no_cajon, Usuario user)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Insertar una nueva asignación en la tabla "Asignacion"
                    string query = "INSERT INTO Asignacion (id_usuario, id_cajon) VALUES (@idUsuario, @idCajon)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idUsuario", user.Id_usuario); // Supongo el nombre de la propiedad en la clase Usuario
                        command.Parameters.AddWithValue("@idCajon", no_cajon);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Visita registrada");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar la base de datos: " + ex.Message);
                }
            }
        }

        public void DeleteAsignacionById(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // First, retrieve the no_cajon value
                    string selectQuery = "SELECT id_cajon FROM Asignacion WHERE id_usuario = @IdUsuario";
                    int noCajon = 0; // Variable to store no_cajon

                    using (MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@IdUsuario", id);
                        using (MySqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                noCajon = reader.GetInt32(0); // Assuming no_cajon is an integer
                            }
                        }
                    }

                    // Now delete the record
                    string deleteQuery = "DELETE FROM Asignacion WHERE id_usuario = @IdUsuario";
                    using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@IdUsuario", id);
                        deleteCmd.ExecuteNonQuery();
                        MessageBox.Show("Asignación eliminada con éxito.");
                        updateDisponibilidadFalse(noCajon);
                    }

                    // Use the noCajon variable as needed
                    // For example: Console.WriteLine("The retrieved no_cajon value is: " + noCajon);

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error al eliminar la asignación: " + ex.Message);
                }
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

    public class Cajon
    {
        public int No_cajon { get; set; }
        public string Id_edificio { get; set; }
        

        public Cajon()
        {
            No_cajon = 0;
            Id_edificio = "";
        }

        public Cajon(int  no_cajon, string id_edificio)
        {
            No_cajon = no_cajon;
            Id_edificio = id_edificio;
        }
    }

    public class Asignacion
    {
        public int Id_Usuario { get; set; }
        public string NombreUsuario { get; set; }
        public int No_cajon { get; set; }
        public string Edificio { get; set; }

        public Asignacion()
        {
            Id_Usuario = 0;
            NombreUsuario = "";
            No_cajon = 0;
            Edificio = "";
        }
        public Asignacion(int id_usuario, string nombreUsuario, int noCajon, string edificio)
        {
            Id_Usuario =id_usuario;
            NombreUsuario= nombreUsuario;
            No_cajon= noCajon;
            Edificio= edificio;
        }
    }

}
