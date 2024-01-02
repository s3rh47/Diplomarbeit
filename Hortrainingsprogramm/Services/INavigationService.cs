using System;

namespace Hortrainingsprogramm.Services
{
    public interface INavigationService
    {
        event EventHandler<NavigationEventArgs> NavigationRequested;

        void NavigateTo(string viewName, object data = null! );

    }
}
