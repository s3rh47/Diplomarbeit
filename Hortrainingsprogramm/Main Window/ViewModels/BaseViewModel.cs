using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Languages;


namespace Hortrainingsprogramm.Main_Window.ViewModels
{
   public abstract class BaseViewModel : ObservableObject
    {
        public abstract BaseLanguage baseLanguage { get; set; }
        public abstract bool isPracticeCalled { get; set; }
        public abstract bool isQuizCalled { get; set; }

    }
}
