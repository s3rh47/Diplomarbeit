using Hortrainingsprogramm.Main_Window.Models;
using System.Collections.Generic;

namespace Hortrainingsprogramm.Languages
{
    public class Turkce : BaseLanguage
    {
        //MainWindow Section
        public override string LoggedUserTitle { get; set; } = "Kullanici adi:";
        public override string logoutButtonText { get; set; } = "Cikis Yap";

        // Modus Section
        public override string ModusTitle { get; set; } = "Mod Secin";
        public override string PracticeButtonTitle { get; set; } = "Calisma";
        public override string QuizButtonTitle { get; set; } = "Test";

        //MenuEntry Section
        public override string NavigateBackButtonTitle { get; set; } = "Modusa geri git";
        public override string WordButtonTitle { get; set; } = "KELIMELER";
        public override string ZahlButtonTitle { get; set; } = "SAYILAR";
        public override string SatzButtonTitle { get; set; } = "CÜMLELER";


        //WordChoiseView Section 
        public override string WordChoiseButtonTitle { get; set; } = "Kelime sec";
        public override string AllWordsTitle { get; set; } = "Tüm Kelimeler";
        public override string CommonWordTitle { get; set; } = "Gündelik Kelimeler";
        public override string WordtypeTitle { get; set; } = "Kelime Cesitleri";




        //CommonView Section
        public override string CommonWordButtonTitle { get; set; } = "Gündelik Kelimeler";
        public override string CommonWordAllWordsTitle { get; set; } = "Tüm Kelimeler (Kar.)";
        public override string NamesTitle { get; set; } = "Isim ve Soyisimler";
        public override string AnimalsTitle { get; set; } = "Hayvanlar";
        public override string PlantsTitle { get; set; } = "Bitkiler";
        public override string ObjectsTitle { get; set; } = "Esyalar";
        public override string CountriesTitle { get; set; } = "Ülkeler";
        public override string CapitalsTitle { get; set; } = "Baskentler";
        public override string MetropoleTitle { get; set; } = "Metropole";
        public override string CitiesTitle { get; set; } = "Sehirler";
        public override string FoodsTitle { get; set; } = "Gidalar";
        public override string DishTitle { get; set; } = "Yemekler";
        public override string DrinksTitle { get; set; } = "Icecekler";
        public override string BrandsTitle { get; set; } = "Markalar";
        public override string FamilyMembersTitle { get; set; } = "Aile üyeleri";
        public override string ColorsTitle { get; set; } = "Renkler";
        public override string BodyPartsTitle { get; set; } = "Vucudumuz";
        public override string MonthsTitle { get; set; } = "Aylar";
        public override string DaysTitle { get; set; } = "Günler";


        //PractiveView Section
        public override string GameOverText { get; set; } = "Oyun Bitti";
        public override string SpeakingRateTitle { get; set; } = "Ses hızı";
        public override string PitchValueTitle { get; set; } = "Ses kalınlığı";
        public override string VolumeTitle { get; set; } = "Ses yüksekliği";
        public override string RoundCounterTitle { get; set; } = "Tur:   ";
        public override string VoiceTitle { get; set; } = "Kişi Sesi";
        public override string MessageBoxCaption { get; set; } = "Mesaj";


        //PractiveView TextBox Section

        public override string OpenLetterButtonText { get; set; } = "Harf ac";
        public override string CheckAnswerButtonText { get; set; } = "Kontrol Et";
        public override string OpenWordButtonText { get; set; } = "Icerigi göster";
        public override string TextBoxWaterMarkText { get; set; } = "Lütfen bir metin girin";
        public override string CorrectAnswerTitle { get; set; } = "Dogru Cevap: ";


        //Quiz Section

        public override string SkipButtonText { get; set; } = "Atla";
        public override string RemainingTitel { get; set; } = "Kalan: ";


        //Setting Section 

        public override string SettingCaption { get; set; } = "Ayarlar";
        public override string ButtonModeText { get; set; } = "Buton Modu";
        public override string TextBoxModeText { get; set; } = "TextBox Modu";
        public override string SaveButtonText { get; set; } = "Kaydet & Cik";

        public override string GamesRoundTitle10 { get; set; } = "10 Tur";
        public override string GamesRoundTitle20 { get; set; } = "20 Tur";
        public override string GamesRoundTitle30 { get; set; } = "30 Tur";

        public override string RoundCaption { get; set; } = "Oyun Turunu Secin (Yeniden yükleme gereklidir)";


        //NumberView Section

        public override string NumberViewButtonTitle { get; set; } = "Sayilar";
        public override string NumberViewlAllNumbersTitle { get; set; } = "Tüm Sayilar";
        public override string OneDigitNumberTitle { get; set; } = "1-Basamakli";
        public override string TwoDigitNumberTitle { get; set; } = "2-Basamakli";
        public override string ThreeDigitNumberTitle { get; set; } = "3-Basamakli";
        public override string ThousandDigitNumberTitle { get; set; } = "Binler Basamagi";
        public override string MillionDigitNumberTitle { get; set; } = "Milyonlar";
        public override string BillionDigitNumberTitle { get; set; } = "Milyarlar";
        public override string TrillionDigitNumberTitle { get; set; } = "Trilyonlar";
        public override string QuadrillionDigitNumberTitle { get; set; } = "Katrilyonlar";


        //MixedWordView Section

        public override string MixedWordViewButtonTitle { get; set; } = "Kelime Secin";
        public override string MixedWordAllWordsTitle { get; set; } = "Tüm Kelimeler";
        public override string TwoOrThreeLetterTitle { get; set; } = "2 ya da 3 Harfli";
        public override string FourLetterTitle { get; set; } = "4 harfli";
        public override string FiveLetterTitle { get; set; } = "5 Harfli";
        public override string SixLetterTitle { get; set; } = "6 Harfli";
        public override string SevenOrHighTitle { get; set; } = "7 ya da cok Harfli";



        //SentenceView Section

        public override string SentenceViewButtonTitle { get; set; } = "Cümle Secin";
        public override string SentenceAllSentencesTitle { get; set; } = "Tüm Cümleler (Kar.)";
        public override string MeaningfullSenctenceTitle { get; set; } = "Anlamli Cümleler";
        public override string MeaninglessSenctenceTitle { get; set; } = "Anlamsiz Cümleler";
        public override string ProverbTitle { get; set; } = "Atasözleri";



        //Google TextToSpeech Section
        public override string googleTSSselectedLanguage { get; set; } = "tr-TR";

        // Tkey , TValue
        public override Dictionary<string, string> googleTTSvoiceList { get; } = new Dictionary<string, string>()
        {
           { "tr-TR-Wavenet-A", "Kadin (Wavenet-A)" },
           { "tr-TR-Wavenet-B", "Erkek (Wavenet-B)" },
           { "tr-TR-Wavenet-C", "Kadin (Wavenet-C)" },
           { "tr-TR-Wavenet-D", "Kadin (Wavenet-D)" },
           { "tr-TR-Wavenet-E", "Erkek (Wavenet-E)" }
        };


        //Database Section

        public override LinkedList<string> databaseList { get; set; }
        public override SprachDatenbank datenbank { get; set; }

    }
}
