using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using Keyk.Infrastructure.Commands;
using Keyk.Infrastructure.Commands.Base;
using Keyk.ViewModels.Base;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using Keyk.Views.Windows;

namespace Keyk.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Clases
        public class Symbol
        {
            public char Char { get; set; }
            public SolidColorBrush Color { get; set; } = Brushes.White;
        }

        #endregion

        #region Methods

        private void OpacityChanged(Key k, double o)
        {
            for (int i = 0; i < _ckeys.Length; i++)
            {
                if (k == _ckeys[i])
                {
                    if (k == Key.System && Keyboard.IsKeyDown(Key.RightAlt)) i++;
                    if (Application.Current.MainWindow?.FindName("CKey" + i) is Button btn) btn.Opacity = o;
                    if (k == Key.System && Keyboard.IsKeyDown(Key.LeftAlt)) break;
                }
            }
        }

        #endregion

        #region Fields
        //------------------------------------------------------------------------------

        private readonly Key[] _keys = new[]
        {
            Key.Oem3, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9, Key.D0, Key.OemMinus, Key.OemPlus,
            Key.Q, Key.W, Key.E, Key.R, Key.T, Key.Y, Key.U, Key.I, Key.O, Key.P, Key.OemOpenBrackets, Key.Oem6, Key.Oem5,
            Key.A, Key.S, Key.D, Key.F, Key.G, Key.H, Key.J, Key.K, Key.L, Key.Oem1, Key.OemQuotes,
            Key.Z, Key.X, Key.C, Key.V, Key.B, Key.N, Key.M, Key.OemComma, Key.OemPeriod, Key.OemQuestion, Key.Space
        };
        private readonly Key[] _ckeys = new[]
        {
            Key.Back,
            Key.Tab,
            Key.CapsLock, Key.Enter,
            Key.LeftShift, Key.RightShift,
            Key.LeftCtrl, Key.LWin, Key.System, Key.System, Key.RWin, Key.RightCtrl
        };
        private readonly char[] _symbols = new[]
        {
            '`', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '=',
            'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', '[', ']', '\\',
            'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ';', '\'',
            'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/', ' '
        };
        private readonly char[] _symbolsShift = new[]
        {
            '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+',
            'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', '{', '}', '|',
            'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', ':', '"',
            'Z', 'X', 'C', 'V', 'B', 'N', 'M', '<', '>', '?', ' '
        };
        private readonly string[] _commandKey = new[]
        {
            "Backspace",
            "Tab",
            "Caps Lock", "Enter",
            "Shift",
            "Ctrl", "Win", "Alt", "Space"
        };

        //------------------------------------------------------------------------------
        #endregion

        public MainWindowViewModel()
        {
            Symbols = Keyboard.IsKeyToggled(Key.CapsLock) ? _symbolsShift : _symbols;
            CommandKey = _commandKey;

            ShowText = new();
            PrintText = new();

            ShowText.Add(new Symbol() { Char = '@', Color = Brushes.Red} );
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
                    if (Application.Current.MainWindow?.FindName("Key" + i) is Button btn) btn.Opacity = 0.6;
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

        #region ObservableCollection<Symbol> : _PrintText
        //---------------------------------------------------------------------

        private ObservableCollection<Symbol> _PrintText;
        public ObservableCollection<Symbol> PrintText
        {
            get => _PrintText;
            set => Set(ref _PrintText, value);
        }

        //---------------------------------------------------------------------
        #endregion

        #region ObservableCollection<Symbol> : _ShowText
        //---------------------------------------------------------------------

        private ObservableCollection<Symbol> _ShowText;
        public ObservableCollection<Symbol> ShowText
        {
            get => _ShowText;
            set => Set(ref _ShowText, value);
        }

        //---------------------------------------------------------------------
        #endregion

        //------------------------------------------------------------------------------
        #endregion
    }
}