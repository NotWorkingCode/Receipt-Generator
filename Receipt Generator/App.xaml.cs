using System.Windows;

namespace Receipt_Generator
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            LogCore.Initialization();
            LogCore.Debug("LogCore successfully launched!");
            LogCore.Info("The application is running!");
        }
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            LogCore.Info("The application is stop!");
            LogCore.Destroy();
        }
    }
}
