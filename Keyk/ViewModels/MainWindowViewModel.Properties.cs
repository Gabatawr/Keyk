using System.Collections.ObjectModel;
using System.Windows;
using Keyk.Models;

namespace Keyk.ViewModels
{
    partial class MainWindowViewModel
    {
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