using System;
using System.Windows;

namespace SystemHub
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Application.Current.DispatcherUnhandledException += (sender, args) =>
            {
                MessageBox.Show(args.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Handled = true;
            };
            Services.ServiceLocator.Initialize();
        }
        
        protected override void OnExit(ExitEventArgs e)
        {
            Services.ServiceLocator.Cleanup();
            base.OnExit(e);
        }
    }
}
