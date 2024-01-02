using Hortrainingsprogramm.Components;
using System.Windows.Media;

namespace Hortrainingsprogramm.Practice_and_Quiz_Menu.Model
{
    public class ButtonModel : ObservableObject
    {
        public Brush BackgroundColor { get; set; }
        public Brush ForegroundColor { get; set; }
        public string Text { get; set; }



        public ButtonModel(string Text, Brush BackgroundColor, Brush ForegroundColor)
        {
            this.Text = Text;
            this.BackgroundColor = BackgroundColor;
            this.ForegroundColor = ForegroundColor;
        }

    }
}
