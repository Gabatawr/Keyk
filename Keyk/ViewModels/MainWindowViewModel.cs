using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Keyk.Infrastructure.Commands;
using Keyk.Infrastructure.Commands.Base;
using Keyk.ViewModels.Base;

namespace Keyk.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Commands

        #region Command : _MoveApplicationCommand

        private Command _MoveApplicationCommand;
        public Command MoveApplicationCommand
        {
            get => _MoveApplicationCommand ?? new ActionCommand
                (
                    param => ExecuteMoveApplicationCommand((MouseEventArgs)param),
                    param => CanExecuteMoveApplicationCommand((MouseEventArgs)param)
                );
            set => _MoveApplicationCommand = value;
        }

        private void ExecuteMoveApplicationCommand(MouseEventArgs e) => Application.Current.MainWindow?.DragMove();
        private bool CanExecuteMoveApplicationCommand(MouseEventArgs e) => ((FrameworkElement)e.Source) == Application.Current.MainWindow;

        #endregion

        #region Command : _DifficultyChangedCommand

        private Command _DifficultyChangedCommand;
        public Command DifficultyChangedCommand
        {
            get => _DifficultyChangedCommand ?? new ActionCommand
                (
                    param => ExecuteDifficultyChangedCommand((MouseEventArgs)param),
                    param => CanExecuteDifficultyChangedCommand((MouseEventArgs)param)
                );
            set => _DifficultyChangedCommand = value;
        }

        private void ExecuteDifficultyChangedCommand(MouseEventArgs e) => Difficulty = e.GetPosition((IInputElement)e.Source).X / ((FrameworkElement)e.Source).ActualWidth;
        private bool CanExecuteDifficultyChangedCommand(MouseEventArgs e) => e.LeftButton == MouseButtonState.Pressed;

        #endregion

        #endregion

        #region Properties

        #region double : _Difficulty

        private double _Difficulty = 0.2;

        /// <summary>Difficulty level</summary>
        public double Difficulty
        {
            get => _Difficulty;
            set => Set(ref _Difficulty, value);
        }

        #endregion

        #endregion
    }
}
