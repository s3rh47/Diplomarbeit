using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Languages;
using Hortrainingsprogramm.Main_Window.Views.LeftMenus;
using Hortrainingsprogramm.Practice_and_Quiz_Menu.Views;
using Hortrainingsprogramm.Services;
using System.Windows.Input;


namespace Hortrainingsprogramm.Main_Window.ViewModels.LeftMenus
{
    public class CommonWordViewModel : BaseViewModel
    {

        private readonly INavigationService navigationService;
        public override BaseLanguage baseLanguage { get; set; }
        public override bool isPracticeCalled { get; set; } = false;
        public override bool isQuizCalled { get; set; }

        public string CommonWordButtonTitle { get; set; }
        public string CommonWordAllWordsTitle { get; set;}
        public string NamesTitle { get; set; } 
        public string AnimalsTitle { get; set; } 
        public string PlantsTitle { get; set; } 
        public string ObjectsTitle { get; set; }
        public string CountriesTitle { get; set; }
        public string CapitalsTitle { get; set; }
        public string MetropoleTitle { get; set; } 
        public string CitiesTitle { get; set; } 
        public string FoodsTitle { get; set; }
        public string DishTitle { get; set; } 
        public string DrinksTitle { get; set; } 
        public string BrandsTitle { get; set; } 
        public string FamilyMembersTitle { get; set; } 
        public string ColorsTitle { get; set; }
        public string BodyPartsTitle { get; set; } 
        public string MonthsTitle { get; set; } 
        public string DaysTitle { get; set; } 




        public CommonWordViewModel(INavigationService navigationService)
        {

            this.navigationService = navigationService;
            this.navigationService.NavigationRequested += OnNavigationRequested;

        }




        private void OnNavigationRequested(object sender, NavigationEventArgs eventArgs)
        {


            if (eventArgs.Data is WordChoiseViewModel choiseModel)
            {
                this.baseLanguage = choiseModel.baseLanguage;
                this.CommonWordButtonTitle = baseLanguage.CommonWordButtonTitle;
                this.CommonWordAllWordsTitle = baseLanguage.CommonWordAllWordsTitle;
                this.NamesTitle = baseLanguage.NamesTitle;
                this.AnimalsTitle = baseLanguage.AnimalsTitle;
                this.PlantsTitle = baseLanguage.PlantsTitle;
                this.ObjectsTitle = baseLanguage.ObjectsTitle;
                this.CountriesTitle = baseLanguage.CountriesTitle;
                this.CapitalsTitle = baseLanguage.CapitalsTitle;
                this.MetropoleTitle = baseLanguage.MetropoleTitle;
                this.CitiesTitle = baseLanguage.CitiesTitle;
                this.FoodsTitle = baseLanguage.FoodsTitle;
                this.DishTitle = baseLanguage.DishTitle;
                this.DrinksTitle = baseLanguage.DrinksTitle;
                this.BrandsTitle = baseLanguage.BrandsTitle;
                this.FamilyMembersTitle = baseLanguage.FamilyMembersTitle;
                this.ColorsTitle = baseLanguage.ColorsTitle;
                this.BodyPartsTitle = baseLanguage.BodyPartsTitle;
                this.MonthsTitle = baseLanguage.MonthsTitle;
                this.DaysTitle = baseLanguage.DaysTitle;
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



        // Mit dem Parameter kommt vom Xaml(CommonWordView) von "CommandParameter" der Text von geklickten Button, welcher für Datenbank eine Spalte ist. 

        public ICommand StartCommand => new RelayCommand(parameter =>
        {

            var sprache = baseLanguage.GetType().Name;

            string query;


            if (parameter.ToString().Equals("mixed"))
            {

                  query =  "SELECT Word FROM Words " +
                           "JOIN Word_categories USING(Word_categorie_id) " +
                           "JOIN Languages USING (Language_id) " +
                           "WHERE categorie = 'DailyWord'  " +
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
