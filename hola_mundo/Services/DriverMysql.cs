using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hola_mundo.Services
{
    /**
     * CONEXION A BASE DE DATOSS
     */
    public static class DriverMysql
    {
        private const string SERVER = @"Server=localhost;Database=cfe;Uid=root;Pwd=admin120324";

        private static MySqlConnection mysqlConn;


        public static MySqlConnection GetInstance()
        {
            if (!ExistConnection())
            {
                CreateConnection(SERVER);
            }
            return mysqlConn;
        }

        /*
         * Crea una instancia de la conexion a base de datos
        */
        private static void CreateConnection(string server)
        {            
            try
            {
                mysqlConn = new MySqlConnection(server);
            }
            catch
            {
                throw new Exception("No hay conexion con la base de datos");
            }
        }

        private static bool ExistConnection()
        {
            if (mysqlConn == null)
            {
                return false;
            }
            return false;
        }
    }
}