using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace SWL.Controllers
{
    public class TicketsController : ApiController
    {
        [HttpPost]
        [Route("api/tickets/procesar")]
        public IHttpActionResult ProcesarTicket()
        {
            ML.Tickets ticket = new ML.Tickets();
            string rutaPendientes = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["rutaPendientes"]);
            string rutaProcesados = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["rutaProcesados"]);
            string[] archivos = Directory.GetFiles(rutaPendientes, "*.fct");
            string archivoSeleccionado = archivos.OrderBy(f => new FileInfo(f).CreationTime).FirstOrDefault();

            if (archivoSeleccionado != null)
            {
                string nombreArchivo = Path.GetFileName(archivoSeleccionado);

                using (StreamReader reader = new StreamReader(archivoSeleccionado))
                {
                    string ln = reader.ReadLine();
                    if (ln != null)
                    {
                        string lnLimpia = ln.Replace(" ", string.Empty)
                                        .Replace("-", string.Empty)
                                        .Replace(">", string.Empty);
                        string[] campos = lnLimpia.Split('|');

                        ticket = new ML.Tickets
                        {
                            Id_Tienda = campos[0],
                            Id_Registradora = campos[1],
                            FechaHora = DateTime.ParseExact(campos[2] + campos[3], "yyyyMMddHHmmss", CultureInfo.InvariantCulture),
                            Ticket = int.Parse(campos[4]),
                            Impuesto = decimal.Parse(campos[5]),
                            Total = decimal.Parse(campos[6]),
                        };
                    }
                    else
                    {
                        Console.WriteLine("El documento esta vacío");
                    }

                }
                if (ticket != null)
                {
                    ML.Result result = BL.Tickets.Add(ticket);
                    //Si leo el archivo nice y se agrega -> procesados 
                    if (result.Correct)
                    {
                        string nuevaRuta = Path.Combine(rutaProcesados, nombreArchivo);
                        File.Move(archivoSeleccionado, nuevaRuta);
                        return Ok($"{nombreArchivo} insertado y procesado");
                    }
                    else
                    {
                        string nuevaExtension = Path.ChangeExtension(archivoSeleccionado, ".fct_error");
                        string nuevaRutaError = Path.Combine(rutaProcesados, Path.GetFileName(nuevaExtension));
                        File.Move(archivoSeleccionado, nuevaRutaError);
                        return Ok($"{Path.GetFileName(nuevaExtension)} no fue insertado, pero sí procesado");
                    }
                }
                //Si lo leo mal y no se registra en ticket, el nombre del archivo se pasa y se cambia la extension a *.fct_error a procesados
                else
                {
                    string nuevaExtension = Path.ChangeExtension(archivoSeleccionado, ".fct_error");
                    string nuevaRutaError = Path.Combine(rutaProcesados, Path.GetFileName(nuevaExtension));
                    File.Move(archivoSeleccionado, nuevaRutaError);
                    return Ok($"{Path.GetFileName(nuevaExtension)} procesado con Error, no se inserto");
                }
            }
            else
            {
                return Ok($"No se encontraron tickets en esta carpeta");
            }
        }
    }
}