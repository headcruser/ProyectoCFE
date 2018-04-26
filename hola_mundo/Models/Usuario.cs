using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Namespace
namespace hola_mundo.Models
{
    //Clase
    public class Usuario
    {
        //Atributos
        public int id           { get; set; }
        public int edad         { get; set; }
        public String nombre    { get; set; }
        public String apellidos { get; set; }
        public String email { get; set; }

        //Constructor
        /* 
           Aqui es un comentatio
         */
        public Usuario() {
            id = 1;
            edad = 18;
            nombre = "Usuario";
            apellidos = "Prueba";
            email = "default@gmail.com";
        }

        //Sobrecarga de constructor
        public Usuario(int id,string nombre,string email)
        {
            this.id = id;
            this.nombre = nombre;
            this.email = email;

            apellidos = "Prueba";
            edad = 18;
        }

    }
}