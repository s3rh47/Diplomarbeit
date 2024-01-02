using System;
using System.Collections.Generic;
using Hortrainingsprogramm.Login_and_Registration.Views;
using Hortrainingsprogramm.Main_Window.Views.LeftMenus;
using Hortrainingsprogramm.Main_Window.Views.RightMenus;
using Hortrainingsprogramm.Main_Window.Views;
using Hortrainingsprogramm.Practice_and_Quiz_Menu.Views;

namespace Hortrainingsprogramm.Services
{
    public class NavigationEventArgs : EventArgs
    {
        public object View { get; set; }
        public object Data { get; set; }


        private static readonly Dictionary<string, Func<object>> ViewFactories = new()
        {
            //Login an Register Section
            { nameof(LeftView), () => new LeftView() },
            { nameof(LoginView), () => new LoginView() },
            { nameof(RegisterView), () => new RegisterView() },
            { nameof(SecondLeftView), () => new SecondLeftView() },

            // Main Section
            { nameof(MainView), () => new MainView() },
            { nameof(LanguageView), () => new LanguageView() },
            { nameof(ModusView), () => new ModusView() },
            { nameof(MenuEntryView), () => new MenuEntryView() },
            { nameof(WordChoiseView), () => new WordChoiseView() },
            { nameof(ZeroView), () => new ZeroView() },
            { nameof(NumberView), () => new NumberView() },
            { nameof(MixedWordView), () => new MixedWordView() },
            { nameof(CommonWordView), () => new CommonWordView() },
            { nameof(WordClassViewDe), () => new WordClassViewDe() },
            { nameof(WordClassViewTr),() => new WordClassViewTr() },
            { nameof(WordClassViewEn), () => new WordClassViewEn() },
            { nameof(SentenceView),() => new SentenceView() },
            
                

            //Practice Window Section
            { nameof(PracticeView), () => new PracticeView() },



            //Quiz Section 
            { nameof(QuizView), () => new QuizView() },

        };
       
        public NavigationEventArgs(string viewName, object data)
        {
           
            View = GetViewByName(viewName);
            Data = data;
        }


        private static object GetViewByName(string viewName)
        {
            if (ViewFactories.TryGetValue(viewName, out var factory))
            {
               
                return factory();
            }
            else
            {
                throw new ArgumentException($"Ungültiger View-Name: {viewName}");
            }


        }
    }
}

//public class NavigationEventArgs : EventArgs
//{

//    public object View { get; set; }
//    public object Data { get; set; }

//    public NavigationEventArgs(string viewName, object data)
//    {
//        View = GetViewByName(viewName);
//        Data = data;
//    }

//    private static object GetViewByName(string viewName)
//    {
//        // Here you can use a switch statement or a dictionary lookup to
//        // return the appropriate view instance based on the view name.
//        // For example:
//        switch (viewName)
//        {

//            // Login und Registration Section
//            case nameof(LeftView):
//                return new LeftView();
//            case nameof(LoginView):
//                return new LoginView();
//            case nameof(RegisterView):
//                return new RegisterView();
//            case nameof(SecondLeftView):
//                return new SecondLeftView();

//            // Main Window Section

//            case nameof(MainView):
//                return new MainView();
//            case nameof(LanguageView):
//                return new LanguageView();
//            case nameof(ModusView):
//                return new ModusView();
//            case nameof(MenuEntryView):
//                return new MenuEntryView();
//            case nameof(WordChoiseView):
//                return new WordChoiseView();
//            case nameof(ZeroView):
//                return new ZeroView();


//            default:
//                throw new ArgumentException($"Invalid view name: {viewName}");
//        }
//    }
//}
