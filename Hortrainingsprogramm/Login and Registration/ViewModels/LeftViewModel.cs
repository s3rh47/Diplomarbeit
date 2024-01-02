using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Login_and_Registration.Views;
using Hortrainingsprogramm.Services;
using System.Windows.Input;

namespace Hortrainingsprogramm.Login_and_Registration.ViewModels
{
    public class LeftViewModel : ObservableObject
    {
       


        private readonly INavigationService navigationService;

        public LeftViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
         
        }


        public ICommand OpenRegistrationMenuCommand => new RelayCommand(parameter => {
          
            this.navigationService.NavigateTo(nameof(RegisterView));
            this.navigationService.NavigateTo(nameof(SecondLeftView));

        });


    }

}
