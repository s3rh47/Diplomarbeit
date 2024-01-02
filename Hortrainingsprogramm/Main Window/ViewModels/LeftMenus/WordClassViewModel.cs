using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Languages;
using Hortrainingsprogramm.Main_Window.Views.LeftMenus;
using Hortrainingsprogramm.Practice_and_Quiz_Menu.Views;
using Hortrainingsprogramm.Services;
using System.Windows.Input;

namespace Hortrainingsprogramm.Main_Window.ViewModels.LeftMenus
{
    public class WordClassViewModel : BaseViewModel
    {

        public string classParamater;
        private readonly INavigationService navigationService;
        public override BaseLanguage baseLanguage { get; set; }
        public override bool isPracticeCalled { get; set; } = false;
        public override bool isQuizCalled { get; set; }

        public WordClassViewModel(INavigationService navigationService)
        {

            this.navigationService = navigationService;
            navigationService.NavigationRequested += OnNavigationRequested;

        }


        private void OnNavigationRequested(object sender, NavigationEventArgs eventArgs)
        {


            if (eventArgs.Data is WordChoiseViewModel choiseModel)
            {
                this.baseLanguage = choiseModel.baseLanguage;

            }

        }


        public ICommand NavigateBack => new RelayCommand(parameter =>
        {


            if (isPracticeCalled)
            {
                this.isPracticeCalled = false;

            }

            this.navigationService.NavigateTo(nameof(WordChoiseView),this);


        });



        public ICommand NounCommand => new RelayCommand(parameter =>
        {
            isPracticeCalled = false;

            this.classParamater = parameter.ToString();

            this.navigationService.NavigateTo(nameof(MixedWordView), this);

        });






        // Mit dem Parameter kommt vom Xaml(CommonWordView) von "CommandParameter" der Text von geklickten Button, welcher für Datenbank eine Spalte ist. 

        public ICommand StartCommand => new RelayCommand(parameter =>
        {

            var sprache = baseLanguage.GetType().Name;

            string query;


            if (parameter.ToString().Equals("mixed"))
            {

                query =    "SELECT Word FROM Words " +
                           "JOIN Word_categories USING(Word_categorie_id) " +
                           "JOIN Languages USING (Language_id) " +
                           "WHERE categorie = 'WordClass'  " +
                           "AND Language = '" + sprache + "' " +
                           "ORDER BY RANDOM() " +
                           "LIMIT 100;";

            }
            else
            {

                query = "SELECT Word FROM Words " +
                        "JOIN Languages USING(Language_id) " +
                        "JOIN Word_types USING(Word_type_id) " +
                        "WHERE Language = '" + sprache + "' " +
                        "AND Word_type = '" + parameter.ToString() + "' " +
                        "ORDER BY RANDOM() " +
                        "LIMIT 100;";

            }


            baseLanguage.databaseList = baseLanguage.datenbank.sqlQuery(query, "Word");

            isPracticeCalled = true;
            this.navigationService.NavigateTo(nameof(PracticeView), this);


        });
    }
}
