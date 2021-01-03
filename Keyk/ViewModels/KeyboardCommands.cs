using System.Windows;
using System.Windows.Input;

namespace Keyk.ViewModels
{
    public class KeyboardCommands
    {
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
    }
}
