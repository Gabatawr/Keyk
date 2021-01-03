using System.Windows;
using System.Windows.Input;
using Keyk.Infrastructure.Commands;
using Keyk.Infrastructure.Commands.Base;
using Keyk.ViewModels.Base;

namespace Keyk.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            Symbols = (Keyboard.IsKeyToggled(Key.CapsLock)) ? _symbolsShift : _symbols;
            CommandKey = _commandKey;
        }

        #region Commands
        //------------------------------------------------------------------------------

        #region Command : _MoveApplicationCommand
        //--------------------------------------------------------------------

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

        //--------------------------------------------------------------------
        #endregion

        #region Command : _DifficultyChangedCommand
        //--------------------------------------------------------------------

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

        //--------------------------------------------------------------------
        #endregion

        #region Command : _ShiftDownCommand
        //--------------------------------------------------------------------

        private Command _ShiftDownCommand;
        public Command ShiftDownCommand
        {
            get => _ShiftDownCommand ?? new ActionCommand
            (
                param => ExecuteShiftDownCommand((KeyEventArgs)param),
                param => CanExecuteShiftDownCommand((KeyEventArgs)param)
            );
            set => _ShiftDownCommand = value;
        }

        private void ExecuteShiftDownCommand(KeyEventArgs e)
        {
            Symbols = (Keyboard.IsKeyToggled(Key.CapsLock)) ? _symbols : _symbolsShift;
        }
        private bool CanExecuteShiftDownCommand(KeyEventArgs e) => e.Key == Key.LeftShift || e.Key == Key.RightShift;

        //--------------------------------------------------------------------
        #endregion

        #region Command : _ShiftUpCommand
        //--------------------------------------------------------------------

        private Command _ShiftUpCommand;
        public Command ShiftUpCommand
        {
            get => _ShiftUpCommand ?? new ActionCommand
            (
                param => ExecuteShiftUpCommand((KeyEventArgs)param),
                param => CanExecuteShiftUpCommand((KeyEventArgs)param)
            );
            set => _ShiftUpCommand = value;
        }

        private void ExecuteShiftUpCommand(KeyEventArgs e)
        {
            Symbols = (Keyboard.IsKeyToggled(Key.CapsLock)) ? _symbolsShift : _symbols;
        }
        private bool CanExecuteShiftUpCommand(KeyEventArgs e) => e.Key == Key.LeftShift || e.Key == Key.RightShift;

        //--------------------------------------------------------------------
        #endregion

        #region Command : _CapsLockDownCommand
        //--------------------------------------------------------------------

        private Command _CapsLockDownCommand;
        public Command CapsLockDownCommand
        {
            get => _CapsLockDownCommand ?? new ActionCommand
            (
                param => ExecuteCapsLockDownCommand((KeyEventArgs)param),
                param => CanExecuteCapsLockDownCommand((KeyEventArgs)param)
            );
            set => _CapsLockDownCommand = value;
        }

        private void ExecuteCapsLockDownCommand(KeyEventArgs e)
        {
            Symbols = (Keyboard.IsKeyToggled(Key.CapsLock)) ? _symbolsShift : _symbols;
        }
        private bool CanExecuteCapsLockDownCommand(KeyEventArgs e) => e.Key == Key.CapsLock;

        //--------------------------------------------------------------------
        #endregion

        //------------------------------------------------------------------------------
        #endregion

        #region Properties
        //------------------------------------------------------------------------------

        #region double : _Difficulty
        //---------------------------------------------------------------------
        private double _Difficulty = 0.2;
        public double Difficulty
        {
            get => _Difficulty;
            set => Set(ref _Difficulty, value);
        }
        //---------------------------------------------------------------------
        #endregion

        #region Symbols

        private readonly char[] _symbols = new[]
        {
            '`', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '=',
            'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', '[', ']', '\\',
            'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ';', '\'',
            'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/',
        };
        private readonly char[] _symbolsShift = new[]
        {
            '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+',
            'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', '{', '}', '|',
            'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', ':', '"',
            'Z', 'X', 'C', 'V', 'B', 'N', 'M', '<', '>', '?',
        };

        #region char[] : _Symbols
        //---------------------------------------------------------------------

        private char[] _Symbols;
        public char[] Symbols
        {
            get => _Symbols;
            set => Set(ref _Symbols, value);
        }

        //---------------------------------------------------------------------
        #endregion

        private readonly string[] _commandKey = new[]
        {
            "Backspace",
            "Tab",
            "Caps Lock", "Enter",
            "Shift",
            "Ctrl", "Win", "Alt", "Space"
        };

        #region string[] : _CommandKey
        //---------------------------------------------------------------------

        private string[] _CommandKey;
        public string[] CommandKey
        {
            get => _CommandKey;
            set => Set(ref _CommandKey, value);
        }

        //---------------------------------------------------------------------
        #endregion

        #endregion

        //------------------------------------------------------------------------------
        #endregion
    }
}
