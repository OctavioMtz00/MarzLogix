using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Layouts_K2
{
    public partial class Nominas : Form
    {
        public Nominas()
        {
            InitializeComponent();
        }

        private void btnMP1_Click(object sender, EventArgs e)
        {
            Form frm = new Bienvenida();
            frm.Show();
            this.Close();
        }

        private void Nominas_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Application.Exit();
        }

        private void Nominas_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* // Verificamos si el cierre fue iniciado por el usuario (X, Alt+F4, etc.)
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

        private void btnSalir_Click(object sender, EventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void PegarDesdeExcel()
        {
            try
            {
                string textoCopiado = Clipboard.GetText();
                if (string.IsNullOrEmpty(textoCopiado)) return;

                string[] lineas = textoCopiado.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                int cantidadFilasReales = lineas.Length;
                if (cantidadFilasReales > 0 && string.IsNullOrWhiteSpace(lineas[cantidadFilasReales - 1]))
                {
                    cantidadFilasReales--;
                }

                if (cantidadFilasReales <= 0) return;

                int filaInicio = dataGridView1.CurrentCell.RowIndex;
                int colInicio = dataGridView1.CurrentCell.ColumnIndex;

                int filasNecesariasTotal = filaInicio + cantidadFilasReales;
                int filasExistentes = dataGridView1.AllowUserToAddRows ? dataGridView1.Rows.Count - 1 : dataGridView1.Rows.Count;

                if (filasNecesariasTotal > filasExistentes)
                {
                    dataGridView1.Rows.Add(filasNecesariasTotal - filasExistentes);
                }

                for (int i = 0; i < cantidadFilasReales; i++)
                {
                    string[] celdas = lineas[i].Split('\t');

                    for (int j = 0; j < celdas.Length; j++)
                    {
                        int colDestino = colInicio + j;

                        // Solo procesamos si está dentro de las 3 columnas (0, 1, 2)
                        if (colDestino >= 0 && colDestino < 3)
                        {
                            string valorCelda = celdas[j];

                            // --- VALIDACIÓN: Si es la columna de IMPORTE (índice 2) ---
                            if (colDestino == 2)
                            {
                                // Limpiamos $, comas y espacios. 
                                // Usamos tu función LimpiarMonto o la lógica directa:
                                valorCelda = valorCelda.Replace("$", "").Replace(",", "").Trim();

                                // Opcional: Forzar formato de 2 decimales al pegar
                                if (decimal.TryParse(valorCelda, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal monto))
                                {
                                    valorCelda = monto.ToString("f2");
                                }
                            }

                            dataGridView1.Rows[filaInicio + i].Cells[colDestino].Value = valorCelda;
                        }
                    }
                }

                ActualizarSumaImportes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al pegar datos: " + ex.Message, "Error de Portapapeles", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnCNOM_Click(object sender, EventArgs e)
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

        private void btnLONOM_Click(object sender, EventArgs e)
        {
            // 1. Validación de integridad (Cuentas de 10 o 18 dígitos)
            bool hayErrores = false;
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.IsNewRow) continue;
                string cuenta = fila.Cells[1].Value?.ToString().Trim() ?? "";
                if (cuenta.Length != 10 && cuenta.Length != 18)
                {
                    hayErrores = true;
                    break;
                }
            }

            if (hayErrores)
            {
                MessageBox.Show("No se puede exportar: Hay cuentas que no tienen 10 o 18 dígitos.",
                                "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Proceso de guardado
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivo de texto (*.txt)|*.txt";
            saveFileDialog.Title = "Guardar Layout de Nómina";
            saveFileDialog.FileName = "Layout_Nomina_" + DateTime.Now.ToString("yyyyMMdd");

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.Default))
                    {
                        int contadorLineas = 1;

                        foreach (DataGridViewRow fila in dataGridView1.Rows)
                        {
                            if (fila.IsNewRow) continue;

                            char[] linea = new string(' ', 108).ToCharArray();

                            // --- OBTENCIÓN DE VALORES ---
                            string nombreTitular = fila.Cells[0].Value?.ToString().Trim() ?? "";
                            string col2_Cuenta = fila.Cells[1].Value?.ToString().Trim() ?? "";
                            string col3_Importe = fila.Cells[2].Value?.ToString().Trim() ?? "";
                            int largoCuenta = col2_Cuenta.Length;

                            // 1. Número de Registro (Pos 1-9)
                            InsertarCampoManual(ref linea, contadorLineas.ToString().PadLeft(9, '0'), 0, 9);

                            // 3. Valor 01 o 40 (Pos 26-27)
                            string val26_27 = (largoCuenta == 18) ? "40" : "01";
                            InsertarCampoManual(ref linea, val26_27, 25, 2);

                            // 4. Cuenta (Pos 28-47)
                            InsertarCampoManual(ref linea, col2_Cuenta.PadRight(20, ' '), 27, 20);

                            // 5. --- PROCESAMIENTO DE IMPORTE (Pos 48-62) ---
                            // Usamos la lógica de limpieza para quitar $, comas y espacios
                            string importeLimpioParaCalculo = LimpiarMonto(col3_Importe);
                            string importeFinalParaArchivo = "000"; // Valor por defecto

                            if (decimal.TryParse(importeLimpioParaCalculo, System.Globalization.NumberStyles.Any,
                                System.Globalization.CultureInfo.InvariantCulture, out decimal montoDecimal))
                            {
                                // "f2" garantiza los dos decimales (ej: 5 -> "5.00")
                                string conDosDecimales = montoDecimal.ToString("f2", System.Globalization.CultureInfo.InvariantCulture);

                                // Quitamos el punto decimal para el archivo plano (ej: "5.00" -> "500")
                                importeFinalParaArchivo = conDosDecimales.Replace(".", "").Replace(",", "");
                            }

                            // Rellenamos a 15 caracteres con ceros a la izquierda
                            InsertarCampoManual(ref linea, importeFinalParaArchivo.PadLeft(15, '0'), 47, 15);

                            // 6. Nombre Titular (Pos 63-102) - Forzamos Mayúsculas
                            InsertarCampoManual(ref linea, nombreTitular.ToUpper().PadRight(40, ' '), 62, 40);

                            // --- LÓGICA 103-108 ---
                            if (largoCuenta == 18)
                            {
                                InsertarCampoManual(ref linea, col2_Cuenta.Substring(0, 3), 102, 3);
                            }
                            else
                            {
                                InsertarCampoManual(ref linea, "012", 102, 3);
                            }

                            InsertarCampoManual(ref linea, "000", 105, 3);

                            // Escribimos la línea final (se puede agregar .ToUpper() por seguridad extra)
                            sw.WriteLine(new string(linea).ToUpper());
                            contadorLineas++;
                        }
                    }
                    MessageBox.Show("Archivo de nómina generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al escribir el archivo: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Evitar validar encabezados o la fila vacía del final (*)
            if (e.RowIndex < 0 || dataGridView1.Rows[e.RowIndex].IsNewRow) return;

            // Obtener el valor actual de la celda de forma segura
            string valor = e.Value?.ToString().Trim() ?? "";

            // --- REGLA GENERAL: VALIDACIÓN DE VACÍOS ---
            if (string.IsNullOrWhiteSpace(valor))
            {
                e.CellStyle.BackColor = Color.LightCoral;
                e.CellStyle.ForeColor = Color.White;
                return; // Salimos de la función ya que el rojo de "vacío" es prioritario
            }

            // --- REGLA ESPECÍFICA: COLUMNA DE CUENTA (Índice 1) ---
            if (e.ColumnIndex == 1)
            {
                int largo = valor.Length;

                if (largo == 10 || largo == 18)
                {
                    e.CellStyle.BackColor = Color.LightGreen; // Correcto
                    e.CellStyle.ForeColor = Color.Black;
                }
                else
                {
                    e.CellStyle.BackColor = Color.LightCoral; // Incorrecto (Largo inválido)
                    e.CellStyle.ForeColor = Color.White;
                }
            }
            else
            {
                // --- COLUMNAS NOMBRE (0) E IMPORTE (2) ---
                // Si tienen texto (ya validado arriba), restauramos el color normal
                e.CellStyle.BackColor = Color.White;
                e.CellStyle.ForeColor = Color.Black;
            }
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Si se editó la columna del importe (índice 2)
            if (e.ColumnIndex == 2)
            {
                ActualizarSumaImportes();
            }
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            // Verificamos que sea la columna de Importe (Columna 3, Índice 2 en tu formulario de Nómina)
            // Nota: Si en este form el importe es la columna 4, cambia el 2 por un 3.
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                var celda = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (celda.Value != null && !string.IsNullOrWhiteSpace(celda.Value.ToString()))
                {
                    string valorEntrada = celda.Value.ToString();

                    // 1. Aplicamos la misma limpieza que en el pegado (quitar $ y comas)
                    string limpio = valorEntrada.Replace("$", "").Replace(",", "").Trim();

                    // 2. Intentar convertir a decimal para forzar los dos decimales
                    // Usamos InvariantCulture para que siempre reconozca el punto decimal
                    if (decimal.TryParse(limpio, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out decimal monto))
                    {
                        // El formato "f2" lo deja como "0.00"
                        celda.Value = monto.ToString("f2", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else if (decimal.TryParse(limpio, out monto))
                    {
                        // Segundo intento por si el Windows del usuario usa comas
                        celda.Value = monto.ToString("f2");
                    }

                    // 3. Actualizamos la suma total del formulario
                    ActualizarSumaImportes();
                }
            }
        }
    }
}
