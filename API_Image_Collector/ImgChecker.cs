using CinnaBerry.Data.Database;
using CinnaBerry.JsonDTO;
using CinnaBerry.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static API_Image_Collector.Program;

namespace API_Image_Collector
{
    public class ImgChecker
    {
        /// <summary>
        /// Looks for an image for the movie, if there is no image in wexo's api it will try to find an image on MoviesDatabase from rapid API
        /// </summary>
        /// <param name="index"> Index number from wexo's API</param>
        public static void ApiImgCheck(int index, MediaType mediaType)
        {
            var httpClient = new HttpClient();
            string url = $"https://feed.entertainment.tv.theplatform.eu/f/jGxigC/bb-all-pas?form=json&fields=guid,tdc$imdbId,title,description,plprogram$year,plprogram$tags,plprogram$thumbnails&byProgramType={mediaType}&range={index}-{index}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = httpClient.Send(request);

                response.EnsureSuccessStatusCode();

                StreamReader reader = new StreamReader(response.Content.ReadAsStream());
                string data = reader.ReadToEnd();

                MovieDTORoot movieDTORoot = JsonConvert.DeserializeObject<MovieDTORoot>(data);
                JObject jsonDom = JsonConvert.DeserializeObject<JObject>(data)!;

                PropertyInfo[] ThumbnailProperties = typeof(PlprogramThumbnails).GetProperties();
                Console.WriteLine($"Looking for images for '{movieDTORoot.entries[0].title}' at index {index}");

                bool imgFound = false;
                List<MovieResolution> movieResolutions = new List<MovieResolution>();

                //Loop going through every image link we get from wexo's API
                foreach (PropertyInfo propinfo in ThumbnailProperties)
                {
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sbWidth = new StringBuilder();
                    StringBuilder sbHeight = new StringBuilder();
                    bool firstNumber = true;
                    bool width = false;
                    bool height = false;
                    char[] namechar = propinfo.Name.ToCharArray();



                    foreach (char c in namechar)
                    {
                        if (char.IsNumber(c) && firstNumber == true)
                        {
                            sb.Append('-');
                            firstNumber = false;
                            width = true;
                        }
                        if (!char.IsNumber(c) && firstNumber == false)
                        {
                            width = false;
                            height = true;
                        }
                        if (char.IsNumber(c) && width == true) sbWidth.Append(c);
                        if (char.IsNumber(c) && height == true) sbHeight.Append(c);
                        sb.Append(c);
                    }


                    string imgUrl = (string)jsonDom.SelectToken($".entries[0].plprogram$thumbnails.{sb}.plprogram$url")!;

                    if (checkForImg(imgUrl))
                    {
                        imgFound = true;
                        Console.WriteLine($"Image found at resolution {sb}");
                        movieResolutions.Add(new(Convert.ToInt32(sbWidth.ToString()), Convert.ToInt32(sbHeight.ToString()), imgUrl));
                        //Database.Instance.ExecNonQuery($"EXEC AddMovieUrl '{movieDTORoot.entries[0].guid}', {index}, '{imgUrl}'");
                    }
                }

                if (movieResolutions.Count > 0)
                {
                    double BiggestDiff = 0;
                    MovieResolution verticalMovie;
                    foreach (MovieResolution movieResolution in movieResolutions)
                    {
                        double diff = (double)movieResolution.width / (double)movieResolution.height;
                        if (BiggestDiff < diff )
                        {
                            Database.Instance.ExecNonQuery($"EXEC AddMovieUrl '{movieDTORoot.entries[0].guid}', {index}, '{movieResolution.name}'");
                            BiggestDiff = diff;
                        }
                    }
                }


                //Looks for an image on MoviesDatabase from rapid API if there is no working image on wexo API
                if (imgFound == false && !string.IsNullOrEmpty(movieDTORoot.entries[0].tdcimdbId))
                {
                    Console.WriteLine("No image found on wexo's api looking on MoviesDatabase");
                    var moviesDbRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"https://moviesdatabase.p.rapidapi.com/titles/{movieDTORoot.entries[0].tdcimdbId}"),
                        Headers =
                         {
                             { "X-RapidAPI-Key", "aff5f50bf8msh2cd68d2c20271d5p1e3ca8jsnd671ba43b0df" },
                             { "X-RapidAPI-Host", "moviesdatabase.p.rapidapi.com" },
                         },
                    };
                    using (moviesDbRequest)
                    {
                        string moviesDbImgUrl = "";
                        var moviesDbResponse = httpClient.Send(moviesDbRequest);
                        moviesDbResponse.EnsureSuccessStatusCode();

                        StreamReader moviesDbReader = new StreamReader(moviesDbResponse.Content.ReadAsStream());
                        string moviesDbData = moviesDbReader.ReadToEnd();

                        MoviesDatabaseDTORoot moviesDatabaseDTORoot = JsonConvert.DeserializeObject<MoviesDatabaseDTORoot>(moviesDbData);
                        try
                        {
                            moviesDbImgUrl = moviesDatabaseDTORoot.results.primaryImage.url;
                        }
                        catch (Exception) { }


                        if (checkForImg(moviesDbImgUrl) == true)
                        {
                            Console.WriteLine($"Image found on MoviesDatabase for {movieDTORoot.entries[0].title}");
                            Database.Instance.ExecNonQuery($"EXEC AddMovieUrl '{movieDTORoot.entries[0].guid}', {index}, '{moviesDbImgUrl}'");
                        }
                        else
                        {
                            Console.WriteLine($"No image could be found for {movieDTORoot.entries[0].title}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if an image url is working
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static bool checkForImg(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {

                if (!url.Contains("bbaws.net"))
                {
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                    request.Method = "head";

                    try
                    {
                        request.GetResponse();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
