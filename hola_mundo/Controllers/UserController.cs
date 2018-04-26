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
            List<Usuario> usuarios = new List<Usuario>();

            string query = "select id,name,email from users";

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
                                id = Convert.ToInt32(sdr["id"]),
                                nombre = sdr["name"].ToString(),
                                email = sdr["email"].ToString()
                            });
                        }
                    }
                    mysqlConn.Close();

                }
            }

            return View(usuarios);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Store()
        {
            string name = Request.Form.Get("nombre");
            string email = Request.Form.Get("email");
            string password = "admin120324";

            try
            {

                string query = "INSERT INTO users (name,email,password) " +
                    "VALUES ('" + name +"' , '" + email + "', '"+password+"' )";

                using (MySqlConnection mysqlConn = new MySqlConnection(__connection))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query))
                    {
                        mysqlConn.Open();
                        cmd.Connection = mysqlConn;
                        cmd.ExecuteNonQuery();
                        mysqlConn.Close();

                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }



        }

        public ActionResult Edit(int id)
        {
            Usuario usuario = null;

            string query = "select id,name,email from users where id="+id;

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
                            usuario = new Usuario(
                                Convert.ToInt32(sdr["id"]),
                                sdr["name"].ToString(),
                                sdr["email"].ToString()
                            );
                        }
                    }
                    mysqlConn.Close();

                }
            }
            return View(usuario);
        }

        /**
        * Detalle Usuario
        */
        public ActionResult Show(int id)
        {
            Usuario usuario = null;

            string query = "select id,name,email from users where id=" + id;

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
                            usuario = new Usuario(
                                Convert.ToInt32(sdr["id"]),
                                sdr["name"].ToString(),
                                sdr["email"].ToString()
                            );
                        }
                    }
                    mysqlConn.Close();

                }
            }
            return View(usuario);

        }

        /**
        * Actualizar Usuario
        */
        [HttpPost]
        public ActionResult Edit()
        {
            string query = "UPDATE users " +
                 "SET name = @name, " +
                 "email = @email " +
                 "WHERE id = @id";

            string id = Request.Form.Get("id");
            string name = Request.Form.Get("nombre");
            string email = Request.Form.Get("email");
            

            using (MySqlConnection mysqlConn = new MySqlConnection(__connection))
            {
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    mysqlConn.Open();
                    cmd.Connection = mysqlConn;
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
                    cmd.ExecuteNonQuery();
                    mysqlConn.Close();

                }
            }
            
            return RedirectToAction("Index");

        }

        //Eliminar Usuario
        public ActionResult Delete( int id)
        {
            try
            {

                string query = "DELETE FROM users " +
                    "WHERE id = "+id;

                using (MySqlConnection mysqlConn = new MySqlConnection(__connection))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query))
                    {
                        mysqlConn.Open();
                        cmd.Connection = mysqlConn;
                        cmd.ExecuteNonQuery();
                        mysqlConn.Close();

                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }
    }
}