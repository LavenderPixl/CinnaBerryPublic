using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinnaBerry.JsonDTO
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Caption
    {
        public string plainText { get; set; }
        public string __typename { get; set; }
    }

    public class OriginalTitleText
    {
        public string text { get; set; }
        public string __typename { get; set; }
    }

    public class PrimaryImage
    {
        public string id { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
        public Caption caption { get; set; }
        public string __typename { get; set; }
    }


    public class Results
    {
        public string _id { get; set; }
        public string id { get; set; }
        public PrimaryImage primaryImage { get; set; }
        public TitleType titleType { get; set; }
        public TitleText titleText { get; set; }
        public OriginalTitleText originalTitleText { get; set; }
    }

    public class MoviesDatabaseDTORoot
    {
        public Results results { get; set; }
    }

    public class TitleText
    {
        public string text { get; set; }
        public string __typename { get; set; }
    }

    public class TitleType
    {
        public string text { get; set; }
        public string id { get; set; }
        public bool isSeries { get; set; }
        public bool isEpisode { get; set; }
        public string __typename { get; set; }
    }


}
