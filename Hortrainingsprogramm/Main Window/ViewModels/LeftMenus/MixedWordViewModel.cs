using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Languages;
using Hortrainingsprogramm.Main_Window.Views.LeftMenus;
using Hortrainingsprogramm.Practice_and_Quiz_Menu.Views;
using Hortrainingsprogramm.Services;
using System;
using System.Windows.Input;

namespace Hortrainingsprogramm.Main_Window.ViewModels.LeftMenus
{
    public class MixedWordViewModel : BaseViewModel
    {
        private string classParamater;
        private bool isWordClassViewModel; 
        private readonly INavigationService navigationService;
        public override BaseLanguage baseLanguage { get; set; }
        public override bool isQuizCalled { get; set; }
        public override bool isPracticeCalled { get; set; } = false;


        public string MixedWordViewButtonTitle { get; set; }
        public string MixedWordAllWordsTitle { get; set; } 
        public string TwoOrThreeLetterTitle { get; set; } 
        public string FourLetterTitle { get; set; } 
        public string FiveLetterTitle { get; set; }
        public string SixLetterTitle { get; set; } 
        public string SevenOrHighTitle { get; set; }

        

        public MixedWordViewModel(INavigationService navigationService)
        {

            this.navigationService = navigationService;
            this.navigationService.NavigationRequested += OnNavigationRequested;

        }



        private void OnNavigationRequested(object sender, NavigationEventArgs eventArgs)
        {



            if (eventArgs.Data is WordChoiseViewModel choiseModel)
            {
           

                this.baseLanguage = choiseModel.baseLanguage;
                this.MixedWordViewButtonTitle = baseLanguage.MixedWordViewButtonTitle;
                this.MixedWordAllWordsTitle = baseLanguage.MixedWordAllWordsTitle;
                this.TwoOrThreeLetterTitle = baseLanguage.TwoOrThreeLetterTitle;
                this.FourLetterTitle = baseLanguage.FourLetterTitle;
                this.FiveLetterTitle = baseLanguage.FiveLetterTitle;
                this.SixLetterTitle = baseLanguage.SixLetterTitle;
                this.SevenOrHighTitle = baseLanguage.SevenOrHighTitle;

                isWordClassViewModel = false;

            }



            if (eventArgs.Data is WordClassViewModel classModel) { 
                

                isWordClassViewModel = true;
                this.classParamater = classModel.classParamater;

            }

              
        }


        public ICommand NavigateBack => new RelayCommand(parameter =>
        {

            if (isPracticeCalled)
            {
                this.isPracticeCalled = false;
                
            }


            if (isWordClassViewModel)
            {
                this.navigationService.NavigateTo(this.classParamater, this);
            }
            else
            {
                this.navigationService.NavigateTo(nameof(WordChoiseView), this);
            }



        });




        public ICommand ThreeWordsCommand => new RelayCommand(parameter =>
        {

            callQueryAndNavigate("3", "<=");

        });



        public ICommand FourWordsCommand => new RelayCommand(parameter =>
        {
            callQueryAndNavigate("4", "=");
        });



        public ICommand FiveWordsCommand => new RelayCommand(parameter =>
        {
            callQueryAndNavigate("5", "=");
        });


        public ICommand SixWordsCommand => new RelayCommand(parameter =>
        {
            callQueryAndNavigate("6", "=");
        });



        public ICommand SevenOrHighWordsCommand => new RelayCommand(parameter =>
        {
            callQueryAndNavigate("7", ">=");
        });



        public ICommand AllWordsCommand => new RelayCommand(parameter =>
        {

            var sprache = baseLanguage.GetType().Name;


            string query;


            if (isWordClassViewModel)
            {

                query = "SELECT Word FROM Words " +
                          "JOIN Languages USING(Language_id) " +
                          "JOIN Word_types USING(Word_type_id) " +
                          "WHERE Language is '" + sprache + "' " +
                          "AND Word_type is 'Noun' " +
                          "ORDER BY RANDOM() " +
                          "LIMIT 100;";
            }
            else
            {

                 query = "SELECT Word FROM Words " +
                         "JOIN Languages USING(Language_id) " +
                         "WHERE Language is '" + sprache + "' " +
                         "ORDER BY RANDOM() " +
                         "LIMIT 100;";

            }


            baseLanguage.databaseList = baseLanguage.datenbank.sqlQuery(query, "Word");


            isPracticeCalled = true;
            this.navigationService.NavigateTo(nameof(PracticeView), this);

        });


        private void callQueryAndNavigate(string anzahl , string operatorZeichen)
        {

            var sprache = baseLanguage.GetType().Name;
 
            string query;


            if (isWordClassViewModel)
            {

                query = "SELECT Word FROM Words " +
                          "JOIN Languages USING(Language_id) " +
                          "JOIN Word_types USING(Word_type_id) " +
                          "WHERE Language is '" + sprache + "' " +
                          "AND Word_type is 'Noun' " +
                          "AND Word_Length " + operatorZeichen + " " + anzahl + " " +
                          "ORDER BY RANDOM() " +
                          "LIMIT 100;";
            }
            else
            {

                query = "SELECT Word FROM Words " +
                       "JOIN Languages USING(Language_id) " +
                       "WHERE Language is '" + sprache + "' " +
                       "AND Word_Length " + operatorZeichen + " " + anzahl + " " +
                       "ORDER BY RANDOM() " +
                       "LIMIT 100;";
            
            }



            baseLanguage.databaseList = baseLanguage.datenbank.sqlQuery(query, "Word");


            isPracticeCalled = true;
            this.navigationService.NavigateTo(nameof(PracticeView), this);

        }
    }
}
