using Avalonia.Controls;
using Avalonia;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using CinnaBerry.JsonDTO;
using AsyncImageLoader.Loaders;

namespace CinnaBerry.Models
{
    abstract public class Media : ReactiveObject
    {
        public string GUId { get; protected set; }
        public string Title { get; set; }
        public string Description { get; protected set; } = string.Empty;
        public uint? ReleaseDate { get; protected set; }
        public string ImgPath { get; protected set; }
        public WatchStage WatchingStage { get; protected set; }
        public BaseWebImageLoader BaseWebImageLoader { get; set; } = new BaseWebImageLoader();
        public enum WatchStage
        {
            Not_Watched,
            Plan_to_watch,
            Watched
        }
        public List<string> Genre = new List<string>();

        string shortDescription;
        public string ShortDescription
        {
            get => shortDescription;
            set => this.RaiseAndSetIfChanged(ref shortDescription, value);
        }
        string shortTitle;
        public string ShortTitle
        {
            get => shortTitle;
            set => this.RaiseAndSetIfChanged(ref shortTitle, value);
        }
        public string FixedYear
        {
            get => "(" + ReleaseDate.ToString() + ")";
        }

        public Media(string guId, string title, string description, uint? releaseDate, string imgPath, WatchStage watchingStage, List<string> genre)
        {
            GUId = guId;
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
            ImgPath = imgPath;
            WatchingStage = watchingStage;
            Genre = genre;
            getShortDescription();
            getShortTitle();
        }

        #region Shortened strings, ends with "..."

        protected void getShortDescription()
        {
            if (Description == null)
            {
                shortDescription = "";
            }
            else
            {
                if (Description.Length >= 370)
                { ShortDescription = Description.Substring(0, 370) + "..."; }
                else
                { ShortDescription = Description; }

            }
        }
        protected void getShortTitle()
        {
            if (Title == null)
            {
                shortTitle = "";
            }
            else
            {
                if (Title.Length >= 25)
                { ShortTitle = Title.Substring(0, 25) + "..."; }
                else
                { ShortTitle = Title; }
            }
        }
        #endregion
    }
}
