using AppProyectoMulta.DB;
using AppProyectoMulta.Model;
using AppProyectoMulta.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace AppProyectoMulta.Controller
{
    public class VehiculoController
    {
        public List<Vehiculo> ObtenerVehiculo()
        {
            List<Vehiculo> lista = new List<Vehiculo>();
            using(MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_MostrarVehiculo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Vehiculo
                        {
                            IdVehiculo = reader.GetInt32("idVehiculo"),
                            Placa = reader.GetString("placa"),
                            Marca = reader.GetString("marca"),
                            Modelo = reader.GetString("modelo"),
                            Color = reader.GetString("color"),
                            Año = reader.GetInt32("año"),
                            IdDueño = reader.GetInt32("idDueño")
                        });
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al obtener Vehiculos: " + e.Message);
                }
            }
            return lista;
        }

        public bool AgregarVehiculo(string placa, string marca, string modelo,
            string color, int año, int idDueño)
        {
            using(MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertarVehiculo", conn);
                    cmd.CommandType =CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("placa", placa);
                    cmd.Parameters.AddWithValue("marca", marca);
                    cmd.Parameters.AddWithValue("modelo", modelo);
                    cmd.Parameters.AddWithValue("color", color);
                    cmd.Parameters.AddWithValue("año", año);
                    cmd.Parameters.AddWithValue("idDueño", idDueño);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al agregar Vehiculo: " + e.Message );
                    return false;
                }
            }
        }

        public bool ActualizarVehiculo(int id, string placa, string marca, string modelo,
            string color, int año, int idDueño)
        {
            using(MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_ModificarVehiculo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idVehiculo", id);
                    cmd.Parameters.AddWithValue("placa", placa);
                    cmd.Parameters.AddWithValue("marca", marca);
                    cmd.Parameters.AddWithValue("modelo", modelo);
                    cmd.Parameters.AddWithValue("color", color);
                    cmd.Parameters.AddWithValue("año", año);
                    cmd.Parameters.AddWithValue("idDueño", idDueño);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al actualizar Vehiculo: " + e.Message );
                    return false;
                }
            }
        }

        public bool EliminarVehiculo(int id)
        {
            using(MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_EliminarVehiculo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idVehiculo", id);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al eliminar Vehiculo: " + e.Message);
                    return false;
                }
            }
        }

        public List<Vehiculo> BuscarVehiculo(int id)
        {
            List<Vehiculo> lista = new List<Vehiculo>();
            using(MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_BuscarVehiculo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idVehiculo", id);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) 
                    {
                        lista.Add(new Vehiculo
                        {
                            IdVehiculo = reader.GetInt32("idVehiculo"),
                            Placa = reader.GetString("placa"),
                            Marca = reader.GetString("marca"),
                            Modelo = reader.GetString("modelo"),
                            Color = reader.GetString("color"),
                            Año = reader.GetInt32("año"),
                            IdDueño = reader.GetInt32("idDueño")
                        });
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al buscar Vehiculo: " + e.Message);
                }
            }
            return lista;
        }
    }
}
