using System.Windows;
using System.Windows.Input;

namespace Keyk.Infrastructure.Commands
{
    public class KeyboardCommands
    {
        #region BackspaceDown

        public static readonly DependencyProperty BackspaceDownProperty = DependencyProperty.RegisterAttached
        (
            "BackspaceDown", typeof(ICommand), typeof(KeyboardCommands),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(BackspaceDownChanged))
        );
        private static void element_BackspaceDown(object sender, KeyEventArgs e) => GetBackspaceDown((FrameworkElement)sender).Execute(e);
        private static void BackspaceDownChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((FrameworkElement)d).KeyDown += element_BackspaceDown;
        public static ICommand GetBackspaceDown(UIElement element) => (ICommand)element.GetValue(BackspaceDownProperty);
        public static void SetBackspaceDown(UIElement element, ICommand value) => element.SetValue(BackspaceDownProperty, value);

        #endregion

        #region ShiftUp

        public static readonly DependencyProperty ShiftUpProperty = DependencyProperty.RegisterAttached
        (
            "ShiftUp", typeof(ICommand), typeof(KeyboardCommands),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(ShiftUpChanged))
        );
        private static void element_ShiftUp(object sender, KeyEventArgs e) => GetShiftUp((FrameworkElement)sender).Execute(e);
        private static void ShiftUpChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((FrameworkElement)d).KeyUp += element_ShiftUp;
        public static ICommand GetShiftUp(UIElement element) => (ICommand)element.GetValue(ShiftUpProperty);
        public static void SetShiftUp(UIElement element, ICommand value) => element.SetValue(ShiftUpProperty, value);

        #endregion

        #region ShiftDown

        public static readonly DependencyProperty ShiftDownProperty = DependencyProperty.RegisterAttached
        (
            "ShiftDown", typeof(ICommand), typeof(KeyboardCommands),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(ShiftDownChanged))
        );
        private static void element_ShiftDown(object sender, KeyEventArgs e) => GetShiftDown((FrameworkElement)sender).Execute(e);
        private static void ShiftDownChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((FrameworkElement)d).KeyDown += element_ShiftDown;
        public static ICommand GetShiftDown(UIElement element) => (ICommand)element.GetValue(ShiftDownProperty);
        public static void SetShiftDown(UIElement element, ICommand value) => element.SetValue(ShiftDownProperty, value);

        #endregion

        #region CapsLockDown

        public static readonly DependencyProperty CapsLockDownProperty = DependencyProperty.RegisterAttached
        (
            "CapsLockDown", typeof(ICommand), typeof(KeyboardCommands),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(CapsLockDownChanged))
        );
        private static void element_CapsLockDown(object sender, KeyEventArgs e) => GetCapsLockDown((FrameworkElement)sender).Execute(e);
        private static void CapsLockDownChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((FrameworkElement)d).KeyDown += element_CapsLockDown;
        public static ICommand GetCapsLockDown(UIElement element) => (ICommand)element.GetValue(CapsLockDownProperty);
        public static void SetCapsLockDown(UIElement element, ICommand value) => element.SetValue(CapsLockDownProperty, value);

        #endregion

        #region PrintKeyDown

        public static readonly DependencyProperty PrintKeyDownProperty = DependencyProperty.RegisterAttached
        (
            "PrintKeyDown", typeof(ICommand), typeof(KeyboardCommands),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(PrintKeyDownChanged))
        );
        private static void element_PrintKeyDown(object sender, KeyEventArgs e) => GetPrintKeyDown((FrameworkElement)sender).Execute(e);
        private static void PrintKeyDownChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((FrameworkElement)d).KeyDown += element_PrintKeyDown;
        public static ICommand GetPrintKeyDown(UIElement element) => (ICommand)element.GetValue(PrintKeyDownProperty);
        public static void SetPrintKeyDown(UIElement element, ICommand value) => element.SetValue(PrintKeyDownProperty, value);

        #endregion

        #region PrintKeyUp

        public static readonly DependencyProperty PrintKeyUpProperty = DependencyProperty.RegisterAttached
        (
            "PrintKeyUp", typeof(ICommand), typeof(KeyboardCommands),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(PrintKeyUpChanged))
        );
        private static void element_PrintKeyUp(object sender, KeyEventArgs e) => GetPrintKeyUp((FrameworkElement)sender).Execute(e);
        private static void PrintKeyUpChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((FrameworkElement)d).KeyUp += element_PrintKeyUp;
        public static ICommand GetPrintKeyUp(UIElement element) => (ICommand)element.GetValue(PrintKeyUpProperty);
        public static void SetPrintKeyUp(UIElement element, ICommand value) => element.SetValue(PrintKeyUpProperty, value);

        #endregion
    }
}
