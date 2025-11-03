using AppProyectoMulta.DB;
using AppProyectoMulta.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace AppProyectoMulta.Controllers
{
    public class MultaController
    {
        public List<Multa> ObtenerMultas()
        {
            List<Multa> lista = new List<Multa>();
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_MostrarMulta", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Multa
                        {
                            IdMulta = reader.GetInt32("idMulta"),
                            FechaMulta = reader.GetDateTime("fechaMulta"),
                            FechaLimite = reader.GetDateTime("fechaLimite"),
                            Importe = reader.GetDecimal("importe"),
                            Lugar = reader.GetString("lugar"),
                            Estado = reader.GetString("estado"),
                            IdVehiculo = reader.GetInt32("idVehiculo"),
                            IdAgente = reader.GetInt32("idAgente"),
                            IdTipoMulta = reader.GetInt32("idTipoMulta")
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener multas: " + ex.Message);
                }
            }
            return lista;
        }

        public bool AgregarMulta(DateTime fechaMulta, DateTime fechaLimite, decimal importe, string lugar, string estado, int idVehiculo, int idAgente, int idTipoMulta)
        {
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_InsertarMulta", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("fechaMulta", fechaMulta);
                    cmd.Parameters.AddWithValue("fechaLimite", fechaLimite);
                    cmd.Parameters.AddWithValue("importe", importe);
                    cmd.Parameters.AddWithValue("lugar", lugar);
                    cmd.Parameters.AddWithValue("estado", estado);
                    cmd.Parameters.AddWithValue("idVehiculo", idVehiculo);
                    cmd.Parameters.AddWithValue("idAgente", idAgente);
                    cmd.Parameters.AddWithValue("idTipoMulta", idTipoMulta);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar multa: " + ex.Message);
                    return false;
                }
            }
        }

        public bool ActualizarMulta(int idMulta, DateTime fechaMulta, DateTime fechaLimite, decimal importe, string lugar, string estado, int idVehiculo, int idAgente, int idTipoMulta)
        {
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_ModificarMulta", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idMulta", idMulta);
                    cmd.Parameters.AddWithValue("fechaMulta", fechaMulta);
                    cmd.Parameters.AddWithValue("fechaLimite", fechaLimite);
                    cmd.Parameters.AddWithValue("importe", importe);
                    cmd.Parameters.AddWithValue("lugar", lugar);
                    cmd.Parameters.AddWithValue("estado", estado);
                    cmd.Parameters.AddWithValue("idVehiculo", idVehiculo);
                    cmd.Parameters.AddWithValue("idAgente", idAgente);
                    cmd.Parameters.AddWithValue("idTipoMulta", idTipoMulta);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar multa: " + ex.Message);
                    return false;
                }
            }
        }

        public bool EliminarMulta(int idMulta)
        {
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_EliminarMulta", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idMulta", idMulta);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar multa: " + ex.Message);
                    return false;
                }
            }
        }

        public List<Multa> BuscarMulta(int idMulta)
        {
            List<Multa> lista = new List<Multa>();
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_BuscarMulta", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idMulta", idMulta);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Multa
                        {
                            IdMulta = reader.GetInt32("idMulta"),
                            FechaMulta = reader.GetDateTime("fechaMulta"),
                            FechaLimite = reader.GetDateTime("fechaLimite"),
                            Importe = reader.GetDecimal("importe"),
                            Lugar = reader.GetString("lugar"),
                            Estado = reader.GetString("estado"),
                            IdVehiculo = reader.GetInt32("idVehiculo"),
                            IdAgente = reader.GetInt32("idAgente"),
                            IdTipoMulta = reader.GetInt32("idTipoMulta")
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar multa: " + ex.Message);
                }
            }
            return lista;
        }
    }
}
