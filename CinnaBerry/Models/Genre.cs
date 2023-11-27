using CinnaBerry.Data.API;
using CinnaBerry.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinnaBerry.Models
{
    public class Genre
    {
        public delegate void GenreChangedEventHandler(object sender, ChangeGenreEventArgs e); 
        public event GenreChangedEventHandler GenreChanged;

        public string GenreName { get; set; }
        
        public Genre(string genreName) 
        {
            GenreName = genreName;
        }

        /// <summary>
        /// Subscribing to ChangeGenreEventArgs.
        /// SelectedGenre (Property in ChangeGenreEventArgs) is changed to GenreName (Property in Genre obj), when the DelChangeGenre event is invoked.
        /// Is currently bound in the View, to the GenreList.
        /// </summary>
        /// <param name="selectedGenre"></param>
        public void OnGenreChanged()
        {
            GenreChanged?.Invoke(this, new ChangeGenreEventArgs { SelectedGenre = GenreName });
        }
    }

    //EventSender
    public class ChangeGenreEventArgs : EventArgs
    {
        public string SelectedGenre { get; set; } = "";
    }
}
