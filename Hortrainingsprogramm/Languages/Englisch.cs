using Hortrainingsprogramm.Main_Window.Models;
using System.Collections.Generic;

namespace Hortrainingsprogramm.Languages
{
    public  class English : BaseLanguage
    {
        //MainWindow Section
        public override string LoggedUserTitle { get; set; } = "Logged as:";
        public override string logoutButtonText { get; set; } = "L o g o u t";

        // Modus Section
        public override string ModusTitle { get; set; } = "Select the mode";
        public override string PracticeButtonTitle { get; set; } = "Practice";
        public override string QuizButtonTitle { get; set; } = "Quiz";

        //MenuEntry Section
        public override string NavigateBackButtonTitle { get; set; } = "Back to Module";
        public override string WordButtonTitle { get; set; } = "WORDS";
        public override string ZahlButtonTitle { get; set; } = "NUMBERS";
        public override string SatzButtonTitle { get; set; } = "SENTENCES";


        //WordChoiseView Section 
        public override string WordChoiseButtonTitle { get; set; } = "Choice word";
        public override string AllWordsTitle { get; set; } = "All Words";
        public override string CommonWordTitle { get; set; } = "Words in daily life";
        public override string WordtypeTitle { get; set; } = "Word Classes";


        //CommonView Section
        public override string CommonWordButtonTitle { get; set; } = "Words in daily life";
        public override string CommonWordAllWordsTitle { get; set; } = "All Words (Mix.)";
        public override string NamesTitle { get; set; } = "First and lastnames";
        public override string AnimalsTitle { get; set; } = "Animals";
        public override string PlantsTitle { get; set; } = "Plants";
        public override string ObjectsTitle { get; set; } = "Objects";
        public override string CountriesTitle { get; set; } = "Countries";
        public override string CapitalsTitle { get; set; } = "Capitals";
        public override string MetropoleTitle { get; set; } = "Metropole";
        public override string CitiesTitle { get; set; } = "Cities";
        public override string FoodsTitle { get; set; } = "Foods";
        public override string DishTitle { get; set; } = "Dishes";
        public override string DrinksTitle { get; set; } = "Drinks";
        public override string BrandsTitle { get; set; } = "Brands";
        public override string FamilyMembersTitle { get; set; } = "Family members";
        public override string ColorsTitle { get; set; } = "Colors";
        public override string BodyPartsTitle { get; set; } = "Body parts";
        public override string MonthsTitle { get; set; } = "Months";
        public override string DaysTitle { get; set; } = "Days";



        //PracticeView Section 
        public override string GameOverText { get; set; } = "Game Over";
        public override string SpeakingRateTitle { get; set; } = "Speaking Rate";
        public override string PitchValueTitle { get; set; } = "Pitch Value";
        public override string VolumeTitle { get; set; } = "Volume";
        public override string RoundCounterTitle { get; set; } = "Round:   ";
        public override string VoiceTitle { get; set; } = "Voice";
        public override string MessageBoxCaption { get; set; } = "Message";



        //PractiveView TextBox Section

        public override string OpenLetterButtonText { get; set; } = "Open a letter";
        public override string CheckAnswerButtonText { get; set; } = "Check Answer";
        public override string OpenWordButtonText { get; set; } = "Show content";
        public override string TextBoxWaterMarkText { get; set; } = "Please enter a text";
        public override string CorrectAnswerTitle { get; set; } = "Correct word: ";



        // Quiz Section 

        public override string SkipButtonText { get; set; } = "Skip";
        public override string RemainingTitel { get; set; } = "Remaining: ";


        //Setting Section 

        public override string SettingCaption { get; set; } = "Settings";
        public override string ButtonModeText { get; set; } = "Button Mode";
        public override string TextBoxModeText { get; set; } = "TextBox Mode";
        public override string SaveButtonText { get; set; } = "Save & Exit";

        public override string GamesRoundTitle10 { get; set; } = "10 Round";
        public override string GamesRoundTitle20 { get; set; } = "20 Round";
        public override string GamesRoundTitle30 { get; set; } = "30 Round";
        public override string RoundCaption { get; set; } = "Select Round count (Reloading is required)";


        //NumberView Section

        public override string NumberViewButtonTitle { get; set; } = "Numbers";
        public override string NumberViewlAllNumbersTitle { get; set; } = "All Numbers (Mix.)";
        public override string OneDigitNumberTitle { get; set; } = "1-Digit";
        public override string TwoDigitNumberTitle { get; set; } = "2-Digit";
        public override string ThreeDigitNumberTitle { get; set; } = "3-Digit";
        public override string ThousandDigitNumberTitle { get; set; } = "Thousands digit";
        public override string MillionDigitNumberTitle { get; set; } = "Millions";
        public override string BillionDigitNumberTitle { get; set; } = "Billions";
        public override string TrillionDigitNumberTitle { get; set; } = "Trillions";
        public override string QuadrillionDigitNumberTitle { get; set; } = "Quadrillion";



        //MixedWordView Section

        public override string MixedWordViewButtonTitle { get; set; } = "Select Word";
        public override string MixedWordAllWordsTitle { get; set; } = "All Words";
        public override string TwoOrThreeLetterTitle { get; set; } = "2 or 3 Letter";
        public override string FourLetterTitle { get; set; } = "4 Letter";
        public override string FiveLetterTitle { get; set; } = "5 Letter";
        public override string SixLetterTitle { get; set; } = "6 Letter";
        public override string SevenOrHighTitle { get; set; } = "7 or more Letter";



        //SentenceView Section

        public override string SentenceViewButtonTitle { get; set; } = "Select Sentence";
        public override string SentenceAllSentencesTitle { get; set; } = "All Sentences (Mix.)";
        public override string MeaningfullSenctenceTitle { get; set; } = "Meaningfull Sentences";
        public override string MeaninglessSenctenceTitle { get; set; } = "Meaningless Sentences";
        public override string ProverbTitle { get; set; } = "Proverbs";



        //Google TextToSpeech Section
        public override string googleTSSselectedLanguage { get; set; } = "en-US";

        // Tkey , TValue
        public override Dictionary<string, string> googleTTSvoiceList { get; } = new()
        {
           { "en-US-Wavenet-A", "Man (Wavenet-A)" },
           { "en-US-Wavenet-B", "Man (Wavenet-B)" },
           { "en-US-Wavenet-C", "Woman (Wavenet-C)" },
           { "en-US-Wavenet-D", "Man (Wavenet-D)" },
           { "en-US-Wavenet-E", "Woman (Wavenet-E)" },
           { "en-US-Wavenet-F", "Woman (Wavenet-F)" },
           { "en-US-Wavenet-G", "Woman (Wavenet-G)" },
           { "en-US-Wavenet-H", "Woman (Wavenet-H)" },
           { "en-US-Wavenet-I", "Man (Wavenet-I)" },
           { "en-US-Wavenet-J", "Man (Wavenet-J)" }
        };


        //Database Section

        public override LinkedList<string> databaseList { get; set; }
        public override SprachDatenbank datenbank { get; set; }




    }
}
