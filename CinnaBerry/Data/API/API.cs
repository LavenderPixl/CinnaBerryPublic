using Avalonia.Controls.Chrome;
using CinnaBerry.JsonDTO;
using CinnaBerry.Models;
using DynamicData.Binding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using static CinnaBerry.JsonDTO.SeriesDTO;

namespace CinnaBerry.Data.API
{
    public class API
    {
        private static string baseUrl = "https://feed.entertainment.tv.theplatform.eu/f/jGxigC/";

        #region Series
        private static ObservableCollection<Series> GetSeriesPrivate(int startRange, int endRange, string apiCall)
        {
            var httpClient = new HttpClient();
            ObservableCollection<Series> series = new();
            string url = baseUrl + apiCall;

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = httpClient.Send(request);
                response.EnsureSuccessStatusCode();
                StreamReader reader = new StreamReader(response.Content.ReadAsStream());
                string data = reader.ReadToEnd();
                SeriesDTORoot seriesDTORoot = JsonConvert.DeserializeObject<SeriesDTORoot>(data);
                endRange = startRange + seriesDTORoot.entryCount - 1;
                for (int i = 0; startRange <= endRange; i++, startRange++)
                {
                    series.Add(new Series(seriesDTORoot.entries[i]));
                }
            }
            return series;
        }

        #endregion

        #region Movies
        private static ObservableCollection<Movie> GetMoviesPrivate(int startRange, int endRange, string apiCall)
        {
            var httpClient = new HttpClient();
            ObservableCollection<Movie> movies = new();
            string url = baseUrl + apiCall;

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = httpClient.Send(request);

                response.EnsureSuccessStatusCode();

                StreamReader reader = new StreamReader(response.Content.ReadAsStream());
                string data = reader.ReadToEnd();

                MovieDTORoot movieDTORoot = JsonConvert.DeserializeObject<MovieDTORoot>(data);

                //JObject jsonDom = JsonConvert.DeserializeObject<JObject>(data)!;
                endRange = startRange + movieDTORoot.entryCount - 1;
                for (int i = 0; startRange <= endRange; i++, startRange++)
                {
                    movies.Add(new Movie(movieDTORoot.entries[i]));
                    //movies.Add(new Movie(jsonDom.SelectToken($".entries[{i}]")!));
                }
            }
            return movies;
        }

        public static ObservableCollection<Series> GetSeries(int startRange, int endRange)
        {
            string apiCall = $"bb-all-pas?form=json&fields=guid,title,description,plprogram$year,plprogram$seriesTvSeasons,plprogram$tags&byProgramType=series&range={startRange}-{endRange}";

            return GetSeriesPrivate(startRange, endRange, apiCall);
        }

        public static ObservableCollection<Series> GetSeries(int startRange, int endRange, string genre)
        {
            string apiCall = $"bb-all-pas?form=json&fields=guid,title,description,plprogram$year,plprogram$seriesTvSeasons,plprogram$tags&byProgramType=series&range={startRange}-{endRange}&byTags=genre:{genre}";

            return GetSeriesPrivate(startRange, endRange, apiCall);
        }

        public static ObservableCollection<Movie> GetMovies(int startRange, int endRange)
        {
            string apiCall = $"bb-all-pas?form=json&fields=guid,title,description,plprogram$year,plprogram$tags&byProgramType=movie&range={startRange}-{endRange}";

            return GetMoviesPrivate(startRange, endRange, apiCall);
        }

        public static ObservableCollection<Movie> GetMovies(int startRange, int endRange, string genre)
        {
            string apiCall = $"bb-all-pas?form=json&fields=guid,title,description,plprogram$year,plprogram$tags&byProgramType=movie&range={startRange}-{endRange}&byTags=genre:{genre}";
            return GetMoviesPrivate(startRange, endRange, apiCall);
        }

        /// <summary>
        /// Give a GUId for the movie.
        /// </summary>
        /// <returns>Returns the movie, with that GUId.</returns>
        public static Movie GetMovie(string guId)
        {
            var httpClient = new HttpClient();
            string url = baseUrl + $"bb-all-pas?form=json&range=1-1&byProgramType=movie&fields=guid,title,description,plprogram$year,plprogram$tags&byguid={guId}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = httpClient.Send(request);
                response.EnsureSuccessStatusCode();

                StreamReader reader = new StreamReader(response.Content.ReadAsStream());
                string data = reader.ReadToEnd();

                MovieDTORoot movieDTORoot = JsonConvert.DeserializeObject< MovieDTORoot>(data);

                return new Movie(movieDTORoot.entries[0]);
            }
        }
        #endregion
    }
}
