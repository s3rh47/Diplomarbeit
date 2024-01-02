using Hortrainingsprogramm.Main_Window.Models;
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace Hortrainingsprogramm.Languages
{


    public abstract class BaseLanguage
    {
        //MainWindow Section
        public abstract string LoggedUserTitle { get; set; }
        public abstract string logoutButtonText { get; set; }


        // Modus Section
        public abstract string ModusTitle { get; set; }
        public abstract string PracticeButtonTitle { get; set; }
        public abstract string QuizButtonTitle { get; set; }


        //MenuEntry Section
        public abstract string NavigateBackButtonTitle { get; set; }
        public abstract string WordButtonTitle { get; set; }
        public abstract string ZahlButtonTitle { get; set; }
        public abstract string SatzButtonTitle { get; set; }


        //WordChoiseView Section 
        public abstract string WordChoiseButtonTitle { get; set; }
        public abstract string AllWordsTitle { get; set; }
        public abstract string CommonWordTitle { get; set; }
        public abstract string WordtypeTitle { get; set; }



        //Google TextToSpeech Section
        public abstract string googleTSSselectedLanguage { get; set; }
        public abstract Dictionary<string, string> googleTTSvoiceList { get; }




        //CommonView Section
        public abstract string CommonWordButtonTitle { get; set; }
        public abstract string CommonWordAllWordsTitle { get; set; }
        public abstract string NamesTitle { get; set; }
        public abstract string AnimalsTitle { get; set; }
        public abstract string PlantsTitle { get; set; }
        public abstract string ObjectsTitle { get; set; }
        public abstract string CountriesTitle { get; set; }
        public abstract string CapitalsTitle { get; set; }
        public abstract string MetropoleTitle { get; set; }
        public abstract string CitiesTitle { get; set; }
        public abstract string FoodsTitle { get; set; }
        public abstract string DishTitle { get; set; }
        public abstract string DrinksTitle { get; set; }
        public abstract string BrandsTitle { get; set; }
        public abstract string FamilyMembersTitle { get; set; }
        public abstract string ColorsTitle { get; set; }
        public abstract string BodyPartsTitle { get; set; }
        public abstract string MonthsTitle { get; set; }
        public abstract string DaysTitle { get; set; }


        //NumberView Section

        public abstract string NumberViewButtonTitle { get; set; }
        public abstract string NumberViewlAllNumbersTitle { get; set; }
        public abstract string OneDigitNumberTitle { get; set; }
        public abstract string TwoDigitNumberTitle { get; set; }
        public abstract string ThreeDigitNumberTitle { get; set; }

        public abstract string ThousandDigitNumberTitle { get; set; }
        public abstract string MillionDigitNumberTitle { get; set; }
        public abstract string BillionDigitNumberTitle { get; set; }
        public abstract string TrillionDigitNumberTitle { get; set; }
        public abstract string QuadrillionDigitNumberTitle { get; set; }



        //MixedWordView Section

        public abstract string MixedWordViewButtonTitle { get; set; }
        public abstract string MixedWordAllWordsTitle { get; set; }
        public abstract string TwoOrThreeLetterTitle { get; set; }
        public abstract string FourLetterTitle { get; set; }
        public abstract string FiveLetterTitle { get; set; }
        public abstract string SixLetterTitle { get; set; }
        public abstract string SevenOrHighTitle { get; set; }


        //SentenceView Section

        public abstract string SentenceViewButtonTitle { get; set; }
        public abstract string SentenceAllSentencesTitle { get; set; }
        public abstract string MeaningfullSenctenceTitle { get; set; }
        public abstract string MeaninglessSenctenceTitle { get; set; }
        public abstract string ProverbTitle { get; set; }



        //PracticeView Section 

        public abstract string SpeakingRateTitle { get; set; }
        public abstract string PitchValueTitle { get; set; }
        public abstract string VolumeTitle { get; set; }
        public abstract string RoundCounterTitle { get; set; }
        public abstract string VoiceTitle { get; set; }
        public abstract string MessageBoxCaption { get; set; }
        public abstract string GameOverText { get; set; }



        //PractiveView TextBox Section

        public abstract string OpenLetterButtonText { get; set; }
        public abstract string CheckAnswerButtonText { get; set; }
        public abstract string OpenWordButtonText { get; set; }
        public abstract string TextBoxWaterMarkText { get; set; }
        public abstract string CorrectAnswerTitle { get; set; }


        //Setting Section 

        public abstract string SettingCaption { get; set; }
        public abstract string ButtonModeText { get; set; }
        public abstract string TextBoxModeText { get; set; }
        public abstract string SaveButtonText { get; set; }

        public abstract string GamesRoundTitle10 { get; set; }
        public abstract string GamesRoundTitle20 { get; set; }
        public abstract string GamesRoundTitle30 { get; set; }

        public abstract string RoundCaption { get; set; }


        //Datenbank Section

        public abstract LinkedList<string> databaseList { get; set; }
        public abstract SprachDatenbank datenbank { get; set; }




        // Quiz Section

        public abstract string SkipButtonText { get;set;}
        public abstract string RemainingTitel { get; set; }


    }
}
