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
        private int id           { get; set; }
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
        }

        //métodos
        public void MostrarDatos()
        {
            System.Console.WriteLine("Objeto Usuario"+ this.id);
        }
    }
}