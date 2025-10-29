using MySql.Data.MySqlClient;

namespace AppProyectoMulta.DB
{
    public class ConexionDB
    {
        private static ConexionDB instancia = null;

        private string cadenaConexion;

        private ConexionDB()
        {
            cadenaConexion = "Server=btyfbw2rzcqmdrin7lya-mysql.services.clever-cloud.com;" +
                           "Port=3306;" +
                           "Database=btyfbw2rzcqmdrin7lya;" +
                           "Uid=uevtlvk5entsmipn;Pwd=5UEOoD9wBWlNA4122KSJ;" +
                           "CharSet=utf8mb4;SslMode=Preferred;";
        }

        public static ConexionDB Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new ConexionDB();
                }
                return instancia;
            }
        }

        public MySqlConnection CrearConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }
    }
}