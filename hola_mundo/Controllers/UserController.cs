using System;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

using hola_mundo.Models;  
using hola_mundo.Services;
using Newtonsoft.Json;

namespace hola_mundo.Controllers
{
    /**
     * Controlador Usuario
     */
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
        * Renderiza la vista Principal del Controlador usuario  
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
            return View(user.find(id));

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
            //Poner Variables de su formulario
            string name = Request.Form.Get("nombre");
            string email = Request.Form.Get("email");
            string password = "admin120324";

            try
            {   //Insertar query, segun la estructura de la tabla creada
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
            Usuario edit = user.find(id);
            return View(edit);
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
        public ActionResult getUsers() {
            return Json(user.All(), JsonRequestBehavior.AllowGet);
        }
    }
}