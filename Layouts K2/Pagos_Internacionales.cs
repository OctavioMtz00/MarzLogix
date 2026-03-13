using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Layouts_K2
{
    public partial class Pagos_Internacionales : Form
    {
        public Pagos_Internacionales()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
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

        private void btnCln_Click(object sender, EventArgs e)
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

        private void ActualizarSumaImportes()
        {
            decimal sumaTotal = 0;

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                // Ignorar la fila vacía del final (asterisco)
                if (fila.IsNewRow) continue;

                // Tomar el valor de la Columna 3 (Índice 2)
                var celdaImporte = fila.Cells[2].Value;

                if (celdaImporte != null)
                {
                    decimal valorCelda = 0;
                    // Intentamos convertir el valor. Si no es un número válido, sumará 0.
                    if (decimal.TryParse(celdaImporte.ToString(), out valorCelda))
                    {
                        sumaTotal += valorCelda;
                    }
                }
            }

            // Formatear como moneda (C2 usa el símbolo de moneda local, N2 solo dos decimales)
            lblSumaImportes.Text = "Total Importe: $" + sumaTotal.ToString("N2");
        }

        private void PegarDesdeExcel()
        {
            try
            {
                string textoCopiado = Clipboard.GetText();
                if (string.IsNullOrEmpty(textoCopiado)) return;

                string[] lineas = textoCopiado.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                int cantidadFilasReales = lineas.Length;
                if (cantidadFilasReales > 0 && string.IsNullOrWhiteSpace(lineas[lineas.Length - 1]))
                    cantidadFilasReales--;

                if (cantidadFilasReales <= 0) return;

                int filaInicio = dataGridView1.CurrentCell.RowIndex;
                int colInicio = dataGridView1.CurrentCell.ColumnIndex;

                // Gestión de filas (Asegurando que existan suficientes para el pegado)
                int filasNecesariasTotal = filaInicio + cantidadFilasReales;
                int filasExistentes = dataGridView1.AllowUserToAddRows ? dataGridView1.Rows.Count - 1 : dataGridView1.Rows.Count;

                if (filasNecesariasTotal > filasExistentes)
                    dataGridView1.Rows.Add(filasNecesariasTotal - filasExistentes);

                dataGridView1.SuspendLayout();

                for (int i = 0; i < cantidadFilasReales; i++)
                {
                    string[] celdas = lineas[i].Split('\t');

                    for (int j = 0; j < celdas.Length; j++)
                    {
                        int colDestino = colInicio + j;

                        // Verificamos que no se salga del rango de 17 columnas (0 a 16)
                        if (colDestino >= 0 && colDestino < 17)
                        {
                            string valorCelda = celdas[j].Trim();

                            // --- VALIDACIÓN: COLUMNA DE IMPORTE (ÍNDICE 3) ---
                            if (colDestino == 3)
                            {
                                // Limpieza de símbolos de moneda
                                valorCelda = valorCelda.Replace("$", "").Replace(",", "");

                                // Forzamos 2 decimales para que el Grid se vea uniforme
                                if (decimal.TryParse(valorCelda, System.Globalization.NumberStyles.Any,
                                    System.Globalization.CultureInfo.InvariantCulture, out decimal monto))
                                {
                                    valorCelda = monto.ToString("f2", System.Globalization.CultureInfo.InvariantCulture);
                                }
                            }
                            else
                            {
                                // --- PARA EL RESTO DE LAS COLUMNAS: Convertir a MAYÚSCULAS ---
                                valorCelda = valorCelda.ToUpper();
                            }

                            dataGridView1.Rows[filaInicio + i].Cells[colDestino].Value = valorCelda;
                        }
                    }
                }

                dataGridView1.ResumeLayout();
                ActualizarSumaImportes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al pegar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dataGridView1.Refresh();
            ActualizarContador();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly)
            {
                e.Cancel = true; // Cancela la edición antes de que empiece
            }
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            // Columna 4 (Índice 3)
            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                var celda = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (celda.Value != null && !string.IsNullOrWhiteSpace(celda.Value.ToString()))
                {
                    string valorEntrada = celda.Value.ToString();

                    // Intentamos convertir a decimal para normalizar el formato
                    if (decimal.TryParse(valorEntrada, out decimal monto))
                    {
                        // Formateamos siempre a 2 decimales (ej: 10.5 -> 10.50)
                        celda.Value = monto.ToString("0.00");
                    }
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                ActualizarSumaImportes();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PegarDesdeExcel();
                e.Handled = true; // Evita que el sistema procese el pegado estándar de Windows
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ActualizarContador();
            ActualizarSumaImportes();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ActualizarContador();
            ActualizarSumaImportes();
        }

        private void btnLOMX_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivo de texto (*.txt)|*.txt";
            saveFileDialog.Title = "MarzLogix | Layout Internacional 352 (13 Columnas)";
            saveFileDialog.FileName = "Layout_OPI_Internacional_" + DateTime.Now.ToString("yyyyMMdd");

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.Default))
                    {
                        foreach (DataGridViewRow fila in dataGridView1.Rows)
                        {
                            if (fila.IsNewRow) continue;

                            // Buffer exacto de 352 caracteres. El salto de línea lo agrega WriteLine.
                            char[] linea = new string(' ', 352).ToCharArray();

                            // 1. 1-3 (3 chars) - ESTÁTICO OPI (Pos: 0)
                            InsertarCampoManual(ref linea, "OPI", 0, 3);

                            // 2. 4-21 (18 chars) - Columna 1 [Índice 0] - Espacios derecha (Pos: 3)
                            string col1 = fila.Cells[0].Value?.ToString() ?? "";
                            InsertarCampoManual(ref linea, col1.PadLeft(18, '0'), 3, 18);

                            // 3. 22-56 (35 chars) - Columna 2 [Índice 1] - Espacios derecha (Pos: 21)
                            string col2 = fila.Cells[1].Value?.ToString() ?? "";
                            InsertarCampoManual(ref linea, col2.ToUpper().PadRight(35, ' '), 21, 35);

                            // 4. 57-72 (16 chars) - Columna 3 [Índice 2] - Ceros izquierda (Pos: 56)
                            string montoRaw = LimpiarMonto(fila.Cells[2].Value?.ToString() ?? "0.00");
                            if (decimal.TryParse(montoRaw, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal montoDec))
                            {
                                string montoFmt = montoDec.ToString("f2", System.Globalization.CultureInfo.InvariantCulture).Replace(",", ".");
                                InsertarCampoManual(ref linea, montoFmt.PadLeft(16, '0'), 56, 16);
                            }

                            // 5. 73-75 (3 chars) - Columna 4 [Índice 3] - Código ISO (Pos: 72)
                            string divisaEntrada = fila.Cells[3].Value?.ToString().Trim().ToUpper() ?? "";
                            string divisaIso = "USD"; // Valor por defecto
                            if (divisaEntrada.Contains("PESO")) divisaIso = "MXP";
                            else if (divisaEntrada.Contains("DOLAR") || divisaEntrada.Contains("DÓLAR")) divisaIso = "USD";
                            else if (divisaEntrada.Contains("EURO")) divisaIso = "EUR";
                            else divisaIso = divisaEntrada.PadRight(3, ' ').Substring(0, 3); // Si se captura manual el ISO
                            InsertarCampoManual(ref linea, divisaIso, 72, 3);

                            // 6. 76-125 (50 chars) - Columna 5 [Índice 4] - Espacios derecha (Pos: 75)
                            string col5 = fila.Cells[4].Value?.ToString() ?? "";
                            InsertarCampoManual(ref linea, col5.ToUpper().PadRight(50, ' '), 75, 50);

                            // 7. 126-140 (15 chars) - Columna 6 [Índice 5] - Espacios derecha (Pos: 125)
                            string col6 = fila.Cells[5].Value?.ToString() ?? "";
                            InsertarCampoManual(ref linea, col6.ToUpper().PadRight(15, ' '), 125, 15);

                            // 8. 141-170 (30 chars) - Columna 11 [Índice 10] - Espacios derecha (Pos: 140)
                            string col11 = fila.Cells[10].Value?.ToString() ?? "";
                            InsertarCampoManual(ref linea, col11.ToUpper().PadRight(30, ' '), 140, 30);

                            // 9. 171-200 (30 chars) - Columna 12 [Índice 11] - Espacios derecha (Pos: 170)
                            string col12 = fila.Cells[11].Value?.ToString() ?? "";
                            InsertarCampoManual(ref linea, col12.ToUpper().PadRight(30, ' '), 170, 30);

                            // 10. 201-230 (30 chars) - Columna 9 [Índice 8] - Espacios derecha (Pos: 200)
                            string col9 = fila.Cells[8].Value?.ToString() ?? "";
                            InsertarCampoManual(ref linea, col9.ToUpper().PadRight(30, ' '), 200, 40);

                            // 11. 231-270 (40 chars) - Columna 7 [Índice 6] - Espacios derecha (Pos: 230)
                            // Nota técnica: Aquí se alineó tu solicitud de 40 caracteres.
                            string col7 = fila.Cells[6].Value?.ToString() ?? "";
                            InsertarCampoManual(ref linea, col7.ToUpper().PadRight(40, ' '), 240, 30);

                            // 12. 271-300 (30 chars) - Columna 8 [Índice 7] - Espacios derecha (Pos: 270)
                            string col8 = fila.Cells[7].Value?.ToString() ?? "";
                            InsertarCampoManual(ref linea, col8.ToUpper().PadRight(30, ' '), 270, 30);

                            // 13. 301-340 (40 chars) - Columna 13 [Índice 12] - Espacios derecha (Pos: 300)
                            string col13 = fila.Cells[12].Value?.ToString() ?? "";
                            InsertarCampoManual(ref linea, col13.ToUpper().PadRight(40, ' '), 300, 40);

                            // 14. 341-352 (12 chars) - Columna 10 [Índice 9] - Espacios derecha (Pos: 340)
                            string col10 = fila.Cells[9].Value?.ToString() ?? "";
                            InsertarCampoManual(ref linea, col10.ToUpper().PadRight(12, ' '), 340, 12);

                            // Escribir registro (WriteLine añade de forma nativa la vuelta de carro al final)
                            sw.WriteLine(new string(linea));
                        }
                    }
                    MessageBox.Show("Archivo generado con éxito.", "Finalizado");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error crítico al generar el layout: " + ex.Message, "MarzLogix Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void Pagos_Internacionales_Load(object sender, EventArgs e)
        {
            ActualizarContador();
        }
    }
}
