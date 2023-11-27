using System;
using System.Collections.Generic;
using CinnaBerry.JsonDTO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinnaBerry.Data.Database;
using System.Diagnostics;
using static CinnaBerry.JsonDTO.SeriesDTO;

namespace CinnaBerry.Models
{
    public class Series : Media
    {
        public List<Season> Seasons { get; set; } = new List<Season>();
        public int SeasonsWatched { get; set; } = 0;
        public Series(
            string guId,
            string title,
            string description,
            uint releaseDate,
            string imgPath,
            WatchStage watchingStage,
            List<string> genre

        ) : base(guId, title, description, releaseDate, imgPath, watchingStage, genre)
        {}

        public Series(SeriesDTO.Entry entry) : base(entry.guid, entry.title, entry.description, (uint?)entry.plprogramyear, "", WatchStage.Not_Watched, new List<string> { })
        {
            ImgPath = Database.Instance.ExecScalar($"EXEC GetImgUrl '{GUId}'");

            for (int i = 0; i < entry.plprogramtags.Count(); i++)
            {
                if ("genre" == entry.plprogramtags[i].plprogramscheme)
                {
                    Genre.Add(entry.plprogramtags[i].plprogramtitle);
                }
            }

            foreach (PlprogramSeriesTvSeason season in entry.plprogramseriesTvSeasons)
            {
                Seasons.Add(new(season, entry.guid));
            }
            foreach(Season season in Seasons)
            {
                if (season.Watched == true) SeasonsWatched++;
            }
        }

        public void SelectSeries()
        {

        }
    }
}
