using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Languages;
using Hortrainingsprogramm.Main_Window.Views.LeftMenus;
using Hortrainingsprogramm.Main_Window.Views.RightMenus;
using Hortrainingsprogramm.Services;
using System.Windows.Input;


namespace Hortrainingsprogramm.Main_Window.ViewModels.LeftMenus
{
    public class WordChoiseViewModel : BaseViewModel
    {

        private readonly INavigationService navigationService;
        public override BaseLanguage baseLanguage { get; set; }
        public override  bool isPracticeCalled { get; set; } = false;
        public override bool isQuizCalled { get; set; }

        public string WordChoiseButtonTitle { get; set; } 
        public string AllWordsTitle { get; set; } 
        public string CommonWordTitle { get; set; } 
        public string WordtypeTitle { get; set;} 


        public WordChoiseViewModel(INavigationService navigationService)
        {
            
            this.navigationService = navigationService;
            this.navigationService.NavigationRequested += OnNavigationRequested;
        }


        private void OnNavigationRequested(object sender, NavigationEventArgs eventArgs)
        {


            if (eventArgs.Data is MenuEntryViewModel entryModel)
            {
                this.baseLanguage = entryModel.baseLanguage;
                this.WordChoiseButtonTitle = baseLanguage.WordChoiseButtonTitle;
                this.AllWordsTitle = baseLanguage.AllWordsTitle;
                this.CommonWordTitle = baseLanguage.CommonWordTitle;
                this.WordtypeTitle = baseLanguage.WordtypeTitle;

            }

        }


        public ICommand SelectAllWordsCommand => new RelayCommand(parameter => {
           

            this.navigationService.NavigateTo(nameof(MixedWordView), this);

        });





        public ICommand OpenCommonWordViewCommand => new RelayCommand(parameter => {

            if (isPracticeCalled)
            {
                this.isPracticeCalled = false;
                this.navigationService.NavigateTo(nameof(ModusView), this);
            }
            
            this.navigationService.NavigateTo(nameof(CommonWordView), this);

        });




        public ICommand OpenWordClassViewCommand => new RelayCommand(parameter => {
            
            //Bunu neden yaptim bilmiyorum

            //if (isPracticeCalled)
            //{
            //    this.isPracticeCalled = false;
            //    this.navigationService.NavigateTo(nameof(ModusView), this);
            //}
           

            switch (baseLanguage)
            {

                case Turkce:
                    this.navigationService.NavigateTo(nameof(WordClassViewTr), this);
                    break;
                case English:
                    this.navigationService.NavigateTo(nameof(WordClassViewEn), this);
                    break;
                default:
                    this.navigationService.NavigateTo(nameof(WordClassViewDe), this);
                    break;
            }


        });




        public ICommand NavigateBack => new RelayCommand(parameter =>
        {
      

            if (isPracticeCalled)
            {
                this.isPracticeCalled = false;
                this.navigationService.NavigateTo(nameof(ModusView), this);
            }


            this.navigationService.NavigateTo(nameof(MenuEntryView),this);


            //this.navigationService.NavigateTo(nameof(ModusView));

            //Quiz eklendikten sonra Übung enable olacak quiz onu acacak gibi seyler eklenmesi gerekiyor. 

        });




    }
}
