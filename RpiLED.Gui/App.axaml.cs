using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using RpiLed.Gui.ViewModels;
using RpiLed.Gui.Views;

namespace RpiLed.Gui
{
    public class App : Application
    {
        #region Public Methods

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindowView
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        #endregion Public Methods
    }
}