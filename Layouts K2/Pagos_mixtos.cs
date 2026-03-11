using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Layouts_K2
{
    public partial class Pagos_mixtos : Form
    {
        public Pagos_mixtos()
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

        private void Pagos_mixtos_Load(object sender, EventArgs e)
        {
            ActualizarContador();

            // Aseguramos que el DataGridView permita mostrar ToolTips
            dataGridView1.ShowCellToolTips = true;

            // Columna 1: TIPO DE OPERACION
            dataGridView1.Columns[0].HeaderCell.ToolTipText = "1.- MISMO BANCO  \n 2.- INTERBANCARIA \n 3.- INTERNACIONAL";

            // Columna 2: CUENTA CARGO
            dataGridView1.Columns[1].HeaderCell.ToolTipText = "1.- MISMO BANCO - Cuenta de 10 dígitos  \n 2.- INTERBANCARIA - Cuenta CABLE de 18 dígitos  \n 3.- INTERNACIONAL ";

            // Columna 3: IMPORTE
            dataGridView1.Columns[3].HeaderCell.ToolTipText = "Monto a pagar \nSe redondea a dos decimales ";

            // Columna 4: DIVISA
            dataGridView1.Columns[4].HeaderCell.ToolTipText = "USD, EUR, COP ";


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

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PegarDesdeExcel();
                e.Handled = true; // Evita que el sistema procese el pegado estándar de Windows
            }
        }

        private void ActualizarSumaImportes()
        {
            decimal sumaTotal = 0;

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                // Ignorar la fila vacía del final (asterisco)
                if (fila.IsNewRow) continue;

                // Tomar el valor de la Columna 4 (Índice 3)
                var celdaImporte = fila.Cells[3].Value;

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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Solo actuamos si el cambio fue en la Columna 1 (Índice 0)
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                ValidarBloqueoColumnas(e.RowIndex);
            }

            if (e.ColumnIndex == 3)
            {
                ActualizarSumaImportes();
            }
        }

        private void ValidarBloqueoColumnas(int filaIndex)
        {
            DataGridViewRow fila = dataGridView1.Rows[filaIndex];
            string operacion = fila.Cells[0].Value?.ToString().ToUpper().Trim() ?? "";

            // Lista de opciones válidas
            string[] opcionesValidas = { "INTERBANCARIA", "CIE", "MISMO BANCO", "INTERNACIONAL" };

            // Resetear estilo de la celda de operación
            fila.Cells[0].Style.BackColor = Color.White;
            fila.Cells[0].Style.ForeColor = Color.Black;

            // Si hay texto pero no es una opción válida, pintar de naranja
            if (!string.IsNullOrEmpty(operacion) && !opcionesValidas.Contains(operacion))
            {
                fila.Cells[0].Style.BackColor = Color.Orange;
                fila.Cells[0].Style.ForeColor = Color.White;

                // Si el nombre es inválido, habilitamos todo para que el usuario corrija
                HabilitarTodas(fila);
                return;
            }

            // Si es válido, aplicamos los bloqueos
            HabilitarTodas(fila); // Limpiar bloqueos previos

            switch (operacion)
            {
                case "INTERBANCARIA":
                    BloquearRango(fila, 8, 16);
                    break;
                case "CIE":
                    BloquearRango(fila, 7, 16);
                    break;
                case "MISMO BANCO":
                    BloquearCelda(fila, 6);
                    BloquearRango(fila, 7, 16);
                    break;
                case "INTERNACIONAL":
                    // Se queda todo habilitado
                    break;
                case "INTERNACIONAL2":
                    // Se queda todo habilitado
                    break;


            }
        }

        private void HabilitarTodas(DataGridViewRow fila)
        {
            for (int i = 1; i < 16; i++)
            {
                fila.Cells[i].ReadOnly = false;
                fila.Cells[i].Style.BackColor = Color.White;
            }
        }

        private void BloquearCelda(DataGridViewRow fila, int colIndex)
        {
            fila.Cells[colIndex].ReadOnly = true;
            fila.Cells[colIndex].Style.BackColor = Color.LightGray;
            fila.Cells[colIndex].Value = null; // Opcional: limpiar valor si se deshabilita
        }

        private void BloquearRango(DataGridViewRow fila, int inicio, int fin)
        {
            for (int i = inicio; i <= fin; i++)
            {
                BloquearCelda(fila, i);
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly)
            {
                e.Cancel = true; // Cancela la edición antes de que empiece
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ActualizarContador();
            ActualizarSumaImportes();
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ActualizarContador();
            ActualizarSumaImportes();
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

        private string LimpiarMonto(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return "0.00";

            // Eliminamos el signo de pesos, comas y espacios
            string resultado = texto.Replace("$", "").Replace(",", "").Trim();

            // Si después de limpiar queda vacío, devolvemos formato base
            return string.IsNullOrEmpty(resultado) ? "0.00" : resultado;
        }

        private void btnLOMX_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivo de texto (*.txt)|*.txt";
            saveFileDialog.Title = "Guardar Pagos Mixtos";
            saveFileDialog.FileName = "PagosMixtos_" + DateTime.Now.ToString("yyyyMMdd");

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.Default))
                    {
                        foreach (DataGridViewRow fila in dataGridView1.Rows)
                        {
                            if (fila.IsNewRow) continue;

                            string tipoOp = fila.Cells[0].Value?.ToString().ToUpper().Trim() ?? "";
                            string lineaResultado = "";

                            // Mandamos a llamar la función según la operación
                            switch (tipoOp)
                            {
                                case "INTERBANCARIA":
                                    lineaResultado = GenerarLineaInterbancaria(fila);
                                    break;
                                case "MISMO BANCO":
                                    lineaResultado = GenerarLineaMismoBanco(fila);
                                    break;
                                case "INTERNACIONAL":
                                    lineaResultado = GenerarLineaInternacional(fila);
                                    break;
                                case "INTERNACIONAL2":
                                    lineaResultado = GenerarLineaInternacional(fila);
                                    break;
                                default:
                                    continue; // Omitir si no coincide
                            }

                            if (!string.IsNullOrEmpty(lineaResultado))
                                sw.WriteLine(lineaResultado.ToUpper());
                        }
                    }
                    MessageBox.Show("Archivo generado con éxito.", "Finalizado");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar: " + ex.Message);
                }
            }
        }

        private string GenerarLineaInterbancaria(DataGridViewRow fila)
        {
            char[] linea = new string(' ', 131).ToCharArray();

            InsertarCampoManual(ref linea, "PSC", 0, 3); // 1-3
            InsertarCampoManual(ref linea, fila.Cells[2].Value?.ToString().PadLeft(18, '0'), 3, 18); // 4-21 (Col 3)
            InsertarCampoManual(ref linea, fila.Cells[1].Value?.ToString().PadLeft(18, '0'), 21, 18); // 22-39 (Col 2)
            InsertarCampoManual(ref linea, fila.Cells[4].Value?.ToString(), 39, 3); // 40-42 (Col 5)

            // Importe Col 4 (43-58): ceros a la izquierda
            string importe = fila.Cells[3].Value?.ToString() ?? "0.00";
            //Limpia signos y comas
            string importeLimpio = LimpiarMonto(importe);
            // 2. Intentar convertir a decimal para normalizarlo
            if (decimal.TryParse(importeLimpio, out decimal montoDecimal))
            {
                // "f2" fuerza 2 decimales: 0.5 -> "0.50" | 10 -> "10.00"
                // Usamos InvariantCulture para asegurar que el punto sea el separador decimal
                string importeFormateado = montoDecimal.ToString("f2", System.Globalization.CultureInfo.InvariantCulture);

                // 3. Insertar con ceros a la izquierda hasta completar 16 espacios
                InsertarCampoManual(ref linea, importeFormateado.PadLeft(16, '0'), 42, 16);
                // Nota: El índice (42 o 56) depende de la función que estés editando
            }
            else
            {
                // Si el valor no es un número, ponemos ceros por seguridad
                InsertarCampoManual(ref linea, "0.00".PadLeft(16, '0'), 42, 16);
            }

            InsertarCampoManual(ref linea, importe.PadLeft(16, '0'), 42, 16);

            InsertarCampoManual(ref linea, fila.Cells[7].Value?.ToString().PadRight(30, ' '), 58, 30); // 59-88 (Col 8)
            InsertarCampoManual(ref linea, "40", 88, 2); // 89-90

            // 91-93: Primeros 3 de Col 3
            string col2 = fila.Cells[2].Value?.ToString() ?? "   ";
            InsertarCampoManual(ref linea, col2.Substring(0, Math.Min(3, col2.Length)), 90, 3);

            InsertarCampoManual(ref linea, fila.Cells[5].Value?.ToString().PadRight(30, ' '), 93, 30); // 94-123 (Col 6)
            InsertarCampoManual(ref linea, fila.Cells[6].Value?.ToString().PadLeft(7, '0'), 123, 7); // 124-130 (Col 7)
            InsertarCampoManual(ref linea, "H", 130, 1); // 131

            return new string(linea);
        }




        private string GenerarLineaInternacional(DataGridViewRow fila)
        {
            char[] linea = new string(' ', 566).ToCharArray();

            // 1-3: OPI
            InsertarCampoManual(ref linea, "OPI", 0, 3);

            // 4-21 (Col 2): Cuenta/Referencia
            InsertarCampoManual(ref linea, fila.Cells[1].Value?.ToString().PadLeft(18, '0'), 3, 18);

            // 22-56 (Col 3): Nombre/Dato
            InsertarCampoManual(ref linea, fila.Cells[2].Value?.ToString().PadRight(35, ' '), 21, 35);

            // --- CORRECCIÓN DE IMPORTE ---
            // Col 4 (Caracteres 57-72): Posición inicial 56, Largo 16
            string importeRaw = fila.Cells[3].Value?.ToString() ?? "0.00";
            string importeLimpio = LimpiarMonto(importeRaw);

            if (decimal.TryParse(importeLimpio, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal montoDecimal))
            {
                string final = montoDecimal.ToString("f2", System.Globalization.CultureInfo.InvariantCulture);
                // INSERTAR SOLO AQUÍ (Posición 56)
                InsertarCampoManual(ref linea, final.PadLeft(16, '0'), 56, 16);
            }
            else
            {
                InsertarCampoManual(ref linea, "0.00".PadLeft(16, '0'), 56, 16);
            }

            // --- ELIMINADA LA LÍNEA QUE REPETÍA EL IMPORTE EN LA POSICIÓN 42 ---

            // 73-75 (Col 5): Divisa
            InsertarCampoManual(ref linea, fila.Cells[4].Value?.ToString().PadRight(3, ' '), 72, 3);

            // Justificados a la izquierda masivos (76 en adelante)
            InsertarCampoManual(ref linea, fila.Cells[5].Value?.ToString().PadRight(50, ' '), 75, 50);    // Col 6 (76-125)
            InsertarCampoManual(ref linea, fila.Cells[6].Value?.ToString().PadRight(15, ' '), 125, 15);   // Col 7 (126-140)
            InsertarCampoManual(ref linea, fila.Cells[8].Value?.ToString().PadRight(30, ' '), 140, 30);   // Col 9 (141-170)
            InsertarCampoManual(ref linea, fila.Cells[9].Value?.ToString().PadRight(30, ' '), 170, 30);   // Col 10 (171-200)
            InsertarCampoManual(ref linea, fila.Cells[10].Value?.ToString().PadRight(40, ' '), 200, 40);  // Col 11 (201-240)
            InsertarCampoManual(ref linea, fila.Cells[11].Value?.ToString().PadRight(12, ' '), 240, 12);  // Col 12 (241-252)
            InsertarCampoManual(ref linea, fila.Cells[7].Value?.ToString().PadRight(140, ' '), 252, 140); // Col 8 (253-392)
            InsertarCampoManual(ref linea, fila.Cells[12].Value?.ToString().PadRight(70, ' '), 392, 70);  // Col 13 (393-462)
            InsertarCampoManual(ref linea, fila.Cells[13].Value?.ToString().PadRight(16, ' '), 462, 16);  // Col 14 (463-478)
            InsertarCampoManual(ref linea, fila.Cells[14].Value?.ToString().PadRight(16, ' '), 478, 16);  // Col 15 (479-494)
            InsertarCampoManual(ref linea, fila.Cells[15].Value?.ToString().PadRight(35, ' '), 494, 35);  // Col 16 (495-529)
            InsertarCampoManual(ref linea, fila.Cells[16].Value?.ToString().PadRight(35, ' '), 529, 35);  // Col 17 (530-564)

            // País ISO (565-566)
            string paisNombre = fila.Cells[9].Value?.ToString() ?? ""; // Referencia Col 10
            if (paisesIso.TryGetValue(paisNombre, out string iso))
                InsertarCampoManual(ref linea, iso, 564, 2);

            return new string(linea);
        }

        private string GenerarLineaMismoBanco(DataGridViewRow fila)
        {
            char[] linea = new string(' ', 88).ToCharArray();

            InsertarCampoManual(ref linea, "PTC", 0, 3); // 1-3
            InsertarCampoManual(ref linea, fila.Cells[2].Value?.ToString().PadLeft(18, '0'), 3, 18); // 4-21 (Col 3)
            InsertarCampoManual(ref linea, fila.Cells[1].Value?.ToString().PadLeft(18, '0'), 21, 18); // 22-39 (Col 2)

            // Divisa (40-42)
            string divisaCod = fila.Cells[4].Value?.ToString().ToUpper().Trim() ?? "";

            // Validación de seguridad: Si la celda está vacía, podrías poner una moneda por defecto (ej. MXP)
            if (string.IsNullOrEmpty(divisaCod))
            {
                divisaCod = "MXP";
            }

            // Insertar en la posición 40 (índice 39), asegurando que siempre ocupe 3 espacios
            InsertarCampoManual(ref linea, divisaCod.PadRight(3, ' '), 39, 3);

            // Importe Col 4 (43-58): ceros a la Izquierda
            string importe = fila.Cells[3].Value?.ToString() ?? "0.00";

            //Limpia signos y comas
            string importeLimpio = LimpiarMonto(importe);

            InsertarCampoManual(ref linea, importe.PadLeft(16, '0'), 42, 16);

            InsertarCampoManual(ref linea, fila.Cells[5].Value?.ToString().PadRight(30, ' '), 58, 30); // 58-88 (Col 6)

            return new string(linea);
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

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Seguridad: No validar encabezados ni la fila de inserción nueva
            if (e.RowIndex < 0 || dataGridView1.Rows[e.RowIndex].IsNewRow) return;

            var celda = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string valor = e.Value?.ToString().Trim() ?? "";

            // --- CASO A: Celdas Bloqueadas (Solo Lectura) ---
            if (celda.ReadOnly)
            {
                e.CellStyle.BackColor = Color.FromArgb(240, 240, 240); // Gris claro
                e.CellStyle.ForeColor = Color.Gray;
                return;
            }

            // --- CASO B: Celdas Obligatorias VACÍAS ---
            if (string.IsNullOrWhiteSpace(valor))
            {
                e.CellStyle.BackColor = Color.LightCoral;
                e.CellStyle.ForeColor = Color.White;
                return;
            }

            // --- CASO C: Validación Especial de Cuentas (Columna 2 - Índice 1) ---
            // Solo si no está vacía y es la columna de cuenta
            if (e.ColumnIndex == 1)
            {
                int largo = valor.Length;
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

            // --- CASO D: Celdas con datos válidos ---
            e.CellStyle.BackColor = Color.White;
            e.CellStyle.ForeColor = Color.Black;
        }
    }

}
