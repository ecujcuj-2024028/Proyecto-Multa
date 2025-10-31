using AppProyectoMulta.DB;
using AppProyectoMulta.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace AppProyectoMulta.Controller
{
    public class AgenteController
    {
        public List<Agente> ObtenerAgentes()
        {
            List<Agente> lista = new List<Agente>();
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_MostrarAgente", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Agente
                        {
                            IdAgente = reader.GetInt32("idAgente"),
                            NombreAgente = reader.GetString("nombreAgente"),
                            Rango = reader.GetString("rango"),
                            Salario = reader.GetDouble("salario"),
                            IdMunicipio = reader.GetInt32("idMunicipio")
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener agentes: " + ex.Message);
                }
            }
            return lista;
        }

        public bool AgregarAgente(string nombreAgente, string rango, double salario, int idMunicipio)
        {
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_InsertarAgente", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("nombreAgente", nombreAgente);
                    cmd.Parameters.AddWithValue("rango", rango);
                    cmd.Parameters.AddWithValue("salario", salario);
                    cmd.Parameters.AddWithValue("idMunicipio", idMunicipio);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar agente: " + ex.Message);
                    return false;
                }
            }
        }

        public bool ActualizarAgente(int idAgente, string nombreAgente, string rango, double salario, int idMunicipio)
        {
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_ModificarAgente", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idAgente", idAgente);
                    cmd.Parameters.AddWithValue("nombreAgente", nombreAgente);
                    cmd.Parameters.AddWithValue("rango", rango);
                    cmd.Parameters.AddWithValue("salario", salario);
                    cmd.Parameters.AddWithValue("idMunicipio", idMunicipio);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar agente: " + ex.Message);
                    return false;
                }
            }
        }

        public bool EliminarAgente(int idAgente)
        {
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_EliminarAgente", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idAgente", idAgente);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar agente: " + ex.Message);
                    return false;
                }
            }
        }

        public List<Agente> BuscarAgente(int idAgente)
        {
            List<Agente> lista = new List<Agente>();
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_BuscarAgente", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idAgente", idAgente);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new Agente
                        {
                            IdAgente = reader.GetInt32("idAgente"),
                            NombreAgente = reader.GetString("nombreAgente"),
                            Rango = reader.GetString("rango"),
                            Salario = reader.GetDouble("salario"),
                            IdMunicipio = reader.GetInt32("idMunicipio")
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar agente: " + ex.Message);
                }
            }
            return lista;
        }
    }
}
