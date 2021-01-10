using System;
using Keyk.Infrastructure.Commands;
using Keyk.Infrastructure.Commands.Base;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Keyk.Models;

namespace Keyk.ViewModels
{
    partial class MainWindowViewModel
    {
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

        private void ExecuteDifficultyChangedCommand(MouseEventArgs e)
        {
            Difficulty = e.GetPosition((IInputElement)e.Source).X / ((FrameworkElement)e.Source).ActualWidth;

            ShowText.Clear();
            PrintText.Clear();
            FailsValue = 0;

            for (int i = 100; i > 0; --i) GenerateSymbol();

            OpacityChangedCorrectBtn();
            OpacityChangedNextBtn();
        }
        private bool CanExecuteDifficultyChangedCommand(MouseEventArgs e) => e.LeftButton == MouseButtonState.Pressed;

        //--------------------------------------------------------------------
        #endregion

        #region Command : _BackspaceDownCommand
        //--------------------------------------------------------------------

        private Command _BackspaceDownCommand;
        public Command BackspaceDownCommand
        {
            get => _BackspaceDownCommand ?? new ActionCommand
            (
                param => ExecuteBackspaceDownCommand((KeyEventArgs)param),
                param => CanExecuteBackspaceDownCommand((KeyEventArgs)param)
            );
            set => _BackspaceDownCommand = value;
        }

        private void ExecuteBackspaceDownCommand(KeyEventArgs e)
        {
            if (PrintText.Count != 0) PrintText.RemoveAt(PrintText.Count - 1);

            OpacityChangedCorrectBtn();
            OpacityChangedNextBtn();
        }
        private bool CanExecuteBackspaceDownCommand(KeyEventArgs e) => e.Key == Key.Back;

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
        private void ExecuteShiftDownCommand(KeyEventArgs e) => Symbols = Keyboard.IsKeyToggled(Key.CapsLock) ? _symbols : _symbolsShift;
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
        private void ExecuteShiftUpCommand(KeyEventArgs e) => Symbols = Keyboard.IsKeyToggled(Key.CapsLock) ? _symbolsShift : _symbols;
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
        private void ExecuteCapsLockDownCommand(KeyEventArgs e) => Symbols = Keyboard.IsKeyToggled(Key.CapsLock) ? _symbolsShift : _symbols;
        private bool CanExecuteCapsLockDownCommand(KeyEventArgs e) => e.Key == Key.CapsLock;

        //--------------------------------------------------------------------
        #endregion

        #region Command : _PrintKeyDownCommand
        //--------------------------------------------------------------------

        private Command _PrintKeyDownCommand;
        public Command PrintKeyDownCommand
        {
            get => _PrintKeyDownCommand ?? new ActionCommand
            (
                param => ExecutePrintKeyDownCommand((KeyEventArgs)param),
                param => CanExecutePrintKeyDownCommand((KeyEventArgs)param)
            );
            set => _PrintKeyDownCommand = value;
        }

        private void ExecutePrintKeyDownCommand(KeyEventArgs e)
        {
            for (int i = 0; i < _keys.Length; i++)
            {
                if (e.Key == _keys[i])
                {
                    PrintText.Add(new Symbol() { Char = Symbols[i] });

                    if (Application.Current.MainWindow?.FindName("Key" + i) is Button btn) btn.Opacity = 1;

                    if (PrintText[^1].Char != ShowText[PrintText.Count - 1].Char)
                    {
                        PrintText[^1].Color = Brushes.Red;
                        OpacityChangedCorrectBtn();
                        FailsValue++;
                    }

                    if (PrintText.Count >= 10)
                    {
                        ShowText.RemoveAt(0);
                        PrintText.RemoveAt(0);
                        GenerateSymbol();
                    }

                    _countPressed++;
                    if (_timer.IsRunning)
                    {
                        int s = _timer.Elapsed.Seconds;
                        if (s > 0) SpeedValue = $"{Math.Round((_countPressed / s * 60), 0)}";
                    }

                    break;
                }
            }
        }

        private bool CanExecutePrintKeyDownCommand(KeyEventArgs e)
        {
            foreach (var k in _keys) 
                if (e.Key == k) return true;

            OpacityChanged(e.Key, 1);
            return false;
        }

        //--------------------------------------------------------------------
        #endregion

        #region Command : _PrintKeyUpCommand
        //--------------------------------------------------------------------

        private Command _PrintKeyUpCommand;
        public Command PrintKeyUpCommand
        {
            get => _PrintKeyUpCommand ?? new ActionCommand
            (
                param => ExecutePrintKeyUpCommand((KeyEventArgs)param),
                param => CanExecutePrintKeyUpCommand((KeyEventArgs)param)
            );
            set => _PrintKeyUpCommand = value;
        }

        private void ExecutePrintKeyUpCommand(KeyEventArgs e)
        {
            for (int i = 0; i < _keys.Length; i++)
            {
                if (e.Key == _keys[i])
                {
                    if (Application.Current.MainWindow?.FindName("Key" + i) is Button btn) btn.Opacity = 0.6;

                    OpacityChangedNextBtn();
                    break;
                }
            }
        }

        private bool CanExecutePrintKeyUpCommand(KeyEventArgs e)
        {
            foreach (var k in _keys)
                if (e.Key == k) return true;

            OpacityChanged(e.Key, 0.6);
            return false;
        }

        //--------------------------------------------------------------------
        #endregion

        #region Command : _StopCommand
        //--------------------------------------------------------------------

        private Command _StopCommand;
        public Command StopCommand
        {
            get => _StopCommand ?? new ActionCommand
            (
                param => ExecuteStopCommand((RoutedEventArgs)param),
                param => CanExecuteStopCommand((RoutedEventArgs)param)
            );
            set => _StopCommand = value;
        }
        private void ExecuteStopCommand(RoutedEventArgs e)
        {
            _timer.Reset();

            StartVisibility = Visibility.Visible;
            StopVisibility = Visibility.Collapsed;

            _countPressed = 0;
        }
        private bool CanExecuteStopCommand(RoutedEventArgs e) => true;

        //--------------------------------------------------------------------
        #endregion

        #region Command : _StartCommand
        //--------------------------------------------------------------------

        private Command _StartCommand;
        public Command StartCommand
        {
            get => _StartCommand ?? new ActionCommand
                (
                    param => ExecuteStartCommand((RoutedEventArgs)param),
                    param => CanExecuteStartCommand((RoutedEventArgs)param)
                );
            set => _StartCommand = value;
        }
        private void ExecuteStartCommand(RoutedEventArgs e)
        {
            _timer.Start();

            StartVisibility = Visibility.Collapsed;
            StopVisibility = Visibility.Visible;

            SpeedValue = "0";
            FailsValue = 0;
        }
        private bool CanExecuteStartCommand(RoutedEventArgs e) => true;

        //--------------------------------------------------------------------
        #endregion

        //------------------------------------------------------------------------------
        #endregion
    }
}