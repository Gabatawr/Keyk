using System.Windows;
using System.Windows.Controls;

namespace Keyk.Views.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IsTabStopProperty.OverrideMetadata(typeof(Control), new FrameworkPropertyMetadata(false));
        }
    }
}
