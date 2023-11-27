using CinnaBerry.Data.Database;
using CinnaBerry.Misc;
using CinnaBerry.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinnaBerry.ViewModels
{
    public class MovieInfoViewModel : ViewModelBase
    {

        int rating = 0;
        public int Rating { get => rating; set => this.RaiseAndSetIfChanged(ref rating, value); }
        Movie selectedMovie;
        public Movie SelectedMovie { get => selectedMovie; set => this.RaiseAndSetIfChanged(ref selectedMovie, value); }

        string writingReviewText;
        public string WritingReviewText { get => writingReviewText; set => this.RaiseAndSetIfChanged(ref writingReviewText, value); }

        bool visWriteReview = false;
        public bool VisWriteReview { get => visWriteReview; set => this.RaiseAndSetIfChanged(ref visWriteReview, value); }

        bool _checkIfUpdated = false;
        public bool CheckIfUpdated { get => _checkIfUpdated; set => this.RaiseAndSetIfChanged(ref _checkIfUpdated, value); }

        string _publishReviewText;
        public string PublishReviewText { get => _publishReviewText; set => this.RaiseAndSetIfChanged(ref _publishReviewText, value); }

        bool _publishReviewVis = false;
        public bool PublishReviewVis { get => _publishReviewVis; set => this.RaiseAndSetIfChanged(ref _publishReviewVis, value); }

        string btnContent = "Write a Review";
        public string BtnContent { get => btnContent; set => this.RaiseAndSetIfChanged(ref btnContent, value); }

        Review _userReview;
        public Review UserReview { get => _userReview; set => this.RaiseAndSetIfChanged(ref _userReview, value); }
        bool _visYourReview = false;
        public bool VisYourReview { get => _visYourReview; set => this.RaiseAndSetIfChanged(ref _visYourReview, value); }

        ObservableCollection<Review> reviews = new ObservableCollection<Review>();
        public ObservableCollection<Review> Reviews { get => reviews; set => this.RaiseAndSetIfChanged(ref reviews, value); }

        ObservableCollection<int> _ratingList = new ObservableCollection<int>();
        public ObservableCollection<int> RatingList { get => _ratingList; set => this.RaiseAndSetIfChanged(ref _ratingList, value); }
        public string GenreList { get; set; } = "";
        public MovieInfoViewModel(Movie movie)
        {

            for (int i = 0; i > 9; i++)
            {
                RatingList.Add(i);
            }
            SelectedMovie = movie;
            Reviews = Database.GetReviewsForMovie(movie.GUId, User.Instance.Id);
            UserReview = Database.GetUserReviewForMovie(movie.GUId, User.Instance.Id);
            if (UserReview != null)
            { VisYourReview = true; }
            else
            { VisYourReview = false; }


            for (int i = 0; i < SelectedMovie.Genre.Count; i++)
            {
                GenreList += SelectedMovie.Genre[i];
                if (i < SelectedMovie.Genre.Count - 1)
                {
                    GenreList += ", ";
                }
            }
        }
        public void BtnWriteReview()
        {
            VisWriteReview = !VisWriteReview;
            if (!VisWriteReview)
            {
                BtnContent = "Write a Review";
            }
            else
            {
                if (PublishReviewVis) { PublishReviewVis = false; }
                BtnContent = "View Description";
                if (UserReview != null)
                {
                    WritingReviewText = UserReview.ReviewText;
                }
            }
        }

        public void PublishReview()
        {
            if (WritingReviewText != null)
            {
                CheckIfUpdated = Convert.ToBoolean(Database.Instance.ExecScalar($"EXEC CreateReview {User.Instance.Id}, '{WritingReviewText.Replace("'", "''")}', {Rating + 1}, '{SelectedMovie.GUId}'"));
                if (CheckIfUpdated)
                { PublishReviewText = "Review Updated."; PublishReviewVis = true; }
                else
                { PublishReviewText = "Review Uploaded."; PublishReviewVis = true; }
                ScreenSelector.Instance.SelectNewScreen("MovieInfo", SelectedMovie);
            }
        }
        public void DeleteMovie()
        {
            Database.Instance.ExecNonQuery($"EXEC DeleteReview '{SelectedMovie.GUId}', {User.Instance.Id}");
            ScreenSelector.Instance.SelectNewScreen("MovieInfo", SelectedMovie);
        }
    }
}
