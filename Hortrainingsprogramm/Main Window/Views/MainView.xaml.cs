using Hortrainingsprogramm.Components;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;


namespace Hortrainingsprogramm.Main_Window.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }


        [DllImport("user32.dll")] // Sadece burasi yok yok ben şeyi kastettim şu dalga animasyonunue
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void mainView_mouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private async void closeButtonClicked(object sender, RoutedEventArgs e)
        {

            var fadeObject = new Effects.FadeInOut(this, 1, 0, 0.3);
            fadeObject.Run();
            await Task.Delay(360);
            Application.Current.Shutdown();

        }


        private async void maximizeButtonClicked(object sender, RoutedEventArgs e)
        {

            var fadeObject = new Effects.FadeInOut(this,1,0,0.3);


            if (this.WindowState == WindowState.Maximized)
            {

                fadeObject.Run();    

                await Task.Delay(300);

                fadeObject.FadeFrom = 0;
                fadeObject.FadeTo = 1;

                fadeObject.Run();

                this.WindowState = WindowState.Normal;


            }
            else
            {

                fadeObject.Run();

                fadeObject.FadeFrom = 1;
                fadeObject.FadeTo = 0;

                await Task.Delay(300);

                fadeObject.FadeFrom = 0;
                fadeObject.FadeTo = 1;

                fadeObject.Run();


                this.WindowState = WindowState.Maximized;


            }

        }

        private void minimizeButtonClicked(object sender, RoutedEventArgs e)
        {

            var minimizeEffect = new Effects.WindowMinimizeEffect();

            minimizeEffect.minimizeEffect(Application.Current.Windows[0]);


        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Effects.WindowBlur.EnableBlur(this);
        }
    }
}
