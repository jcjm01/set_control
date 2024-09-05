using System.Windows;

namespace SetControl_WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Inicia la aplicación con la ventana de login
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}
