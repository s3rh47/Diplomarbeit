using Hortrainingsprogramm.Components;
using Hortrainingsprogramm.Custom_MessageBox;
using Hortrainingsprogramm.Login_and_Registration.Models;
using Hortrainingsprogramm.Login_and_Registration.Views;
using Hortrainingsprogramm.Services;
using System.Windows.Input;
using System.Windows.Media;


namespace Hortrainingsprogramm.Login_and_Registration.ViewModels
{
    public class RegisterViewModel : ObservableObject
    {

        private readonly INavigationService navigationService;
        


        private readonly SQLiteLoginDatabase databaseObject = new SQLiteLoginDatabase();


        public string registerUserNameTextBoxProperty { get; set; }

        public RegisterViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

        }


    public ICommand CloseRegistrationMenuCommand => new RelayCommand(parameter => {

            this.navigationService.NavigateTo(nameof(LeftView));
            this.navigationService.NavigateTo(nameof(LoginView),this);

    });


    public ICommand registerUserCommand => new RelayCommand(parameter =>
        {


            //Wenn es keinen Namen gegeben wurde, warne und verlasse die Methode.
            if (string.IsNullOrEmpty(registerUserNameTextBoxProperty))
            {
                var warning = "Bitte geben einen beliebigen Namen ein!";
                MessageBoxMethod("Error", warning, "warningImg");
                return;
            }

            // Wenn User den Namen gegeben hat, dann hier weiter.

            // Überprüfen, ob die gegebene Username in Datenbank schon vorhanden ist. 
            if (databaseObject.selectNameFromUserTabelle(registerUserNameTextBoxProperty) != null)
            {
                var warning = "Dieser Benutzername ist schon vorhanden!";
                MessageBoxMethod("Error", warning, "warningImg");
                return;
            }

            // Wenn alles passt, dann speichere den Usernamen in Datenbank. 

            var erfolgText = "Registration is erfolgreich!";
            MessageBoxMethod("Erfolgreich", erfolgText, "okImg");

            // Danach erlaube Username in Datenbank zu speichern.. 
            databaseObject.insertInToForUserTabelle(registerUserNameTextBoxProperty);

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
