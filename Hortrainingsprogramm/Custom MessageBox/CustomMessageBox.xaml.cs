using Hortrainingsprogramm.Components;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace Hortrainingsprogramm.Custom_MessageBox
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public string message { get; set; }
        public string caption { get; set; }
        public ImageSource icon { get; set; }

        public CustomMessageBox()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();

        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Effects.WindowBlur.EnableBlur(this);

            messageID.Text = message;
            captionID.Text = caption;
            iconID.Source = icon;

            var wordCount = messageID.Text.Split();

            messageID.Text = "";

            int x = 0;

            foreach (var item in wordCount)
            {


                if (x == 6)
                {
                    messageID.Text += Environment.NewLine;
                    x = 0;
                }
                x++;

                messageID.Text += item + " ";

            }
        }
    }
}
