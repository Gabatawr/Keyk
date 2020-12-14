using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Keyk.ViewModels
{
    public class MouseBehaviour
    {
        #region MouseUp

        public static readonly DependencyProperty MouseUpCommandProperty = DependencyProperty.RegisterAttached
        (
            "MouseUpCommand", typeof(ICommand), typeof(MouseBehaviour),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseUpCommandChanged))
        );
        private static void MouseUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
            => ((FrameworkElement)d).MouseUp += element_MouseUp;
        static void element_MouseUp(object sender, MouseButtonEventArgs e) 
            => GetMouseUpCommand((FrameworkElement)sender).Execute(e);
        public static void SetMouseUpCommand(UIElement element, ICommand value)
            => element.SetValue(MouseUpCommandProperty, value);
        public static ICommand GetMouseUpCommand(UIElement element)
            => (ICommand)element.GetValue(MouseUpCommandProperty);

        #endregion

        #region MouseDown

        public static readonly DependencyProperty MouseDownCommandProperty = DependencyProperty.RegisterAttached
        (
            "MouseDownCommand", typeof(ICommand), typeof(MouseBehaviour),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseDownCommandChanged))
        );
        private static void MouseDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
            => ((FrameworkElement)d).MouseDown += element_MouseDown;
        static void element_MouseDown(object sender, MouseButtonEventArgs e) 
            => GetMouseDownCommand((FrameworkElement)sender).Execute(e);
        public static void SetMouseDownCommand(UIElement element, ICommand value)
            => element.SetValue(MouseDownCommandProperty, value);
        public static ICommand GetMouseDownCommand(UIElement element)
            => (ICommand)element.GetValue(MouseDownCommandProperty);

        #endregion

        #region MouseLeave

        public static readonly DependencyProperty MouseLeaveCommandProperty = DependencyProperty.RegisterAttached
        (
            "MouseLeaveCommand", typeof(ICommand), typeof(MouseBehaviour),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseLeaveCommandChanged))
        );
        private static void MouseLeaveCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
            => ((FrameworkElement)d).MouseLeave += new MouseEventHandler(element_MouseLeave);
        static void element_MouseLeave(object sender, MouseEventArgs e) 
            => GetMouseLeaveCommand((FrameworkElement)sender).Execute(e);
        public static void SetMouseLeaveCommand(UIElement element, ICommand value) 
            => element.SetValue(MouseLeaveCommandProperty, value);
        public static ICommand GetMouseLeaveCommand(UIElement element) 
            => (ICommand)element.GetValue(MouseLeaveCommandProperty);
        
        #endregion

        #region MouseLeftButtonDown

        public static readonly DependencyProperty MouseLeftButtonDownCommandProperty = DependencyProperty.RegisterAttached
        (
            "MouseLeftButtonDownCommand", typeof(ICommand), typeof(MouseBehaviour),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseLeftButtonDownCommandChanged))
        );
        private static void MouseLeftButtonDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
            => ((FrameworkElement)d).MouseLeftButtonDown += element_MouseLeftButtonDown;
        static void element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) 
            => GetMouseLeftButtonDownCommand((FrameworkElement)sender).Execute(e);
        public static void SetMouseLeftButtonDownCommand(UIElement element, ICommand value) 
            => element.SetValue(MouseLeftButtonDownCommandProperty, value);
        public static ICommand GetMouseLeftButtonDownCommand(UIElement element) 
            => (ICommand)element.GetValue(MouseLeftButtonDownCommandProperty);

        #endregion

        #region MouseLeftButtonUp

        public static readonly DependencyProperty MouseLeftButtonUpCommandProperty = DependencyProperty.RegisterAttached
        (
            "MouseLeftButtonUpCommand", typeof(ICommand), typeof(MouseBehaviour),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseLeftButtonUpCommandChanged))
        );

        private static void MouseLeftButtonUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
            => ((FrameworkElement)d).MouseLeftButtonUp += element_MouseLeftButtonUp;
        static void element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) 
            => GetMouseLeftButtonUpCommand((FrameworkElement)sender).Execute(e);
        public static void SetMouseLeftButtonUpCommand(UIElement element, ICommand value) 
            => element.SetValue(MouseLeftButtonUpCommandProperty, value);
        public static ICommand GetMouseLeftButtonUpCommand(UIElement element) 
            => (ICommand)element.GetValue(MouseLeftButtonUpCommandProperty);

        #endregion

        #region MouseMove

        public static readonly DependencyProperty MouseMoveCommandProperty = DependencyProperty.RegisterAttached
        (
            "MouseMoveCommand", typeof(ICommand), typeof(MouseBehaviour),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseMoveCommandChanged))
        );
        private static void MouseMoveCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
            => ((FrameworkElement) d).MouseMove += new MouseEventHandler(element_MouseMove);
        static void element_MouseMove(object sender, MouseEventArgs e) 
            => GetMouseMoveCommand((FrameworkElement)sender).Execute(e);
        public static void SetMouseMoveCommand(UIElement element, ICommand value) 
            => element.SetValue(MouseMoveCommandProperty, value);
        public static ICommand GetMouseMoveCommand(UIElement element) 
            => (ICommand)element.GetValue(MouseMoveCommandProperty);

        #endregion

        #region MouseRightButtonDown

        public static readonly DependencyProperty MouseRightButtonDownCommandProperty = DependencyProperty.RegisterAttached(
            "MouseRightButtonDownCommand", typeof(ICommand), typeof(MouseBehaviour),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseRightButtonDownCommandChanged))
        );
        private static void MouseRightButtonDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
            => ((FrameworkElement)d).MouseRightButtonDown += element_MouseRightButtonDown;
        static void element_MouseRightButtonDown(object sender, MouseButtonEventArgs e) 
            => GetMouseRightButtonDownCommand((FrameworkElement)sender).Execute(e);
        public static void SetMouseRightButtonDownCommand(UIElement element, ICommand value) 
            => element.SetValue(MouseRightButtonDownCommandProperty, value);
        public static ICommand GetMouseRightButtonDownCommand(UIElement element) 
            => (ICommand)element.GetValue(MouseRightButtonDownCommandProperty);

        #endregion

        #region MouseRightButtonUp

        public static readonly DependencyProperty MouseRightButtonUpCommandProperty = DependencyProperty.RegisterAttached
        (
            "MouseRightButtonUpCommand", typeof(ICommand), typeof(MouseBehaviour),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseRightButtonUpCommandChanged))
        );
        private static void MouseRightButtonUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
            =>  ((FrameworkElement)d).MouseRightButtonUp += element_MouseRightButtonUp;
        static void element_MouseRightButtonUp(object sender, MouseButtonEventArgs e) 
            => GetMouseRightButtonUpCommand((FrameworkElement)sender).Execute(e);
        public static void SetMouseRightButtonUpCommand(UIElement element, ICommand value) 
            => element.SetValue(MouseRightButtonUpCommandProperty, value);
        public static ICommand GetMouseRightButtonUpCommand(UIElement element) 
            => (ICommand)element.GetValue(MouseRightButtonUpCommandProperty);

        #endregion

        #region MouseWheel

        public static readonly DependencyProperty MouseWheelCommandProperty = DependencyProperty.RegisterAttached
        (
            "MouseWheelCommand", typeof(ICommand), typeof(MouseBehaviour),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseWheelCommandChanged))
        );
        private static void MouseWheelCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
            => ((FrameworkElement)d).MouseWheel += new MouseWheelEventHandler(element_MouseWheel);
        static void element_MouseWheel(object sender, MouseWheelEventArgs e) 
            => GetMouseWheelCommand((FrameworkElement)sender).Execute(e);
        public static void SetMouseWheelCommand(UIElement element, ICommand value) 
            => element.SetValue(MouseWheelCommandProperty, value);
        public static ICommand GetMouseWheelCommand(UIElement element) 
            => (ICommand)element.GetValue(MouseWheelCommandProperty);
        
        #endregion
    }
}
