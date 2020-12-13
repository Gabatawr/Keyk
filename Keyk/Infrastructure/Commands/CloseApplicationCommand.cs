using System.Windows;
using Keyk.Infrastructure.Commands.Base;

namespace Keyk.Infrastructure.Commands
{
    class MinimizeApplicationCommand : Command
    {
        public override void Execute(object parameter) => Application.Current.MainWindow.WindowState = WindowState.Minimized;
        public override bool CanExecute(object parameter) => true;
    }
}
