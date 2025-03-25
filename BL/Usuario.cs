using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {

        public static ML.Result CambiarEstatus(int idUsuario, bool estatus)
        {
            ML.Result result = new ML.Result();

            using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
            {
                try
                {
                    var query = context.CambiarEstatus(idUsuario, estatus);

                    if (idUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Estatus = estatus;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                    result.Ex = ex;
                }
            }


            return result;
        }

        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();

            Console.WriteLine("Carga masiva");
            string ruta = @"C:\Users\digis\Documents\LILIANA_GARCIA_GONZALEZ\Otros\cargaMasiva.txt"; //Ruta absoluta del txt
            try
            {
                StreamReader streamReader = new StreamReader(ruta); //Se crea un streamreader con el parametro de ruta
                string fila = "";

                fila = streamReader.ReadLine(); //Se lee la primera fila que es de los encabezados y la ignora

                while ((fila = streamReader.ReadLine()) != null) //si la siguiente fila no es nula...
                {
                    string[] valores = fila.Split('|'); //Los valores del txt se separan cada que encuentre un |

                    //Se instancian los modelos a utilizar
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Rol = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                    //Se van sacando los valores del txt
                    usuario.Nombre = valores[0];
                    usuario.ApellidoPaterno = valores[1];
                    usuario.ApellidoMaterno = valores[2];
                    usuario.Celular = valores[3];
                    usuario.UserName = valores[4];
                    usuario.Email = valores[5];
                    usuario.Password = valores[6];
                    usuario.FechaNacimiento = valores[7];
                    usuario.Sexo = valores[8];
                    usuario.Telefono = valores[9];
                    usuario.Estatus = Convert.ToBoolean(Convert.ToInt32(valores[10]));
                    usuario.CURP = valores[11].ToUpper();
                    usuario.Rol.IdRol = Convert.ToInt32(valores[12]);
                    usuario.Direccion.Calle = valores[13];
                    usuario.Direccion.NumeroInterior = valores[14];
                    usuario.Direccion.NumeroExterior = valores[15];
                    usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(valores[16]);

                    BL.Usuario.AddEF(usuario); //Se agregan a la BD 
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result LeerExcel(string cadenaConexion)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(cadenaConexion))
                {
                    string query = "SELECT * FROM[Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand(query))
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter adapter = new OleDbDataAdapter();
                        adapter.SelectCommand = cmd;

                        DataTable tablaUsuario = new DataTable();
                        adapter.Fill(tablaUsuario);

                        if (tablaUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tablaUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Rol = new ML.Rol();
                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                                usuario.Nombre = row[0].ToString();
                                usuario.ApellidoPaterno = row[1].ToString();
                                usuario.ApellidoMaterno = row[2].ToString();
                                usuario.Celular = row[3].ToString();
                                usuario.UserName = row[4].ToString();
                                usuario.Email = row[5].ToString();
                                usuario.Password = row[6].ToString();
                                usuario.FechaNacimiento = row[7].ToString();
                                usuario.Sexo = row[8].ToString();
                                usuario.Telefono = row[9].ToString();
                                usuario.CURP = row[10].ToString();
                                if (row[11].ToString() == "" || row[11].ToString() == null)
                                {
                                    usuario.Rol.IdRol = 0;
                                }
                                else
                                {
                                    usuario.Rol.IdRol = Convert.ToInt32(row[11].ToString());
                                }
                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroInterior = row[13].ToString();
                                usuario.Direccion.NumeroExterior = row[14].ToString();

                                if (row[15].ToString() == "" || row[15].ToString() == null)
                                {
                                    usuario.Direccion.Colonia.IdColonia = 0;
                                }
                                else
                                {
                                    usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(row[15].ToString());
                                }

                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.ResultExcel ValidarExcel(List<object> registros)
        {
            ML.ResultExcel result = new ML.ResultExcel();

            result.Errores = new List<object>();

            int contador = 1;
            foreach (ML.Usuario usuario in registros)
            {
                ML.ResultExcel resultValidacion = new ML.ResultExcel();
                result.NumeroRegistro = contador;

                if (usuario.Nombre.Length > 50 || usuario.Nombre == "" || usuario.Nombre == null)
                {
                    resultValidacion.ErrorMessage += "El nombre del registro " + contador + " es muy largo o está vacío \n";
                }
                if (usuario.ApellidoPaterno.Length > 50 || usuario.ApellidoPaterno == "" || usuario.ApellidoPaterno == null)
                {
                    resultValidacion.ErrorMessage += "El apellido paterno del registro " + contador + " está vacio o es muy largo \n";
                }
                if (usuario.ApellidoMaterno.Length > 50)
                {
                    resultValidacion.ErrorMessage += "El apellido materno del registro " + contador + " es muy largo \n";
                }
                if (usuario.Celular.Length > 20 || usuario.Celular == "" || usuario.Celular == null)
                {
                    resultValidacion.ErrorMessage += "El celular  del registro " + contador + " está vacio o es muy largo \n";
                }
                if (usuario.UserName.Length > 50 || usuario.UserName == "" || usuario.UserName == null)
                {
                    resultValidacion.ErrorMessage += "El Username del registro " + contador + " está vacio o es muy largo \n";
                }
                if (usuario.Email.Length > 254 || usuario.Email == "" || usuario.Email == null)
                {
                    resultValidacion.ErrorMessage += "El email del registro " + contador + " está vacio o es muy largo \n";
                }
                if (usuario.Password.Length > 50 || usuario.Password == "" || usuario.Password == null)
                {
                    resultValidacion.ErrorMessage += "La contraseña del registro " + contador + " está vacio o es muy largo \n";
                }
                if (usuario.FechaNacimiento == "" || usuario.FechaNacimiento == null)
                {
                    resultValidacion.ErrorMessage += "La fecha de nacimiento  del registro " + contador + "está vacio o es muy largo \n";
                }
                if (usuario.Sexo.Length > 2 || usuario.Sexo == "" || usuario.Sexo == null)
                {
                    resultValidacion.ErrorMessage += "El sexo del registro " + contador + " está vacio o es muy largo \n";
                }
                if (usuario.Telefono.Length > 20 || usuario.Telefono == "" || usuario.Telefono == null)
                {
                    resultValidacion.ErrorMessage += "El telefono del registro " + contador + " está vacio o es muy largo \n";
                }
                if (usuario.CURP.Length > 50 || usuario.CURP == "" || usuario.CURP == null)
                {
                    resultValidacion.ErrorMessage += "El CURP del registro " + contador + " está vacio o es muy largo \n";
                }
                if (usuario.Rol.IdRol == 0 || usuario.Rol.IdRol > 6)
                {
                    resultValidacion.ErrorMessage += "El Id rol del registro " + contador + " está vacio o no existe \n";
                }
                if (usuario.Direccion.Calle.Length > 50 || usuario.Direccion.Calle == "" || usuario.Direccion.Calle == null)
                {
                    resultValidacion.ErrorMessage += "La calle del registro " + contador + " está vacio o es muy largo \n";
                }
                if (usuario.Direccion.NumeroInterior.Length > 20)
                {
                    resultValidacion.ErrorMessage += "El número interior del registro " + contador + " es muy largo \n";
                }
                if (usuario.Direccion.NumeroExterior.Length > 20 || usuario.Direccion.NumeroExterior == "" || usuario.Direccion.NumeroExterior == null)
                {
                    resultValidacion.ErrorMessage += "El número exterior del registro " + contador + " está vacio o es muy largo \n";
                }
                if (usuario.Direccion.Colonia.IdColonia == 0 || usuario.Direccion.Colonia.IdColonia > 3598)
                {
                    resultValidacion.ErrorMessage += "El Id colonia del registro " + contador + " está vacío o no existe \n";
                }

                if (resultValidacion.ErrorMessage != null || resultValidacion.ErrorMessage == "")
                {
                    result.Errores.Add(resultValidacion);
                }

                contador++;
            }

            return result;
        }

        //METODOS CON LINQ
        //Metodo Add
        public static ML.Result AddLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result(); //Nuevo modelo result

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities()) //Conexion a BD
                {
                    DL_EF.Usuario usuarioDB = new DL_EF.Usuario(); //Crea un nuevo modelo de usuario pero en la capa DL_EF
                    usuarioDB.Nombre = usuario.Nombre; //Al usuario en DB se le pasa la informacion del Pl
                    usuarioDB.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuarioDB.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioDB.Celular = usuario.Celular;
                    usuarioDB.UserName = usuario.UserName;
                    usuarioDB.Email = usuario.Email;
                    usuarioDB.Password = usuario.Password;
                    usuarioDB.FechaNacimiento = DateTime.Parse(usuario.FechaNacimiento);
                    usuarioDB.Sexo = usuario.Sexo;
                    usuarioDB.Telefono = usuario.Telefono;
                    usuarioDB.Estatus = usuario.Estatus;
                    usuarioDB.CURP = usuario.CURP;
                    usuarioDB.Imagen = usuario.Imagen;
                    usuarioDB.IdRol = usuario.Rol.IdRol;

                    context.Usuarios.Add(usuarioDB); //Se crea el query

                    int filasAfectadas = context.SaveChanges(); //Se ejecuta el query

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result DeleteLINQ(int idUsuario)
        {
            ML.Result result = new ML.Result();

            using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
            {
                try
                {
                    //Busca el id. Se usa SELECT * FROM ... WHERE ...
                    var soloDelete = (from usuario in context.Usuarios //De usuario en la BD usuarios creada por EF
                                      where usuario.IdUsuario == idUsuario //Donde el id del usuario es igual a la variable idUsuario
                                      select usuario).SingleOrDefault(); //Selecciona el usuario, el registro debe ser unico, de lo contrario dará error

                    //Busca el id de la manera correct. SELECT (columnas) FROM ... WHERE ...
                    //        var selectCorrecto = (from usuario in context.Usuarios
                    //                              where usuario.Id == idUsuario
                    //                              select new
                    //                              {
                    //                                  //Alias = Valor
                    //                                  idUsuario = usuario.Id,
                    //                                  Nombre = usuario.Nombre,
                    //                                  ApellidoPaterno = usuario.ApellidoPaterno,
                    //                                  ApellidoMaterno = usuario.ApellidoMaterno,
                    //                                  Celular = usuario.Celular,
                    //                                  UserName = usuario.UserName,
                    //                                  Email = usuario.Email,
                    //                                  Password = usuario.Password,
                    //                                  FechaNacimiento = usuario.FechaNacimiento,
                    //                                  Sexo = usuario.Sexo,
                    //                                  Telefono = usuario.Telefono,
                    //                                  Estatus = usuario.Estatus,
                    //                                  CURP = usuario.CURP, 
                    //                                  IdRol = usuario.IdRol
                    //                              }).SingleOrDefault

                    if (soloDelete != null)
                    {
                        //2. Delete 
                        context.Usuarios.Remove(soloDelete); //Genera el query DELETE ... WHERE ID = @ID
                        int filasAfectadas = context.SaveChanges(); //Ejecuta el query
                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo eliminar el registro";
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                    result.Ex = ex;
                }
            }
            return result;
        }

        public static ML.Result UpdateLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
            {
                try
                {
                    //1. Buscar
                    var buscar = (from usuarioDB in context.Usuarios //Para usuario en la BD llamada Usuarios en EF 
                                  where usuarioDB.IdUsuario == usuario.IdUsuario //Donde el id del usuario es igual a idusuario
                                  select usuarioDB).SingleOrDefault(); //Se selecciona el usuario

                    //2. Actualizar
                    if (buscar != null)
                    {
                        //Si existe el ID
                        //Se asigna al modelo de la BD del DL_EF = Se saca la informacion del usuario del modelo de ML 
                        buscar.Nombre = usuario.Nombre;
                        buscar.ApellidoPaterno = usuario.ApellidoPaterno;
                        buscar.ApellidoMaterno = usuario.ApellidoMaterno;
                        buscar.Celular = usuario.Celular;
                        buscar.UserName = usuario.UserName;
                        buscar.Email = usuario.Email;
                        buscar.Password = usuario.Password;
                        buscar.FechaNacimiento = DateTime.Parse(usuario.FechaNacimiento);
                        buscar.Sexo = usuario.Sexo;
                        buscar.Telefono = usuario.Telefono;
                        buscar.Estatus = usuario.Estatus;
                        buscar.CURP = usuario.CURP;
                        buscar.IdRol = usuario.Rol.IdRol;

                        int filasAfectadas = context.SaveChanges(); //se ejecuta el query y se guarda el valor de filas afectadas

                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al actualizar";
                        }
                    }
                    else
                    {
                        //Si no existe el ID
                        result.Correct = false;
                        result.ErrorMessage = "No existe un registro con ese ID";
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                    result.Ex = ex;
                }
            }
            return result;
        }

        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities()) //Conexion a BD 
                {
                    var select = (from usuario in context.Usuarios
                                  select new
                                  {
                                      //Alias = Valor
                                      IdUsuario = usuario.IdUsuario,
                                      Nombre = usuario.Nombre,
                                      ApellidoPaterno = usuario.ApellidoPaterno,
                                      ApellidoMaterno = usuario.ApellidoMaterno,
                                      Celular = usuario.Celular,
                                      UserName = usuario.UserName,
                                      Email = usuario.Email,
                                      Password = usuario.Password,
                                      FechaNacimiento = usuario.FechaNacimiento,
                                      Sexo = usuario.Sexo,
                                      Telefono = usuario.Telefono,
                                      Estatus = usuario.Estatus,
                                      CURP = usuario.CURP,
                                      IdRol = usuario.IdRol
                                  }).ToList();


                    if (select.Count > 0)
                    {
                        result.Objects = new List<object>(); //Nueva lista de objetos 
                        foreach (var objBD in select)
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.IdUsuario = objBD.IdUsuario;
                            usuario.Nombre = objBD.Nombre;
                            usuario.ApellidoPaterno = objBD.ApellidoPaterno;
                            usuario.ApellidoMaterno = objBD.ApellidoMaterno;
                            usuario.Celular = objBD.Celular;
                            usuario.UserName = objBD.UserName;
                            usuario.Email = objBD.Email;
                            usuario.Password = objBD.Password;
                            usuario.FechaNacimiento = Convert.ToString(objBD.FechaNacimiento);
                            usuario.Sexo = objBD.Sexo;
                            usuario.Telefono = objBD.Telefono;
                            usuario.Estatus = objBD.Estatus;
                            usuario.CURP = objBD.CURP;
                            usuario.Rol.IdRol = Convert.ToInt32(objBD.IdRol);

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result GetByIdLINQ(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    //1. Buscar
                    var select = (from usuarioDB in context.Usuarios //Para usuario en la BD llamada Usuarios en EF 
                                  where usuarioDB.IdUsuario == idUsuario //Donde el id del usuario es igual a idusuario
                                  select usuarioDB).SingleOrDefault(); //Se selecciona el usuario

                    //2. Actualizar
                    if (select != null)
                    {
                        //Si existe el ID
                        //Se asigna al modelo de la BD del DL_EF = Se saca la informacion del usuario del modelo de ML 
                        result.Object = new ML.Result();
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = select.IdUsuario;
                        usuario.Nombre = select.Nombre;
                        usuario.ApellidoPaterno = select.ApellidoPaterno;
                        usuario.ApellidoMaterno = select.ApellidoMaterno;
                        usuario.Celular = select.Celular;
                        usuario.UserName = select.UserName;
                        usuario.Email = select.Email;
                        usuario.Password = select.Password;
                        usuario.FechaNacimiento = Convert.ToString(select.FechaNacimiento);
                        usuario.Sexo = select.Sexo;
                        usuario.Telefono = select.Telefono;
                        usuario.Estatus = select.Estatus;
                        usuario.CURP = select.CURP;
                        usuario.Rol.IdRol = Convert.ToInt32(select.IdRol);

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        //Si no existe el ID
                        result.Correct = false;
                        result.ErrorMessage = "No existe un registro con ese ID";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }




        //METODOS CON ENTITY FRAMEWORK
        //Metodo Add
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result(); //Se crea el modelo result

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    int filasAfectadas = context.UsuarioDireccionAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Celular, usuario.UserName, usuario.Email, usuario.Password, Convert.ToDateTime(usuario.FechaNacimiento), usuario.Sexo, usuario.Telefono, usuario.CURP.ToUpper(), usuario.Imagen, usuario.Rol.IdRol, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                        result.ErrorMessage = "Se realizó la inserción correctamente";
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se realizó la inserción correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        } //Cierre metodo Add

        //Metodo Update
        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result(); //Se crea el modelo result

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    int filasAfectadas = context.UsuarioDireccioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Celular, usuario.UserName, usuario.Email, usuario.Password, Convert.ToDateTime(usuario.FechaNacimiento), usuario.Sexo, usuario.Telefono, usuario.Estatus, usuario.CURP.ToUpper(), usuario.Imagen, usuario.Rol.IdRol, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        } //Cierre metodo Update

        //Metodo Delete
        public static ML.Result DeleteEF(int idUsuario)
        {
            ML.Result result = new ML.Result(); //Se crea el modelo result

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    int filasAfectadas = context.UsuarioDireccioDelete(idUsuario);
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        } //Cierre metodo Delete

        //Metodo GetAll
        public static ML.Result GetAllEF(ML.Usuario usuarioBusquedaAbierta)
        {
            ML.Result result = new ML.Result(); //Se crea el modelo result
            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {

                    var objDB = context.UsuarioGetAllViewBusquedaAbierta(usuarioBusquedaAbierta.Nombre, usuarioBusquedaAbierta.ApellidoPaterno, usuarioBusquedaAbierta.ApellidoMaterno, usuarioBusquedaAbierta.Rol.IdRol).ToList(); //objDB se convierte en una lista en la que se guardan los objetos usuarios

                    if (objDB.Count() > 0)
                    {
                        result.Objects = new List<object>(); //Se crea una nueva lista de objetos
                        foreach (var obj in objDB)
                        {
                            //Si hay registros
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();

                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre = obj.NombreUsuario;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Celular = obj.Celular;
                            usuario.UserName = obj.UserName;
                            usuario.Email = obj.Email;
                            usuario.Password = obj.Password;
                            usuario.FechaNacimiento = obj.FechaNacimiento;
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Estatus = obj.Estatus;
                            usuario.CURP = obj.CURP.ToUpper();
                            usuario.ImagenBase64 = usuario.Imagen == null ? "" :Convert.ToBase64String(obj.Imagen);
                            usuario.Rol.Nombre = obj.NombreRol;
                            if (obj.IdRol != null)
                            {
                                usuario.Rol.IdRol = Convert.ToInt32(obj.IdRol);
                            }
                            else
                            {
                                usuario.Rol.IdRol = 0;
                            }
                            usuario.Direccion.Calle = obj.Calle;
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior;
                            usuario.Direccion.Colonia.Nombre = obj.NombreColonia;
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;


                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        //No hay registros
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        } //Cierre metodo GetAll

        //Metodo GetById
        public static ML.Result GetByIdEF(int idUsuario)
        {
            ML.Result result = new ML.Result(); //Se crea el modelo result
            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities()) //Se llama el modelo result 
                {
                    var query = context.UsuarioGetById(idUsuario).SingleOrDefault(); //Se pasa el stored procedure y la variable

                    if (query != null)
                    {
                        //Si trajo registros 
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.NombreUsuario;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Celular = query.Celular;
                        usuario.UserName = query.UserName;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.FechaNacimiento = query.FechaNacimiento;
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Estatus = query.Estatus;
                        usuario.CURP = query.CURP.ToUpper();
                        usuario.Imagen = query.Imagen;
                        if (query.IdRol != null)
                        {
                            usuario.Rol.IdRol = query.IdRol.Value;
                        }
                        else
                        {
                            usuario.Rol.IdRol = 0;
                        }
                        usuario.Direccion.Calle = query.Calle;
                        usuario.Direccion.NumeroInterior = query.NumeroInterior;
                        usuario.Direccion.NumeroExterior = query.NumeroExterior;
                        usuario.Direccion.Colonia.IdColonia = query.IdColonia == null ? 0 : query.IdColonia.Value;
                        usuario.Direccion.Colonia.Nombre = query.NombreColonia;
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio == null ? 0 : query.IdMunicipio.Value;
                        usuario.Direccion.Colonia.Municipio.Nombre = query.NombreMunicipio;
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado == null ? 0 : query.IdEstado.Value;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.NombreEstado;

                        result.Object = usuario;
                        result.Correct = true;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        } //Cierre metodo GetById




        //METODOS CON STORED PROCEDURE
        //Metodo Add
        public static ML.Result Add(ML.Usuario usuario) //Se crea el modelo usuario mandando a llamar la capa ML y la clase Usuario. El ultimo "Usuario" es el nombre del modelo
        {
            ML.Result result = new ML.Result(); //Crea un nuevo modelo de result llamado result

            //Se abre una conexion con la BD usando try catch para disminuir errores
            try
            {
                using (SqlConnection context = new SqlConnection(DL.connection.ObtenerConexion())) //Para eliminar conexion cuando se termine de ejecutar el bloque de codigo try. Para indicar a que BD se va a conectar
                {
                    string query = "UsuarioAdd"; //Se le indica el nombre del stored procedure

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Sustitucion de valores de parametros. Se toma el valor en el query, se sustituye por el nombre del valor en el modelo
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    cmd.Parameters.AddWithValue("@Imagen", usuario.Imagen);

                    context.Open(); //Se abre la BD
                    //Variable para indicar cuantas filas en la BD fueron afectadas
                    int filasAfectadas = cmd.ExecuteNonQuery(); //ExecuteNonQuery ejecuta el Query

                    //If para indicar si el insert fue exitoso
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.ErrorMessage = "Error al insertar el usuario";
                    }
                } //Se cierra la BD y se elimina el bloque de codigo
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        } //Cierre metodo Add

        //Metodo Update
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.connection.ObtenerConexion()))
                {
                    string query = "UsuarioUpdate";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    //cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    //cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    //cmd.Parameters.AddWithValue("@Imagen", usuario.Imagen);

                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al actualizar el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        } //Cierre metodo Update

        //Metodo Delete
        public static ML.Result Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.connection.ObtenerConexion()))
                {
                    string query = "UsuarioDelete";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        } //Cierre de metodo Delete

        //Metodo GetAll
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result(); //Crea un nuevo modelo de result llamado result

            try
            {
                using (SqlConnection context = new SqlConnection(DL.connection.ObtenerConexion())) //Obtener conexion
                {
                    string query = "UsuarioGetAll"; //Stored procedur que se va a usar
                    SqlCommand cmd = new SqlCommand(); //Se crea un nuevo cmd
                    cmd.Connection = context; //Se le pasa la conexion
                    cmd.CommandText = query; //Se le pasa el SP
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //Se crea el puente entre el codigo y la BD indicando que query y conexion se van a usar
                    DataTable dataTable = new DataTable(); //Se crea la tabla en donde se va a vaciar la informacion 
                    adapter.Fill(dataTable); //Se le pasa la informacion del adapter a la tabla

                    if (dataTable.Rows.Count > 0) //Si el numero de filas en la tabla es mayor a 0
                    {
                        //Si hay registros 
                        result.Objects = new List<object>(); //El resultados de Objects se almacena en una nueva lista de objects

                        foreach (DataRow row in dataTable.Rows) //Foreach recorre el arreglo de columnas de principio a fin sin excepcion
                        {
                            ML.Usuario usuario = new ML.Usuario(); //Se crea un nuevo modelo de usuario 
                            //El tipo de dato row devuelve un dato row, asi que se debe parsear para que coincida con los tipos de datos asignados en la BD
                            usuario.IdUsuario = Convert.ToInt32(row[0].ToString()); //Se indica que la columna 0 es el id del usuario, se convierte a string y despues a int para que coinvita con los tipos de datos asignados
                            usuario.Nombre = row[1].ToString(); //Se indica que la columna 1 es el nombre del usuario y se convierte a string
                            usuario.ApellidoPaterno = row[2].ToString(); //Se indica que la columna 2 es el apellido paterno del usuario y se convierte a string
                            usuario.ApellidoMaterno = row[3].ToString(); //Se indica que la columna 3 es el apellido materno del usuario y se convierte a string
                            usuario.Celular = row[4].ToString();
                            usuario.UserName = row[5].ToString();
                            usuario.Email = row[6].ToString();
                            usuario.Password = row[7].ToString();
                            usuario.FechaNacimiento = row[8].ToString();
                            usuario.Sexo = row[9].ToString();
                            usuario.Telefono = row[10].ToString();
                            usuario.Estatus = Convert.ToBoolean(row[11].ToString());
                            usuario.CURP = row[12].ToString();

                            if (row[13].ToString() == "")
                            {
                                usuario.Rol.IdRol = 0;
                            }
                            else
                            {
                                usuario.Rol.IdRol = Convert.ToInt32(row[13]);
                            }


                            result.Objects.Add(usuario); //El resultado del foreach se almacena en el metodo objects de la clase result en el modelo usuario
                        }
                        result.Correct = true; //Si el proceso se hizo correctamente, se manda a llamar el metodo correct de la clase result para indicarle al usuario que el proceso se hizo de manera correcta
                    }
                    else
                    {
                        //No hay registros
                        result.Correct = false; //Se indica que el proceso no se hizo correctamente
                        result.ErrorMessage = "No hay registros en la base de datos"; //Se manda el mensaje de error
                    }

                }
            }
            catch (Exception ex)
            {
                //Error
                result.Correct = false; //Se indica que el proceso no se hizo correctamente
                result.ErrorMessage = ex.Message; //Se manda el detalle del error como mensaje de error
                result.Ex = ex;
            }
            return result; //Retorna el resultado
        } //Cierre metodo GetAll

        //Metodo GetById
        public static ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.connection.ObtenerConexion()))
                {
                    string query = "UsuarioGetById";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        //Si hay registros 
                        DataRow row = dataTable.Rows[0]; //Toma la posicion 0 del arreglo para leer su informacion

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                        usuario.Nombre = row[1].ToString();
                        usuario.ApellidoPaterno = row[2].ToString();
                        usuario.ApellidoMaterno = row[3].ToString();
                        usuario.Celular = row[4].ToString();
                        usuario.UserName = row[5].ToString();
                        usuario.Email = row[6].ToString();
                        usuario.Password = row[7].ToString();
                        usuario.FechaNacimiento = row[8].ToString();
                        usuario.Sexo = row[9].ToString();
                        usuario.Telefono = row[10].ToString();
                        usuario.Estatus = Convert.ToBoolean(row[11].ToString());
                        usuario.CURP = row[12].ToString();

                        if (row[13].ToString() == "")
                        {
                            usuario.Rol.IdRol = 0;
                        }
                        else
                        {
                            usuario.Rol.IdRol = Convert.ToInt32(row[13]);
                        }

                        result.Object = usuario; //BOXING. En la caja Object se guarda el usuario
                        result.Correct = true;
                    }
                    else
                    {
                        //No hay registros
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros en la base de datos";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        } //Cierre metodo GetById




        //METODOS SIN STORED PROCEDURE
        //Metodo AddSqlClient
        public static ML.Result AddSqlClient(ML.UsuarioSqlClient usuario) //Se crea el modelo usuario mandando a llamar la capa ML y la clase Usuario. El ultimo "Usuario" es el nombre del modelo
        {
            ML.Result result = new ML.Result(); //Crea un nuevo modelo de result llamado result

            //Se abre una conexion con la BD usando try catch para disminuir errores
            try
            {
                using (SqlConnection context = new SqlConnection(DL.connection.ObtenerConexion())) //Para eliminar conexion cuando se termine de ejecutar el bloque de codigo try. Para indicar a que BD se va a conectar
                {
                    //Se crea una variable query de tipo string en donde se va a ingresar el query de la accion que se hace
                    //Los parametos (valores) se cambian poniendo un @ 
                    string query = "INSERT INTO Usuario VALUES (@nombre, @apellidoPaterno, @apellidoMaterno, @edad)";

                    //Se crea un nuevo SqlCommand para ejecutar el query
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query; //Se le indica que query se va a ejecutar
                    cmd.Connection = context; //Se indica a que connectionString va a acceder

                    //Sustitucion de valores de parametros. Se toma el valor en el query, se sustituye por el nombre del valor en el modelo
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@apellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@apellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@edad", usuario.Edad);

                    context.Open(); //Se abre la BD
                    //Variable para indicar cuantas filas en la BD fueron afectadas
                    int filasAfectadas = cmd.ExecuteNonQuery(); //ExecuteNonQuery ejecuta el Query

                    //If para indicar si el insert fue exitoso
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.ErrorMessage = "Error al insertar el usuario";
                    }
                } //Se cierra la BD y se elimina el bloque de codigo
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        } //Cierre metodo Add

        //Metodo UpdateSqlClient
        public static ML.Result UpdateSqlClient(ML.UsuarioSqlClient usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.connection.ObtenerConexion()))
                {
                    string query = "UPDATE Usuario SET Nombre = @nombre, ApellidoPaterno = @apellidoPaterno, ApellidoMaterno = @apellidoMaterno, Edad = @edad WHERE IdUsuario = @idUsuario";
                    //UPDATE Usuario SET @nombre = '', @apellidoPaterno = '', @apellidoMaterno = '', @edad = 0 WHERE @id = 0;

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@idUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@apellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@apellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@edad", usuario.Edad);

                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al actualizar el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        } //Cierre metodo Update

        //Metodo DeleteSqlClient
        public static ML.Result DeleteSqlClient(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.connection.ObtenerConexion()))
                {
                    string query = "DELETE FROM Usuario WHERE IdUsuario = @idUsuario";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        } //Cierre de metodo Delete

        //Metodo GetAllSqlClient
        public static ML.Result GetAllSqlClient()
        {
            ML.Result result = new ML.Result(); //Crea un nuevo modelo de result llamado result

            try
            {
                using (SqlConnection context = new SqlConnection(DL.connection.ObtenerConexion())) //Obtener conexion
                {
                    string query = "SELECT * FROM Usuario"; //Query que se va a usar
                    SqlCommand cmd = new SqlCommand(); //Se crea un nuevo cmd
                    cmd.CommandText = query; //Se le pasa el query
                    cmd.Connection = context; //Se le pasa la conexion

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //Se crea el puente entre el codigo y la BD indicando que query y conexion se van a usar
                    DataTable dataTable = new DataTable(); //Se crea la tabla en donde se va a vaciar la informacion 
                    adapter.Fill(dataTable); //Se le pasa la informacion del adapter a la tabla

                    if (dataTable.Rows.Count > 0) //Si el numero de filas en la tabla es mayor a 0
                    {
                        //Si hay registros 
                        result.Objects = new List<object>(); //El resultados de Objects se almacena en una nueva lista de objects

                        foreach (DataRow row in dataTable.Rows) //Foreach recorre el arreglo de columnas de principio a fin sin excepcion
                        {
                            ML.UsuarioSqlClient usuario = new ML.UsuarioSqlClient(); //Se crea un nuevo modelo de usuario 
                            //El tipo de dato row devuelve un dato row, asi que se debe parsear para que coincida con los tipos de datos asignados en la BD
                            usuario.IdUsuario = Convert.ToInt32(row[0].ToString()); //Se indica que la columna 0 es el id del usuario, se convierte a string y despues a int para que coinvita con los tipos de datos asignados
                            usuario.Nombre = row[1].ToString(); //Se indica que la columna 1 es el nombre del usuario y se convierte a string
                            usuario.ApellidoPaterno = row[2].ToString(); //Se indica que la columna 2 es el apellido paterno del usuario y se convierte a string
                            usuario.ApellidoMaterno = row[3].ToString(); //Se indica que la columna 3 es el apellido materno del usuario y se convierte a string
                            usuario.Edad = Convert.ToInt32(row[4].ToString()); //Se indica que la columna 4 tiene la edad del usuario, se convierte a string y despues a entero

                            result.Objects.Add(usuario); //El resultado del foreach se almacena en el metodo objects de la clase result en el modelo usuario
                        }
                        result.Correct = true; //Si el proceso se hizo correctamente, se manda a llamar el metodo correct de la clase result para indicarle al usuario que el proceso se hizo de manera correcta
                    }
                    else
                    {
                        //No hay registros
                        result.Correct = false; //Se indica que el proceso no se hizo correctamente
                        result.ErrorMessage = "No hay registros en la base de datos"; //Se manda el mensaje de error
                    }

                }
            }
            catch (Exception ex)
            {
                //Error
                result.Correct = false; //Se indica que el proceso no se hizo correctamente
                result.ErrorMessage = ex.Message; //Se manda el detalle del error como mensaje de error
                result.Ex = ex;
            }
            return result; //Retorna el resultado
        } //Cierre metodo GetAll

        //Metodo GetByIdSqlClient
        public static ML.Result GetByIdSqlClient(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.connection.ObtenerConexion()))
                {
                    string query = "SELECT * FROM Usuario WHERE IdUsuario = @idUsuario";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        //Si hay registros 
                        DataRow row = dataTable.Rows[0]; //Toma la posicion 0 del arreglo para leer su informacion

                        ML.UsuarioSqlClient usuario = new ML.UsuarioSqlClient();
                        usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                        usuario.Nombre = row[1].ToString();
                        usuario.ApellidoPaterno = row[2].ToString();
                        usuario.ApellidoMaterno = row[3].ToString();
                        usuario.Edad = Convert.ToInt32(row[4].ToString());

                        result.Object = usuario; //BOXING. En la caja Object se guarda el usuario
                        result.Correct = true;
                    }
                    else
                    {
                        //No hay registros
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros en la base de datos";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        } //Cierre metodo GetById

    }
}
