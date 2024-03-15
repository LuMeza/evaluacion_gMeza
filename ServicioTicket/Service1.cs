using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace ServicioTicket
{
    public partial class ServicioTicket : ServiceBase
    {
        private System.Timers.Timer timer1;
        private EventLog eventLog1;
        private HttpClient httpClient;

        public ServicioTicket()
        {
            InitializeComponent();

            eventLog1 = new EventLog();
            if (!EventLog.SourceExists("ServicioTicket"))
            {
                EventLog.CreateEventSource("ServicioTicket", "Application");
            }
            eventLog1.Source = "ServicioTicket";
            eventLog1.Log = "Application";

            httpClient = new HttpClient();

            timer1 = new System.Timers.Timer();
            timer1.Interval = 60000;
            timer1.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
        }

        protected override void OnStart(string[] args)
        {
            timer1.Start();
        }

        protected override void OnStop()
        {
            timer1.Stop();
        }

        private async void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            //string porcentaje = (SystemInformation.PowerStatus.BatteryLifePercent * 100).ToString() + "%";
            //eventLog1.WriteEntry("Porcentaje actual de la batería: " + porcentaje);
            try
            {
                HttpResponseMessage response = await httpClient.PostAsync("http://localhost:51427/api/tickets/procesar", null);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    eventLog1.WriteEntry("Respuesta de la API: " + data);
                }
                else
                {
                    eventLog1.WriteEntry("Error al llamar a la API. Código de estado: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("Error al llamar a la API: " + ex.Message);
            }

        }
    }
}