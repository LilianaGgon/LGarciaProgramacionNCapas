using Microsoft.Owin.BuilderProperties;
using Microsoft.Owin.Security.Provider;
using ML;
using PL_MVC.UsuarioReference;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {

        //CONSUMIENDO REST
        //GET ALL
        [NonAction]
        public static ML.Result GetAllREST()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (var client = new HttpClient())
                {
                    string inicioEndpoint = ConfigurationManager.AppSettings["CrudUsuarioEndPoint"].ToString(); //Se obtiene el endpoint configurado en el web config
                    client.BaseAddress = new Uri(inicioEndpoint); //se asigna la direccion 

                    var respuestaApi = client.GetAsync("GetAll"); //Se completa el endpoint y la respuesta se guarda en una variable
                    respuestaApi.Wait(); //espera a que se termine de obtener toda la respuesta

                    var resultApi = respuestaApi.Result; //Se guarda el result en una variable

                    if (resultApi.IsSuccessStatusCode) //evalua si el codigo de respuesta es un 200
                    {
                        var leerResult = resultApi.Content.ReadAsAsync<ML.Result>(); //en una variable se guarda la deserealizacion del result 
                        leerResult.Wait(); //Espera a que se termine de leer todo el result 

                        result.Objects = new List<object>();
                        foreach (var obj in leerResult.Result.Objects)
                        {
                            ML.Usuario usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(obj.ToString()); //se deserealiza el json como modelos de usuario y los va a guardar en el modelo usuario

                            result.Objects.Add(usuario); //el modelo usuario se guarda en la lista de objetos del result
                        }
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
        }
        [HttpGet]
        public ActionResult GetAllApiRest()
        {
            ML.Result usuarios = GetAllREST();
            ML.Usuario usuario = new ML.Usuario();
            if (usuarios.Objects.Count > 0 && usuarios.Objects != null)
            {
                usuario.Usuarios = new List<object>();
                usuario.Usuarios = usuarios.Objects;
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }
            usuario.Rol = new ML.Rol();
            ML.Result resultDDLRol = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDLRol.Objects;

            return View(usuario);
        }


        [NonAction]
        public static ML.Result GetByIdREST(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (var client = new HttpClient())
                {
                    string inicioEndpoint = ConfigurationManager.AppSettings["CrudUsuarioEndPoint"].ToString();
                    client.BaseAddress = new Uri(inicioEndpoint);

                    var respuestaApi = client.GetAsync("GetById/" + idUsuario);
                    respuestaApi.Wait();

                    var resultApi = respuestaApi.Result;

                    if (resultApi.IsSuccessStatusCode)
                    {
                        var leerResult = resultApi.Content.ReadAsAsync<ML.Result>();
                        leerResult.Wait();

                        ML.Usuario usuario = new ML.Usuario();
                        usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(leerResult.Result.Object.ToString());
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
        }


        [NonAction]
        public static ML.Result AddREST(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (var client = new HttpClient())
                {
                    string inicioEndpoint = ConfigurationManager.AppSettings["CrudUsuarioEndPoint"].ToString();
                    client.BaseAddress = new Uri(inicioEndpoint);

                    var respuestaApi = client.PostAsJsonAsync<ML.Usuario>("Add", usuario);
                    respuestaApi.Wait();

                    var resultApi = respuestaApi.Result;

                    if (resultApi.IsSuccessStatusCode)
                    {
                        var leerResult = resultApi.Content.ReadAsAsync<ML.Result>();
                        leerResult.Wait();

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
        }

        [NonAction]
        public static ML.Result UpdateREST(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var client = new HttpClient())
                {
                    string inicioEndpoint = ConfigurationManager.AppSettings["CrudUsuarioEndPoint"].ToString();
                    client.BaseAddress = new Uri(inicioEndpoint);

                    //HTTP POST
                    var respuesta = client.PutAsJsonAsync<ML.Usuario>("Update/" + usuario.IdUsuario, usuario);
                    respuesta.Wait();

                    var resultRespuesta = respuesta.Result;
                    if (resultRespuesta.IsSuccessStatusCode)
                    {
                        var leerResult = resultRespuesta.Content.ReadAsAsync<ML.Result>();
                        leerResult.Wait();

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
        }


        [HttpGet]
        public ActionResult FormREST(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            if (IdUsuario == null)
            {
                usuario.Rol = new ML.Rol();

                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            }
            else
            {
                ML.Result resultREST = GetByIdREST(IdUsuario.Value);
                usuario = (ML.Usuario)resultREST.Object;

                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
            }

            ML.Result resultRol = BL.Rol.GetAll();
            usuario.Rol.Roles = resultRol.Objects;

            ML.Result resultEstado = BL.Estado.GetAll();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

            return View(usuario);
        }

        [HttpPost]
        public ActionResult FormApiRest(ML.Usuario usuario)
        {
            HttpPostedFileBase file = Request.Files["inptFileImgUsuario"];
            if (file.ContentLength <= 0)
            {
                usuario.Imagen = usuario.Imagen;
            }
            else
            {
                usuario.Imagen = ConvertirArrayBytes(file);
            }

            if (usuario.IdUsuario == 0)
            {
                ML.Result result = AddREST(usuario);

                if (result.Correct)
                {
                    ViewBag.MensajeBLUsuario = "Se realizó la actualización correctamente";
                    return PartialView("_ResultBLUsuario");
                }
                else
                {
                    ViewBag.MensajeBLUsuario = "Hubo un error en la inserción";
                    return PartialView("_ResultBLUsuario");
                }
            }
            else
            {
                ML.Result result = UpdateREST(usuario);

                if (result.Correct)
                {
                    ViewBag.MensajeBLUsuario = "Se realizó la actualización correctamente";
                    ViewBag.Result = true;

                    return PartialView("_ResultBLUsuario");
                }
                else
                {
                    ViewBag.MensajeBLUsuario = "ERROR en la actualización";
                    ViewBag.Result = false;

                    return PartialView("_ResultBLUsuario");
                }

            }
        }


        [NonAction]
        public static ML.Result DeleteREST(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (var client = new HttpClient())
                {
                    string inicioEndpoint = ConfigurationManager.AppSettings["CrudUsuarioEndPoint"].ToString();
                    client.BaseAddress = new Uri(inicioEndpoint);

                    var respuestaApi = client.DeleteAsync("Delete/" + idUsuario);
                    respuestaApi.Wait();

                    var resultApi = respuestaApi.Result;

                    if (resultApi.IsSuccessStatusCode)
                    {
                        var leerResult = resultApi.Content.ReadAsAsync<ML.Result>();
                        leerResult.Wait();

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
        }

        [HttpGet]
        public ActionResult DeleteApi(int idUsuario)
        {
            ML.Result result = DeleteREST(idUsuario);

            if (result.Correct)
            {
                ViewBag.Result = true;
                ViewBag.MensajeBLUsuario = "Se eliminó correctamente";
                return PartialView("_ResultBLUsuario");
            }
            else
            {
                ViewBag.Result = false;
                ViewBag.MensajeBLUsuario = "ERROR al eliminar";
                return PartialView("_ResultBLUsuario");
            }
        }





        //CONSUMIENDO SOAP
        //GET ALL
        [HttpGet]
        public ActionResult GetAllSOAP()
        {
            string action = "http://tempuri.org/ICrud/GetAll"; //Ruta en donde se coloca el nombre de la interfaz y el nombre del metodo 
            string url = "http://localhost:50971/Crud.svc"; // Cambia a la URL del servicio
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST"; // Cambia a POST ya que estás usando un servicio SOAP

            // Crear el sobre SOAP
            string soapEnvelope = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
           <soapenv:Header/>
           <soapenv:Body>
              <tem:GetAll>
                 <tem:usuario>
                    <ml:ApellidoMaterno></ml:ApellidoMaterno>
                    <ml:ApellidoPaterno></ml:ApellidoPaterno>
                    <ml:Nombre></ml:Nombre>
                    <ml:Rol>
                       <ml:IdRol>0</ml:IdRol>
                    </ml:Rol>
                 </tem:usuario>
              </tem:GetAll>
           </soapenv:Body>
        </soapenv:Envelope>";

            // Enviar la solicitud
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = System.Text.Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }

            // Obtener la respuesta
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();

                        // Deserializar el XML
                        ML.Result usuarios = GetAllSOAP(result);
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Usuarios = usuarios.Objects; // Captura el objeto completo

                        usuario.Rol = new ML.Rol();
                        ML.Result resultDDLRol = BL.Rol.GetAll();
                        usuario.Rol.Roles = resultDDLRol.Objects;

                        return View(usuario); // Asegúrate de que tu vista esté lista para recibir este objeto
                    }
                }
            }
            catch (WebException ex)
            {
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
            }

            return View(); // Devuelve la vista
        }

        private ML.Result GetAllSOAP(string xml)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            var xdoc = XDocument.Parse(xml);

            // Acceder a GetAllUsuarioResult
            var objects = xdoc.Descendants("{http://schemas.microsoft.com/2003/10/Serialization/Arrays}anyType");

            foreach (var elem in objects)
            {
                ML.Usuario usuario = new ML.Usuario();
                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                usuario.ApellidoPaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoPaterno")?.Value ?? string.Empty);
                usuario.ApellidoMaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty);
                usuario.CURP = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}CURP")?.Value ?? string.Empty);
                usuario.Celular = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Celular")?.Value ?? string.Empty);

                //Direccion
                usuario.Direccion.Calle = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion").Element("{http://schemas.datacontract.org/2004/07/ML}Calle")?.Value ?? string.Empty);

                usuario.Direccion.NumeroExterior = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion").Element("{http://schemas.datacontract.org/2004/07/ML}NumeroExterior")?.Value ?? string.Empty);

                usuario.Direccion.NumeroInterior = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion").Element("{http://schemas.datacontract.org/2004/07/ML}NumeroInterior")?.Value ?? string.Empty);

                usuario.Direccion.Colonia.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion").Element("{http://schemas.datacontract.org/2004/07/ML}Colonia").Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);

                usuario.Direccion.Colonia.Municipio.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion").Element("{http://schemas.datacontract.org/2004/07/ML}Colonia").Element("{http://schemas.datacontract.org/2004/07/ML}Municipio").Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);

                usuario.Direccion.Colonia.Municipio.Estado.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion").Element("{http://schemas.datacontract.org/2004/07/ML}Colonia").Element("{http://schemas.datacontract.org/2004/07/ML}Municipio").Element("{http://schemas.datacontract.org/2004/07/ML}Estado").Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);

                usuario.Email = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Email")?.Value ?? string.Empty);

                bool estatus;
                estatus = bool.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Estatus")?.Value, out estatus); //Convertir a bool
                usuario.Estatus = estatus;

                usuario.FechaNacimiento = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}FechaNacimiento")?.Value ?? string.Empty);

                // Manejo de IdUsuario null
                //byte[]
                int idUsuario;
                int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value, out idUsuario); //0
                usuario.IdUsuario = idUsuario;

                usuario.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty); //Al nombre del usuario le asigna el valor de la etiqueta nombre, evalua si es nulo con ?? y en caso de serlo lo pone vacio

                usuario.Password = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Password")?.Value ?? string.Empty);

                //Rol
                usuario.Rol.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Rol").Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);

                usuario.Sexo = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty);

                usuario.Telefono = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Telefono")?.Value ?? string.Empty);

                usuario.UserName = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}UserName")?.Value ?? string.Empty);

                result.Objects.Add(usuario);
            }

            return result; // Devuelve el objeto completo
        }

        //        //AGREGAR O ACTUALIZAR
        [HttpPost]
        public ActionResult FormSOAP(ML.Usuario usuario)
        {
            string url = "http://localhost:50971/Crud.svc"; // URL del servicio
            string soapEnvelope;
            string action; // Declarar la variable action aquí

            // Verificar si IdUsuario es null o 0 (o algún valor que determines como "nuevo")
            if (usuario.IdUsuario == 0)
            {
                // Crear el sobre SOAP para agregar un nuevo usuario
                action = "http://tempuri.org/ICrud/Add";
                soapEnvelope = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
           <soapenv:Header/>
           <soapenv:Body>
              <tem:Add>
                 <tem:usuario>
                    <ml:ApellidoMaterno>{usuario.ApellidoMaterno}</ml:ApellidoMaterno>
                    <ml:ApellidoPaterno>{usuario.ApellidoPaterno}</ml:ApellidoPaterno>
                    <ml:CURP>{usuario.CURP}</ml:CURP>
                    <ml:Celular>{usuario.Celular}</ml:Celular>
                    <ml:Direccion>
                       <ml:Calle>{usuario.Direccion.Calle}</ml:Calle>
                       <ml:Colonia>
                          <ml:IdColonia>{usuario.Direccion.Colonia.IdColonia}</ml:IdColonia>
                          <ml:Municipio>
                             <ml:Estado>
                                <ml:IdEstado>{usuario.Direccion.Colonia.Municipio.Estado.IdEstado}</ml:IdEstado>
                             </ml:Estado>
                             <ml:IdMunicipio>{usuario.Direccion.Colonia.Municipio.IdMunicipio}</ml:IdMunicipio>
                          </ml:Municipio>
                       </ml:Colonia>
                       <ml:NumeroExterior>{usuario.Direccion.NumeroExterior}</ml:NumeroExterior>
                       <ml:NumeroInterior>{usuario.Direccion.NumeroInterior}</ml:NumeroInterior>
                    </ml:Direccion>
                    <ml:Email>{usuario.Email}</ml:Email>
                    <ml:FechaNacimiento>{usuario.FechaNacimiento}</ml:FechaNacimiento>
                    <ml:Nombre>{usuario.Nombre}</ml:Nombre>
                    <ml:Password>{usuario.Password}</ml:Password>
                    <ml:Rol>
                       <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
                    </ml:Rol>
                    <ml:Sexo>{usuario.Sexo}</ml:Sexo>
                    <ml:Telefono>{usuario.Telefono}</ml:Telefono>
                    <ml:UserName>{usuario.UserName}</ml:UserName>
                 </tem:usuario>
              </tem:Add>
           </soapenv:Body>
        </soapenv:Envelope>";
            }
            else
            {
                // Crear el sobre SOAP para actualizar un usuario existente
                action = "http://tempuri.org/ICrud/Update";
                soapEnvelope = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
           <soapenv:Header/>
           <soapenv:Body>
              <tem:Update>
                 <!--Optional:-->
                 <tem:usuario>
                    <!--Optional:-->
                    <ml:ApellidoMaterno>{usuario.ApellidoMaterno}</ml:ApellidoMaterno>
                    <!--Optional:-->
                    <ml:ApellidoPaterno>{usuario.ApellidoPaterno}</ml:ApellidoPaterno>
                    <!--Optional:-->
                    <ml:CURP>{usuario.CURP}</ml:CURP>
                    <!--Optional:-->
                    <ml:Celular>{usuario.Celular}</ml:Celular>
                    <!--Optional:-->
                    <ml:Direccion>
                       <!--Optional:-->
                       <ml:Calle>{usuario.Direccion.Calle}</ml:Calle>
                       <!--Optional:-->
                       <ml:Colonia>
                          <!--Optional:-->
                          <ml:IdColonia>{usuario.Direccion.Colonia.IdColonia}</ml:IdColonia>
                          <!--Optional:-->
                          <ml:Municipio>
                             <!--Optional:-->
                             <ml:Estado>
                                <!--Optional:-->
                                <ml:IdEstado>{usuario.Direccion.Colonia.Municipio.Estado.IdEstado}</ml:IdEstado>
                             </ml:Estado>
                             <!--Optional:-->
                             <ml:IdMunicipio>{usuario.Direccion.Colonia.Municipio.IdMunicipio}</ml:IdMunicipio>
                          </ml:Municipio>
                       </ml:Colonia>
                       <!--Optional:-->
                       <ml:NumeroExterior>{usuario.Direccion.NumeroExterior}</ml:NumeroExterior>
                       <!--Optional:-->
                       <ml:NumeroInterior>{usuario.Direccion.NumeroInterior}</ml:NumeroInterior>
                    </ml:Direccion>
                    <!--Optional:-->
                    <ml:Email>{usuario.Email}</ml:Email>
                    <!--Optional:-->
                    <ml:FechaNacimiento>{usuario.FechaNacimiento}</ml:FechaNacimiento>
                    <!--Optional:-->
                    <ml:IdUsuario>{usuario.IdUsuario}</ml:IdUsuario>
                    <!--Optional:-->
                    <ml:Nombre>{usuario.Nombre}</ml:Nombre>
                    <!--Optional:-->
                    <ml:Password>{usuario.Password}</ml:Password>
                    <!--Optional:-->
                    <ml:Rol>
                       <!--Optional:-->
                       <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
                    </ml:Rol>
                    <!--Optional:-->
                    <ml:Sexo>{usuario.Sexo}</ml:Sexo>
                    <!--Optional:-->
                    <ml:Telefono>{usuario.Telefono}</ml:Telefono>
                    <!--Optional:-->
                    <ml:UserName>{usuario.UserName}</ml:UserName>
                 </tem:usuario>
              </tem:Update>
           </soapenv:Body>
        </soapenv:Envelope>";
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action); // Aquí ya existe la variable action
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";

            // Enviar la solicitud
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }

            // Obtener la respuesta
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine("Respuesta SOAP:");
                        Console.WriteLine(result);
                        // Aquí puedes manejar la respuesta según sea necesario
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
            }

            return RedirectToAction("GetAll"); // Redirigir a la lista de usuarios después de agregar o actualizar
        }

        //        //GET BY ID
        [HttpGet]
        public ActionResult FormSOAP(int? IdUsuario)
        {
            ML.Usuario resultUsuario = null;

            if (IdUsuario.HasValue)
            {
                // Obtener el usuario por ID
                string action = "http://tempuri.org/ICrud/GetById";
                string url = "http://localhost:50971/Crud.svc";

                // Crear el sobre SOAP para obtener un usuario por su ID
                string soapEnvelope = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
           <soapenv:Header/>
           <soapenv:Body>
              <tem:GetById>
                 <tem:idUsuario>{IdUsuario}</tem:idUsuario>
              </tem:GetById>
           </soapenv:Body>
        </soapenv:Envelope>";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("SOAPAction", action);
                request.ContentType = "text/xml;charset=\"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST";

                // Enviar la solicitud
                using (Stream stream = request.GetRequestStream())
                {
                    byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                    stream.Write(content, 0, content.Length);
                }

                // Obtener la respuesta
                try
                {
                    using (WebResponse response = request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string result = reader.ReadToEnd();
                            Console.WriteLine("Respuesta SOAP:");
                            Console.WriteLine(result);

                            // Deserializar el usuario
                            resultUsuario = GetByIdSOAP(result);
                        }
                    }
                }
                catch (WebException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
                }
            }

            ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(resultUsuario.Direccion.Colonia.Municipio.IdMunicipio);
            resultUsuario.Direccion.Colonia.Colonias = resultColonia.Objects;

            ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(resultUsuario.Direccion.Colonia.Municipio.Estado.IdEstado);
            resultUsuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;

            ML.Result resultRol = BL.Rol.GetAll();
            resultUsuario.Rol.Roles = resultRol.Objects;

            ML.Result resultEstado = BL.Estado.GetAll();
            resultUsuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

            return View(resultUsuario); // Devuelve la vista con el usuario si existe
        }

        private ML.Usuario GetByIdSOAP(string xml)
        {
            var xdoc = XDocument.Parse(xml);
            // Acceder a GetUsuarioByIdResult usando el namespace correcto
            var elem = xdoc.Descendants().FirstOrDefault(e =>
                e.Name.LocalName == "Object" &&
                e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");

            if (elem != null)
            {
                ML.Usuario usuario = new ML.Usuario();
                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                usuario.ApellidoPaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoPaterno")?.Value ?? string.Empty);
                usuario.ApellidoMaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty);
                usuario.CURP = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}CURP")?.Value ?? string.Empty);
                usuario.Celular = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Celular")?.Value ?? string.Empty);
                //Direccion
                usuario.Direccion.Calle = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion").Element("{http://schemas.datacontract.org/2004/07/ML}Calle")?.Value ?? string.Empty);
                usuario.Direccion.NumeroExterior = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion").Element("{http://schemas.datacontract.org/2004/07/ML}NumeroExterior")?.Value ?? string.Empty);
                usuario.Direccion.NumeroInterior = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion").Element("{http://schemas.datacontract.org/2004/07/ML}NumeroInterior")?.Value ?? string.Empty);
                usuario.Direccion.Colonia.IdColonia = int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion").Element("{http://schemas.datacontract.org/2004/07/ML}Colonia").Element("{http://schemas.datacontract.org/2004/07/ML}IdColonia")?.Value, out int idColonia) ? idColonia : 0;
                usuario.Direccion.Colonia.Municipio.IdMunicipio = int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion").Element("{http://schemas.datacontract.org/2004/07/ML}Colonia").Element("{http://schemas.datacontract.org/2004/07/ML}Municipio").Element("{http://schemas.datacontract.org/2004/07/ML}IdMunicipio")?.Value, out int idMunicipio) ? idMunicipio : 0;
                usuario.Direccion.Colonia.Municipio.Estado.IdEstado = int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion").Element("{http://schemas.datacontract.org/2004/07/ML}Colonia").Element("{http://schemas.datacontract.org/2004/07/ML}Municipio").Element("{http://schemas.datacontract.org/2004/07/ML}Estado").Element("{http://schemas.datacontract.org/2004/07/ML}IdEstado")?.Value, out int idEstado) ? idEstado : 0;

                usuario.Email = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Email")?.Value ?? string.Empty);

                bool estatus;
                estatus = bool.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Estatus")?.Value, out estatus); //Convertir a bool
                usuario.Estatus = estatus;

                usuario.FechaNacimiento = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}FechaNacimiento")?.Value ?? string.Empty);

                usuario.IdUsuario = int.TryParse(elem.Element(XName.Get("IdUsuario", "http://schemas.datacontract.org/2004/07/WCFJAT"))?.Value, out int idUsuario) ? idUsuario : 0;
                usuario.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty); //Al nombre del usuario le asigna el valor de la etiqueta nombre, evalua si es nulo con ?? y en caso de serlo lo pone vacio
                usuario.Password = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Password")?.Value ?? string.Empty);
                //Rol
                usuario.Rol.IdRol = int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Rol").Element("{http://schemas.datacontract.org/2004/07/ML}IdRol")?.Value, out int idRol) ? idRol : 0;
                usuario.Sexo = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty);

                usuario.Telefono = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Telefono")?.Value ?? string.Empty);

                usuario.UserName = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}UserName")?.Value ?? string.Empty);

                return usuario;
            }

            return null; // O lanzar una excepción si no se encontró el usuario
        }


        //        [HttpGet]
        public ActionResult DeleteSOAP(int? IdUsuario)
        {
            string action = "http://tempuri.org/ICrud/Delete"; //Ruta en donde se coloca el nombre de la interfaz y el nombre del metodo 
            string url = "http://localhost:50971/Crud.svc"; // Cambia a la URL del servicio
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST"; // Cambia a POST ya que estás usando un servicio SOAP

            // Crear el sobre SOAP
            string soapEnvelope = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
           <soapenv:Header/>
           <soapenv:Body>
              <tem:Delete>
                 <!--Optional:-->
                 <tem:idUsuario>{IdUsuario}</tem:idUsuario>
              </tem:Delete>
           </soapenv:Body>
        </soapenv:Envelope>";

            // Enviar la solicitud
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = System.Text.Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }

            // Obtener la respuesta
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();

                        // Deserializar el XML
                        var usuario = DeleteSOAP(result);

                        return RedirectToAction("GetAll"); // Asegúrate de que tu vista esté lista para recibir este objeto
                    }
                }
            }
            catch (WebException ex)
            {
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
            }

            return View(); // Devuelve la vista
        }


        private ML.Result DeleteSOAP(string xml)
        {
            ML.Result result = new ML.Result();

            var xdoc = XDocument.Parse(xml);

            var elem = xdoc.Descendants().FirstOrDefault(e =>
                e.Name.LocalName == "DeleteResult" &&
                e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");

            bool correct;
            correct = bool.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/SL_WCF}Correct")?.Value, out correct);
            result.Correct = correct;

            result.ErrorMessage = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/SL_WCF}ErrorMessage")?.Value ?? string.Empty);

            return result; // Devuelve el objeto completo
        }



        //        //ENTITY FRAMEWORK
        // GET ALL
        [HttpGet] //Decorador. Indica que la pagina web va a recibir
        public ActionResult GetAll() //El nombre del metodo es GetAll y va a devolver una vista
        {
            ML.Usuario usuario = new ML.Usuario(); //Se crea un modelo nuevo de usuario
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;

            //Entity Framework
            ML.Result result = BL.Usuario.GetAllEF(usuario); //Se agrega al modelo result el return del GetAllEF

            //Web Services SOAP
            //UsuarioReference.CrudClient objeto = new UsuarioReference.CrudClient();
            //var resultSOAP = objeto.GetAll(usuario);

            ////SOAP
            //if (resultSOAP.Correct) 
            //{
            //    //Si obtuvo toda la informacion usa la propiedad objects del result
            //    usuario.Usuarios = resultSOAP.Objects.ToList(); //SOAP
            //}
            //else
            //{
            //    //No se obtuvieron registros
            //    //Imprime un mensaje de error 
            //    usuario.Usuarios = new List<object>(); //Se crea una nueva lista vacia de Usuarios 
            //}

            //EF
            if (result.Correct)
            {
                //Si obtuvo toda la informacion usa la propiedad objects del result
                usuario.Usuarios = result.Objects.ToList(); //Al modelo usuario en su propiedad usuarios (lista de objetos) se agrega la lista del objetos del result  EF
            }
            else
            {
                //No se obtuvieron registros
                //Imprime un mensaje de error 
                usuario.Usuarios = new List<object>(); //Se crea una nueva lista vacia de Usuarios 
            }

            ML.Result resultDDLRol = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDLRol.Objects;

            return View(usuario); //Devuelve las vista. La vista solo puede retornar un solo tipo de dato. En este caso devuelve 
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            //Ternarios
            usuario.Nombre = usuario.Nombre == null ? "" : usuario.Nombre; //A usuario.Nombre le asigna el resultado del ternario. Si usuario.Nombre es nulo, se asigna un string vacío, si no es nulo se asigna el valor que ya trae. El ? indica cuando se acaba la condición. Los ternarios cumplen la misma función que un if pero disminuyen código
            usuario.ApellidoPaterno = usuario.ApellidoPaterno == null ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = usuario.ApellidoMaterno == null ? "" : usuario.ApellidoMaterno;
            usuario.Rol.IdRol = usuario.Rol.IdRol == 0 ? 0 : usuario.Rol.IdRol; //Si el valor de idRol es 0, se mantiene el 0, sino se mantiene el valor que ya trae

            ML.Result result = BL.Usuario.GetAllEF(usuario);
            usuario.Usuarios = result.Objects;

            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects;

            return View(usuario);
        }


        // AGREGAR / ACTUALIZAR
        [HttpGet] //Decorador de metodo Form para obtener informacion
        public ActionResult Form(int? IdUsuario) //El IdUsuario puede ser nulo o no
        {
            ML.Usuario usuario = new ML.Usuario(); //Nuevo modelo de usuario

            if (IdUsuario == null) //Se evalua si es nulo o no para saber que tipo de formulario se necesita (agregar o actualizar)
            {
                //ADD
                //Se manda un formulario vacio 
                usuario.Rol = new ML.Rol(); //Se abre la puerta para poder entar al modelo desde el modelo usuario

                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            }
            else //El IdUsuario no es nulo
            {
                //ACTUALIZAR
                //Se manda el formulario lleno con la informacion del id solicitado
                ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value); //Al modelo result en la propiedad object se le asigna el valor del GBI con el parametro de IdUsuario porque solo va a tener un registro //EF
                usuario = (ML.Usuario)result.Object; //UNBOXING. Al modelo usuario se le asigna el usuario que se va a sacar de la caja del result en la propiedad object. //EF
                usuario.Sexo = usuario.Sexo.Trim();
                //UsuarioReference.CrudClient objeto = new UsuarioReference.CrudClient();
                //var resultSOAP = objeto.GetById(IdUsuario.Value);
                //usuario = (ML.Usuario)resultSOAP.Object;

                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
            }

            ML.Result resultRol = BL.Rol.GetAll(); //Al metodo result de roles se le asigna el result del GetAll de roles
            usuario.Rol.Roles = resultRol.Objects; //Entramos al modelo usuario, despues al modelo rol para agarrar su propiedad de lista de roles en donde se guardaron los objetos rol despues de haber ejecutado el GetAll
                                                   //usuario.Rol = new ML.Rol(); //Si abrimos la puerta de nuevo, el modelo se reescribe y perdemos la informacion

            ML.Result resultEstado = BL.Estado.GetAll();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;


            return View(usuario);
        }


        // ACTUALIZAR / AGREGAR
        [HttpPost] //Decorador de metodo Form para agregar informacion
        public ActionResult Form(ML.Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                //regresa informacion dio
                //llenar ddl
                ML.Result resultRol = BL.Rol.GetAll();
                usuario.Rol.Roles = resultRol.Objects;

                ML.Result resultEstado = BL.Estado.GetAll();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;

                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                return View(usuario);
            }

            HttpPostedFileBase file = Request.Files["inptFileImgUsuario"];
            if (file.ContentLength <= 0)
            {
                usuario.Imagen = usuario.Imagen;
            }
            else
            {
                usuario.Imagen = ConvertirArrayBytes(file);
            }

            if (usuario.IdUsuario == 0)
            {
                //Si no encuentra un registro con ese ID, crea uno nuevo 

                //Entity Framework
                ML.Result result = BL.Usuario.AddEF(usuario); //Ejecuta el metodo de agregar de EF

                //Web Services SOAP
                //UsuarioReference.CrudClient objeto = new UsuarioReference.CrudClient();
                //var resultSOAP = objeto.Add(usuario);

                if (result.Correct)
                {
                    ViewBag.MensajeBLUsuario = "Se realizó la inserción correctamente";
                    ViewBag.Result = true;
                }
                else
                {
                    ViewBag.MensajeBLUsuario = "Error al agregar el registro";
                    ViewBag.Result = false;
                }

                return PartialView("_ResultBLUsuario");
            }
            else
            {
                //Si el registro existe, actualiza los datos
                ML.Result result = BL.Usuario.UpdateEF(usuario); //Ejecuta el metodo de actualizar de EF 

                //Web Services SOAP 
                //UsuarioReference.CrudClient objeto = new UsuarioReference.CrudClient();
                //var resultSOAP = objeto.Update(usuario);
                //usuario = (ML.Usuario)resultSOAP.Object;

                if (result.Correct)
                {
                    ViewBag.MensajeBLUsuario = "Se realizó la actualización correctamente";
                    ViewBag.Result = true;
                }
                else
                {
                    ViewBag.MensajeBLUsuario = "Error al actualizar el registro";
                    ViewBag.Result = false;
                }

                return PartialView("_ResultBLUsuario");
            }
        }

        // DELETE
        [HttpGet]
        public ActionResult Delete(int? idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            //Entity Framework
            ML.Result result = BL.Usuario.DeleteEF(idUsuario.Value);

            //Web Services SOAP
            //UsuarioReference.CrudClient objeto = new UsuarioReference.CrudClient();
            //var resultSOAP = objeto.Delete(idUsuario.Value);

            if (result.Correct)
            {
                ViewBag.MensajeBLUsuario = "Se eliminó el registro correctamente";
                ViewBag.Result = true;
            }
            else
            {
                ViewBag.MensajeBLUsuario = "Error al eliminar el registro";
                ViewBag.Result = false;
            }

            return PartialView("_ResultBLUsuario");
        }



        //CAMBIAR ESTATUS
        [HttpPost]
        public JsonResult CambiarEstatus(int idUsuario, bool estatus)
        {
            ML.Result JsonResult = BL.Usuario.CambiarEstatus(idUsuario, estatus); //El modelo result se llama JsonResult y recibe el result del metodo CambiarEstatus del BL
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        //DDL ESTADO
        [HttpGet]
        public JsonResult MunicipioGetByIdEstado(int idEstado)
        {
            ML.Result JsonResult = BL.Municipio.GetByIdEstado(idEstado);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ColoniaGetByIdMunicipio(int idMunicipio)
        {
            ML.Result JsonResult = BL.Colonia.GetByIdMunicipio(idMunicipio);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        public byte[] ConvertirArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);

            return data;
        }

        public ActionResult CargaMasivaExcel()
        {
            if (Session["RutaExcel"] == null)
            {

                HttpPostedFileBase excelUsuario = Request.Files["ExcelUsuario"]; //Cachamos el excel del usuario

                string extension = ConfigurationManager.AppSettings["ExtensionExcel"].ToString();
                string extensionValida = extension; //Declaramos la extension permitida

                if (excelUsuario.ContentLength > 0) //Si el contenido del usuario es mayor a 0
                {
                    //El usuario si adjuntó un archivo
                    string extensionArchivoUsuario = Path.GetExtension(excelUsuario.FileName); //Obtenemos la extension del archivo que nos adjunto el usuario

                    if (extensionArchivoUsuario == extensionValida) //Si la extension del archivo adjunto es valida
                    {
                        string ruta = Server.MapPath("~/CargaMasiva/") + Path.GetFileNameWithoutExtension(excelUsuario.FileName) + "-" + DateTime.Now.ToString("ddMMyyyyHHmmssfff") + ".xlsx"; //Se crea una copia del archivo excel. Se le asigna la ruta relativa de donde se va a guardar, se obtiene el nombre del archivo sin extension, se le concatena un - y la fecha y hora para hacerlo unico, despues se le adjunta la extension .xlsx

                        if (!System.IO.File.Exists(ruta)) //Si el archivo no existe en la ruta especificada se guarda y se crea uan cadena de conexion para el archivo con el proveedor ole db
                        {
                            excelUsuario.SaveAs(ruta); //Se guarda el archivo en en la ruta y con el nombre especificado

                            string cadenaConexion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + ruta; //Creacion de la cadena de conexion
                            ML.Result resultExcel = BL.Usuario.LeerExcel(cadenaConexion);

                            if (resultExcel.Objects.Count > 0)
                            {
                                ML.ResultExcel resultValidacion = BL.Usuario.ValidarExcel(resultExcel.Objects);

                                if (resultValidacion.Errores.Count > 0)
                                {
                                    ViewBag.ErroresExcel = resultValidacion.Errores;
                                    return PartialView("_ErroresExcel");
                                }
                                else
                                {
                                    Session["RutaExcel"] = ruta;
                                }
                            }
                        }
                        else
                        {
                            //Vuelve a cargar el archivo porque ya existe
                            ViewBag.MensajeError = "El archivo ya existe, vuelve a cargar uno";
                            ViewBag.ErroresExcel = new List<object>();
                            return PartialView("_ErroresExcel");
                        }
                    }
                    else
                    {
                        //El archivo no es un excel
                        ViewBag.MensajeError = "El archivo adjunto no es un Excel";
                        ViewBag.ErroresExcel = new List<object>();
                        return PartialView("_ErroresExcel");
                    }
                }
                else
                {
                    //El usuario no adjuntó un archivo
                    ViewBag.MensajeError = "No adjuntaste un archivo";
                    ViewBag.ErroresExcel = new List<object>();
                    return PartialView("_ErroresExcel");
                }

            }
            else
            {
                string cadenaConexion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + Session["RutaExcel"].ToString();

                ML.Result resultLeer = BL.Usuario.LeerExcel(cadenaConexion);

                if (resultLeer.Objects.Count > 0)
                {
                    int contador = 1;

                    foreach (ML.Usuario usuario in resultLeer.Objects)
                    {
                        //contador++;
                        //if (ModelState.IsValid)
                        //{
                        ML.Result resultInsertar = BL.Usuario.AddEF(usuario);
                        if (!resultInsertar.Correct)
                        {
                            //Mostrar el error que salió
                            ViewBag.MensajeError = "Hubo un error en la inserción del usuario " + contador;
                            ViewBag.ErroresExcel = new[] { resultInsertar };
                        }
                        //}
                        //else
                        //{
                        //    ViewBag.ErroresExcel = "El usuario " + contador + " tiene uno o más campos incorrectos";
                        //}
                    }
                    Session["RutaExcel"] = null;
                    ViewBag.MensajeCorrecto = "La inserción se hizo correctamente";
                    ViewBag.ErroresExcel = new List<object>();
                    return PartialView("_ErroresExcel");
                }
            }
            return RedirectToAction("GetAll");
        }
    }


}