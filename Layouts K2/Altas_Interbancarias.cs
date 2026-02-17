using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Layouts_K2
{
    public partial class Altas_Interbancarias : Form
    {
        public Altas_Interbancarias()
        {
            InitializeComponent();
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

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivo de texto (*.txt)|*.txt";
            saveFileDialog.Title = "Guardar Layout Bancario";
            saveFileDialog.FileName = "Layout_Bancario_" + DateTime.Now.ToString("yyyyMMdd");

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.Default))
                    {
                        foreach (DataGridViewRow fila in dataGridView1.Rows)
                        {
                            if (fila.IsNewRow) continue;

                            // El registro total suma 182 caracteres
                            char[] linea = new string(' ', 182).ToCharArray();

                            // --- COLUMNA 1: CLABE (Índice 0) ---
                            string clabe = fila.Cells[0].Value?.ToString().Trim() ?? "";

                            // 1-3: Primeros 3 dígitos de la CLABE
                            string primeros3 = clabe.Length >= 3 ? clabe.Substring(0, 3) : clabe.PadRight(3, '0');
                            InsertarCampoManual(ref linea, primeros3, 0, 3);

                            // 4-21: Los 18 dígitos de la CLABE (Relleno ceros a la izquierda para justificar derecha)
                            InsertarCampoManual(ref linea, clabe.PadLeft(18, '0'), 3, 18);

                            // --- FIJO: MXP (22-24) ---
                            InsertarCampoManual(ref linea, "MXP", 21, 3);

                            // --- COLUMNA 2: IMPORTE (25-40) (Índice 1) ---
                            string importeRaw = fila.Cells[1].Value?.ToString() ?? "0.00";
                            string importeLimpio = LimpiarMonto(importeRaw); // Usando tu función de limpieza

                            if (decimal.TryParse(importeLimpio, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal monto))
                            {
                                string montoFmt = monto.ToString("f2", System.Globalization.CultureInfo.InvariantCulture);
                                // Ceros a la izquierda (justificado a la derecha)
                                InsertarCampoManual(ref linea, montoFmt.PadLeft(16, '0'), 24, 16);
                            }
                            else
                            {
                                InsertarCampoManual(ref linea, "0.00".PadLeft(16, '0'), 24, 16);
                            }

                            // --- COLUMNA 3: TITULAR (41-70) (Índice 2) ---
                            string titular = fila.Cells[2].Value?.ToString().ToUpper().Trim() ?? "";
                            InsertarCampoManual(ref linea, titular.PadRight(30, ' '), 40, 30);

                            // --- COLUMNA 4: ALIAS (71-100) (Índice 3) ---
                            string alias = fila.Cells[3].Value?.ToString().ToUpper().Trim() ?? "";
                            InsertarCampoManual(ref linea, alias.PadRight(30, ' '), 70, 30);

                            // --- FIJO: 40 (101-102) ---
                            InsertarCampoManual(ref linea, "40", 100, 2);

                            // --- COLUMNA 5: EMAIL (103-182) (Índice 4) ---
                            string email = fila.Cells[4].Value?.ToString().ToLower().Trim() ?? "";
                            InsertarCampoManual(ref linea, email.PadRight(80, ' '), 102, 80);

                            // sw.WriteLine agrega automáticamente el salto convencional 0D0A
                            sw.WriteLine(new string(linea));
                        }
                    }
                    MessageBox.Show("Archivo generado con éxito.", "FINCORE");
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Evitar validar encabezados o la fila vacía del final (*)
            if (e.RowIndex < 0 || dataGridView1.Rows[e.RowIndex].IsNewRow) return;

            string valor = e.Value?.ToString().Trim() ?? "";

            // --- ESCENARIO 1: CELDA VACÍA (Prioridad máxima) ---
            if (string.IsNullOrWhiteSpace(valor))
            {
                e.CellStyle.BackColor = Color.LightCoral; // Rojo suave
                e.CellStyle.ForeColor = Color.White;
                return;
            }

            // --- ESCENARIO 2: VALIDACIÓN DE CLABE (Columna 1 - Índice 0) ---
            if (e.ColumnIndex == 0)
            {
                // En este layout pedimos 18 dígitos para la CLABE
                if (valor.Length == 18)
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                    e.CellStyle.ForeColor = Color.Black;
                }
                else
                {
                    e.CellStyle.BackColor = Color.Orange;
                    e.CellStyle.ForeColor = Color.White;
                }
                return;
            }

            // --- ESCENARIO 3: CAMPOS CON DATOS (Nombre, Alias, Email, etc.) ---
            // Si llegamos aquí, la celda no está vacía. Restauramos el blanco.
            e.CellStyle.BackColor = Color.White;
            e.CellStyle.ForeColor = Color.Black;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PegarDesdeExcel();
            }
        }

        private void Altas_Interbancarias_Load(object sender, EventArgs e)
        {
            ActualizarContador();

            // Aseguramos que el DataGridView permita mostrar ToolTips
            dataGridView1.ShowCellToolTips = true;

            // Columna 1: CLABE
            dataGridView1.Columns[0].HeaderCell.ToolTipText = "Ingrese la cuenta CLABE de 18 dígitos numéricos.\nEl sistema marcará en verde cuando el largo sea correcto.";

            // Columna 2: IMPORTE
            dataGridView1.Columns[1].HeaderCell.ToolTipText = "Monto del pago.\nNo incluya símbolos ($) ni comas.\nEl sistema forzará 2 decimales automáticamente.";

            // Columna 3: TITULAR
            dataGridView1.Columns[2].HeaderCell.ToolTipText = "Nombre oficial del beneficiario.\nSe convertirá a MAYÚSCULAS automáticamente.";

            // Columna 4: ALIAS
            dataGridView1.Columns[3].HeaderCell.ToolTipText = "Nombre corto de referencia (Máx. 30 caracteres).";

            // Columna 5: EMAIL
            dataGridView1.Columns[4].HeaderCell.ToolTipText = "Correo electrónico para envío de comprobante.\nSe convertirá a minúsculas automáticamente.";
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
    }
}
