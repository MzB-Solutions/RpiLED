using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RpiLed.Gui.Views
{
    public partial class MainWindowView : Window
    {
        #region Private Methods

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        #endregion Private Methods

        #region Public Constructors

        public MainWindowView()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        #endregion Public Constructors
    }
}