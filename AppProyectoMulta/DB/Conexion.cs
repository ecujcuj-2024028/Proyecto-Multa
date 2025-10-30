using MySql.Data.MySqlClient;
using System;

namespace AppProyectoMulta.DB
{
    public class ConexionDB
    {
        private static ConexionDB instancia = null;
        private readonly string cadenaConexion;

        private ConexionDB()
        {
            string host = Environment.GetEnvironmentVariable("MYSQL_ADDON_HOST");
            string db = Environment.GetEnvironmentVariable("MYSQL_ADDON_DB");
            string user = Environment.GetEnvironmentVariable("MYSQL_ADDON_USER");
            string pass = Environment.GetEnvironmentVariable("MYSQL_ADDON_PASSWORD");
            string port = Environment.GetEnvironmentVariable("MYSQL_ADDON_PORT");

            cadenaConexion = $"Server={host};Port={port};Database={db};Uid={user};Pwd={pass};SslMode=Required;";
        }

        public static ConexionDB Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new ConexionDB();
                return instancia;
            }
        }

        public MySqlConnection CrearConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }
    }
}
