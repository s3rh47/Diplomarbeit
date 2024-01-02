using Hortrainingsprogramm.Login_and_Registration.ViewModels;
using Hortrainingsprogramm.Main_Window.ViewModels;
using Hortrainingsprogramm.Main_Window.ViewModels.LeftMenus;
using Hortrainingsprogramm.Main_Window.ViewModels.RightMenus;
using Hortrainingsprogramm.Practice_and_Quiz_Menu.ViewModels;

namespace Hortrainingsprogramm.Services
{
    public class ViewModelLocator
    {

        // SingleTon machen.
        public static ViewModelLocator Instance { get; } = new();

        private ViewModelLocator() { }

        // Login und Register Section 

        public NavigatorLogin NavigatorLogin => new(NavigationService.Instance);

        public LoginViewModel LoginViewModel { get; } = new(NavigationService.Instance);

        public LeftViewModel LeftViewModel { get; } = new(NavigationService.Instance);

        public RegisterViewModel RegisterViewModel { get; } = new(NavigationService.Instance);

        public SecondViewModel SecondViewModel { get; } = new(NavigationService.Instance);


        // Main Window Section 

        public MainViewModel MainViewModel => new(NavigationService.Instance);

        public LanguageViewModel LanguageViewModel { get; } = new(NavigationService.Instance);

        public ModusViewModel ModusViewModel { get; } = new(NavigationService.Instance);

        public MenuEntryViewModel MenuEntryViewModel { get; } = new(NavigationService.Instance);

        public WordChoiseViewModel WordChoiseViewModel { get; } = new(NavigationService.Instance);

        public CommonWordViewModel CommonWordViewModel { get; } = new(NavigationService.Instance);

        public NumberViewModel NumberViewModel { get; } = new(NavigationService.Instance);

        public MixedWordViewModel MixedWordViewModel { get; } = new(NavigationService.Instance);

        public SentenceViewModel SentenceViewModel { get; } = new(NavigationService.Instance);

        public WordClassViewModel WordClassViewModel { get; } = new(NavigationService.Instance);




        //Practice Window Section

        public PracticeViewModel PracticeViewModel { get; } = new(NavigationService.Instance);

        public SettingViewModel SettingViewModel { get; } = new();


        //Quiz Section

        public QuizViewModel QuizViewModel { get; } = new(NavigationService.Instance);

    }
}
