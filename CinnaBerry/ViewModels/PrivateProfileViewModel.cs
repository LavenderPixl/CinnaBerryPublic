using CinnaBerry.Data.API;
using CinnaBerry.Data.Database;
using CinnaBerry.Misc;
using CinnaBerry.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinnaBerry.ViewModels
{
    public class PrivateProfileViewModel : ViewModelBase
    {
        private string _username;
        public string Username
        {
            get => "Username: " + User.Instance.Username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        private string _email;
        public string Email
        {
            get => "Email: " + User.Instance.Email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        private bool _changePassword = false;
        public bool ChangePassword
        {
            get => _changePassword;
            set => this.RaiseAndSetIfChanged(ref _changePassword, value);
        }

        private ObservableCollection<Review> _reviews = new();
        public ObservableCollection<Review> Reviews
        {
            get => _reviews;
            set => this.RaiseAndSetIfChanged(ref _reviews, value);
        }

        public PrivateProfileViewModel()
        {
            Reviews = Database.GetReviews(User.Instance.Id);
        }

        public void ToggleChangePassword()
        {
            ChangePassword = !ChangePassword;
        }

        public void ViewPublicProfile()
        {
            ScreenSelector.Instance.SelectNewScreen("PublicProfile", null);
        }
    }
}
