using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var select = (from empresaBD in context.Empresas
                                  select new
                                  {
                                      IdEmpresa = empresaBD.IdEmpresa,
                                      Nombre = empresaBD.Nombre,
                                      Latitud = empresaBD.Latitud,
                                      Longitud = empresaBD.Longitud
                                  }).ToList();

                    if (select.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objBD in select)
                        {
                            ML.Empresa empresa = new ML.Empresa();
                            empresa.IdEmpresa = objBD.IdEmpresa;
                            empresa.Nombre = objBD.Nombre;
                            empresa.Latitud = objBD.Latitud;
                            empresa.Longitud = objBD.Longitud;

                            result.Objects.Add(empresa);
                        }
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }



        public static ML.Result GetById(int idEmpresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var select = (from empresa in context.Empresas
                                  where empresa.IdEmpresa == idEmpresa
                                  select new
                                  {
                                      IdEmpresa = empresa.IdEmpresa,
                                      Nombre = empresa.Nombre,
                                      Latitud = empresa.Latitud,
                                      Longitud = empresa.Longitud
                                  }).SingleOrDefault();

                    if (select != null)
                    {
                        result.Object = new object();
                        ML.Empresa empresa = new ML.Empresa();

                        empresa.IdEmpresa = select.IdEmpresa;
                        empresa.Nombre = select.Nombre;
                        empresa.Latitud = select.Latitud;
                        empresa.Longitud = select.Longitud;

                        result.Object = empresa;
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }



        public static ML.Result Delete(int idEmpresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var busqueda = (from empresa in context.Empresas
                                    where empresa.IdEmpresa == idEmpresa
                                    select empresa).SingleOrDefault();
                    
                    if (busqueda != null)
                    {
                        context.Empresas.Remove(busqueda);

                        int filasAfectadas = context.SaveChanges();
                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        } else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al eliminar el registro";
                        }
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }



        public static ML.Result Add(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    DL_EF.Empresa empresaBD = new DL_EF.Empresa();
                    //empresaBD.IdEmpresa = empresa.IdEmpresa;
                    empresaBD.Nombre = empresa.Nombre;
                    empresaBD.Latitud = empresa.Latitud;
                    empresaBD.Longitud = empresa.Longitud;

                    context.Empresas.Add(empresaBD);

                    int filasAfectadas = context.SaveChanges();
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al agregar el registro";
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }



        public static ML.Result Update(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var busqueda = (from empresaBD in context.Empresas
                                    where empresaBD.IdEmpresa == empresa.IdEmpresa
                                    select empresaBD).SingleOrDefault();

                    if (busqueda != null)
                    {
                        busqueda.Nombre = empresa.Nombre;
                        busqueda.Latitud = empresa.Latitud;
                        busqueda.Longitud = empresa.Longitud;

                        int filasAfectadas = context.SaveChanges();
                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        } else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al actualizar el registro";
                        }
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


    }
}
