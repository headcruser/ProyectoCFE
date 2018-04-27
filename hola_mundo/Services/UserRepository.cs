using hola_mundo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hola_mundo.Services
{
    public class UserRepository
    {
        string __connection;

        MySqlConnection mysqlConn;

        public UserRepository()
        {
            createConnection();
        }

        /*
         * Crea una instancia de la conexion a base de datos
        */
        private void createConnection()
        {
            __connection = @"Server=localhost;Database=cfe;Uid=root;Pwd=admin120324";
            try
            {
                mysqlConn = new MySqlConnection(__connection);
            }
            catch
            {
                throw new Exception("No hay conexion con la base de datos");
            }
        }

        private String Select()
        {
            return "SELECT id,name,email FROM users ";
        }

        private String SelectWhereID(int id)
        {
            return Select() + "WHERE id = " + id;
        }

        private String Delete(int id)
        {
            return "DELETE FROM users WHERE id="+id;
        }

        public List<Usuario> All()
        {
            List<Usuario> usuarios = null;

            using (MySqlCommand cmd = new MySqlCommand(Select()))
            {
                cmd.Connection = mysqlConn;
                mysqlConn.Open();
                usuarios = ReadData(cmd);
                mysqlConn.Close();
            }

            return usuarios;
        }

        public Usuario find(int id)
        {
            Usuario userFind = null;
            using (MySqlCommand cmd = new MySqlCommand(SelectWhereID(id)))
            {
                cmd.Connection = mysqlConn;
                mysqlConn.Open();
                using (MySqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.Read())
                    {
                        userFind = BuildCollection(sdr);
                    }
                    
                }
                mysqlConn.Close();
            }
            return userFind;
        }

        public bool Destroy(int id)
        {
            using (MySqlCommand cmd = new MySqlCommand(Delete(id)))
            {
                mysqlConn.Open();
                cmd.Connection = mysqlConn;
                cmd.ExecuteNonQuery();
                mysqlConn.Close();
                return true;
            }
        }

        private List<Usuario> ReadData(MySqlCommand command)
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (MySqlDataReader sdr = command.ExecuteReader())
            {
                while (sdr.Read())
                {
                    usuarios.Add(BuildCollection(sdr));
                }
            }

            return usuarios;
        }
        /**
         * Build Collection Item For User
         */
        private Usuario BuildCollection(MySqlDataReader reader){
            return new Usuario(
                Convert.ToInt32(reader["id"]),
                reader["name"].ToString(),
                reader["email"].ToString()
            );
        }

    }
}