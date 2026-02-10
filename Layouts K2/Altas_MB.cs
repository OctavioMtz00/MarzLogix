using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Layouts_K2
{
    public partial class Altas_MB : Form
    {
        public Altas_MB()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivo de texto (*.txt)|*.txt";
            saveFileDialog.Title = "Guardar Altas Mismo Banco";
            saveFileDialog.FileName = "AltasMB_" + DateTime.Now.ToString("yyyyMMdd");

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.Default))
                    {
                        foreach (DataGridViewRow fila in dataGridView1.Rows)
                        {
                            if (fila.IsNewRow) continue;

                            // Crear buffer de 177 caracteres
                            char[] linea = new string(' ', 177).ToCharArray();

                            // --- COLUMNA 1: CUENTA (1 a 18) ---
                            string cuenta = fila.Cells[0].Value?.ToString().Trim() ?? "";
                            InsertarCampoManual(ref linea, cuenta.PadRight(18, '0'), 0, 18);

                            // --- COLUMNA 2: DIVISA (19 a 21) ---
                            string divisa = fila.Cells[1].Value?.ToString().ToUpper().Trim() ?? "MXP";
                            InsertarCampoManual(ref linea, divisa.PadRight(3, ' '), 18, 3);

                            // --- COLUMNA 3: IMPORTE MAX (22 a 37) ---
                            string importeRaw = fila.Cells[2].Value?.ToString() ?? "0.00";
                            string importeLimpio = LimpiarMonto(importeRaw); // Reutilizamos tu función de limpieza

                            if (decimal.TryParse(importeLimpio, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal monto))
                            {
                                string fmt = monto.ToString("f2", System.Globalization.CultureInfo.InvariantCulture);
                                InsertarCampoManual(ref linea, fmt.PadLeft(16, '0'), 21, 16);
                            }
                            else
                            {
                                InsertarCampoManual(ref linea, "0.00".PadLeft(16, '0'), 21, 16);
                            }

                            // --- ESPACIOS EN BLANCO (38 a 67) ---
                            // No es necesario hacer nada, el array ya tiene espacios por defecto.

                            // --- COLUMNA 4: ALIAS (68 a 97) ---
                            string alias = fila.Cells[3].Value?.ToString().ToUpper().Trim() ?? "";
                            InsertarCampoManual(ref linea, alias.PadRight(30, ' '), 67, 30);

                            // --- COLUMNA 5: EMAIL (98 a 177) ---
                            string email = fila.Cells[4].Value?.ToString().Trim() ?? "";
                            InsertarCampoManual(ref linea, email.PadRight(80, ' '), 97, 80);

                            // Escribir línea + Vuelta de carro (WriteLine agrega el 0D0A automáticamente)
                            sw.WriteLine(new string(linea).ToUpper());

                            /* 0D25
                            string contenidoLinea = new string(linea).ToUpper();
                            // 1. Escribimos el texto de la línea
                            sw.Write(contenidoLinea);

                            // 2. Escribimos manualmente los bytes 0D y 25
                            byte[] finDeLineaEspecial = new byte[] { 0x0D, 0x25 };

                            // Para escribir bytes usando StreamWriter, accedemos a su BaseStream
                            sw.Flush(); // Aseguramos que el texto anterior se haya escrito
                            sw.BaseStream.Write(finDeLineaEspecial, 0, finDeLineaEspecial.Length);
                            */
                        }
                    }
                    MessageBox.Show("Archivo de Altas generado con éxito.", "Finalizado");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void InsertarCampoManual(ref char[] linea, string texto, int inicio, int largo)
        {
            if (string.IsNullOrEmpty(texto)) return;
            if (texto.Length > largo) texto = texto.Substring(0, largo);

            for (int i = 0; i < texto.Length; i++)
            {
                if (inicio + i < linea.Length)
                    linea[inicio + i] = texto[i];
            }
        }

        private string LimpiarMonto(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return "0.00";

            // Eliminamos el signo de pesos, comas y espacios
            string resultado = texto.Replace("$", "").Replace(",", "").Trim();

            // Si después de limpiar queda vacío, devolvemos formato base
            return string.IsNullOrEmpty(resultado) ? "0.00" : resultado;
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

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ActualizarContador();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ActualizarContador();
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dataGridView1.Rows[e.RowIndex].IsNewRow) return;

            string valor = e.Value?.ToString().Trim() ?? "";

            // 1. Marcar Vacíos en Rojo (Faltan datos)
            if (string.IsNullOrWhiteSpace(valor))
            {
                e.CellStyle.BackColor = Color.LightCoral;
                e.CellStyle.ForeColor = Color.White;
                return;
            }

            // 2. Validación específica de DIVISA (Columna 2 - Índice 1)
            if (e.ColumnIndex == 1)
            {
                string[] permitidas = { "MXP", "EUR", "USD" };
                if (permitidas.Contains(valor.ToUpper()))
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                    e.CellStyle.ForeColor = Color.Black;
                }
                else
                {
                    e.CellStyle.BackColor = Color.Orange; // Aviso de moneda no soportada
                    e.CellStyle.ForeColor = Color.White;
                }
                return;
            }

            // 3. Todo lo demás con datos se queda en blanco
            e.CellStyle.BackColor = Color.White;
            e.CellStyle.ForeColor = Color.Black;
        }
    }
}
