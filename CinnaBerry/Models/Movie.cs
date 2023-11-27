using AsyncImageLoader.Loaders;
using CinnaBerry.Data.Database;
using CinnaBerry.JsonDTO;
using CinnaBerry.Misc;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinnaBerry.Models
{
    public class Movie : Media
    {
        #region Properties

        #endregion

        private bool _watched;
        public bool Watched
        {
            get => _watched;
            set 
            {
                this.RaiseAndSetIfChanged(ref _watched, value);
                if (value == true)
                {
                    PlanToWatch = false;
                    WatchingStage = WatchStage.Watched;
                    Database.Instance.ExecNonQuery($"EXEC UpdateMovieWatched '{GUId}', {User.Instance.Id}, 0");
                }
                else
                {
                    WatchingStage = WatchStage.Not_Watched;
                }
                DeleteMoviesWhatched();
            }
        }


    private bool _planToWatch;
        public bool PlanToWatch
        {
            get => _planToWatch;
            set 
            {
                this.RaiseAndSetIfChanged(ref _planToWatch, value);
                if (value == true)
                {
                    Watched = false;
                    WatchingStage = WatchStage.Plan_to_watch;
                    Database.Instance.ExecNonQuery($"EXEC UpdateMovieWatched '{GUId}', {User.Instance.Id}, 1");
                }
                else
                {
                    WatchingStage = WatchStage.Not_Watched;
                }
                DeleteMoviesWhatched();
            }
        }


        public Movie(
            string guId,
            string title,
            string description,
            uint releaseDate,
            string imgPath,
            WatchStage watchingStage,
            List<string> genre

        ) : base(guId, title, description, releaseDate, imgPath, watchingStage, genre)
        {
            
        }

        public Movie(Entry entry) : base(entry.guid, entry.title, entry.description, (uint)entry.plprogramyear, "", WatchStage.Not_Watched, new List<string> { })
        {
            ImgPath = Database.Instance.ExecScalar($"EXEC GetImgUrl '{GUId}'");

            for (int i = 0; i < entry.plprogramtags.Count(); i++)
            {
                if ("genre" == entry.plprogramtags[i].plprogramscheme)
                {
                    Genre.Add(entry.plprogramtags[i].plprogramtitle);
                }
            }

            string watched = Database.Instance.ExecScalar($"exec GetMovieWatched '{entry.guid}', {User.Instance.Id}");

            if (!string.IsNullOrEmpty(watched))
            {
                if (watched == "False") Watched = true;
                if (watched == "True") PlanToWatch = true;
            }

        }

        public void SelectMovie()
        {
            ScreenSelector.Instance.SelectNewScreen("MovieInfo", this); 
        }
        private void DeleteMoviesWhatched()
        {
            if (Watched == false && PlanToWatch == false)
            {
                Database.Instance.ExecNonQuery($"EXEC DeleteMoviewatched '{GUId}', {User.Instance.Id}");
            }
        }
    }
}
