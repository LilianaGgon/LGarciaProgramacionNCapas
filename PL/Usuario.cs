using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Usuario
    {
        public static void Add() //Metodo para solicitar los datos del usuario a insertar
        {
            ML.Usuario usuario = new ML.Usuario(); //Creacion del modelo usuario

            Console.WriteLine("Ingresa el nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingresa el apellido paterno");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingresa el apellido materno");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingresa el celular");
            usuario.Celular = Console.ReadLine();

            Console.WriteLine("Ingresa el username");
            usuario.UserName = Console.ReadLine();

            Console.WriteLine("Ingresa el email");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Ingresa la contraseña");
            usuario.Password = Console.ReadLine();

            Console.WriteLine("Ingresa la fecha de nacimiento (dd/mm/aaaa)");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingresa el sexo");
            usuario.Sexo = Console.ReadLine();

            Console.WriteLine("Ingresa el telefono");
            usuario.Telefono = Console.ReadLine(); ;

            //Console.WriteLine("Ingresa el estatus");
            usuario.Estatus = true;

            Console.WriteLine("Ingresa el CURP");
            usuario.CURP = Console.ReadLine(); ;

            //Console.WriteLine("Ingresa la imagen");
            //usuario.Imagen = Convert.ToByte[](Console.ReadLine());

            ML.Result result = BL.Usuario.AddLINQ(usuario); //Se envia el modelo del nuevo usuario a BL y el result

            if (result.Correct)
            {
                Console.WriteLine("Registro exitoso");
            }
            else
            {
                Console.WriteLine("ERROR. " + result.ErrorMessage);
            }

        } //Cierre solicitar datos para agregar

        public static void Delete()
        {
            Console.WriteLine("Ingresa el ID del usuario que deseas eliminar");
            int idUsuario = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.DeleteLINQ(idUsuario); //Se envia el modelo del nuevo usuario a BL y el result

            if (result.Correct)
            {
                Console.WriteLine("Se eliminó el registro con éxito");
            }
            else
            {
                Console.WriteLine("ERROR. " + result.ErrorMessage);
            }

        } //Cierre solicitar datos para eliminar

        public static void GetAll() //Metodo para mostrar todos los registros
        {
            ML.Result result = BL.Usuario.GetAllLINQ(); //Devolvemos un modelo de BL a ML llamado result

            if (result.Correct)//Si el proceso de result es correcto
            {
                //Mostrar registros
                foreach (ML.Usuario usuario in result.Objects) //Recorre el arreglo del modelo en ML llamado usuario que esta en el objeto del result llamado results
                {
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("Nombre: " + usuario.Nombre); //Imprime el nombre
                    Console.WriteLine("Apellido Paterno: " + usuario.ApellidoPaterno); //Imprime el apellido paterno 
                    Console.WriteLine("Apellido Materno: " + usuario.ApellidoMaterno); //Imprime el apellido materno 
                    Console.WriteLine("Celular: " + usuario.Celular);
                    Console.WriteLine("User Name: " + usuario.UserName);
                    Console.WriteLine("Email: " + usuario.Email);
                    Console.WriteLine("Password: " + usuario.Password);
                    Console.WriteLine("Fecha de Nacimiento: " + usuario.FechaNacimiento);
                    Console.WriteLine("Sexo: " + usuario.Sexo);
                    Console.WriteLine("Telefono: " + usuario.Telefono);
                    Console.WriteLine("Estatus: " + usuario.Estatus);
                    Console.WriteLine("CURP: " + usuario.CURP);
                    Console.WriteLine("Id del Rol: " + usuario.Rol.IdRol);
                    Console.WriteLine("-------------------------------");

                }
            }
            else
            {
                Console.WriteLine("ERROR. " + result.ErrorMessage); //Trae el detalle del error
            }
        }

        public static void GetById()
        {
            Console.WriteLine("Ingresa el Id del usuario que quieres consultar");
            int idusuario = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.GetByIdLINQ(idusuario); //El modelo result guarda el id y lo manda al metodo GetById

            if (result.Correct)
            {
                //Imprimir la informacion del usuario 
                ML.Usuario usuario = (ML.Usuario)result.Object; //UNBOXING. De la caja result.Object se saca el modelo usuario y se le da al modelo usuario llamado usuario
                Console.WriteLine("Nombre: " + usuario.Nombre); //Imprime el nombre
                Console.WriteLine("Apellido Paterno: " + usuario.ApellidoPaterno); //Imprime el apellido paterno 
                Console.WriteLine("Apellido Materno: " + usuario.ApellidoMaterno); //Imprime el apellido materno 
                Console.WriteLine("Celular: " + usuario.Celular);
                Console.WriteLine("User Name: " + usuario.UserName);
                Console.WriteLine("Email: " + usuario.Email);
                Console.WriteLine("Password: " + usuario.Password);
                Console.WriteLine("Fecha de Nacimiento: " + usuario.FechaNacimiento);
                Console.WriteLine("Sexo: " + usuario.Sexo);
                Console.WriteLine("Telefono: " + usuario.Telefono);
                Console.WriteLine("Estatus: " + usuario.Estatus);
                Console.WriteLine("CURP: " + usuario.CURP);
                Console.WriteLine("Id del Rol: " + usuario.Rol.IdRol);
            }
            else
            {
                Console.WriteLine("ERROR. " + result.ErrorMessage);
            }
        } //cierre solicitar datos para consultar

        public static void Update()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingresa el ID del usuario que deseas actualizar");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingresa el nombre actualizado");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingresa el apellido paterno actualizado");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingresa el apellido materno actualizado");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingresa el celular actualizado");
            usuario.Celular = Console.ReadLine();

            Console.WriteLine("Ingresa el username actualizado");
            usuario.UserName = Console.ReadLine(); ;

            Console.WriteLine("Ingresa el email actualizado");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Ingresa la contraseña actualizada");
            usuario.Password = Console.ReadLine();

            Console.WriteLine("Ingresa la fecha de nacimiento actualizado");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingresa el sexo actualizado");
            usuario.Sexo = Console.ReadLine();

            Console.WriteLine("Ingresa el telefono actualizado");
            usuario.Telefono = Console.ReadLine(); ;

            Console.WriteLine("Ingresa el estatus actualizado");
            usuario.Estatus = Convert.ToBoolean(Console.ReadLine());

            Console.WriteLine("Ingresa el CURP actualizado");
            usuario.CURP = Console.ReadLine(); ;

            //Console.WriteLine("Ingresa la imagen actualizada");
            //usuario.Imagen = Convert.ToByte(Console.ReadLine());


            ML.Result result = BL.Usuario.UpdateLINQ(usuario); //Se envia el modelo del nuevo usuario a BL y el result

            if (result.Correct)
            {
                Console.WriteLine("Actualizacion exitosa");
            }
            else
            {
                Console.WriteLine("ERROR. " + result.ErrorMessage);
            }

        } //Cierre solicitar datos para actualizar

        public static void CargaMasiva()
        {
            Console.WriteLine("Ejecutando carga masiva");

            ML.Result result = BL.Usuario.CargaMasiva();

            if (result.Correct)
            {
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("ERROR. " + result.ErrorMessage);
            }
        }
        
    }
}
