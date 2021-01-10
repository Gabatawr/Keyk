using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Keyk.Models;

namespace Keyk.ViewModels
{
    partial class MainWindowViewModel
    {
        #region Methods
        //------------------------------------------------------------------------------

        private void OpacityChanged(Key k, double o)
        {
            for (int i = 0; i < _commandKeys.Length; i++)
            {
                if (k == _commandKeys[i])
                {
                    if (k == Key.System && Keyboard.IsKeyDown(Key.RightAlt)) i++;
                    if (Application.Current.MainWindow?.FindName("CKey" + i) is Button btn) btn.Opacity = o;
                    if (k == Key.System && Keyboard.IsKeyDown(Key.LeftAlt)) break;
                }
            }
        }
        //------------------------------------------------------------------------------
        private void OpacityChangedNextBtn()
        {
            char c = ShowText[PrintText.Count].Char;
            int p = Array.IndexOf(_symbols, c);
            if (p == -1) p = Array.IndexOf(_symbolsShift, c);

            if (Application.Current.MainWindow?.FindName("Key" + p) is Button btnNext)
            {
                _correctBtn = btnNext;
                btnNext.Opacity = 0.60 + (0.35 - (0.35 * Difficulty));
            }
        }
        //------------------------------------------------------------------------------
        private void OpacityChangedCorrectBtn()
        {
            if (_correctBtn is not null) _correctBtn.Opacity = 0.6;
        }
        //------------------------------------------------------------------------------
        private void GenerateSymbol()
        {
            int i = (int)(Difficulty * 100);
            if (i < 7) i = 7;

            char c = _difficultyLine[_rand.Next(i)];
            while (c == ' ' && ShowText.Count > 0 && ShowText[^1].Char == ' ')
                c = _difficultyLine[_rand.Next(i)];

            ShowText.Add(new Symbol { Char = c });
        }

        //------------------------------------------------------------------------------
        #endregion
    }
}