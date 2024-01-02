using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Languages;
using Hortrainingsprogramm.Main_Window.Views.LeftMenus;
using Hortrainingsprogramm.Services;
using System.Windows.Input;
using Hortrainingsprogramm.Main_Window.Views.RightMenus;
using Hortrainingsprogramm.Main_Window.ViewModels.RightMenus;

namespace Hortrainingsprogramm.Main_Window.ViewModels.LeftMenus
{
    public class MenuEntryViewModel : BaseViewModel
    {
        private readonly INavigationService navigationService;
        public override BaseLanguage baseLanguage { get; set; }
        public override bool isPracticeCalled { get; set; } = false;
        public override bool isQuizCalled { get; set; }
        public bool isModusCalled { get; set; }
        public string NavigateBackButtonTitle { get; set; }
        public string WordButtonTitle { get; set; }
        public string ZahlButtonTitle { get; set; }
        public string SatzButtonTitle { get; set; }



        public MenuEntryViewModel(INavigationService navigationService)
        {

            this.navigationService = navigationService;
            this.navigationService.NavigationRequested += OnNavigationRequested;

        }


        private void OnNavigationRequested(object sender, NavigationEventArgs eventArgs)
        {
            this.isModusCalled = false;

            if (eventArgs.Data is ModusViewModel modusModel)
            {
      
                this.baseLanguage = modusModel.baseLanguage;
                this.NavigateBackButtonTitle = baseLanguage.NavigateBackButtonTitle;
                this.WordButtonTitle = baseLanguage.WordButtonTitle;
                this.ZahlButtonTitle = baseLanguage.ZahlButtonTitle;
                this.SatzButtonTitle = baseLanguage.SatzButtonTitle;
            }

        }



        public ICommand NavigateBack => new RelayCommand(parameter =>
        {

            this.isModusCalled = true;


            if (isPracticeCalled)
            {
                this.isPracticeCalled = false;

            }

            this.navigationService.NavigateTo(nameof(ModusView), this);
            this.navigationService.NavigateTo(nameof(ZeroView));


        });



        public ICommand WorterViewCommand => new RelayCommand(parameter =>
        {
            this.navigationService.NavigateTo(nameof(WordChoiseView), this);


        });



        public ICommand ZahlenViewCommand => new RelayCommand(parameter =>
        {

            this.navigationService.NavigateTo(nameof(NumberView), this);

        });


        public ICommand SentenceViewCommand => new RelayCommand(parameter =>
        {

            this.navigationService.NavigateTo(nameof(SentenceView), this);

        });


    }
}
