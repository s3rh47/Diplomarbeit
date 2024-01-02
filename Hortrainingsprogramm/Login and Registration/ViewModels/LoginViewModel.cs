using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Custom_MessageBox;
using Hortrainingsprogramm.Login_and_Registration.Models;
using Hortrainingsprogramm.Main_Window.Views;
using Hortrainingsprogramm.Services;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Hortrainingsprogramm.Login_and_Registration.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        
        private readonly INavigationService navigationService;
        private bool textBoxClickChecker = true;

        //Kontrolliert wird mit Fody. 
        public bool isChecked { get; set; } = Settings.Default.checkBoxCurrentStatus;
        public string userNameTextBoxProperty { get; set; } = Settings.Default.username;

        // Erstelle Database mit dem Namen "Logindatabase" und Datentyp "db"
        private readonly SQLiteLoginDatabase databaseObject = new SQLiteLoginDatabase();


        public LoginViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.navigationService.NavigationRequested += OnNavigationRequested;

        }


        private void OnNavigationRequested(object sender, NavigationEventArgs eventArgs)
        {

            if (eventArgs.Data is RegisterViewModel Model)
            userNameTextBoxProperty = Model.registerUserNameTextBoxProperty;

        }

   
        //Command für die Schließung des Programms
        public ICommand CloseApplicationCommand => new RelayCommand(async parameter =>
        {

            var fadeObject = new Effects.FadeInOut(Application.Current.MainWindow, 1, 0, 0.5);
            fadeObject.Run();
            await Task.Delay(360);
            Application.Current.Shutdown();

        });



        //Command für die Minimizierung des Programms
        public ICommand MinimizeApplicationCommand => new RelayCommand(parameter =>
        {
            var minimizeEffect = new Effects.WindowMinimizeEffect();
            minimizeEffect.minimizeEffect(Application.Current.Windows[0]);

        });

        //Command für Textbox, der nur einmal beim Clicken löscht. Bei einem zweiten Click wird nicht mehr funktinoeren.
        public ICommand DeleteTextBoxCommand => new RelayCommand(parameter =>
        {
            if (textBoxClickChecker)
            {
                userNameTextBoxProperty = "";
                textBoxClickChecker = false;
            }

        });


        


        //Command für die Registrierung des Users
        public ICommand openMenuCommand => new RelayCommand(async parameter =>
        {

            //Kontrolliere, ob Name Textbox leer ist
            if (string.IsNullOrEmpty(userNameTextBoxProperty))
            {
                var warning = "Bitte geben Sie den Benutzernamen ein!!!";
                MessageBoxMethod("Error", warning, "warningImg");
                return;

            }


            // Es wird kontrolliert, ob der User sich schon registriert hat"
            if (databaseObject.selectNameFromUserTabelle(userNameTextBoxProperty) != null)
            {
                // Speichere in die Setting Datei, momentane Checboxstatus.
                // Das speichert, indem der User Chechbox ändert. 

                if (Settings.Default.checkBoxCurrentStatus = isChecked)
                {
                    //Wenn Checkbox Checked ist, wird der geschriebene Name in Settings Dateis gepeichert.
                    Settings.Default.username = userNameTextBoxProperty;
                }
                else
                {
                    //Wenn Checkbox unchecked ist, dann muss der Name leer sein.
                    Settings.Default.username = "";
                }

                // Speichere angemeldete User in die Settings Datei.
                Settings.Default.loggedUser = userNameTextBoxProperty;


                // Speichere Setting Datei nach der neuen Änderungen.
                Settings.Default.Save();



                var mainWindow = Application.Current.MainWindow;
                Application.Current.MainWindow = new MainView();

                var fadeObject = new Effects.FadeInOut(mainWindow, 1, 0, 0.5);

                fadeObject.Run();
               
                // Wait for the animation to finish before closing the window
                await Task.Delay(360);

                textBoxClickChecker = true; // Wenn Login Succes ist, und man dann wieder logout macht , dann wieder Click beim textbox erlauben , um text zu löschen

                mainWindow.Close(); // Schieließe  das Loginfenster

                fadeObject = new Effects.FadeInOut(Application.Current.MainWindow, 0, 1, 0.7);
                fadeObject.Run();
                Application.Current.MainWindow.Show(); // Öffne das Hauptfenster

            }
            else
            {
                // Gib Fehlermeldung, wenn der Benutzername nicht registriert ist. 

                var warning = "Dieser Benützername ist nicht vorhanden. \nBitte registierieren Sie sich!";
                MessageBoxMethod("Error", warning, "warningImg");
               
            }

        });

        private void MessageBoxMethod(string caption, string message, string resourceKey)
        {

            CustomMessageBox customMessage = new CustomMessageBox();
            customMessage.caption = caption;
            customMessage.message = message;
            customMessage.icon = (ImageSource)customMessage.FindResource(resourceKey);
            customMessage.ShowDialog();
            customMessage = null;

        }







    }
}
