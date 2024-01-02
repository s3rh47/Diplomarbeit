using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Languages;
using Hortrainingsprogramm.Main_Window.ViewModels.RightMenus;
using Hortrainingsprogramm.Main_Window.Views.LeftMenus;
using Hortrainingsprogramm.Main_Window.Views.RightMenus;
using Hortrainingsprogramm.Practice_and_Quiz_Menu.Views;
using Hortrainingsprogramm.Services;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;



namespace Hortrainingsprogramm.Main_Window.ViewModels
{
    public class MainViewModel : ObservableObject
    {

        private readonly INavigationService navigationService;

        public object CurrentViewLeft { get; set; }
        public object CurrentViewRight { get; set; }
        public string LoggedUserTitle { get; set; } = "Angemeldet als:";
        public string LoggedUser { get; set; } = Settings.Default.loggedUser;
        public string LogoutButtonText { get; set; } = "Ausloggen";
        public BaseLanguage baseLanguage { get; set; }

 

        public MainViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.navigationService.NavigationRequested += OnNavigationRequested;

            // Navigate to the Langauge in Right Menu on Startup
            this.navigationService.NavigateTo(nameof(LanguageView));

        }


        private void OnNavigationRequested(object sender, NavigationEventArgs eventArgs)
        {
            switch (eventArgs.View)
            {
                case WordChoiseView:
                case MenuEntryView:
                case CommonWordView:
                case WordClassViewDe:
                case WordClassViewTr:
                case WordClassViewEn:
                case NumberView:
                case MixedWordView:
                case SentenceView:
                case ZeroView: CurrentViewLeft = eventArgs.View; break;

                case LanguageView:
                case ModusView:
                
                case PracticeView:
                case QuizView: CurrentViewRight = eventArgs.View; break;

            }


            if (eventArgs.Data is LanguageViewModel Model)
            {
               
                this.baseLanguage = Model.baseLanguage;
                this.LogoutButtonText = baseLanguage.logoutButtonText;
                this.LoggedUserTitle = baseLanguage.LoggedUserTitle;
            }

        }


        public ICommand LogoutCommand => new RelayCommand(async parameter =>
        {

            var mainWindow = Application.Current.MainWindow;

            Application.Current.MainWindow = new StartWindow();

            var fadeObject = new Effects.FadeInOut(mainWindow, 1, 0, 0.4);
            fadeObject.Run();

            // Wait for the animation to finish before closing the window
            await Task.Delay(400);
            mainWindow.Close(); // Schieließe  das Loginfenster

            mainWindow = null;

            Application.Current.MainWindow.Show(); // Öffne das Hauptfenster

        });


    }
}
