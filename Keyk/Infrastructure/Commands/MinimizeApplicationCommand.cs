using System.Windows;
using Keyk.Infrastructure.Commands.Base;

namespace Keyk.Infrastructure.Commands
{
    class CloseApplicationCommand : Command
    {
        public override void Execute(object parameter) => Application.Current.Shutdown();
        public override bool CanExecute(object parameter) => true;
    }
}
