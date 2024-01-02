using Hortrainingsprogramm.Login_and_Registration.ViewModels;
using Hortrainingsprogramm.Services;
using Hortrainingsprogramm.Components;
using System.Windows;
using System.Windows.Input;

namespace Hortrainingsprogramm
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
            DataContext = new NavigatorLogin(NavigationService.Instance);
        }

        private void StartWindow_Loaded(object sender, RoutedEventArgs e)
        {

            Effects.WindowBlur.EnableBlur(this);

        }

        private void StartWindow_mouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) // Vermeiden rechts Click
                this.DragMove();
           
        }

    }
}
