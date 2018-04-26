using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

using hola_mundo.Models;  //Rerencia al modulo que estamos utilizando
using System.Data;

namespace hola_mundo.Controllers
{
    public class UserController : Controller
    {
        string __connection = @"Server=localhost;Database=cfe;Uid=root;Pwd=admin120324";

        /**
        * Método Index. Renderiz la vista de usuario  
        */
        public ActionResult Index()
        {
            List<Usuario> lista = new List<Usuario>();

            Usuario user1 = new Usuario();

            lista.Add(user1);

            return View(lista);
        }

        /**
        * Detalle Usuario
        */
        public ActionResult Show()
        {

            Usuario showUser = new Usuario();
            return View(showUser);
        }

        /**Conexion a base de datos*/
        public ActionResult Database()
        {
            List<Usuario> usuarios = new List<Usuario>();

            string query = "select id,name,email from users limit 1";
            
            using (MySqlConnection mysqlConn = new MySqlConnection(__connection))
            {
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = mysqlConn;
                    mysqlConn.Open();

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            usuarios.Add(new Usuario
                            {
                                id      = Convert.ToInt32(sdr["id"]),
                                nombre  = sdr["name"].ToString(),
                                email   = sdr["email"].ToString()
                            });
                        }
                    }
                    mysqlConn.Close();

                }
            }

            return View(usuarios);
        }
    }
}