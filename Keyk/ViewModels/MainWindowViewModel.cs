using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Timers;
using Keyk.Infrastructure.Commands;
using Keyk.Infrastructure.Commands.Base;
using Keyk.ViewModels.Base;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        private void OpacityChangedNextBtn()
        {
            char c = ShowText[PrintText.Count].Char;
            int p = Array.IndexOf(_symbols, c);
            if (p == -1) p = Array.IndexOf(_symbolsShift, c);

            if (Application.Current.MainWindow?.FindName("Key" + p) is Button btnNext)
            {
                correctBtn = btnNext;
                btnNext.Opacity = 0.60 + (0.35 - (0.35 * Difficulty));
            }
        }
        private void OpacityChangedCorrectBtn()
        {
            if (correctBtn is not null) correctBtn.Opacity = 0.6;
        }

        private void GenerateSymbol()
        {
            int i = (int)(Difficulty * 100);
            if (i < 7) i = 7;

            char c = _difficultyLine[_rand.Next(i)];
            while (c == ' ' && ShowText.Count > 0 && ShowText[^1].Char == ' ')
                c = _difficultyLine[_rand.Next(i)];

            ShowText.Add(new Symbol { Char = c });
        }

        #endregion

        #region Fields
        //------------------------------------------------------------------------------

        private static Random _rand = new Random();

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
        private readonly char[] _difficultyLine = new[]
        {
            ' ', ' ', ' ', ' ', ' ', ' ',

            'f', 'j', 'g', 'h', 't', 'y', 'b', 'n', 'r', 'u', 'v', 'm',
            'd', 'k', 'e', 'i', 'c', ',',
            's', 'l', 'w', 'o', 'x', '.',
            'a', ';', 'q', 'p', 'z', '/',
            '`', '-', '=', '[', ']', '\\', '\'',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',

            'F', 'J', 'G', 'H', 'T', 'Y', 'B', 'N', 'R', 'U', 'V', 'M',
            'D', 'K', 'E', 'I', 'C', '<',
            'S', 'L', 'W', 'O', 'X', '>',
            'A', ':', 'Q', 'P', 'Z', '?',
            '~', '_', '+', '{', '}', '|', '"',
            ')', '!', '@', '#', '$', '%', '^', '&', '*', '('
        };

        private Button correctBtn;
        private Stopwatch timer;
        private double countPressed;

        //------------------------------------------------------------------------------
        #endregion

        public MainWindowViewModel()
        {
            Symbols = Keyboard.IsKeyToggled(Key.CapsLock) ? _symbolsShift : _symbols;
            CommandKey = _commandKey;

            ShowText = new();
            for (int i = 100; i > 0; --i) GenerateSymbol();

            PrintText = new();
            OpacityChangedNextBtn();

            SpeedValue = "0";
            timer = new Stopwatch();
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

                    countPressed++;
                    if (timer.IsRunning)
                    {
                        int s = timer.Elapsed.Seconds;
                        if (s > 0) SpeedValue = $"{Math.Round((countPressed / s * 60), 0)}";
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
            timer.Reset();

            StartVisibility = Visibility.Visible;
            StopVisibility = Visibility.Collapsed;

            countPressed = 0;
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
            timer.Start();

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

        #region Properties
        //------------------------------------------------------------------------------

        #region Visibility : _StopVisibility
        //---------------------------------------------------------------------

        private Visibility _StopVisibility = Visibility.Collapsed;
        public Visibility StopVisibility
        {
            get => _StopVisibility;
            set => Set(ref _StopVisibility, value);
        }

        //---------------------------------------------------------------------
        #endregion

        #region Visibility : _StartVisibility
        //---------------------------------------------------------------------

        private Visibility _StartVisibility = Visibility.Visible;
        public Visibility StartVisibility
        {
            get => _StartVisibility;
            set => Set(ref _StartVisibility, value);
        }

        //---------------------------------------------------------------------
        #endregion

        #region string : _SpeedValue
        //---------------------------------------------------------------------

        private string _SpeedValue;
        public string SpeedValue
        {
            get => _SpeedValue;
            set => Set(ref _SpeedValue, value);
        }

        //---------------------------------------------------------------------
        #endregion

        #region int : _FailsValue
        //---------------------------------------------------------------------

        private int _FailsValue;
        public int FailsValue
        {
            get => _FailsValue;
            set => Set(ref _FailsValue, value);
        }

        //---------------------------------------------------------------------
        #endregion

        #region double : _Difficulty
        //---------------------------------------------------------------------
        private double _Difficulty = 0.15;
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