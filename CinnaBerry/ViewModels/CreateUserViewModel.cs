using Avalonia.Styling;
using CinnaBerry.Data.Database;
using CinnaBerry.Misc;
using CinnaBerry.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CinnaBerry.ViewModels
{
    public class CreateUserViewModel : ViewModelBase
    {
        #region Login properties
        string username = "";
        public string Username { get => username; set => this.RaiseAndSetIfChanged(ref username, value); }

        string email = "";
        public string Email { get => email; set => this.RaiseAndSetIfChanged(ref email, value); }

        string password = "";
        public string Password { get => password; set => this.RaiseAndSetIfChanged(ref password, value); }

        string passwordAgain = "";
        public string PasswordAgain { get => passwordAgain; set => this.RaiseAndSetIfChanged(ref passwordAgain, value); }
        #endregion

        #region Error messages
        bool usernameError;
        public bool UsernameError { get => usernameError; set => this.RaiseAndSetIfChanged(ref usernameError, value); }

        //bool usernameTaken;
        //public bool UsernameTaken { get => usernameTaken; set => this.RaiseAndSetIfChanged(ref usernameTaken, value); }

        bool emailError;
        public bool EmailError { get => emailError; set => this.RaiseAndSetIfChanged(ref emailError, value); }

        bool emailTaken;
        public bool EmailTaken { get => emailTaken; set => this.RaiseAndSetIfChanged(ref emailTaken, value); }

        bool passwordError;
        public bool PasswordError { get => passwordError; set => this.RaiseAndSetIfChanged(ref passwordError, value); }

        bool passwordAgainError;
        public bool PasswordAgainError { get => passwordAgainError; set => this.RaiseAndSetIfChanged(ref passwordAgainError, value); }

        string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set => this.RaiseAndSetIfChanged(ref errorMessage, value); }

        public string? NewUserId;
        #endregion

        public void BackBtn()
        {
            ScreenSelector.Instance.SelectNewScreen("Login", null);
        }
        public void CreateNewUser()
        {
            //Username must be between 1 and 30 characters.
            if (Username.Length >= 31 || Username.Length < 1)
            {   
                UsernameError = true;
                ErrorMessage = "The username must be between 1 and 31 characters.";
            }
            else UsernameError = false;

            //Email must be between 1 and 320 characters.
            if (Email.Length >= 321 || Email.Length < 1)
            { EmailError = true; }
            else EmailError = false;


            //Passwords must match.
            if (Password != PasswordAgain)
            { PasswordAgainError = true; }
            else PasswordAgainError = false;

            //Password must be between 1 and 500 Characters.
            if (Password.Length < 1 || Password.Length > 501)
            { PasswordError = true; }
            else PasswordError = false;
            
            //If above properties are false, try to create an account, using a stored procedure.  
            if (UsernameError == false && EmailError == false && PasswordAgainError == false && PasswordError == false)
            {
                try
                {
                    NewUserId = Database.Instance.ExecScalar($"EXEC CreateUser '{Username}', '{Password}', '{Email}'");
                    User user = new User(Convert.ToInt32(NewUserId), Username, Email, Password);
                    ScreenSelector.Instance.SelectNewScreen("Home", null);
                }
                catch (SqlException errorMessage)
                {
                    ErrorMessage = errorMessage.Message;

                    if (errorMessage.Number == 99001)
                    { UsernameError = true; }
                    else
                    { UsernameError = false; }

                    if (errorMessage.Number == 99002)
                    { EmailTaken = true; }
                    else
                    { EmailTaken = false; }
                }
            }
        }
    }
}
