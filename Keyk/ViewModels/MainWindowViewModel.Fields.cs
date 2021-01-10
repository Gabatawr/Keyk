using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace Keyk.ViewModels
{
    partial class MainWindowViewModel
    {
        #region Fields
        //------------------------------------------------------------------------------

        private static readonly Random _rand = new Random();

        private readonly Stopwatch _timer;

        private Button _correctBtn;
        private double _countPressed;

        private readonly Key[] _keys = new[]
        {
            Key.Oem3, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9, Key.D0, Key.OemMinus, Key.OemPlus,
            Key.Q, Key.W, Key.E, Key.R, Key.T, Key.Y, Key.U, Key.I, Key.O, Key.P, Key.OemOpenBrackets, Key.Oem6, Key.Oem5,
            Key.A, Key.S, Key.D, Key.F, Key.G, Key.H, Key.J, Key.K, Key.L, Key.Oem1, Key.OemQuotes,
            Key.Z, Key.X, Key.C, Key.V, Key.B, Key.N, Key.M, Key.OemComma, Key.OemPeriod, Key.OemQuestion, Key.Space
        };
        private readonly Key[] _commandKeys = new[]
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
        private readonly string[] _commandKeyName = new[]
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

        //------------------------------------------------------------------------------
        #endregion
    }
}