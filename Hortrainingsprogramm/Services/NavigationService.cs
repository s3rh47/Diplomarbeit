using System;

namespace Hortrainingsprogramm.Services
{
    public class NavigationService : INavigationService
    {
        // SingleTon machen.
        public static NavigationService Instance { get; } = new();

        private NavigationService() {}

        public event EventHandler<NavigationEventArgs> NavigationRequested;

        public void NavigateTo(string viewName, object data = null)
        {
            NavigationRequested?.Invoke(this, new NavigationEventArgs(viewName, data));
        }


    }
}

