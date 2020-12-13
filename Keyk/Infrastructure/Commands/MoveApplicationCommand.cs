using System.Windows;
using Keyk.Infrastructure.Commands.Base;

namespace Keyk.Infrastructure.Commands
{
    class MoveApplicationCommand : Command
    {
        public override void Execute(object parameter) => Application.Current.MainWindow.DragMove();
        public override bool CanExecute(object parameter) => true;
    }
}
