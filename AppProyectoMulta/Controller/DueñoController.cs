using AppProyectoMulta.DB;
using AppProyectoMulta.Model;
using AppProyectoMulta.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace AppProyectoMulta.Controllers
{
    public class DueñoController
    {
        public List<Dueño> ObtenerDueños()
        {
            List<Dueño> lista = new List<Dueño>();
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_MostrarDueño", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Dueño
                        {
                            IdDueño = reader.GetInt32("idDueño"),
                            NombreDueño = reader.GetString("nombreDueño"),
                            Direccion = reader.GetString("direccion"),
                            Telefono = reader.GetString("telefono"),
                            Correo = reader.GetString("correo")
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener dueños: " + ex.Message);
                }
            }
            return lista;
        }

        public bool AgregarDueño(string nombre, string direccion, string telefono, string correo)
        {
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_InsertarDueño", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("nombreDueño", nombre);
                    cmd.Parameters.AddWithValue("direccion", direccion);
                    cmd.Parameters.AddWithValue("telefono", telefono);
                    cmd.Parameters.AddWithValue("correo", correo);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar dueño: " + ex.Message);
                    return false;
                }
            }
        }

        public bool ActualizarDueño(int id, string nombre, string direccion, string telefono, string correo)
        {
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_ModificarDueño", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idDueño", id);
                    cmd.Parameters.AddWithValue("nombreDueño", nombre);
                    cmd.Parameters.AddWithValue("direccion", direccion);
                    cmd.Parameters.AddWithValue("telefono", telefono);
                    cmd.Parameters.AddWithValue("correo", correo);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar dueño: " + ex.Message);
                    return false;
                }
            }
        }

        public bool EliminarDueño(int id)
        {
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_EliminarDueño", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idDueño", id);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar dueño: " + ex.Message);
                    return false;
                }
            }
        }

        public List<Dueño> BuscarDueño(int id)
        {
            List<Dueño> lista = new List<Dueño>();
            using (MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_BuscarDueño", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idDueño", id);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new Dueño
                        {
                            IdDueño = reader.GetInt32("idDueño"),
                            NombreDueño = reader.GetString("nombreDueño"),
                            Direccion = reader.GetString("direccion"),
                            Telefono = reader.GetString("telefono"),
                            Correo = reader.GetString("correo")
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar dueño: " + ex.Message);
                }
            }
            return lista;
        }
    }
}