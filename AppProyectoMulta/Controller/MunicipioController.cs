using AppProyectoMulta.DB;
using AppProyectoMulta.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace AppProyectoMulta.Controllers
{
    public class MunicipioController
    {
        public List<Municipio> ObtenerMunicipios()
        {
            List<Municipio> lista = new List<Municipio>();

            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_MostrarMunicipios", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Municipio
                        {
                            IdMunicipio = reader.GetInt32("idMunicipio"),
                            NombreMunicipio = reader.GetString("nombreMunicipio")
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener municipios: " + ex.Message);
                }
            }
            return lista;
        }
        public bool AgregarMunicipio(string nombre)
        {
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_InsertarMunicipio", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("nombreMunicipio", nombre);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar municipio: " + ex.Message);
                    return false;
                }
            }
        }

        public bool ActualizarMunicipio(int id, string nombre)
        {
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_ModificarMunicipio", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idMunicipio", id);
                    cmd.Parameters.AddWithValue("nombreMunicipio", nombre);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar municipio: " + ex.Message);
                    return false;
                }
            }
        }

        public bool EliminarMunicipio(int id)
        {
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_EliminarMunicipio", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idMunicipio", id);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar municipio: " + ex.Message);
                    return false;
                }
            }
        }

        public List<Municipio> BuscarMunicipio(int id)
        {
            List<Municipio> lista = new List<Municipio>();

            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_BuscarMunicipio", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("idMunicipio", id);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Municipio
                        {
                            IdMunicipio = reader.GetInt32("idMunicipio"),
                            NombreMunicipio = reader.GetString("nombreMunicipio")
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar municipio: " + ex.Message);
                }
            }
            return lista;
        }
    }
}