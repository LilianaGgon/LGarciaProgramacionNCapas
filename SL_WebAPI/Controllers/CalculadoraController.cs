using SL_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    [RoutePrefix("api")] //Ruta ya definida
    public class CalculadoraController : ApiController
    {
        [HttpPost]
        [Route("Calculadora/Suma/{numero1}/{numero2}")] //Ruta personalizada. Se envian los parametros por la url. Recomendada para enviar los url
        public IHttpActionResult Suma(int numero1, int numero2)
        {
            int suma = numero1 + numero2;

            return Content(HttpStatusCode.OK, suma);
        }

        [HttpPost]
        [Route("Calculadora/Resta")] //Ruta personalizada. Se envia un parametro por url y el otro desde el body
        public IHttpActionResult Resta(int numero3, [FromBody]int numero4)
        {
            int resta = numero3 - numero4;

            return Content(HttpStatusCode.OK, resta);
        }

        [HttpPost]
        [Route("Calculadora/Multiplicacion")] //Ruta personalizada. Se envian ambos parametros por url y ambos aparecen en el endpoint
        public IHttpActionResult Multiplicacion(int numero5, int numero6)
        {
            int multiplicacion = numero5 * numero6;

            return Content(HttpStatusCode.OK, multiplicacion);
        }

        [HttpPost]
        [Route("Calculadora/Division")] //Ruta personalizada. Enviar datos mediante el body como un Json
        public IHttpActionResult Division([FromBody] Calculadora calculadora)
        {
            int division = calculadora.Numero1 / calculadora.Numero2;

            return Content(HttpStatusCode.OK, division);
        }
    }
}
