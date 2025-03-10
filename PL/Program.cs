using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Program
    {
        static void Main(string[] args)
        {
            //INTERFAZ DE USUARIO
            int opcion = 0;
            do
            {
                Console.WriteLine("\nIngresa el numero de accion que quieres realizar");
                Console.WriteLine(" 1. Ingresar registro \n 2. Actualizar registro \n 3. Eliminar registro \n 4. Ver todos los registros \n 5. Ver solo un registro \n 6. Salir");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Usuario.Add(); //Se manda a llamar al metodo Add
                        break;

                    case 2:
                        Usuario.Update(); //Se manda a llamar al metodo Update
                        break;

                    case 3:
                        Usuario.Delete(); //Se manda a llamar el metodo Delete
                        break;

                    case 4:
                        Usuario.GetAll(); //Se manda a llamar el metodo GetAll
                        break;

                    case 5:
                        Usuario.GetById(); //Se manda a llamar el metodo GetById
                        break;

                    case 6:
                        Usuario.GetById(); //Se manda a llamar el metodo CargaMasiva
                        break;

                    default:
                        Console.WriteLine("Opcion invalida");
                        break;
                }

            } while (opcion != 6);

        }
    }
}
