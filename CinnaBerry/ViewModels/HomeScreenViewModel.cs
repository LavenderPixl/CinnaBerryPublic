using CinnaBerry.Data.API;
using CinnaBerry.Misc;
using CinnaBerry.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace CinnaBerry.ViewModels
{
    public class HomeScreenViewModel : ViewModelBase
    {
        //Which page we're on.
        int pageNumber;
        public int PageNumber
        {
            get => pageNumber;
            set => this.RaiseAndSetIfChanged(ref pageNumber, value);
        }
        int startIndex = 1;
        int endIndex = 8;

        private ObservableCollection<Movie> _movieList = new();
        public ObservableCollection<Movie> MovieList { get => _movieList; set => this.RaiseAndSetIfChanged(ref _movieList, value); }

        private ObservableCollection<Series> _seriesList = new();
        public ObservableCollection<Series> SeriesList 
        { 
            get => _seriesList; 
            set => this.RaiseAndSetIfChanged(ref _seriesList, value); 
        }

        public ObservableCollection<Genre> GenreList { get; set; } = new ObservableCollection<Genre>();

        string selectedGenre;
        public string SelectedGenre { get => selectedGenre; set => this.RaiseAndSetIfChanged(ref selectedGenre, value); }
        private string genre = string.Empty;
        
        private bool _isMovie = true;

        public bool IsMovie
        {
            get => _isMovie;
            set => this.RaiseAndSetIfChanged(ref _isMovie, value);
        }
        private string _swapMediaStr = "View Series";

        public string SwapMediaStr
        {
            get => _swapMediaStr;
            set => this.RaiseAndSetIfChanged(ref _swapMediaStr, value);
        }



        public HomeScreenViewModel()
        {
            genre = string.Empty;
            PageNumber = 1;
            MovieList = API.GetMovies(startIndex, endIndex);
            SeriesList = API.GetSeries(startIndex, endIndex);

            #region GenreList Adds

            GenreList.Add(new Genre("Horror"));
            GenreList.Add(new Genre("Romance"));
            GenreList.Add(new Genre("Action"));
            GenreList.Add(new Genre("Comedy"));
            GenreList.Add(new Genre("Thriller"));
            GenreList.Add(new Genre("Science Fiction"));
            GenreList.Add(new Genre("Documentary"));
            GenreList.Add(new Genre("Drama"));

            //Subscribes to ChangeGenreEventArgs, with each Genre object.
            foreach (var item in GenreList)
            {
                item.GenreChanged += OnChangedGenre;
            }
            #endregion
        }


        /// <summary>
        /// Subscribed to OnChangedGenreEventArgs
        /// Genre = Genre obj's name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChangedGenre(object sender, ChangeGenreEventArgs e)
        {
            genre = e.SelectedGenre;
            ChangeGenreList(genre);
        }

        public void ChangeGenreList(string genre)
        {
            MovieList.Clear();
            PageNumber = 1;
            startIndex = 1;
            endIndex = 8;
            if (IsMovie) MovieList = API.GetMovies(startIndex, endIndex, genre);
            else SeriesList = API.GetSeries(startIndex, endIndex, genre);
        }

        /// <summary>
        /// Goes one Page Up.
        /// Is bound in the View.
        /// </summary>
        public void PageUp()
        {
            PageNumber++;
            startIndex += 8;
            endIndex += 8;

            getMedia();
        }
        /// <summary>
        /// Goes one Page Down.
        /// Is bound in the View.
        /// </summary>
        public void PageDown()
        {
            if (PageNumber > 1)
            {
                PageNumber--;
                startIndex -= 8;
                endIndex -= 8;

                getMedia();
            }
        }

        public void SwapMedia()
        {
            IsMovie = !IsMovie;
            getMedia();
            if (IsMovie) SwapMediaStr = "View Series";
            else SwapMediaStr = "View Movies";
        }

        private void getMedia()
        {
            if (string.IsNullOrEmpty(genre))
            {
                if (IsMovie) MovieList = API.GetMovies(startIndex, endIndex);
                else SeriesList = API.GetSeries(startIndex, endIndex);
            }
            else
            {
                if (IsMovie) MovieList = API.GetMovies(startIndex, endIndex, genre);
                else SeriesList = API.GetSeries(startIndex, endIndex, genre);
            }
            if (IsMovie) SeriesList.Clear();
            else MovieList.Clear();
        }
    }
}
