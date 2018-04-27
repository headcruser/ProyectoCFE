using System;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

using hola_mundo.Models;  
using hola_mundo.Services;

namespace hola_mundo.Controllers
{
    public class UserController : Controller
    {
        string __connection;

        UserRepository user;

        public UserController()
        {
            __connection = @"Server=localhost;Database=cfe;Uid=root;Pwd=admin120324";

            try
            {
                user = new UserRepository();
            }
            catch
            {
                throw new Exception("No hay conexion con la base de datos");
            }
            
        }
        
        /**
        * Renderiz la vista Principal del Controlador usuario  
        */
        public ActionResult Index()
        {
            return View(user.All());
        }

        /**
         * Muestra el detalle del usuario
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
         * Renderiza el Formulario de creacion
         */
        public ActionResult Create()
        {
            return View();
        }

        /**
         * Inserta un elemento desde la base de datos
         */
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

        /**
         * Muestra la informacion del usuario a editarr
         */
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
        * Actualizar Usuario en la base de datos
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

        /**
         * Elimina un usuario de la base de datos
         *
         */
        public ActionResult Delete( int id)
        {
            try
            {
                user.Destroy(id);
                return RedirectToAction("Index");
            }
            catch
            {
                throw new Exception("Error al Insertar en la Base de datos");
            }
        }

        /**
         * Renderiza el ejemplo de una ventana Modal
         */
        public ActionResult Modal()
        {
            return View();
        }

        /**
         * Regresa una peticion en formato Json/Ajax
         */
        public JsonResult getUsers() {
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}