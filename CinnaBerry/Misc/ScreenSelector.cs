using CinnaBerry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinnaBerry.Misc
{
    public class ScreenSelector
    {

        public delegate void SelectorEventHandler(object sender, SelectorEventArgs e);
        public event SelectorEventHandler SelectionChanged;

        private static ScreenSelector instance;

        public static ScreenSelector Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenSelector();
                }
                return instance;
            }
        }
        /// <summary>
        /// Which screen do you want to display?
        /// </summary>
        /// <param name="selection"></param>
        public void SelectNewScreen(string selection, Movie SelectedMovie)
        {
            SelectionChanged?.Invoke(this, new SelectorEventArgs(selection,SelectedMovie));
        }
    }


    /// <summary>
    /// Sends data from caller to receiver.
    /// </summary>
    public class SelectorEventArgs : EventArgs
    {
        /// <summary>
        /// Which screen do you want to display?
        /// </summary>
        public string Selection { get; private set; }
        public Movie SelectedMovie { get; private set; } = null;
        public SelectorEventArgs(string Selection, Movie SelectedMovie)
        {
            this.Selection = Selection;
            this.SelectedMovie = SelectedMovie;
        }
    }
}
