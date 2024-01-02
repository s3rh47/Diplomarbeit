using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Languages;
using Hortrainingsprogramm.Main_Window.ViewModels.LeftMenus;
using Hortrainingsprogramm.Main_Window.Views.LeftMenus;
using Hortrainingsprogramm.Main_Window.Views.RightMenus;
using Hortrainingsprogramm.Practice_and_Quiz_Menu.Views;
using Hortrainingsprogramm.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hortrainingsprogramm.Main_Window.ViewModels.RightMenus
{
    public class ModusViewModel : BaseViewModel
    {

        public string ModusTitle { get; set; }
        public string PracticeButtonTitle { get; set; }
        public string QuizButtonTitle { get; set; }
        public override BaseLanguage baseLanguage { get; set; }
        public override bool isPracticeCalled { get; set; } = false;
        public override bool isQuizCalled { get; set; } = false;
        public bool ButtonStatus { get; set; }

        private readonly INavigationService navigationService;

        public ModusViewModel(INavigationService navigationService)
        {

            this.navigationService = navigationService;
            this.navigationService.NavigationRequested += OnNavigationRequested;

        }


        private void OnNavigationRequested(object sender, NavigationEventArgs eventArgs)
        {

            this.ButtonStatus = false;

            if (eventArgs.Data is LanguageViewModel languageModel )
            {
                this.ButtonStatus = true;
                this.baseLanguage = languageModel.baseLanguage;
                this.ModusTitle = baseLanguage.ModusTitle;
                this.PracticeButtonTitle = baseLanguage.PracticeButtonTitle;
                this.QuizButtonTitle = baseLanguage.QuizButtonTitle;

            }else if (eventArgs.Data is MenuEntryViewModel entyModel)
            {
                if(entyModel.isModusCalled) this.ButtonStatus = true;
            }
            else
            {
                if (eventArgs.View is ZeroView)
                {
                    this.ButtonStatus = true;
                }
            }

        }

        public ICommand UbungCommand => new RelayCommand(parameter =>
        {

            this.navigationService.NavigateTo(nameof(MenuEntryView), this);

        });


        public ICommand QuizCommand => new RelayCommand( async parameter =>
        {
            this.ButtonStatus = false;

            var sprache = baseLanguage.GetType().Name;

                string   query = "SELECT Word FROM Words " +
                             "JOIN Languages USING (Language_id) " +
                             "WHERE Language = '" + sprache + "' " +
                             "ORDER BY RANDOM() " +
                             "LIMIT 100;";

                baseLanguage.databaseList = baseLanguage.datenbank.sqlQuery(query, "Word");

                isQuizCalled = true;

                await Task.Delay(450);

                this.navigationService.NavigateTo(nameof(QuizView), this);

                isQuizCalled = false;

        });


        public ICommand HomeCommand => new RelayCommand(parameter =>
        {


            this.navigationService.NavigateTo(nameof(LanguageView));
            this.navigationService.NavigateTo(nameof(ZeroView));
           

        });
    }
}
