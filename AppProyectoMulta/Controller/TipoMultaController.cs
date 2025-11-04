using AppProyectoMulta.DB;
using AppProyectoMulta.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppProyectoMulta.Controller
{
    public class TipoMultaController
    {
        public List<TipoMulta> ObtenerTipoMulta()
        {
            List<TipoMulta> lista = new List<TipoMulta>();
            using(MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_MostrarTipoMulta", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read()) 
                    {
                        lista.Add(new TipoMulta
                        {
                            IdTipoMulta = reader.GetInt32("idTipoMulta"),
                            DescripcionTipoMulta = reader.GetString("descripcion")
                        });
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al obtener Tipos de Multas: "+e.Message);
                }
            }
            return lista;
        }

        public bool AgregarTipoMulta(string descripcion) 
        {
            using(MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_insertarTipoMulta", conn);
                    cmd.CommandType =CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("descripcion", descripcion);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al agregar Tipo Multa: " + e.Message);
                    return false;
                }
            }
        }

        public bool ActualizarTipoMulta(int id, string descripcion) 
        {
            using(MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_ModificarTipoMulta", conn);
                    cmd.CommandType =CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idTipoMulta", id);
                    cmd.Parameters.AddWithValue("descripcion", descripcion);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al actualizar Tipo de Multa: " + e.Message );
                    return false;
                }
            }
        }

        public bool EliminarTipoMulta(int id)
        {
            using(MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_EliminarTipoMulta", conn);
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idTipoMulta", id);
                    
                    cmd.ExecuteNonQuery ();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al eliminar Tipo de Multa: " + e.Message);
                    return false;
                }
            }
        }

        public List<TipoMulta> BuscarTipoMulta(int id)
        {
            List<TipoMulta> lista = new List<TipoMulta>();
            using(MySqlConnection conn = ConexionDB.Instancia.CrearConexion())
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("sp_BuscarTipoMulta", conn );
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue ("idTipoMulta", id);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) 
                    {
                        lista.Add(new TipoMulta
                        {
                            IdTipoMulta = reader.GetInt32("idTipoMulta"),
                            DescripcionTipoMulta = reader.GetString("descripcion")
                        });
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show ("Error al Buscar Tipo de Multa: "+e.Message);
                }
            }
            return lista;
        }
    }
}
