using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RpiLED.Gui.Views
{
    public partial class LED_control : UserControl
    {
        public LED_control()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
