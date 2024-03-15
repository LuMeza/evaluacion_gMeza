using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Tickets
    {
        public static ML.Result Add(ML.Tickets ticket)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection con = new SqlConnection(DL.Conexion.Get()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("AddTicket", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("Id_Tienda", ticket.Id_Tienda);
                    cmd.Parameters.AddWithValue("Id_Registradora", ticket.Id_Registradora);
                    cmd.Parameters.AddWithValue("FechaHora", ticket.FechaHora);
                    cmd.Parameters.AddWithValue("Ticket", ticket.Ticket);
                    cmd.Parameters.AddWithValue("Impuesto", ticket.Impuesto);
                    cmd.Parameters.AddWithValue("Total", ticket.Total);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Ocurrio un error al añadir el ticket \n Detalle:";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al añadir";
            }
            return result;
        }
    }
}
