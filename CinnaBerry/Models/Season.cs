using Avalonia.Controls;
using CinnaBerry.Data.Database;
using CinnaBerry.JsonDTO;
using DynamicData.Experimental;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CinnaBerry.JsonDTO.SeriesDTO;
using static CinnaBerry.Models.Media;

namespace CinnaBerry.Models
{
    public class Season : ReactiveObject
    {
        public string Guid { get; private set; }
        public string SeriesGUId { get; private set; }
        public uint ReleaseDate { get; private set; }
        public Media.WatchStage WatchingStage { get; private set; }
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
                    Database.Instance.ExecNonQuery($"EXEC UpdateSeriesWatched '{SeriesGUId}', '{Guid}', {User.Instance.Id}, 0");
                }
                else
                {
                    WatchingStage = WatchStage.Not_Watched;
                }
                DeleteSeasonWatched();
            }
        }

        private bool _planToWatch;

        public bool PlanToWatch
        {
            get => _planToWatch; 
            set {
                this.RaiseAndSetIfChanged(ref _planToWatch, value);
                if (value == true)
                {
                    Watched = false;
                    WatchingStage = WatchStage.Plan_to_watch;
                    Database.Instance.ExecNonQuery($"EXEC UpdateSeriesWatched '{SeriesGUId}', '{Guid}', {User.Instance.Id}, 1");
                }
                else
                {
                    WatchingStage = WatchStage.Not_Watched;
                }
                DeleteSeasonWatched();
            }
        }



        public Season(string guid, uint releaseDate, WatchStage watchingStage)
        {
            Guid = guid;
            ReleaseDate = releaseDate;
            WatchingStage = watchingStage;
        }
        public Season(PlprogramSeriesTvSeason season, string seriesGUId)
        {
            Guid = season.plprogramguid;
            SeriesGUId = seriesGUId;
            ReleaseDate = (uint)season.plprogramstartYear;

            string watched = Database.Instance.ExecScalar($"exec GetSeriesWatched '{SeriesGUId}', '{Guid}', {User.Instance.Id}");

            if (!string.IsNullOrEmpty(watched))
            {
                if (watched == "False") Watched = true;
                if (watched == "True") PlanToWatch = true;
            }
        }

        private void DeleteSeasonWatched()
        {
            if (Watched == false && PlanToWatch == false)
            {
                Database.Instance.ExecNonQuery($"EXEC DeleteSeriesWatched");
            }
        }
    }
}
