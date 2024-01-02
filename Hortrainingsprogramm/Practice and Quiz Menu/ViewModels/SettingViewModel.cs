using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Languages;
using System.Windows;
using System.Windows.Input;

namespace Hortrainingsprogramm.Practice_and_Quiz_Menu.ViewModels
{
    public class SettingViewModel : ObservableObject
    {

        private static SettingViewModel _instance;

        public static SettingViewModel Instance => _instance ??= new();

        public BaseLanguage baseLanguage { get; set; }

        public string practiceModeSetting { get; set; }
        public int roundCount { get; set; }


        public bool isButtonModeChecked { get; set; }
        public bool isTextBoxModeChecked { get; set; }



        public bool isGameRound10Checked { get; set; }
        public bool isGameRound20Checked { get; set; } 
        public bool isGameRound30Checked { get; set; } 



        public string SettingCaption { get; set; }
        public string ButtonModeText { get; set; }
        public string TextBoxModeText { get; set; }
        public string SaveButtonText { get; set; }
        public string RoundCaption { get; set; }


        public string GamesRoundTitle10 { get; set; } 
        public string GamesRoundTitle20 { get; set; } 
        public string GamesRoundTitle30 { get; set; }





        public void InitalizeTitles()
        {

            if (this.baseLanguage != null)
            {
                this.SettingCaption = baseLanguage.SettingCaption;
                this.SaveButtonText = baseLanguage.SaveButtonText;
                this.TextBoxModeText = baseLanguage.TextBoxModeText;
                this.ButtonModeText = baseLanguage.ButtonModeText;
                this.GamesRoundTitle10 = baseLanguage.GamesRoundTitle10;
                this.GamesRoundTitle20 = baseLanguage.GamesRoundTitle20;
                this.GamesRoundTitle30 = baseLanguage.GamesRoundTitle30;
                this.RoundCaption = baseLanguage.RoundCaption;
            }


            if (Settings.Default.PracticeModeSetting.Equals("ButtonMode"))
            {
                isButtonModeChecked = true;
            }
            else
            {
                isTextBoxModeChecked = true;
            }


            if (Settings.Default.roundCount.Equals(10))
            {
                isGameRound10Checked = true;
            }
            else if (Settings.Default.roundCount.Equals(20))
            {
                isGameRound20Checked = true;
            }
            else
            {
                isGameRound30Checked = true;
            }

        }


        public ICommand SettingSaveCommand => new RelayCommand(parameter =>
        {

            Settings.Default.Save();

            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }


        });




        public ICommand ButtonModeCommand => new RelayCommand(parameter =>
        {

            Settings.Default.PracticeModeSetting = "ButtonMode";
          

        });




        public ICommand TextBoxModeCommand => new RelayCommand(parameter =>
        {
            Settings.Default.PracticeModeSetting = "TextBoxMode";
            

        });



        public ICommand GameRound10Command => new RelayCommand(parameter =>
        {
            Settings.Default.roundCount = 10;
            

        });


        public ICommand GameRound20Command => new RelayCommand(parameter =>
        {
            Settings.Default.roundCount = 20;
            

        });



        public ICommand GameRound30Command => new RelayCommand(parameter =>
        {
            Settings.Default.roundCount = 30;

        });



    }
}
