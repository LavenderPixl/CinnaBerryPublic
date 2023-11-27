using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinnaBerry.Models
{
    public class Review
    {
        public string User { get; set; }
        public string MediaTitle { get; set; }
        public string ReviewText { get; set; }
        public double Rating { get; set; }
        public string MovieGUId { get; set; }
        
       public Review(string user, string mediaTitle, string reviewText, double rating, string movieGUid)
       {
            User = user;
            MediaTitle = mediaTitle;
            ReviewText = reviewText;
            Rating = rating;
            MovieGUId = movieGUid;
       }
    }
}
