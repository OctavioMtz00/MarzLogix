using Layouts_K2;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace Layouts_K2 // Cambia esto por tu namespace real
{
    static class Program
    {
        // Pega aquí la URL "Raw" de tu Gist Secreto
        private const string UrlLicencia = "https://gist.githubusercontent.com/OctavioMtz00/78a51439ef21945e5a25d2c44aa3d514/raw/47baef2b53869336bbe18e137d6e2e61c4864bd3/licencia.json";
        private const int DiasDeGracia = 45; // Los días de tolerancia sin pago o sin internet

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // El escudo de seguridad. Si devuelve false, la app se cierra.
            if (!VerificarLicencia())
            {
                return;
            }

            // Si pasa la validación, arranca tu aplicación normalmente
            Application.Run(new Bienvenida()); // Formulario principal
        }

        static bool VerificarLicencia()
        {
            try
            {
                // Intentamos conectar al servidor silenciosamente
                using (HttpClient client = new HttpClient())
                {
                    // Configuramos un tiempo de espera muy corto (3 segundos) para que la app abra rápido
                    client.Timeout = TimeSpan.FromSeconds(3);

                    // .Result fuerza a la operación a ejecutarse de forma síncrona en el arranque
                    string respuestaJson = client.GetStringAsync(UrlLicencia).Result;

                    if (respuestaJson.Contains("\"estado\": \"inactivo\""))
                    {
                        // El cliente no pagó. El Kill Switch está activado.
                        MessageBox.Show("El periodo de su licencia ha concluido o ha sido suspendida. Por favor, contacte a soporte técnico para regularizar su servicio.", "Licencia Suspendida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // Si el servidor dice "activo", actualizamos la fecha local a HOY y guardamos
                    Properties.Settings.Default.UltimaValidacion = DateTime.Now;
                    Properties.Settings.Default.Save();

                    return true;
                }
            }
            catch
            {
                // MODO OFFLINE O SIN INTERNET: Entra a funcionar el periodo de gracia
                DateTime ultimaVez = Properties.Settings.Default.UltimaValidacion;
                TimeSpan tiempoTranscurrido = DateTime.Now - ultimaVez;

                if (tiempoTranscurrido.TotalDays > DiasDeGracia)
                {
                    // Se agotaron los 45 días de gracia. Bloqueo total.
                    MessageBox.Show($"No se ha podido conectar al servidor de licencias en los últimos {DiasDeGracia} días. Por seguridad, la aplicación ha sido bloqueada. Conéctese a internet e intente de nuevo.", "Validación Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    // Aún está dentro de los 45 días. Le avisamos amablemente (Opcional)
                    int diasRestantes = DiasDeGracia - (int)tiempoTranscurrido.TotalDays;
                    // Puedes comentar la siguiente línea si prefieres que sea 100% silencioso para el usuario
                    Console.WriteLine($"Aviso interno: Operando en periodo de gracia. Quedan {diasRestantes} días.");

                    return true;
                }
            }
        }
    }
}