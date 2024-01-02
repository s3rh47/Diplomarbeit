using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Languages;
using Hortrainingsprogramm.Main_Window.Views.LeftMenus;
using Hortrainingsprogramm.Services;
using System.Windows.Input;

namespace Hortrainingsprogramm.Main_Window.ViewModels.LeftMenus
{
    public class SentenceViewModel : BaseViewModel
    {

        private readonly INavigationService navigationService;
        public override BaseLanguage baseLanguage { get; set; }
        public override bool isPracticeCalled { get; set; } = false;
        public override bool isQuizCalled { get; set; }

        public string SentenceViewButtonTitle { get; set; }
        public string SentenceAllSentencesTitle { get; set; } 
        public string MeaningfullSenctenceTitle { get; set; }
        public string MeaninglessSenctenceTitle { get; set; } 
        public string ProverbTitle { get; set; }


        
        public SentenceViewModel(INavigationService navigationService)
        {

            this.navigationService = navigationService;
            this.navigationService.NavigationRequested += OnNavigationRequested!;

        }



        private void OnNavigationRequested(object sender, NavigationEventArgs eventArgs)
        {

            if (eventArgs.Data is MenuEntryViewModel entryModel)
            {
                this.baseLanguage = entryModel.baseLanguage;
                this.SentenceViewButtonTitle = baseLanguage.SentenceViewButtonTitle;
                this.SentenceAllSentencesTitle = baseLanguage.SentenceAllSentencesTitle;
                this.MeaningfullSenctenceTitle = baseLanguage.MeaningfullSenctenceTitle;
                this.MeaninglessSenctenceTitle = baseLanguage.MeaninglessSenctenceTitle;
                this.ProverbTitle = baseLanguage.ProverbTitle;

            }

        }


        public ICommand NavigateBack => new RelayCommand(parameter =>
        {



            this.navigationService.NavigateTo(nameof(MenuEntryView), this);




        });




    }
}
