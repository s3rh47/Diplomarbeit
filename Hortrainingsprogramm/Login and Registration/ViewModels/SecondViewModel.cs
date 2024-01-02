using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Services;

namespace Hortrainingsprogramm.Login_and_Registration.ViewModels
{
    public class SecondViewModel : ObservableObject
    {


        private readonly INavigationService navigationService;

        public SecondViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

        }

    }
}
