using Hortrainingsprogramm.Main_Window.Models;
using System.Collections.Generic;

namespace Hortrainingsprogramm.Languages
{
    public class Deutsch : BaseLanguage
    {
        //MainWindow Section
        public override string LoggedUserTitle { get; set; } = "Angemeldet als:";
        public override string logoutButtonText { get; set; } = "Ausloggen";


        //Modus Section
        public override string ModusTitle { get; set; } = "Mode auswählen";
        public override string PracticeButtonTitle { get; set; } = "Übung";
        public override string QuizButtonTitle { get; set; } = "Test";


        //MenuEntry Section
        public override string NavigateBackButtonTitle { get; set; } = "Zurück zu Modus";
        public override string WordButtonTitle { get; set; } = "WÖRTER";
        public override string ZahlButtonTitle { get; set; } = "ZAHLEN";
        public override string SatzButtonTitle { get; set; } = "SÄTZE";


        //WordChoiseView Section 
        public override string WordChoiseButtonTitle { get; set; } = "Wort wählen";
        public override string AllWordsTitle { get; set; } = "Alle Wörter";
        public override string CommonWordTitle { get; set; } = "Wörter im Alltag";
        public override string WordtypeTitle { get; set; } = "Wortarten";



        //CommonWordView Section
        public override string CommonWordButtonTitle { get; set; } = "Wörter im Alltag";
        public override string CommonWordAllWordsTitle { get; set; } = "Alle Wörter (Gem.)";
        public override string NamesTitle { get; set; } = "Vor- und Nachnamen";
        public override string AnimalsTitle { get; set; } = "Tiere";
        public override string PlantsTitle { get; set; } = "Pflanzen";
        public override string ObjectsTitle { get; set; } = "Gegenstände";
        public override string CountriesTitle { get; set; } = "Länder";
        public override string CapitalsTitle { get; set; } = "Hauptstädte";
        public override string MetropoleTitle { get; set; } = "Metropolen";
        public override string CitiesTitle { get; set; } = "Städte (DACH)";
        public override string FoodsTitle { get; set; } = "Nahrungsmittel";
        public override string DishTitle { get; set; } = "Speisen";
        public override string DrinksTitle { get; set; } = "Getränke";
        public override string BrandsTitle { get; set; } = "Marken";
        public override string FamilyMembersTitle { get; set; } = "Familien Mitglieder";
        public override string ColorsTitle { get; set; } = "Farben";
        public override string BodyPartsTitle { get; set; } = "Körperteile";
        public override string MonthsTitle { get; set; } = "Monate";
        public override string DaysTitle { get; set; } = "Tage";


        //PracticeView Section 
        public override string GameOverText { get; set; } = "Spiel beendet";
        public override string SpeakingRateTitle { get; set; } = "Geschwindigkeit";
        public override string PitchValueTitle { get; set; } = "Tonhöhe";
        public override string VolumeTitle { get; set; } = "Lautstärke";
        public override string RoundCounterTitle { get; set; } = "Runde:   ";
        public override string VoiceTitle { get; set; } = "Stimmen";
        public override string MessageBoxCaption { get; set; } = "Meldung";



        //PractiveView TextBox Section

        public override string OpenLetterButtonText { get; set; } = "Öffne Buchstabe";
        public override string CheckAnswerButtonText { get; set; } = "Prüfen";
        public override string OpenWordButtonText { get; set; } = "Zeige Inhalt";
        public override string TextBoxWaterMarkText { get; set; } = "Bitte geben Sie den Text ein";
        public override string CorrectAnswerTitle { get; set; } = "Richtige Antwort: ";


        //Setting Section 

        public override string SettingCaption { get; set; } = "Einstellungen";
        public override string ButtonModeText { get; set; } = "Button Modus";
        public override string TextBoxModeText { get; set; } = "TextBox Modus";
        public override string SaveButtonText { get; set; } = "Speichern & Exit";

        public override string GamesRoundTitle10 { get; set; } = "10 Runde";
        public override string GamesRoundTitle20 { get; set; } = "20 Runde";
        public override string GamesRoundTitle30 { get; set; } = "30 Runde";
        public override string RoundCaption { get; set; } = "Wähle Rundenanzahl aus (Neuladen ist erförderlich)";


        //Quiz Section 

        public override string SkipButtonText { get; set; } = "Überspringen";
        public override string RemainingTitel { get; set; } = "Rest: ";


        //NumberView Section

        public override string NumberViewButtonTitle { get; set; } = "Wähle Zahl aus";
        public override string NumberViewlAllNumbersTitle { get; set; } = "Alle Zahlen (Gem.)";
        public override string OneDigitNumberTitle { get; set; } = "1-Stellig";
        public override string TwoDigitNumberTitle { get; set; } = "2-Stellig";
        public override string ThreeDigitNumberTitle { get; set; } = "3-Stellig";
        public override string ThousandDigitNumberTitle { get; set; } = "Tausenderstelle";
        public override string MillionDigitNumberTitle { get; set; } = "Millionen";
        public override string BillionDigitNumberTitle { get; set; } = "Milliarden";
        public override string TrillionDigitNumberTitle { get; set; } = "Billionen";
        public override string QuadrillionDigitNumberTitle { get; set; } = "Billiarden";




        //MixedWordView Section

        public override string MixedWordViewButtonTitle { get; set; } = "Wähle Wörter aus";
        public override string MixedWordAllWordsTitle { get; set; } = "Alle Wörter";
        public override string TwoOrThreeLetterTitle { get; set; } = "2 oder 3 Buchstaben";
        public override string FourLetterTitle { get; set; } = "4 Buchstaben";
        public override string FiveLetterTitle { get; set; } = "5 Buchstaben";
        public override string SixLetterTitle { get; set; } = "6 Buchstaben";
        public override string SevenOrHighTitle { get; set; } = "7 oder mehr";




        //SentenceView Section

        public override string SentenceViewButtonTitle { get; set; } = "Wähle Satz aus";
        public override string SentenceAllSentencesTitle { get; set; } = "Alle Sätze (Gem.)";
        public override string MeaningfullSenctenceTitle { get; set; } = "Sinnvolle Sätze";
        public override string MeaninglessSenctenceTitle { get; set; } = "Sinnlose Sätze";
        public override string ProverbTitle { get; set; } = "Sprichwörter";





        //Google TextToSpeech Section
        public override string googleTSSselectedLanguage { get; set; } = "de-DE";
        
        // Tkey , TValue
        public override Dictionary<string, string> googleTTSvoiceList { get; } = new Dictionary<string, string>()
        {

           { "de-DE-Wavenet-A", "Frau (Wavenet-A)" },
           { "de-DE-Wavenet-B", "Mann (Wavenet-B)" },
           { "de-DE-Wavenet-C", "Frau (Wavenet-C)" },
           { "de-DE-Wavenet-D", "Mann (Wavenet-D)" },
           { "de-DE-Wavenet-E", "Mann (Wavenet-E)" },
           { "de-DE-Wavenet-F", "Frau (Wavenet-F)" }

        };


        //Database Section

        public override LinkedList<string> databaseList { get; set; }
        public override SprachDatenbank datenbank { get; set; }

    }
}
