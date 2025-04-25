using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Candidato
    {
        //Get All
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var query = context.CandidatoGetAll().ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var candidatoBD in query)
                        {
                            ML.Candidato candidato = new ML.Candidato();
                            candidato.Universidad = new ML.Universidad();
                            candidato.Carrera = new ML.Carrera();
                            candidato.BolsaTrabajo = new ML.BolsaTrabajo();
                            candidato.Vacante = new ML.Vacante();

                            candidato.IdCandidato = candidatoBD.IdCandidato;
                            candidato.Nombre = candidatoBD.Nombre;
                            candidato.ApellidoPaterno = candidatoBD.ApellidoPaterno;
                            candidato.ApellidoMaterno = candidatoBD.ApellidoMaterno;
                            candidato.Edad = candidatoBD.Edad;
                            candidato.Correo = candidatoBD.Correo;
                            candidato.Telefono = candidatoBD.Telefono;
                            candidato.Direccion = candidatoBD.Direccion;
                            candidato.Foto = candidatoBD.Foto;
                            candidato.Curriculum = candidatoBD.Curriculum;
                            candidato.Universidad.IdUniversidad = candidatoBD.IdUniversidad.Value;
                            candidato.Universidad.Nombre = candidatoBD.NombreUniversidad;
                            candidato.Carrera.IdCarrera = candidatoBD.IdCarrera.Value;
                            candidato.Carrera.Nombre = candidatoBD.NombreCarrera;
                            candidato.BolsaTrabajo.IdBolsaTrabajo = candidatoBD.IdBolsaTrabajo.Value;
                            candidato.BolsaTrabajo.Nombre = candidatoBD.NombreBolsaTrabajo;

                            result.Objects.Add(candidato);
                        }
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
        } //Fin Get All

        //GetAllBusqueda
        public static ML.Result GetAllBusqueda(int idVacante)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var query = context.CandidatoBusquedaAbierta(idVacante).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var candidatoBD in query)
                        {
                            ML.Candidato candidato = new ML.Candidato();
                            candidato.Universidad = new ML.Universidad();
                            candidato.Carrera = new ML.Carrera();
                            candidato.BolsaTrabajo = new ML.BolsaTrabajo();
                            candidato.Vacante = new ML.Vacante();

                            candidato.IdCandidato = candidatoBD.IdCandidato;
                            candidato.Nombre = candidatoBD.NombreCandidato;
                            candidato.ApellidoPaterno = candidatoBD.ApellidoPaterno;
                            candidato.ApellidoMaterno = candidatoBD.ApellidoMaterno;
                            candidato.Edad = candidatoBD.Edad;
                            candidato.Correo = candidatoBD.Correo;
                            candidato.Telefono = candidatoBD.Telefono;
                            candidato.Direccion = candidatoBD.Direccion;
                            candidato.Foto = candidatoBD.Foto;
                            candidato.Curriculum = candidatoBD.Curriculum;
                            candidato.Universidad.IdUniversidad = candidatoBD.IdUniversidad.Value;
                            candidato.Universidad.Nombre = candidatoBD.NombreUniversidad;
                            candidato.Carrera.IdCarrera = candidatoBD.IdCarrera.Value;
                            candidato.Carrera.Nombre = candidatoBD.NombreCarrera;
                            candidato.BolsaTrabajo.IdBolsaTrabajo = candidatoBD.IdBolsaTrabajo.Value;
                            candidato.BolsaTrabajo.Nombre = candidatoBD.NombreBolsaTrabajo;

                            result.Objects.Add(candidato);
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

        //Delete
        public static ML.Result Delete(int idCandidato)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var filasAfectadas = context.CandidatoDelete(idCandidato);

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar el candidato";
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
        } // Fin Delete

        //Get By Id
        public static ML.Result GetById(int idCandidato)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var query = context.CandidatoGetById(idCandidato).SingleOrDefault();

                    if (query != null)
                    {
                        ML.Candidato candidato = new ML.Candidato();
                        candidato.Universidad = new ML.Universidad();
                        candidato.BolsaTrabajo = new ML.BolsaTrabajo();
                        candidato.Carrera = new ML.Carrera();
                        candidato.Vacante = new ML.Vacante();

                        candidato.IdCandidato = query.IdCandidato;
                        candidato.Nombre = query.Nombre;
                        candidato.ApellidoPaterno = query.ApellidoPaterno;
                        candidato.ApellidoMaterno = query.ApellidoMaterno;
                        candidato.Edad = query.Edad;
                        candidato.Correo = query.Correo;
                        candidato.Direccion = query.Direccion;
                        candidato.Telefono = query.Telefono;
                        if (candidato.Foto != null)
                        {
                            candidato.FotoBase64 = Convert.ToBase64String(query.Foto);
                        }
                        else
                        {
                            candidato.FotoBase64 = "";

                        }
                        candidato.Foto = query.Foto;
                        if (candidato.Curriculum != null)
                        {
                            candidato.CurriculumBase64 = Convert.ToBase64String(query.Curriculum);
                        }
                        else
                        {
                            candidato.CurriculumBase64 = "";
                        }
                        candidato.Curriculum = query.Curriculum;
                        candidato.Universidad.IdUniversidad = query.IdUniversidad.Value;
                        candidato.Universidad.Nombre = query.NombreUniversidad;
                        candidato.BolsaTrabajo.IdBolsaTrabajo = query.IdBolsaTrabajo.Value;
                        candidato.BolsaTrabajo.Nombre = query.NombreBolsaTrabajo;
                        candidato.Carrera.IdCarrera = query.IdCarrera.Value;
                        candidato.Carrera.Nombre = query.NombreCarrera;
                        candidato.Vacante.IdVacante = query.IdVacante.Value;
                        candidato.Vacante.Nombre = query.NombreVacante;

                        result.Object = candidato;
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
        } // Fin Get By Id

        // Add
        public static ML.Result Add(ML.Candidato candidato)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var filasAfectadas = context.CandidatoAdd(candidato.Nombre, candidato.ApellidoPaterno, candidato.ApellidoMaterno, candidato.Edad, candidato.Correo, candidato.Telefono, candidato.Direccion, candidato.Foto, candidato.Curriculum, candidato.Universidad.IdUniversidad, candidato.Carrera.IdCarrera, candidato.BolsaTrabajo.IdBolsaTrabajo, candidato.Vacante.IdVacante);

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al agregar el candidato";
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
        } // Fin Add


        // Update
        public static ML.Result Update(ML.Candidato candidato)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var filasAfectadas = context.CandidatoUpdate(candidato.IdCandidato, candidato.Nombre, candidato.ApellidoPaterno, candidato.ApellidoMaterno, candidato.Edad, candidato.Correo, candidato.Telefono, candidato.Direccion, candidato.Foto, candidato.Curriculum, candidato.Universidad.IdUniversidad, candidato.Carrera.IdCarrera, candidato.BolsaTrabajo.IdBolsaTrabajo, candidato.Vacante.IdVacante);

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al actualizar el candidato";
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

    }
}
