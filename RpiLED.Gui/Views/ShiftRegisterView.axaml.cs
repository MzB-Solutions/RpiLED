using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RpiLed.Gui.Views
{
    public partial class ShiftRegisterView : UserControl
    {
        public ShiftRegisterView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
