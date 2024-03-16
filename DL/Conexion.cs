using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Conexion
    {
        public static string Get()
        {
            string conexion = "Data Source=MEZACARRILLOGUA\\LUPIZ;Initial Catalog=evaluacion_gmeza;Integrated Security=True;";

            return conexion;
        }
    }
}
