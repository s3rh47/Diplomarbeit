using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Animation;


namespace Hortrainingsprogramm.Components
{
    public class Effects
    {
        #region Windows Blur Effect Class

        public static class WindowBlur
        {
            [DllImport("user32.dll")]
            internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

            internal struct WindowCompositionAttributeData
            {
                public WindowCompositionAttribute Attribute;
                public IntPtr Data;
                public int SizeOfData;
            }

            internal enum WindowCompositionAttribute
            {
                WCA_ACCENT_POLICY = 19
            }

            internal enum AccentState
            {
                ACCENT_DISABLED = 0,
                ACCENT_ENABLE_GRADIENT = 1,
                ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
                ACCENT_ENABLE_BLURBEHIND = 3,
                ACCENT_INVALID_STATE = 4
            }

            [StructLayout(LayoutKind.Sequential)]
            internal struct AccentPolicy
            {
                public AccentState AccentState;
                public int AccentFlags;
                public int GradientColor;
                public int AnimationId;
            }

            public static void EnableBlur(Window window)
            {
                var windowHelper = new WindowInteropHelper(window);

                var accent = new AccentPolicy();
                accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

                var accentStructSize = Marshal.SizeOf(accent);

                var accentPtr = Marshal.AllocHGlobal(accentStructSize);
                Marshal.StructureToPtr(accent, accentPtr, false);

                var data = new WindowCompositionAttributeData();
                data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
                data.SizeOfData = accentStructSize;
                data.Data = accentPtr;

                SetWindowCompositionAttribute(windowHelper.Handle, ref data);

                Marshal.FreeHGlobal(accentPtr);
            }

            public static void DisableBlur(Window window)
            {
                var windowHelper = new WindowInteropHelper(window);

                var accent = new AccentPolicy();
                accent.AccentState = AccentState.ACCENT_DISABLED;

                var accentStructSize = Marshal.SizeOf(accent);

                var accentPtr = Marshal.AllocHGlobal(accentStructSize);
                Marshal.StructureToPtr(accent, accentPtr, false);

                var data = new WindowCompositionAttributeData();
                data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
                data.SizeOfData = accentStructSize;
                data.Data = accentPtr;

                SetWindowCompositionAttribute(windowHelper.Handle, ref data);

                Marshal.FreeHGlobal(accentPtr);
            }
        }

        #endregion


        #region FadeIn/FadeOut Class

        public class FadeInOut
        {

            private int fadeFrom, fadeTo;
            private double fadeDuration;
            private Window window;

            public int FadeFrom { get => fadeFrom; set => fadeFrom = value; }
            public int FadeTo { get => fadeTo; set => fadeTo = value; }
            public double FadeDuration { get => fadeDuration; set => fadeDuration = value; }
            public Window Window { get => window; set => window = value; }


            private Storyboard storyboard = new Storyboard();
            private DoubleAnimation fadeInAnimation = new DoubleAnimation();
            private PropertyPath opacityPath = new PropertyPath("Opacity");


            public FadeInOut(Window window, int fadeFrom, int fadeTo, double fadeDuration)
            {
                this.window = window;
                this.fadeFrom = fadeFrom;
                this.fadeTo = fadeTo;
                this.fadeDuration = fadeDuration;
            }



            public void Run()
            {
                TaskFade();
            }


            private void TaskFade()
            {

                fadeInAnimation.From = fadeFrom; // 1
                fadeInAnimation.To = fadeTo; // 0
                fadeInAnimation.Duration = new Duration(TimeSpan.FromSeconds(fadeDuration)); //0.3

                // Set the Storyboard's TargetProperty to the Opacity property of the window
                Storyboard.SetTargetProperty(fadeInAnimation, opacityPath);

                // Add the DoubleAnimation to the Storyboard
                storyboard.Children.Add(fadeInAnimation);

                // Start the Storyboard
                storyboard.Begin(window);

            }
        }


        #endregion


        #region  WindowMinimize Effect Class


        public class WindowMinimizeEffect
        {

            public async void minimizeEffect(Window window)
            {
                // Save the original Top property value
                double originalTop = window.Top;

                double currentHeight = window.Height;

                // Get the screen height
                double screenHeight = SystemParameters.PrimaryScreenHeight;

                // Animate the Top property from its current value to the bottom of the screen over 500 milliseconds

                window.BeginAnimation(FrameworkElement.HeightProperty, new DoubleAnimation
                    (window.ActualHeight, 0, TimeSpan.FromMilliseconds(500)));


                // Wait for the animation to finish before minimizing the window
                await Task.Delay(500);

                // Minimize the window
                window.WindowState = WindowState.Minimized;


                // Add an event handler for the Window's StateChanged event
                window.StateChanged += (sender, e) =>
                {

                    window.BeginAnimation(FrameworkElement.HeightProperty, new DoubleAnimation
                        (window.ActualHeight, currentHeight, TimeSpan.FromMilliseconds(0)));


                    // Check if the WindowState is Normal
                    if (window.WindowState == WindowState.Normal)
                    {
                        // Animate the Top property from the bottom of the screen to its original value over 500 milliseconds
                        window.BeginAnimation(Window.TopProperty, new DoubleAnimation
                            (screenHeight, originalTop, TimeSpan.FromMilliseconds(400)));
                    }
                };

            }

        }

        #endregion

    }
}
