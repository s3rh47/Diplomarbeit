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
using System.Windows;
using Hortrainingsprogramm.Main_Window.Views;
using System.Windows.Threading;
using Hortrainingsprogramm.Main_Window.ViewModels;
using Hortrainingsprogramm.Main_Window.ViewModels.RightMenus;


namespace Hortrainingsprogramm.Practice_and_Quiz_Menu.ViewModels
{

    public class QuizViewModel : BaseViewModel
    {

        #region Variablem, Getter, Setter

        /// <summary>
        /// Variablen im xaml zum Binden 
        /// Müssen public sein
        /// Außer override Variablen müssen Groß geschrieben werden
        /// </summary>
        public override BaseLanguage baseLanguage { get; set; }
        public ObservableCollection<ButtonModel> Buttons { get; set; } = new();
        public override bool isPracticeCalled { get; set; } = true;

        public string CorrectWord { get; set; }
        public bool IsButtonEnabled { get; set; } = true;
        public string ReplayCountToBinding { get; set; }
        public string RoundCounterToBinding { get; set; }
        public bool IsRepeatButtonEnabled { get; set; }
        public bool IsSkipButtonEnabled { get; set; } = true;


        // Für Titles die Sprache ändert
        public string GameOverText { get; set; }
        public string RoundCounterTitle { get; set; }
        public string MessageBoxCaption { get; set; }
        public string CheckAnswerButtonText { get; set; }
        public string TextBoxWaterMarkText { get; set; }
        public string CorrectAnswerTitle { get; set; }
        public string RemainingTitel { get; set; }
        public string SkipButtonText { get; set; }


        // Für Visibilities 
        public Visibility ButtonModeVisibility { get; set; }
        public Visibility TextBoxModeVisibility { get; set; }
        public Visibility RichtigeTextBlockVisibility { get; set; } = Visibility.Collapsed;



        //Für Progressbar
        public double ProgresBarMaxValue { get; set; }
        public double ProgresBarCurrentValue { get; set; }


        //TextBox Bereich 
        public Brush TextBoxBackground { get; set; } = Brushes.Transparent;
        public string IconColor { get; set; } = "white";
        public string IconKindName { get; set; } = "VolumeUpSolid";
        public string CorrectWordTextBlockText { get; set; }
        public string TextBoxText { get; set; }


        /// <summary>
        /// Public Variablen (nicht zum Binden)
        /// </summary>
        /// 
        public override bool isQuizCalled { get; set; }


        /// <summary>
        /// Private Variablen
        /// </summary>
        private readonly INavigationService navigationService;
        private GoogleTextToSpeech googleTextToSpeech = new();
        private List<string> words = new();
        private AudioPlayer audioPlayer = new();
        private Random random = new();
        private CancellationTokenSource cancellationTokenSource;
        private ButtonModel correctButton;
        private bool isButtonModeSelected { get; set; } = false;
        private bool isTextboxSelected { get; set; } = true;


        //Variblen für Runde und Einstellungen
        private int spielCount;
        private bool gameOver = false;
        private string letzterText;
        private int[] spielCounts = { 10,20};
        private int replayCount { get; set; } = 3;
        private double[] speakingRates = { 0.75, 1, 1.25 };
        private int tippRemain; 


        #endregion



        #region Konstruktur und Methoden

        /// <summary>
        /// Konstruktur.
        /// </summary>
        public QuizViewModel(INavigationService navigationService)
        {

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
        /// Wird vom Contruktur gerufen
        /// Diese Methode braucht man, um Property Chance machen zu können
        /// </summary>
        private async void OnNavigationRequested(object sender, NavigationEventArgs eventArgs)
        {

            if (eventArgs.Data is ModusViewModel Model)
            {
                if (Model.isQuizCalled)
                {
                    cancellationTokenSource = new CancellationTokenSource();

                    ModeInitalizer();

                    this.isQuizCalled = Model.isQuizCalled;

                    words.Clear();

                    this.baseLanguage = Model.baseLanguage;


                    foreach (var word in baseLanguage.databaseList)
                    {
                        words.Add(word);
                    }

                    this.MessageBoxCaption = baseLanguage.MessageBoxCaption;
                    this.GameOverText = baseLanguage.GameOverText;
                    this.RoundCounterTitle = baseLanguage.RoundCounterTitle;
                    this.CheckAnswerButtonText = baseLanguage.CheckAnswerButtonText;
                    this.TextBoxWaterMarkText = baseLanguage.TextBoxWaterMarkText;
                    this.CorrectAnswerTitle = baseLanguage.CorrectAnswerTitle;
                    this.spielCount = spielCounts[random.Next(spielCounts.Length)];
                    this.SkipButtonText = baseLanguage.SkipButtonText;
                    this.RemainingTitel = baseLanguage.RemainingTitel;


                    gameOver = false;

                    InitializeRound();

                    if (!gameOver) await generateSound();

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

            for (int i = 0; i < spielCount; i++)
            {

                foreach (var button in Buttons)
                {

                    int index = random.Next(this.words.Count);
                    button.Text = this.words[index];
                    button.ForegroundColor = Brushes.White;
                    button.BackgroundColor = Brushes.Transparent;
                    this.words.RemoveAt(index);

                }


                CorrectWord = Buttons.ElementAt(random.Next(4)).Text;




                foreach (var button in Buttons)
                {

                    if (button.Text.Equals(CorrectWord))
                    {
                        correctButton = button;
                    }

                }


                RoundCounterToBinding = (i + 1 + "  |  " + spielCount);


                await WaitForContinueButtonAsync(cancellationTokenSource.Token);

            }


            isQuizCalled = false;
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

            tippRemain = 3;

            TextBoxBackground = Brushes.Transparent;
            TextBoxText = "";
            

            IconColor = "white" ;
            IconKindName = "VolumeUpSolid";

            RichtigeTextBlockVisibility = Visibility.Collapsed;


            replayCount = 3;
            ReplayCountToBinding = replayCount.ToString();


           
            // Wenn die Zahlen kleiner als 50 ist, dann Button 
            // Wenn nicht dann TextModus
            if (random.Next(100) < 50 )
            {
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





        public async Task generateSound()
        {

            var speakingRate = speakingRates[random.Next(speakingRates.Length)];

            var voiceItem = GetRandomVoice(baseLanguage.googleTTSvoiceList);


            if (CorrectWord != null && CorrectWord != string.Empty)
            {
                googleTextToSpeech.synthesisOptions(baseLanguage.googleTSSselectedLanguage, voiceItem, speakingRate, 0);
                googleTextToSpeech.inputText(CorrectWord);

                var content = googleTextToSpeech.getSoundContent();

                if (content != null)
                {


                    IsButtonEnabled = false;
                    IsSkipButtonEnabled = false;
                    IsRepeatButtonEnabled = false;


                    audioPlayer.SetContent(content);
                    ProgresBarMaxValue = audioPlayer.GetDuration();
                    ProgressBarUpdater();
                    await audioPlayer.PlayAudioAsync();

                    IsButtonEnabled = true;
                    IsSkipButtonEnabled = true;
                    IsRepeatButtonEnabled = true;


                }
            }
        }



        /// <summary>
        /// Diese Methode wird gerufen, wenn man auf richtigen Button clickt
        /// </summary>
        private async Task CorrectAnswerSequenceAsync(ButtonModel button)
        {
       
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


            await AudioReplayMethode(2);

        }



        /// <summary>
        /// Erlaubt times mal Audio wiederzugeben
        /// </summary>
        /// <returns></returns>
        private async Task AudioReplayMethode(int times)
        {

            for (int i = 0; i < times; i++)
            {
                ProgresBarMaxValue = audioPlayer.GetDuration();
                ProgressBarUpdater();
                await audioPlayer.ReplayAudio();
                await Task.Delay(500);
            }

        }



        /// <summary>
        /// Diese Methode bearbeitet input Text
        /// Um Speziale Karaktere im Text mit anderen karakteren zu ersetzen
        /// </summary>
        public string TextSchoner(string input)
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
        /// Diese Methode nimmt zufällig Stimme aus Langauge-Klassen
        /// </summary>
        public string GetRandomVoice(Dictionary<string, string> dictionary)
        {
            var keys = new List<string>(dictionary.Keys);
            int randomIndex = random.Next(keys.Count);
            return keys[randomIndex];
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

            // Start the timer
            timer.Start();

            // Stop the timer when the audio playback has stopped
            audioPlayer.PlaybackStopped += async (sender, e) =>
            {

                timer.Stop();
                await Task.Delay(100);
                ProgresBarCurrentValue = 0;

            };



        }


        /// <summary>
        /// Diese Methode sorgt Forschleife weiterzumachen.  
        /// </summary>
        /// <returns></returns>
        private async Task ContinueAsync()
        {
            cancellationTokenSource.Cancel();
            await Task.Delay(100); // Debouncing için küçük bir bekleme süresi


            if (!gameOver)
            {

                ModeInitalizer();
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
        /// Diese Methode erstellt custom MessageBox
        /// </summary>
        private void MessageBoxMethod(string caption, string message, string resourceKey)
        {
            CustomMessageBox customMessage = new CustomMessageBox();
            customMessage.caption = caption;
            customMessage.message = message;
            customMessage.icon = (ImageSource)customMessage.FindResource(resourceKey);

            // MainView penceresini bulun ve CustomMessageBox penceresini ortalamak için pozisyonu hesaplayın
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
        /// Dieses Command überpüft getippte Buttons.
        /// Wenns richtig ist werden die Buttons grün leuchten , wenn nicht dann rot
        /// </summary>
        public ICommand CheckAnswerCommand => new RelayCommand(async parameter =>
        {
            if (isButtonModeSelected)
            {
                ButtonModel button = (ButtonModel)parameter;

                if (button.Text.Equals(CorrectWord))
                {
                    IsButtonEnabled = false;
                    IsRepeatButtonEnabled = false;
                    await CorrectAnswerSequenceAsync(button);
                }
                else
                {


                    // Button rot leuchten lassen
                    IsButtonEnabled = false;
                    IsRepeatButtonEnabled = false;

                    button.BackgroundColor = Brushes.Red;
                    correctButton.BackgroundColor = Brushes.Green;


                    await AudioReplayMethode(3);


                    button.BackgroundColor = Brushes.Transparent;
                    correctButton.BackgroundColor = Brushes.Transparent;

                }
            }

            await ContinueAsync();

      
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
                    
                    IsSkipButtonEnabled = false;
                    IsRepeatButtonEnabled = false;

                    letzterText = TextBoxText;

                    var kleinWord = TextSchoner(CorrectWord);

                    var kleinTextBoxText = TextSchoner(TextBoxText);

                    if (kleinTextBoxText.Equals(kleinWord))
                    {
                        tippRemain = 0;
                        IconKindName = "SmileRegular";
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

                        tippRemain--;

                        IsSkipButtonEnabled = true;
                        IsRepeatButtonEnabled = true;

                        TextBoxBackground = Brushes.Red;
                        IconKindName = "TimesCircleRegular";
                        IconColor = "red";

                        RichtigeTextBlockVisibility = Visibility.Visible;
                        CorrectAnswerTitle = baseLanguage.RemainingTitel;
                        CorrectWordTextBlockText = tippRemain.ToString();


                        if(tippRemain == 0)
                        {
                            IsSkipButtonEnabled = false;
                            IsRepeatButtonEnabled = false;
                            CorrectAnswerTitle = baseLanguage.CorrectAnswerTitle;
                            CorrectWordTextBlockText = CorrectWord;
                            await AudioReplayMethode(3);
           
                        }
                        


                    }

                    if(tippRemain == 0) await ContinueAsync();


                }
            }

        });


        /// <summary>
        /// Dieses Command erlaubt uns Audio Content zu wiederholen
        /// Man darf nur 3 Mal rufen. Nach 3 Anufruf wird Button disabled
        /// </summary>
        public ICommand RepeatCommand => new RelayCommand(async parameter =>
        {

            IsRepeatButtonEnabled = false;

            if ((replayCount--) > 0)
            {
                
                ReplayCountToBinding = replayCount.ToString();
                //ProgresBarMaxValue = audioPlayer.GetDuration();
                //ProgressBarUpdater();
                //await audioPlayer.ReplayAudio();
                await AudioReplayMethode(1);
                IsRepeatButtonEnabled = true;

            }
          

        });



        /// <summary>
        /// Dieses Command lääst zum Hauptmenu zukehren
        /// </summary>
        public ICommand NavigateHome => new RelayCommand(parameter =>
        {
            this.isQuizCalled = false;
            gameOver = false;
            navigationService.NavigateTo(nameof(LanguageView));

        });

     

        /// <summary>
        /// Dieses Command gehört zum TextBox Modus und erlaubt uns Round zu passen
        /// </summary>
        public ICommand PassCommand => new RelayCommand( async parameter =>
        {

            CorrectWordTextBlockText = CorrectWord;
            
            RichtigeTextBlockVisibility = Visibility.Visible;

            IconKindName = "TimesCircleRegular";
           
            IconColor = "red";

            IsSkipButtonEnabled = false;

            IsRepeatButtonEnabled = false;

            await AudioReplayMethode(3);

            await ContinueAsync();

        });

        #endregion



    }
}
