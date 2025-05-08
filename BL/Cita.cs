using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cita
    {
        public static ML.Result GetAll(int idVacante)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    //var select = (from cita in context.Citas
                    //              join candidatos in context.Candidatoes on cita.IdCandidato equals candidatos.IdCandidato into citaCandidato from a in citaCandidato.DefaultIfEmpty()
                    //              join candidato in context.Candidatoes on cita.IdCandidato equals candidato.IdCandidato 
                    //              join vacante in context.Vacantes on candidato.IdVacante equals vacante.IdVacante
                    //              join piso in context.Pisoes on cita.IdPiso equals piso.IdPiso
                    //              join estatusCita in context.EstatusCitas on cita.IdEstatusCita equals estatusCita.IdEstatusCita
                    //              where cita.Candidato.IdVacante == idVacante

                    //                  //join rightJoinCandidato in context.Candidatoes on cita.IdCandidato equals rightJoinCandidato.IdCandidato into joined
                    //                  //from j in joined.DefaultIfEmpty()
                    //              select cita).ToList();

                    var select = (from candidatos in context.Candidatoes
                                  join citas in context.Citas on candidatos.IdCandidato equals citas.IdCandidato into citaCandidato
                                  from a in citaCandidato.DefaultIfEmpty()
                                  join vacante in context.Vacantes on a.Candidato.IdVacante equals vacante.IdVacante into vacanteCandidato
                                  from vacantes in vacanteCandidato.DefaultIfEmpty()
                                  join piso in context.Pisoes on a.IdPiso equals piso.IdPiso into pisoCandidato
                                  from pisos in pisoCandidato.DefaultIfEmpty()
                                  join estatusCita in context.EstatusCitas on a.EstatusCita.IdEstatusCita equals estatusCita.IdEstatusCita into EstatusCita
                                  from estatus in EstatusCita.DefaultIfEmpty()
                                  where candidatos.IdVacante == idVacante
                                  select new
                                  {
                                      Candidatos = candidatos,
                                      Citas = a,
                                      Vacantes = vacantes,
                                      Pisos = pisos,
                                      Estatus = estatus
                                  }).ToList();


                    if (select.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objBD in select)
                        {
                            ML.Cita cita = new ML.Cita();
                            cita.Candidato = new ML.Candidato();
                            cita.Candidato.Vacante = new ML.Vacante();
                            cita.Piso = new ML.Piso();
                            cita.EstatusCita = new ML.EstatusCita();


                            if (objBD.Citas != null)
                            {
                                cita.IdCita = objBD.Citas.IdCita;
                                cita.FechaHora = objBD.Citas.FechaHora == null ? "" : Convert.ToString(objBD.Citas.FechaHora);

                                cita.EstatusCita.IdEstatusCita = objBD.Estatus.IdEstatusCita;
                                cita.EstatusCita.Nombre = objBD.Estatus.Nombre;

                                cita.Piso.IdPiso = (byte)(objBD.Pisos == null ? 0 : objBD.Pisos.IdPiso);
                                cita.Piso.Nombre = objBD.Pisos == null ? "" : objBD.Pisos.Nombre;

                                cita.Url = objBD.Citas.Url == null ? "" : objBD.Citas.Url;
                            }
                            else
                            {
                                cita.IdCita = 0;
                                cita.FechaHora = "";

                                cita.EstatusCita.IdEstatusCita = 0;
                                cita.EstatusCita.Nombre = "";

                                cita.Piso.IdPiso = 0;
                                cita.Piso.Nombre = "";

                                cita.Url = "";
                            }

                            cita.Candidato.IdCandidato = objBD.Candidatos.IdCandidato;
                            cita.Candidato.Nombre = objBD.Candidatos.Nombre;
                            cita.Candidato.ApellidoPaterno = objBD.Candidatos.ApellidoPaterno;
                            cita.Candidato.ApellidoMaterno = objBD.Candidatos.ApellidoMaterno;
                            cita.Candidato.Edad = objBD.Candidatos.Edad;
                            cita.Candidato.Correo = objBD.Candidatos.Correo;
                            cita.Candidato.Telefono = objBD.Candidatos.Telefono;
                            cita.Candidato.Foto = objBD.Candidatos.Foto;
                            if (cita.Candidato.Foto != null)
                            {
                                cita.Candidato.FotoBase64 = Convert.ToBase64String(cita.Candidato.Foto);
                            }
                            else
                            {
                                cita.Candidato.FotoBase64 = "";
                            }
                            cita.Candidato.Vacante.IdVacante = objBD.Candidatos.Vacante.IdVacante;
                            cita.Candidato.Vacante.Nombre = objBD.Candidatos.Vacante.Nombre;

                            result.Objects.Add(cita);
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

        public static ML.Result GetById(int idCandidato)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var select = (from candidatos in context.Candidatoes
                                  join citas in context.Citas on candidatos.IdCandidato equals citas.IdCandidato into citaCandidato
                                  from a in citaCandidato.DefaultIfEmpty()
                                  join vacante in context.Vacantes on a.Candidato.IdVacante equals vacante.IdVacante into vacanteCandidato
                                  from vacantes in vacanteCandidato.DefaultIfEmpty()
                                  join piso in context.Pisoes on a.IdPiso equals piso.IdPiso into pisoCandidato
                                  from pisos in pisoCandidato.DefaultIfEmpty()
                                  join estatusCita in context.EstatusCitas on a.EstatusCita.IdEstatusCita equals estatusCita.IdEstatusCita into EstatusCita
                                  from estatus in EstatusCita.DefaultIfEmpty()
                                  where candidatos.IdCandidato == idCandidato
                                  select new
                                  {
                                      Candidatos = candidatos,
                                      Citas = a,
                                      Vacantes = vacantes,
                                      Pisos = pisos,
                                      Estatus = estatus
                                  }).SingleOrDefault();

                    if (select != null)
                    {
                        result.Object = new object();
                        ML.Cita cita = new ML.Cita();
                        cita.Candidato = new ML.Candidato();
                        cita.Candidato.Vacante = new ML.Vacante();
                        cita.EstatusCita = new ML.EstatusCita();
                        cita.Piso = new ML.Piso();

                        if (select.Citas != null) //Cuando hay cita Citas viene lleno
                        {
                            cita.IdCita = select.Citas.IdCita;
                            cita.FechaHora = select.Citas.FechaHora == null ? "" : Convert.ToString(select.Citas.FechaHora);

                            cita.EstatusCita.IdEstatusCita = select.Estatus.IdEstatusCita;
                            cita.EstatusCita.Nombre = select.Estatus.Nombre;

                            cita.Piso.IdPiso = (byte)(select.Pisos == null ? 0 : select.Pisos.IdPiso);
                            cita.Piso.Nombre = select.Pisos == null ? "" : select.Pisos.Nombre;

                            cita.Url = select.Citas.Url == null ? "" : select.Citas.Url;
                        }
                        else //Si no tiene cita, Citas viene nulo
                        {
                            cita.IdCita = 0;
                            cita.FechaHora = "";

                            cita.EstatusCita.IdEstatusCita = 0;
                            cita.EstatusCita.Nombre = "";

                            cita.Piso.IdPiso = 0;
                            cita.Piso.Nombre = "";
                        }

                        cita.Candidato.IdCandidato = select.Candidatos.IdCandidato;
                        cita.Candidato.Nombre = select.Candidatos.Nombre;
                        cita.Candidato.ApellidoPaterno = select.Candidatos.ApellidoPaterno;
                        cita.Candidato.ApellidoMaterno = select.Candidatos.ApellidoMaterno;
                        cita.Candidato.Edad = select.Candidatos.Edad;
                        cita.Candidato.Correo = select.Candidatos.Correo;
                        cita.Candidato.Telefono = select.Candidatos.Telefono;
                        cita.Candidato.Foto = select.Candidatos.Foto;
                        if (cita.Candidato.Foto != null)
                        {
                            cita.Candidato.FotoBase64 = Convert.ToBase64String(cita.Candidato.Foto);
                        }
                        else
                        {
                            cita.Candidato.FotoBase64 = "";
                        }
                        cita.Candidato.Vacante.IdVacante = select.Candidatos.Vacante.IdVacante;
                        cita.Candidato.Vacante.Nombre = select.Candidatos.Vacante.Nombre;

                        result.Object = cita;
                        result.Correct = true;
                    } else
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

        public static ML.Result Add(ML.Cita cita)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    DL_EF.Cita citaBD = new DL_EF.Cita();

                    citaBD.FechaHora = Convert.ToDateTime(cita.FechaHora);
                    citaBD.IdPiso = Convert.ToByte(cita.Piso.IdPiso);
                    citaBD.IdCandidato = cita.Candidato.IdCandidato;
                    citaBD.IdEstatusCita = cita.EstatusCita.IdEstatusCita;
                    citaBD.Url = cita.Url;

                    context.Citas.Add(citaBD);

                    int filasAfectadas = context.SaveChanges();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al agregar una cita";
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

        public static ML.Result Update(ML.Cita cita)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var select = (from citaBD in context.Citas
                                  where citaBD.IdCita == cita.IdCita
                                  select citaBD).SingleOrDefault();

                    if (select != null)
                    {
                        select.FechaHora = Convert.ToDateTime(cita.FechaHora);
                        if (cita.Piso.IdPiso == null)
                        {
                            select.IdPiso = null;
                        } else
                        {
                            select.IdPiso = cita.Piso.IdPiso;
                        }
                        select.Candidato.IdCandidato = cita.Candidato.IdCandidato;
                        select.EstatusCita.IdEstatusCita = cita.EstatusCita.IdEstatusCita;
                        select.Url = cita.Url;

                        int filasAfectadas = context.SaveChanges();

                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        } else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al actualizar cita";
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

        public static ML.Result Delete(int idCita)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var select = (from cita in context.Citas
                                  where cita.IdCita == idCita
                                  select cita).SingleOrDefault();

                    if (select != null)
                    {
                        context.Citas.Remove(select);

                        int filasAfectadas = context.SaveChanges();

                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        } else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Error al eliminar la cita";
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
