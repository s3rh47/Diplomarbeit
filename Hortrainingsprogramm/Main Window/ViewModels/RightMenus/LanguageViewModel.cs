using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Languages;
using Hortrainingsprogramm.Main_Window.Models;
using Hortrainingsprogramm.Main_Window.Views.RightMenus;
using Hortrainingsprogramm.Services;
using System.Windows.Input;

namespace Hortrainingsprogramm.Main_Window.ViewModels.RightMenus
{
    public class LanguageViewModel : BaseViewModel
    {

        private readonly INavigationService navigationService;
        public override BaseLanguage baseLanguage { get; set; }
        public override bool isPracticeCalled { get; set; } = false;
        public override bool isQuizCalled { get; set; } = false;


        public SprachDatenbank datenbank { get; set; }

        public LanguageViewModel(INavigationService navigationService)
        {
            this.datenbank = new();
            this.navigationService = navigationService;
        }


        public ICommand LanguageButtonCommand => new RelayCommand(parameter => {

            this.baseLanguage = parameter switch
            {
                "Deutsch" => new Deutsch(),
                "Turkce" => new Turkce(),
                "English" => new English()
            };

            this.baseLanguage.datenbank = this.datenbank;

            this.navigationService.NavigateTo(nameof(ModusView), this);

        });



    }
}
