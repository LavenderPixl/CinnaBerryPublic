using CinnaBerry.Data.Database;
using CinnaBerry.Misc;
using CinnaBerry.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinnaBerry.ViewModels
{
    public class LoginScreenViewModel : ViewModelBase
    {
        string? username;
        public string Username { get => username; set => this.RaiseAndSetIfChanged(ref username, value); }

        string? password;
        public string Password { get => password; set => this.RaiseAndSetIfChanged(ref password, value); }
        
        bool displayError;
        public bool DisplayError { get => displayError; set => this.RaiseAndSetIfChanged(ref displayError, value); }
        string? loginError;
        public string LoginError { get => loginError; set => this.RaiseAndSetIfChanged(ref loginError, value); }

        public void CreateUser()
        {
            ScreenSelector.Instance.SelectNewScreen("CreateUser", null);
        }
        public void Login()
        {
            try
            {
                string UserId = Database.Instance.ExecScalar($"EXEC Login '{Username}', '{Password}'");
                Database.GetUser(Convert.ToInt32(UserId));
                ScreenSelector.Instance.SelectNewScreen("Home", null);
                DisplayError = false;
            }
            catch (SqlException Exc)
            {
                DisplayError = true;
                LoginError = Exc.Message;
            }
        }
    }
}
