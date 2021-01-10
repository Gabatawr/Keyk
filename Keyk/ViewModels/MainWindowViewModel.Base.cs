using System.Diagnostics;
using Keyk.ViewModels.Base;
using System.Windows.Input;

namespace Keyk.ViewModels
{
    partial class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            Symbols = Keyboard.IsKeyToggled(Key.CapsLock) ? _symbolsShift : _symbols;
            CommandKey = _commandKeyName;

            ShowText = new();
            for (int i = 100; i > 0; --i) GenerateSymbol();

            PrintText = new();
            OpacityChangedNextBtn();

            SpeedValue = "0";
            _timer = new Stopwatch();
        }
    }
}