using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Styling;
using CinnaBerry;
using CinnaBerry.Misc;
using CinnaBerry.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;

namespace CinnaBerry.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        Movie selectedMovie = null;
        #region Properties
        private bool mainWindowEnabled = true;
        public bool MainWindowEnabled { get => mainWindowEnabled; set => this.RaiseAndSetIfChanged(ref mainWindowEnabled, value); }

        bool loggedIn;
        public bool LoggedIn { get => loggedIn; set => this.RaiseAndSetIfChanged(ref loggedIn, value); }
        private WindowState winState;
        public WindowState WinState { get => winState; set => this.RaiseAndSetIfChanged(ref winState, value); }
        string search;
        public string Search { get => search; set => this.RaiseAndSetIfChanged(ref search, value); }
        #endregion
        public MainWindowViewModel()
        {
            LoggedIn = true;
            User.SetUser(new User(12, "ooga", "email@ooga.dk", "123"));
            //SetContentArea.Navigate(new MovieInfoViewModel());
            SetContentArea.Navigate(new LoginScreenViewModel());
            ScreenSelector.Instance.SelectionChanged += Instance_SelectionChanged;
        }

        private void Instance_SelectionChanged(object sender, SelectorEventArgs e)
        {
            selectedMovie = e.SelectedMovie;
            SwapScreen(e.Selection);
        }

        public void ExitProgram()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifeTime) { lifeTime.Shutdown(); }
        }
        public void MinimizeWindow()
        {
            if (WinState == WindowState.Minimized)
            {
                WinState = WindowState.Normal;
                WinState = WindowState.Minimized;
            }
            else { WinState = WindowState.Minimized; }
        }

        public void SwapScreen(string screen)
        {
            switch (screen)
            {
                case "Login":
                    SetContentArea.Navigate(new LoginScreenViewModel());
                    break;
                case "CreateUser":
                    SetContentArea.Navigate(new CreateUserViewModel());
                    break;
                case "Home":
                    LoggedIn = true;
                    SetContentArea.Navigate(new HomeScreenViewModel());
                    break;
                case "PrivateProfile":
                    SetContentArea.Navigate(new PrivateProfileViewModel());
                    break;
                case "PublicProfile":
                    SetContentArea.Navigate(new PublicProfileViewModel());
                    break;
                case "PlanToWatch":
                    SetContentArea.Navigate(new PlanToWatchViewModel());
                    break;
                case "Movies":
                    SetContentArea.Navigate(new MoviesViewModel());
                    break;
                case "Series":
                    SetContentArea.Navigate(new SeriesViewModel());
                    break;
                case "MovieInfo":
                    SetContentArea.Navigate(new MovieInfoViewModel(selectedMovie));
                    break;
            }
        }
        public void CinnaBerryButton()
        {
            if (LoggedIn == true)
            {
                SetContentArea.Navigate(new HomeScreenViewModel());
            }
        }
    }
}