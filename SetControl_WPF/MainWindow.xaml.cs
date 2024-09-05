using QRCoder;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace SetControl_WPF
{
    public partial class MainWindow : Window
    {
        private WriteableBitmap qrPreviewImage;
        private string logFilePath = "loginHistory.txt"; // Ruta del archivo de historial de logins

        public MainWindow()
        {
            InitializeComponent();
            EnsureRunAsAdmin(); // Verificar ejecución como administrador
            SetInitialStatus();
            ClearPreview(); // Limpiar la vista previa al inicio
            LogLogin(); // Registrar login al iniciar
        }

        private void EnsureRunAsAdmin()
        {
            if (!IsRunAsAdmin())
            {
                try
                {
                    var processInfo = new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        FileName = System.Reflection.Assembly.GetExecutingAssembly().Location,
                        Verb = "runas"
                    };

                    Process.Start(processInfo);
                    Application.Current.Shutdown(); // Cierra la instancia actual
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al intentar ejecutar como administrador: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool IsRunAsAdmin()
        {
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);
            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void LogLogin()
        {
            try
            {
                string userType = IsRunAsAdmin() ? "admin" : "user";
                string julianDate = DateTime.Now.DayOfYear.ToString();
                string time = DateTime.Now.ToString("HH:mm:ss");
                string logEntry = $"{userType}; {julianDate} {time}";

                // Guardar en un archivo
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el login: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetInitialStatus()
        {
            try
            {
                pbStatusLed.Source = new BitmapImage(new Uri("Images/led_off.png", UriKind.Relative));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la imagen inicial: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            lblEstado.Content = "Estado: Detenido";
            progressBarMarcado.Value = 0;
            btnIniciarMarcado.IsEnabled = false;  // Deshabilita el botón de iniciar marcado hasta previsualizar
        }

        private void ClearPreview()
        {
            textPreviewProducto.Text = string.Empty;
            textPreviewLote.Text = string.Empty;
            textPreviewElab.Text = string.Empty;
            textPreviewCad.Text = string.Empty;
            pictureBoxQRCode.Source = null;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close(); // Cierra la ventana actual
        }

        private void btnPrevisualizar_Click(object sender, RoutedEventArgs e)
        {
            ClearPreview();
            pictureBoxQRCode.Source = null;  // Limpiar la imagen anterior
            qrPreviewImage = GenerarQRCode();

            if (qrPreviewImage != null)
            {
                pictureBoxQRCode.Source = qrPreviewImage;

                // Actualiza el texto de la vista previa
                string tipoProducto = textBoxTipoProducto.Text;
                string lote = textBoxLote.Text;
                string elaboracion = dateTimePickerElaboracion.SelectedDate?.ToString("ddMMyyyy") ?? string.Empty;
                string caducidad = dateTimePickerCaducidad.SelectedDate?.ToString("ddMMyyyy") ?? string.Empty;

                textPreviewProducto.Text = tipoProducto;
                textPreviewLote.Text = $"LOTE: {lote}";
                textPreviewElab.Text = $"ELAB: {elaboracion}";
                textPreviewCad.Text = $"CAD: {caducidad}";

                btnIniciarMarcado.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Error al generar el código QR. Por favor, revise los datos ingresados.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private WriteableBitmap GenerarQRCode()
        {
            try
            {
                string tipoProducto = textBoxTipoProducto.Text;
                string lote = textBoxLote.Text;
                string elaboracion = dateTimePickerElaboracion.SelectedDate?.ToString("ddMMyyyy") ?? string.Empty;
                string caducidad = dateTimePickerCaducidad.SelectedDate?.ToString("ddMMyyyy") ?? string.Empty;

                string qrData = $"{tipoProducto}\nLOTE {lote}\nELAB {elaboracion}\nCAD {caducidad}";

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);

                var qrImage = ConvertQRCodeToBitmapImage(qrCodeData, 3000, 3000);  // Tamaño ajustado

                MessageBox.Show($"Tamaño del QR: {qrImage.PixelWidth}x{qrImage.PixelHeight}");  // Mensaje de depuración

                return qrImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el código QR: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private WriteableBitmap ConvertQRCodeToBitmapImage(QRCodeData qrCodeData, int width, int height)
        {
            try
            {
                var bitmap = new WriteableBitmap(200, 200, 96, 96, PixelFormats.Gray8, null);

                bitmap.Lock();
                for (int y = 0; y < qrCodeData.ModuleMatrix.Count; y++)
                {
                    for (int x = 0; x < qrCodeData.ModuleMatrix.Count; x++)
                    {
                        byte color = qrCodeData.ModuleMatrix[y][x] ? (byte)0 : (byte)255;
                        bitmap.WritePixels(new Int32Rect(x, y, 1, 1), new byte[] { color }, 1, 0);
                    }
                }
                bitmap.Unlock();

                return bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al convertir el código QR a imagen: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private void btnGuardarQR_Click(object sender, RoutedEventArgs e)
        {
            if (qrPreviewImage == null)
            {
                MessageBox.Show("Por favor, previsualiza el QR antes de guardar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SaveQRCodeWithText();
        }


        private void SaveQRCodeWithText()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png",
                    Title = "Guardar Código QR",
                    FileName = "CodigoQR.png"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    // Ajusta el tamaño del área para QR y texto
                    RenderTargetBitmap renderBitmap = new RenderTargetBitmap(800, 400, 96, 96, PixelFormats.Pbgra32); // Área más grande para el QR y el texto
                    DrawingVisual dv = new DrawingVisual();
                    using (DrawingContext dc = dv.RenderOpen())
                    {
                        // Agregar un fondo blanco para que el contenido sea visible
                        dc.DrawRectangle(Brushes.White, null, new Rect(new Size(800, 400)));

                        // Dibuja la imagen QR de tamaño 200x200
                        dc.DrawImage(qrPreviewImage, new Rect(20, 20, 150, 150));  // Ajustar tamaño del QR

                        // Ajustar formato y alineación del texto
                        FormattedText formattedText = new FormattedText(
                            $"{textPreviewProducto.Text}\nLOTE: {textPreviewLote.Text}\nELAB: {textPreviewElab.Text}\nCAD: {textPreviewCad.Text}",
                            System.Globalization.CultureInfo.InvariantCulture,
                            FlowDirection.LeftToRight,
                            new Typeface("Arial"),
                            18,  // Tamaño de fuente ajustado
                            Brushes.Black,
                            VisualTreeHelper.GetDpi(this).PixelsPerDip
                        );

                        // Alineación del texto con mayor espacio desde el QR (margen de 200px desde la izquierda)
                        dc.DrawText(formattedText, new Point(200, 50));
                    }

                    renderBitmap.Render(dv);

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                        encoder.Save(fileStream);
                    }

                    MessageBox.Show("Código QR guardado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el código QR: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void comboBoxBidon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Aquí puedes manejar el cambio de selección del bidón
        }

        private void ShowLoginHistory()
        {
            if (File.Exists(logFilePath))
            {
                string[] logEntries = File.ReadAllLines(logFilePath);
                string logHistory = string.Join(Environment.NewLine, logEntries);
                MessageBox.Show(logHistory, "Historial de Logins", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No hay historial de logins disponible.", "Historial", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnIniciarMarcado_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Iniciando el proceso de marcado...");
        }

        private void btnShowHistory_Click(object sender, RoutedEventArgs e)
        {
            ShowLoginHistory();
        }
    }
}
