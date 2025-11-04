using AppProyectoMulta.DB;
using AppProyectoMulta.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace AppProyectoMulta.Controllers
{
    public class PagoMultaController
    {
        public List<PagoMulta> ObtenerPagos()
        {
            List<PagoMulta> lista = new List<PagoMulta>();
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_MostrarPagosMulta", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new PagoMulta
                        {
                            IdPago = reader.GetInt32("idPago"),
                            IdMulta = reader.GetInt32("idMulta"),
                            FechaPago = reader.GetDateTime("fechaPago"),
                            MetodoPago = reader.GetString("metodoPago"),
                            MontoPagado = reader.GetDecimal("montoPagado")
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener pagos: " + ex.Message);
                }
            }
            return lista;
        }

        public bool AgregarPago(int idMulta, DateTime fechaPago, string metodoPago, decimal montoPagado)
        {
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertarPagoMulta", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idMulta", idMulta);
                    cmd.Parameters.AddWithValue("fechaPago", fechaPago);
                    cmd.Parameters.AddWithValue("metodoPago", metodoPago);
                    cmd.Parameters.AddWithValue("montoPagado", montoPagado);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar pago: " + ex.Message);
                    return false;
                }
            }
        }

        public bool ActualizarPago(int idPago, int idMulta, DateTime fechaPago, string metodoPago, decimal montoPagado)
        {
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_ModificarPagoMulta", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idPago", idPago);
                    cmd.Parameters.AddWithValue("idMulta", idMulta);
                    cmd.Parameters.AddWithValue("fechaPago", fechaPago);
                    cmd.Parameters.AddWithValue("metodoPago", metodoPago);
                    cmd.Parameters.AddWithValue("montoPagado", montoPagado);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar pago: " + ex.Message);
                    return false;
                }
            }
        }

        public bool EliminarPago(int idPago)
        {
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_EliminarPagoMulta", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idPago", idPago);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar pago: " + ex.Message);
                    return false;
                }
            }
        }

        public List<PagoMulta> BuscarPago(int id)
        {
            List<PagoMulta> lista = new List<PagoMulta>();
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_BuscarPagoMulta", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idPago", id);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new PagoMulta
                        {
                            IdPago = reader.GetInt32("idPago"),
                            IdMulta = reader.GetInt32("idMulta"),
                            FechaPago = reader.GetDateTime("fechaPago"),
                            MetodoPago = reader.GetString("metodoPago"),
                            MontoPagado = reader.GetDecimal("montoPagado")
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar pago: " + ex.Message);
                }
            }
            return lista;
        }
    }
}
