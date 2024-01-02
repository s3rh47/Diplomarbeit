using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Login_and_Registration.Views;
using Hortrainingsprogramm.Services;


namespace Hortrainingsprogramm.Login_and_Registration.ViewModels
{
    public class NavigatorLogin : ObservableObject
    {

        private readonly INavigationService navigationService;


        public object CurrentViewLeft { get; set; }
        public object CurrentViewRight { get; set; }


        public NavigatorLogin(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.navigationService.NavigationRequested += OnNavigationRequested;

            // Navigate to the LeftView in Left Menu on startup
            this.navigationService.NavigateTo(nameof(LeftView));
            // Navigate to the LoginView in Right Menu on Startup
            this.navigationService.NavigateTo(nameof(LoginView));
        }


        private void OnNavigationRequested(object sender, NavigationEventArgs eventArgs)
        {
            switch (eventArgs.View)
            {
                case LeftView:
                case SecondLeftView: CurrentViewLeft = eventArgs.View; break;

                case LoginView:
                case RegisterView: CurrentViewRight = eventArgs.View; break;

            }
        }

    }
}
