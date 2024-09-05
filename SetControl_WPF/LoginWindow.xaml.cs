using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Gma.System.MouseKeyHook;

namespace SetControl_WPF
{
    public partial class LoginWindow : Window
    {
        private IKeyboardMouseEvents _hook;
        private DatabaseManager dbManager;
        private int currentUserId;

        public LoginWindow()
        {
            InitializeComponent();
            dbManager = new DatabaseManager();
            Subscribe();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (VerifyCredentials(username, password))
            {
                bool isSuperUser = username == "admin";
                currentUserId = GetUserId(username);
                RegisterLogin(currentUserId);

                // Aquí es donde se decide si abrir el MainWindow
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close(); // Cierra la ventana de login
            }
            else
            {
                MessageBox.Show("Credenciales inválidas");
            }
        }

        private bool VerifyCredentials(string username, string password)
        {
            // Aquí puedes implementar la lógica de autenticación
            // Por simplicidad, se usa un chequeo básico
            return (username == "admin" && password == "admin") ||
                   (username == "user" && password == "user");
        }

        private void RegisterLogin(int userId)
        {
            dbManager.LogLogin(userId, GetJulianDate(DateTime.Now));
        }

        private int GetUserId(string username)
        {
            return username == "admin" ? 1 : 2;
        }

        private static double GetJulianDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            double day = date.Day + (date.Hour / 24.0) + (date.Minute / 1440.0) + (date.Second / 86400.0);

            if (month <= 2)
            {
                year -= 1;
                month += 12;
            }

            int A = year / 100;
            int B = 2 - A + (A / 4);

            double JD = Math.Floor(365.25 * (year + 4716)) +
                        Math.Floor(30.6001 * (month + 1)) +
                        day + B - 1524.5;

            return JD;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _hook = Hook.GlobalEvents();
            _hook.KeyDown += OnKeyDown;
        }

        private void Unsubscribe()
        {
            _hook.KeyDown -= OnKeyDown;
            _hook.Dispose();
        }

        private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                RegisterButtonClick("Red(F1)");
            }
            if (e.KeyCode == System.Windows.Forms.Keys.F2)
            {
                RegisterButtonClick("Mark(F2)");
            }
        }

        private void RegisterButtonClick(string buttonName)
        {
            dbManager.LogEvent(currentUserId, buttonName, GetJulianDate(DateTime.Now));
        }

        
    }
}
