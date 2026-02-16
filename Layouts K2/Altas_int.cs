using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Layouts_K2
{
    public partial class Altas_int : Form
    {
        public Altas_int()
        {
            InitializeComponent();
        }

        private void Altas_int_Load(object sender, EventArgs e)
        {
            ActualizarContador();
        }

        private void PegarDesdeExcel()
        {
            try
            {
                string textoCopiado = Clipboard.GetText();
                if (string.IsNullOrEmpty(textoCopiado)) return;

                // 1. Separar por líneas y eliminar la última si está vacía (común en Excel)
                string[] lineas = textoCopiado.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                // Si la última línea es puro espacio blanco, la ignoramos para evitar la fila extra
                int cantidadFilasReales = lineas.Length;
                if (string.IsNullOrWhiteSpace(lineas[lineas.Length - 1]))
                {
                    cantidadFilasReales--;
                }

                if (cantidadFilasReales <= 0) return;

                int filaInicio = dataGridView1.CurrentCell.RowIndex;
                int colInicio = dataGridView1.CurrentCell.ColumnIndex;

                // 2. AJUSTE DE AUTO-CRECIMIENTO EXACTO
                int filasNecesariasTotal = filaInicio + cantidadFilasReales;

                // Detectar si existe la fila de edición "nueva" (*)
                int filasExistentesParaDatos = dataGridView1.AllowUserToAddRows ? dataGridView1.Rows.Count - 1 : dataGridView1.Rows.Count;

                if (filasNecesariasTotal > filasExistentesParaDatos)
                {
                    int filasPorAñadir = filasNecesariasTotal - filasExistentesParaDatos;
                    dataGridView1.Rows.Add(filasPorAñadir);
                }

                // 3. LLENADO DE DATOS
                for (int i = 0; i < cantidadFilasReales; i++)
                {
                    string[] celdas = lineas[i].Split('\t');
                    for (int j = 0; j < celdas.Length; j++)
                    {
                        int colDestino = colInicio + j;

                        if (colDestino >= 0 && colDestino < dataGridView1.Columns.Count)
                        {
                            dataGridView1.Rows[filaInicio + i].Cells[colDestino].Value = celdas[j];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al pegar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PegarDesdeExcel();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buildLayoutInt_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivo de texto (*.txt)|*.txt";
            saveFileDialog.Title = "Guardar Archivo Bancario Normalizado";
            saveFileDialog.FileName = "AltasInternacionales_" + DateTime.Now.ToString("yyyyMMdd");

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.Default))
                    {
                        foreach (DataGridViewRow fila in dataGridView1.Rows)
                        {
                            if (fila.IsNewRow) continue;

                            char[] linea = new string(' ', 846).ToCharArray();

                            // 1. Valor estático CI (Pos 1-2)
                            InsertarCampo(ref linea, "CI", 0, 2, false);

                            // --- MAPEADO DE CELDAS (Ajustado por eliminación de Col A y Penúltima Col) ---
                            // Grid actual: 0:Nombre, 1:Ref, 2:Monto, 3:Dir, 4:Ciudad, 5:Estado, 
                            //              6:H, 7:I, 8:J, 9:K, 10:L, 11:NombrePaís (M y N automáticos)

                            InsertarCampo(ref linea, fila.Cells[0].Value, 2, 35, false);  // Col B (3-37)
                            InsertarCampo(ref linea, fila.Cells[1].Value, 37, 3, false);  // Col C (38-40)
                            InsertarCampoMonto(ref linea, fila.Cells[2].Value, 40, 16);   // Col D (41-56)
                            InsertarCampo(ref linea, fila.Cells[3].Value, 56, 140);       // Col E (57-196)
                            InsertarCampo(ref linea, fila.Cells[4].Value, 196, 30);       // Col F (197-226)
                            InsertarCampo(ref linea, fila.Cells[5].Value, 226, 15);       // Col G (227-241)

                            InsertarCampo(ref linea, fila.Cells[6].Value, 381, 70);       // Col H (382-451)
                            InsertarCampo(ref linea, fila.Cells[7].Value, 451, 16);       // Col I (452-467)
                            InsertarCampo(ref linea, fila.Cells[8].Value, 658, 16);       // Col J (659-674)
                            InsertarCampo(ref linea, fila.Cells[9].Value, 709, 35);       // Col K (710-744)
                            InsertarCampo(ref linea, fila.Cells[10].Value, 779, 35);      // Col L (780-814)

                            // --- LÓGICA AUTOMÁTICA PARA COLUMNA M (ISO CODE) Y N (COUNTRY NAME) ---
                            string nombrePaisGrid = fila.Cells[11].Value?.ToString().Trim() ?? "";
                            string codigoIso = "";

                            // Buscamos el código en el diccionario
                            if (paisesIso.TryGetValue(nombrePaisGrid, out codigoIso))
                            {
                                // Insertar Código ISO en Col M (Posición 815 - Largo 2 según estándar ISO, 
                                // pero tu regla dice Col M 815 al 815 y Col N 817 al 846)

                                // Si Col M es solo la pos 815 (1 carácter), ajusta el largo. 
                                // Normalmente ISO son 2 chars. Usaré largo 1 según tu instrucción previa para M.
                                InsertarCampo(ref linea, codigoIso, 814, 2); // Col M (815)
                            }

                            // Insertar Nombre del País en Col N (Posición 817-846)
                            InsertarCampo(ref linea, nombrePaisGrid, 816, 30); // Col N (817-846)

                            sw.WriteLine(new string(linea));
                        }
                    }
                    MessageBox.Show("Archivo generado con éxito.", "Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InsertarCampo(ref char[] linea, object valor, int inicio, int largo, bool mayusculas = true)
        {
            string texto = valor?.ToString() ?? "";
            if (mayusculas) texto = texto.ToUpper();

            texto = texto.Trim();
            if (texto.Length > largo) texto = texto.Substring(0, largo);

            for (int i = 0; i < texto.Length; i++)
            {
                linea[inicio + i] = texto[i];
            }
        }

        // Función especial para Columna D (Monto) con ceros a la izquierda
        private void InsertarCampoMonto(ref char[] linea, object valor, int inicio, int largo)
        {
            decimal monto = 0;
            decimal.TryParse(valor?.ToString(), out monto);

            // Formato con 2 decimales y punto
            string textoMonto = monto.ToString("0.00").Replace(',', '.');

            // Rellenar con ceros a la izquierda
            string montoFinal = textoMonto.PadLeft(largo, '0');
            if (montoFinal.Length > largo) montoFinal = montoFinal.Substring(montoFinal.Length - largo);

            for (int i = 0; i < montoFinal.Length; i++)
            {
                linea[inicio + i] = montoFinal[i];
            }
        }

        private void DelAll_Click(object sender, EventArgs e)
        {
            // 1. Confirmación de seguridad
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de que desea eliminar todos los registros del listado?",
                "Confirmar Limpieza",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    // 2. Limpiar todas las filas
                    dataGridView1.Rows.Clear();
                    ActualizarContador();

                    MessageBox.Show("La tabla ha sido vaciada.", "Limpieza Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al limpiar la tabla: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frm = new Bienvenida();
            frm.Show();
            this.Close();
        }

        private void ActualizarContador()
        {
            // Restamos 1 solo si la fila de edición nueva (*) está activa
            int total = dataGridView1.AllowUserToAddRows ? dataGridView1.Rows.Count - 1 : dataGridView1.Rows.Count;

            // Si por alguna razón el conteo da negativo (grid vacío con *), ponemos 0
            if (total < 0) total = 0;

            lblContador.Text = "Total de registros: " + total.ToString();

            // Opcional: Cambiar color si no hay datos
            lblContador.ForeColor = (total == 0) ? Color.Red : Color.DarkBlue;
        }



        private void dataGridView1_RowsAdded_1(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ActualizarContador();
        }

        private void dataGridView1_RowsRemoved_1(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ActualizarContador();
        }

        private void Altas_int_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*  // Verificamos si el cierre fue iniciado por el usuario (X, Alt+F4, etc.)
              if (e.CloseReason == CloseReason.UserClosing)
              {
                  DialogResult resultado = MessageBox.Show(
                      "¿Está seguro de que desea salir del programa?",
                      "Confirmar Salida",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Question);

                  if (resultado == DialogResult.No)
                  {
                      // Detenemos el cierre
                      e.Cancel = true;
                  }
              }*/
        }

        private void btnCloseAI_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                   "¿Está seguro de que desea salir?",
                   "Confirmar Salida",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
                this.Close();
            Form frm1 = new Bienvenida();
            frm1.Show();
        }

        private void Altas_int_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Close();
        }

        private readonly Dictionary<string, string> paisesIso = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"Andorra", "AD"}, {"United Arab Emirates", "AE"}, {"Afghanistan", "AF"}, {"Antigua and Barbuda", "AG"},
                {"Albania", "AL"}, {"Armenia", "AM"}, {"Angola", "AO"}, {"Argentina", "AR"}, {"Austria", "AT"},
                {"Australia", "AU"}, {"Azerbaijan", "AZ"}, {"Belgium", "BE"}, {"Brazil", "BR"}, {"Canada", "CA"},
                {"Switzerland", "CH"}, {"China", "CN"}, {"Germany", "DE"},{"Ecuador","EC" }, {"Spain", "ES"}, {"France", "FR"},
                {"United Kingdom", "GB"}, {"India", "IN"}, {"Italy", "IT"}, {"Japan", "JP"}, {"South Korea", "KR"},
                {"Mexico", "MX"}, {"Netherlands", "NL"}, {"Norway", "NO"}, {"New Zealand", "NZ"},
                {"Russian Federation", "RU"}, {"Saudi Arabia", "SA"}, {"Sweden", "SE"}, {"Singapore", "SG"},
                {"Türkiye", "TR"}, {"United States of America", "US"}, {"South Africa", "ZA"}
            };

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Ignorar encabezados y la fila de asterisco (*)
            if (e.RowIndex < 0 || dataGridView1.Rows[e.RowIndex].IsNewRow) return;

            string valor = e.Value?.ToString().Trim() ?? "";

            // --- ESCENARIO 1: CELDAS VACÍAS (Prioridad Máxima) ---
            if (string.IsNullOrWhiteSpace(valor))
            {
                e.CellStyle.BackColor = Color.LightCoral;
                e.CellStyle.ForeColor = Color.White;
                return;
            }

            // --- ESCENARIO 2: VALIDACIÓN DE CUENTA (Columna 2 - Índice 1) ---
            if (e.ColumnIndex == 0)
            {
                int largo = valor.Length;
                // En internacionales, a veces las cuentas varían, 
                // pero mantenemos tu regla de 10 u 18 si el banco lo exige.
                if (largo == 10 || largo == 18)
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                    e.CellStyle.ForeColor = Color.Black;
                }
                else
                {
                    e.CellStyle.BackColor = Color.LightCoral;
                    e.CellStyle.ForeColor = Color.White;
                }
                return;
            }

            // --- ESCENARIO 3: CAMPOS COMPLETOS ---
            // Si la celda tiene datos y no es la cuenta, vuelve al color normal
            e.CellStyle.BackColor = Color.White;
            e.CellStyle.ForeColor = Color.Black;                                                                                                               
        }
    }
}
