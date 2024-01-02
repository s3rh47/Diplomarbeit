using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Services;
using System;
using System.Windows.Input;
using System.Windows.Media;
using Hortrainingsprogramm.Practice_and_Quiz_Menu.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hortrainingsprogramm.Languages;
using System.Linq;
using Hortrainingsprogramm.Main_Window.Views.LeftMenus;
using Hortrainingsprogramm.Main_Window.Views.RightMenus;
using System.Collections.ObjectModel;
using System.Threading;
using Hortrainingsprogramm.Custom_MessageBox;
using Hortrainingsprogramm.Practice_and_Quiz_Menu.Views;
using System.Windows;
using Hortrainingsprogramm.Main_Window.Views;
using System.Windows.Threading;
using Hortrainingsprogramm.Main_Window.ViewModels;
using System.Xml;

namespace Hortrainingsprogramm.Practice_and_Quiz_Menu.ViewModels
{

    public class PracticeViewModel : BaseViewModel
    {

        #region Variablen , Getter und Setter


        /// <summary>
        /// Variablen im xaml zum Binden 
        /// Müssen public sein
        /// Außer override Variablen müssen Groß geschrieben werden
        /// </summary>
        public override BaseLanguage baseLanguage { get; set; }
        public ObservableCollection<ButtonModel> Buttons { get; set; } = new();
        public override bool isPracticeCalled { get; set; } = true;
        public double OpacityWert { get; set; } = 1;
        public string CorrectWord { get; set; }
        public string RoundCounterToBinding { get; set; }
        public bool IsButtonEnabled { get; set; } = true;

        // Für Titles die Sprache ändert
        public string GameOverText { get; set; }
        public string SpeakingRateTitle { get; set; }
        public string PitchValueTitle { get; set; }
        public string VolumeTitle { get; set; }
        public string RoundCounterTitle { get; set; }
        public string VoiceTitle { get; set; }
        public string MessageBoxCaption { get; set; }
        public string OpenLetterButtonText { get; set; }
        public string CheckAnswerButtonText { get; set; }
        public string OpenWordButtonText { get; set; }
        public string TextBoxWaterMarkText { get; set; }
        public string CorrectAnswerTitle { get; set; }
        public string SettingCaption { get; set; }
        public string ButtonModeText { get; set; }
        public string TextBoxModeText { get; set; }
        public string SaveButtonText { get; set; }


        // Für Visibilities 
        public Visibility ButtonModeVisibility { get; set; }
        public Visibility TextBoxModeVisibility { get; set; }
        public Visibility RichtigeTextBlockVisibility { get; set; }



        //Für Progressbar
        public double ProgresBarMaxValue { get; set; }
        public double ProgresBarCurrentValue { get; set; }


        //Für System Volume
        public int SystemVolume { get; set; }


        //Für Combobox (Stimme)
        public string SelectedComboboxItem { get; set; }


        //Variblen für GTTS Config
        public double SpeakingRate { get; set; } = 1;
        public double PitchValue { get; set; } = 0;
        public double VolumeDecibel { get; set; } = 0;


        //TextBox Bereich 
        public Brush TextBoxBackground { get; set; } = Brushes.Transparent;
        public string IconColor { get; set; } = "white";
        public string KindName { get; set; } = "VolumeUpSolid";
        public string CorrectWordTextBlockText { get; set; }
        public string TextBoxText { get; set; }


        /// <summary>
        /// Public Variablen (nicht zum Binden)
        /// </summary>


        public override bool isQuizCalled { get; set; }


        //Settings Bereich
        public string practiceModeSetting { get; set; } = Settings.Default.PracticeModeSetting;


        /// <summary>
        /// Private Variablen
        /// </summary>
        private readonly INavigationService navigationService;
        private GoogleTextToSpeech googleTextToSpeech = new();
        private List<string> words = new();
        private List<string> savedWords = new();
        private AudioPlayer audioPlayer = new();
        private Random random = new();
        private CancellationTokenSource cancellationTokenSource;
        private string previousLanguage;
        private bool isButtonModeSelected { get; set; } = false;
        private bool isTextboxSelected { get; set; } = true;
        private string letzterText;
        private string temporerText; 


        //Variablen für GTTS Config
        private bool isSpeakingRateTrigged { get; set; }
        private bool isPitchValueTrigged { get; set; }
        private bool isComboboxTrigged { get; set; } = false;


        //Variblen für Runde und Einstellungen
        private int spielCount;
        private bool isBigCount = false;
        private bool gameOver = false;
        private int letterCounter = 0;
        private bool isTempTextLeer = true;


        #endregion

        #region Konstruktur und Methoden

    

        /// <summary>
        /// Konstruktur.
        /// </summary>
        public PracticeViewModel(INavigationService navigationService)
        {

            ModeInitalizer();

            SystemVolume = audioPlayer.GetWindowsVolume();

            isComboboxTrigged = true;

            gameOver = false;

            Buttons.Clear();


            for (int i = 0; i < 4; i++)
            {
                Buttons.Add(new ButtonModel(null, null, null));
            }


            this.navigationService = navigationService;
            this.navigationService.NavigationRequested += OnNavigationRequested;

        }




        /// <summary>
        /// Wird vom Konstruktur gerufen
        /// Diese Methode braucht man, um PropertyChance auszulösen
        /// </summary>
        private async void OnNavigationRequested(object sender, NavigationEventArgs eventArgs)
        {

            if (eventArgs.Data is BaseViewModel Model)
            {
                if (Model.isPracticeCalled)
                {

                    this.cancellationTokenSource = new CancellationTokenSource();



                    this.RichtigeTextBlockVisibility = Visibility.Collapsed;

                    this.KindName = "VolumeUpSolid";
                    this.IconColor = "white";
                    //this.IconGridVisibility = Visibility.Visible;

                    this.isTempTextLeer = true;
                    this.TextBoxBackground = Brushes.Transparent;
                    this.TextBoxText = "";
                    this.letzterText = "";
                    this.letterCounter = 0;

                    this.OpacityWert = 1;

                    this.isPracticeCalled = Model.isPracticeCalled;

                    words.Clear();

                    this.baseLanguage = Model.baseLanguage;



                    if (baseLanguage.googleTSSselectedLanguage != previousLanguage)
                    {
                        previousLanguage = baseLanguage.googleTSSselectedLanguage;

                        // Hier muss man true setzen, weil trigger bei einer Änderung ausgelöst wird.
                        // Das ist unwerwünscht,sonst wird zweimal hinterher GTT abgerufen. 
                        isComboboxTrigged = true;


                        this.SelectedComboboxItem = baseLanguage.googleTTSvoiceList.Keys.First();

                        // setz man wieder false , damit es wieder ausgelöst sein soll. 
                        isComboboxTrigged = false;
                    }


                    foreach (var word in baseLanguage.databaseList)
                    {
                        words.Add(word);
                    }

                    this.MessageBoxCaption = baseLanguage.MessageBoxCaption;
                    this.GameOverText = baseLanguage.GameOverText;
                    this.SpeakingRateTitle = baseLanguage.SpeakingRateTitle;
                    this.VolumeTitle = baseLanguage.VolumeTitle;
                    this.PitchValueTitle = baseLanguage.PitchValueTitle;
                    this.RoundCounterTitle = baseLanguage.RoundCounterTitle;
                    this.VoiceTitle = baseLanguage.VoiceTitle;

                    this.OpenLetterButtonText = baseLanguage.OpenLetterButtonText;
                    this.OpenWordButtonText = baseLanguage.OpenWordButtonText;
                    this.CheckAnswerButtonText = baseLanguage.CheckAnswerButtonText;
                    this.CorrectAnswerTitle = baseLanguage.CorrectAnswerTitle;
                    this.TextBoxWaterMarkText = baseLanguage.TextBoxWaterMarkText;
                    this.spielCount = Settings.Default.roundCount;


                    gameOver = false;

                    InitializeRound();



                    if (!gameOver) await generateSound();

                }
                else
                {
                    this.OpacityWert = 0.2;
                    this.isPracticeCalled = false;
                    //return; Bunu neden yaptim  ?
                }
            }
        }




        /// <summary>
        /// Initalisiert Round
        /// Setzt die neuen Wörter auf Buttons 
        /// Initalisiert auch Buttons und Wörter für Google Text to Speech
        /// </summary>
        private async void InitializeRound()
        {

            if (words.Count > spielCount)
            {
                isBigCount = true;

            }
            else if (words.Count >= spielCount && (words.Count % 4) == 0)
            {
                isBigCount = true;
            }
            else
            {
                isBigCount = false;
            }

            for (int i = 0; i < spielCount; i++)
            {
                savedWords.Clear();

                foreach (var button in Buttons)
                {

                    int index = random.Next(this.words.Count);
                    button.Text = this.words[index];
                    button.ForegroundColor = Brushes.White;
                    button.BackgroundColor = Brushes.Transparent;
                    savedWords.Add(words[index]);
                    this.words.RemoveAt(index);

                }


                //if (!gameOver) // Burasi belki problem cikarabilir. 
                //{


                CorrectWord = Buttons.ElementAt(random.Next(4)).Text;

                if (isBigCount) savedWords.Remove(CorrectWord);

                foreach (var item in savedWords)
                {
                    words.Add(item);
                }


                RoundCounterToBinding = (i + 1 + "  |  " + spielCount);

                await WaitForContinueButtonAsync(cancellationTokenSource.Token);

            }


            gameOver = true;
            MessageBoxMethod(MessageBoxCaption, GameOverText, "okImg");
            words.Clear();

        }




        /// <summary>
        /// Initalisiert die Mode, wenn man von Button
        /// oder TextBox Mode auf die Button oder TextBox Mode wechselt. 
        /// </summary>
        private void ModeInitalizer()
        {

            if (practiceModeSetting.Equals("ButtonMode"))
            {
                letterCounter = 0;
                TextBoxText = "";
                letzterText = "";
                isTempTextLeer = true;
                IconColor = "white";
                KindName = "VolumeUpSolid";
                TextBoxBackground = Brushes.Transparent;
                RichtigeTextBlockVisibility = Visibility.Collapsed;
                ButtonModeVisibility = Visibility.Visible;
                TextBoxModeVisibility = Visibility.Collapsed;
                isButtonModeSelected = true;
                isTextboxSelected = false;
            }
            else
            {

                ButtonModeVisibility = Visibility.Collapsed;
                TextBoxModeVisibility = Visibility.Visible;
                isButtonModeSelected = false;
                isTextboxSelected = true;

            }




        }


        /// <summary>
        /// Diese Methode ruft Google Text to Speech und gereniert Audio Content
        /// </summary>
        public async Task generateSound()
        {
            if (SelectedComboboxItem != null && CorrectWord != null && CorrectWord != string.Empty)
            {
                googleTextToSpeech.synthesisOptions(baseLanguage.googleTSSselectedLanguage, SelectedComboboxItem, SpeakingRate, PitchValue);
                googleTextToSpeech.inputText(CorrectWord);

                var content = googleTextToSpeech.getSoundContent();

                if (content != null)
                {
                    IsButtonEnabled = false;
                    audioPlayer.SetContent(content);
                    ProgresBarMaxValue =  audioPlayer.GetDuration();
                    ProgressBarUpdater();
                    await audioPlayer.PlayAudioAsync();
                    IsButtonEnabled = true;
                }
            }
        }


   

        /// <summary>
        /// Diese Methode wird gerufen, wenn man auf richtigen Button clickt
        /// </summary>
        private async Task CorrectAnswerSequenceAsync(ButtonModel button)
        {
            IsButtonEnabled = false;

            if (button != null)
            {
                // Button grün leuchten lassen

                for (int i = 0; i < 3; i++)
                {

                    button.BackgroundColor = Brushes.Green;
                    await Task.Delay(200);
                    button.BackgroundColor = Brushes.Transparent;
                    await Task.Delay(200);
                    button.BackgroundColor = Brushes.Green;

                }
            }


            // Audio zwei mal abspielen lassen. 

            for (int i = 0; i < 2; i++)
            {
                ProgresBarMaxValue = audioPlayer.GetDuration();
                ProgressBarUpdater();
                await audioPlayer.ReplayAudio();
                await Task.Delay(500);
            }

           

            TextBoxBackground = Brushes.Transparent;

            KindName = "VolumeUpSolid";
            IconColor = "white";
            CorrectWordTextBlockText = "";
            TextBoxText = "";
            letzterText = "";
            isTempTextLeer = true;
            RichtigeTextBlockVisibility = Visibility.Collapsed;
            letterCounter = 0;

            await ContinueAsync();


            if (!gameOver)
            {
                await generateSound();
            }
            else
            {
                gameOver = false;
                navigationService.NavigateTo(nameof(ModusView));
                navigationService.NavigateTo(nameof(ZeroView));
            }
        }



        /// <summary>
        /// Diese Methode sorgt die Funktionalität für Progressbar
        /// </summary>
        private void ProgressBarUpdater()
        {
            // Create a DispatcherTimer to update the ProgressBar's value on the UI thread
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100) // 100 ms interval
            };

            timer.Tick += (sender, e) =>
            {
                // Update the ProgressBar's value using the new method
                ProgresBarCurrentValue = audioPlayer.GetCurrentPlaybackPosition();
            };


            // Stop the timer when the audio playback has stopped


            audioPlayer.PlaybackStopped += async (sender, e) =>
            {
                timer.Stop();
                await Task.Delay(100);
                ProgresBarCurrentValue = 0;
            };


            // Start the timer
            timer.Start();



        }


        /// <summary>
        /// Diese Methode sorgt Forschleife weiterzumachen.  
        /// </summary>
        /// <returns></returns>
        private async Task ContinueAsync()
        {
            cancellationTokenSource.Cancel();
            await Task.Delay(100); // Debouncing için küçük bir bekleme süresi
        }


        /// <summary>
        /// Diese Methode sorgt Forschleife zu warten
        /// </summary>
        private async Task WaitForContinueButtonAsync(CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(Timeout.Infinite, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                cancellationTokenSource.Dispose();
                cancellationTokenSource = new CancellationTokenSource();
            }
        }



        /// <summary>
        /// Diese Methode bearbeitet input Text
        /// Um Speziale Karaktere im Text mit anderen karakteren zu ersetzen
        /// </summary>
        private string TextSchoner(string input)
        {

            input = input.ToLower();

            if (this.baseLanguage is Deutsch)
            {


                input = input.Replace("ß", "ss");
                input = input.Replace("ä", "ae");
                input = input.Replace("ö", "oe");
                input = input.Replace("ü", "ue");

            }
            else if (this.baseLanguage is Turkce)
            {


                input = input.Replace("ğ", "g");
                input = input.Replace("ç", "c");
                input = input.Replace("ş", "s");
                input = input.Replace("ı", "i");

            }



            input = input.Replace("-", "");
            input = input.Replace("'", "");
            input = input.Replace(" ", "");
            input = input.Replace(".", "");
            input = input.Replace("!", "");
            input = input.Replace("?", "");
            input = input.Replace(",", "");
            input = input.Replace("´", "");
            input = input.Replace("`", "");

            input = input.Replace("é", "e");
            input = input.Replace("è", "e");
            input = input.Replace("ê", "e");
            input = input.Replace("ë", "e");
            
            input = input.Replace("â", "a");
            input = input.Replace("à", "a");
            input = input.Replace("å", "a");
            input = input.Replace("ã", "a");
           

            input = input.Replace("î", "i");
            input = input.Replace("ï", "i");
            input = input.Replace("í", "i");
            input = input.Replace("ì", "i");
           
            input = input.Replace("ô", "o");
            input = input.Replace("ò", "o");
            input = input.Replace("ó", "o");
            input = input.Replace("õ", "o");
           
            input = input.Replace("û", "u");
            input = input.Replace("ù", "u");
            input = input.Replace("ú", "u");
           
            return input;

        }



        /// <summary>
        /// Diese Methode erstellt custom MessageBox
        /// </summary>
        private void MessageBoxMethod(string caption, string message, string resourceKey)
        {
            CustomMessageBox customMessage = new CustomMessageBox();
            customMessage.caption = caption;
            customMessage.message = message;
            customMessage.icon = (ImageSource)customMessage.FindResource(resourceKey);

            var mainView = Application.Current.Windows.OfType<MainView>().FirstOrDefault();
            if (mainView != null)
            {
                customMessage.WindowStartupLocation = WindowStartupLocation.Manual;
                customMessage.Loaded += (s, e) =>
                {
                    customMessage.Left = mainView.Left + (mainView.ActualWidth - customMessage.ActualWidth) / 2;
                    customMessage.Top = mainView.Top + (mainView.ActualHeight - customMessage.ActualHeight) / 2;
                };
            }

            customMessage.ShowDialog();
            customMessage = null;
        }


        #endregion

        #region Commanden für Buttons und Triggers


        /// <summary>
        /// Dieses Command überpüft getipte Buttons.
        /// Wenns richtig ist werden die Buttons grün leuchten , wenn nicht dann rot
        /// </summary>
        public ICommand CheckAnswerCommand => new RelayCommand(async parameter =>
        {
            if (isButtonModeSelected)
            {
                ButtonModel button = (ButtonModel)parameter;

                if (button.Text.Equals(CorrectWord))
                {
                    await CorrectAnswerSequenceAsync(button);
                }
                else
                {
                    // Button rot leuchten lassen
                    button.BackgroundColor = Brushes.Red;
                }
            }
        });


        /// <summary>
        /// Dieses Command sorgt die Ändurungen auf Oberfläche
        /// für Geschwindigkeit und Tönhöhe warhzunehmen. 
        /// </summary>
        public ICommand GttsConfigCommand => new RelayCommand(parameter =>
        {
            isSpeakingRateTrigged = true;
            isPitchValueTrigged = true;
            isPracticeCalled = true;
        });


        /// <summary>
        /// Dieses Command erhählt current System Volume
        /// </summary>
        public ICommand VolumeCommand => new RelayCommand(parameter =>
        {

            audioPlayer.SetWindowsVolume(SystemVolume);

        });


        /// <summary>
        /// Dieses Command lässt neues Audio Content zu generieren
        /// Wenn beim Combobox eine Änderung ausgelöst wird
        /// </summary>
        public ICommand ComboboxCommand => new RelayCommand( async parameter =>
        {
            if (!isComboboxTrigged)
            {
                await generateSound();

            }

        });


        /// <summary>
        /// Dieses Command erlaubt uns Audio Content zu wiederholen
        /// Wenn bei den Sliders (Geschwindigkeit oder Tönhöhe) eine Änderung gemacht wird
        /// erlaubt wieder ein neues Audio Content zu genereieren. 
        /// </summary>
        public ICommand RepeatCommand => new RelayCommand( async parameter =>
            {


                if (isSpeakingRateTrigged || isPitchValueTrigged)
                {
                    

                    await generateSound();

                    isSpeakingRateTrigged = false;
                    isPitchValueTrigged = false;
                }
                else
                {
                    ProgresBarMaxValue = audioPlayer.GetDuration();
                    ProgressBarUpdater();
                    await audioPlayer.ReplayAudio();
                  
                }


            });



        /// <summary>
        /// Dieses Command lääst zum Hauptmenu zukehren
        /// </summary>
        public ICommand NavigateHome => new RelayCommand(parameter =>
        {
            isComboboxTrigged = true;
            gameOver = false;
            navigationService.NavigateTo(nameof(LanguageView));
            navigationService.NavigateTo(nameof(ZeroView));

        });



        /// <summary>
        /// Dieses Command gehört zum TextBox Mode
        /// und lässt Input zu überprüfen
        /// </summary>
        public ICommand TextBoxCheckCommand => new RelayCommand(async parameter =>
        {

            if (!string.IsNullOrEmpty(TextBoxText) && !TextBoxText.Equals(letzterText))
            {

                if (isTextboxSelected)
                {

                    this.letzterText = TextBoxText;

                    var kleinWord = TextSchoner(CorrectWord);

                    var kleinTextBoxText = TextSchoner(TextBoxText);

                    if (kleinTextBoxText.Equals(kleinWord))
                    {

                        KindName = "SmileRegular";
                        IconColor = "green";
                        CorrectWordTextBlockText = CorrectWord;
                        RichtigeTextBlockVisibility = Visibility.Visible;


                        for (int i = 0; i < 3; i++)
                        {
                            TextBoxBackground = Brushes.Transparent;
                            await Task.Delay(200);
                            TextBoxBackground = Brushes.Green;
                            await Task.Delay(200);
                        }


                        await CorrectAnswerSequenceAsync(null);


                    }
                    else
                    {

                        TextBoxBackground = Brushes.Red;
                        KindName = "TimesCircleRegular";
                        IconColor = "red";

                    }

                }
            }
        });


        /// <summary>
        /// Dieses Command öffnet ein neues Settings Fenster
        /// Erlaubt uns Einstellungen zu ändern und zu speichern.
        /// </summary>
        public ICommand SettingsCommand => new RelayCommand(async parameter =>
        {
            var settingViewModel = ViewModelLocator.Instance.SettingViewModel;
            settingViewModel.baseLanguage = this.baseLanguage;
            settingViewModel.InitalizeTitles();

            var settingsWindow = new SettingView
            {
                DataContext = settingViewModel
            };



            // MainView penceresini bulun ve SettingView penceresini ortalamak için pozisyonu hesaplayın
            var mainView = Application.Current.Windows.OfType<MainView>().FirstOrDefault();
            if (mainView != null)
            {
                settingsWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                settingsWindow.SizeChanged += (s, e) =>
                {
                    settingsWindow.Left = mainView.Left + (mainView.ActualWidth - settingsWindow.ActualWidth) / 2;
                    settingsWindow.Top = mainView.Top + (mainView.ActualHeight - settingsWindow.ActualHeight) / 2;
                };
            }

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            settingsWindow.Closed += (s, e) =>
            {
                tcs.SetResult(true);
                settingViewModel.SettingSaveCommand.Execute(null); // Pencere kapanmadan önce ayarları kaydedin
            };

            settingsWindow.ShowDialog();

            _ = await tcs.Task; // Pencere kapanana kadar bekleyin

            this.practiceModeSetting = Settings.Default.PracticeModeSetting;

            ModeInitalizer();


        });


        /// <summary>
        /// Dieses Command gehört zum TextBox Mode
        /// Es zeigt uns richtiges Wort
        /// </summary>
        public ICommand ShowAnswerCommand => new RelayCommand(parameter =>
        {

            RichtigeTextBlockVisibility = Visibility.Visible;
            CorrectWordTextBlockText = CorrectWord;

        });


        /// <summary>
        /// Dieses Command gehört zum TextBox Mode
        /// Es öffnet schrittweise Buchstaben
        /// </summary>
        public ICommand OpenLetterCommand => new RelayCommand(parameter =>
        {


            if (isTempTextLeer)
            {
                if (letterCounter < CorrectWord.Length)
                    temporerText = "";
            }
            

            if (letterCounter < CorrectWord.Length)
            {
                temporerText += CorrectWord[letterCounter];
                letterCounter++;
                isTempTextLeer = false;
            }
            else
            {
                isTempTextLeer = true;
            }
            
            TextBoxText = temporerText;
            

        });


        /// <summary>
        /// Dieses Command erlaubt uns zwischen TextBox Mode und Button Mode zu wechseln
        /// Speichert keine Einstellungen
        /// </summary>
        public ICommand SwapCommand => new RelayCommand(parameter =>
        {

            if (practiceModeSetting.Equals("ButtonMode"))
            {
                practiceModeSetting = "TextBoxMode";
                ModeInitalizer();

            }
            else
            {
                practiceModeSetting = "ButtonMode";
                ModeInitalizer();

            }


        });


        #endregion

    }

}


// Nichtbrauchbare Methoden und Commanden

#region Eine andere Command zur Überprüfung des Buttonsklick (correctWord). Dies wird  nicht benutzt

//public ICommand CheckAnswerCommand => new RelayCommand(async parameter =>
//{
//    ButtonModel button = (ButtonModel)parameter;

//    if (button.Text.Equals(CorrectWord))
//    {
//        IsButtonEnabled = false;
//        // Button grün leuchten lassen
//        button.BackgroundColor = Brushes.Green;
//        await Task.Delay(600);
//        button.BackgroundColor = Brushes.Transparent;
//        await Task.Delay(200);
//        button.BackgroundColor = Brushes.Green;
//        await Task.Delay(200);
//        button.BackgroundColor = Brushes.Transparent;
//        await Task.Delay(200);
//        button.BackgroundColor = Brushes.Green;
//        await Task.Delay(200);
//        button.BackgroundColor = Brushes.Transparent;
//        await Task.Delay(200);
//        button.BackgroundColor = Brushes.Green;



//        // Wenn richtig ist nächtes Wort warte auf 1 Sekunde

//        await Task.Delay(600);

//        await ContinueAsync();

//        //InitializeRound();

//        IsButtonEnabled = true;

//        if (!gameOver)
//        {
//           /*await*/ generateSound();

//            //navigationService.NavigateTo(nameof(PracticeView));

//        }
//        else
//        {
//            gameOver = false;
//            navigationService.NavigateTo(nameof(ModusView));
//            navigationService.NavigateTo(nameof(MenuEntryView));
//        }

//    }
//    else
//    {
//        // Wenn falsch ist, dann warte bis man richtig antippt.
//        // Button rot leuchten lassen
//        button.BackgroundColor = Brushes.Red;


//    }
//});


#endregion

#region Die Methode , die beim Audio Generate Verzögerung sorgt. Diese wird oben benutzt.

//public async Task generateSound()
//{
//    if (SelectedComboboxItem != null && CorrectWord != null && CorrectWord != string.Empty)
//    {
//        googleTextToSpeech.synthesisOptions(baseLanguage.googleTSSselectedLanguage, SelectedComboboxItem, SpeakingRate, PitchValue);
//        googleTextToSpeech.inputText(CorrectWord);

//        var content = googleTextToSpeech.getSoundContent();

//        if (content != null)
//        {
//            IsButtonEnabled = false;
//            await audioPlayer.PlayAudioAsync(content);
//            IsButtonEnabled = true;
//        }
//    }
//}




// Diese Methode ist ohne Verzögerung. Wird nicht benutzt

/// <summary>
/// Diese Methode ruft Google Text to Speech und gereniert Audio Content
/// </summary>
//public void generateSound()
//{

//    if (SelectedComboboxItem != null && CorrectWord != null && CorrectWord != string.Empty)
//    {
//        googleTextToSpeech.synthesisOptions(baseLanguage.googleTSSselectedLanguage, SelectedComboboxItem, SpeakingRate, PitchValue);
//        googleTextToSpeech.inputText(CorrectWord);

//        var content = googleTextToSpeech.getSoundContent();

//        if (content != null)
//        {
//            audioPlayer.PlayAudio(content);
//            ProgresBarMaxValue = audioPlayer.getDuration;
//            ProgressBarUpdater();

//        }
//    }
//}



#endregion

#region Diese Command ist SettingsCommand ohne Positionberechnung. Es wird nicht benutzt

//public ICommand SettingsCommand => new RelayCommand(async parameter =>
//{
//    var settingViewModel = ViewModelLocator.Instance.SettingViewModel;
//    var settingsWindow = new SettingView
//    {
//        DataContext = settingViewModel
//    };

//    TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
//    settingsWindow.Closed += (s, e) =>
//    {
//        tcs.SetResult(true);
//        settingViewModel.SettingSaveCommand.Execute(null); // Pencere kapanmadan önce ayarları kaydedin
//    };

//    settingsWindow.ShowDialog();

//    _ = await tcs.Task; // Pencere kapanana kadar bekleyin

//    this.practiceModeSetting = settingViewModel.isChecked; // Değişiklikleri uygulayın
//    ModeInitalizer();
//});


#endregion




