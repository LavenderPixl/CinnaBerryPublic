using CinnaBerry.Models;
using HarfBuzzSharp;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinnaBerry.ViewModels
{
    public class PublicProfileViewModel : ViewModelBase
    {
		private string _username = "Username";
		public string Username
		{
			get => _username;
			set => this.RaiseAndSetIfChanged(ref _username, value);
		}

		private string _favGenre = "None";
		public string FavGenre
		{
			get => "Favorite Genre: " + _favGenre;
			set => this.RaiseAndSetIfChanged(ref _favGenre, value);
		}

        private string _moviesWatched = "None";
        public string MoviesWatched
        {
            get => "Movies Watched: " + _moviesWatched;
            set => this.RaiseAndSetIfChanged(ref _moviesWatched, value);
        }

        private string _seriesWatched = "None";
        public string SeriesWatched
        {
            get => "Series Watched: " + _seriesWatched;
            set => this.RaiseAndSetIfChanged(ref _seriesWatched, value);
        }

        private string _reviewsWritten = "None";
        public string ReviewsWritten
        {
            get => "Reviews Written: " + _reviewsWritten;
            set => this.RaiseAndSetIfChanged(ref _reviewsWritten, value);
        }

        private string _averageRating = "None";
        public string AverageRating
        {
            get => "Average Rating: " + _averageRating;
            set => this.RaiseAndSetIfChanged(ref _averageRating, value);
        }

        private ObservableCollection<Review> _reviews = new();
        public ObservableCollection<Review> Reviews
        {
            get => _reviews;
            set => this.RaiseAndSetIfChanged(ref _reviews, value);
        }

        public PublicProfileViewModel()
        {
            Reviews.Add(new Review("peter", "film", "This is a lesson to the movie industry on how to use a budget. 80 million dollars was used splendidly. The cinematography was amazing, (Not terribly surprising because Rogue One) acting was great, and the story was decent.\r\n\r\nIt wasn't without problems though. The story moves at an increasing pace and at some points you lose track of what's happening. Suspension of disbelief will be needed in some moments.\r\n\r\nThe theme of the story was to make AI to be more than just robots. I think they succeeded there, but at the expense of the humans. Most of the humans in the story ended up being one faced - except for Joshua.\r\n\r\nThe dynamic between Joshua and Alfie was by far the best part of the movie. The acting was great between the two.\r\n\r\nIt was a good movie. Not great by any means, but I'm all for supporting a movie that is trying something new.\r\n\r\nOverall, I think Gareth Edwards should be given some more projects. AND filmmakers everywhere should learn how a budget should be used.", 10, "gfgfdl"));
            Reviews.Add(new Review("peter", "film", "My wife and I caught The Creator (2023) in theaters last night. The storyline unfolds in a futuristic society where humans and AI robots find themselves in conflict, triggered by an AI robot-initiated a nuclear attack on Los Angeles that claimed a million human lives. A man, once assigned to bring down the leader of the AI robots, experiences a devastating loss of his wife and child during an undercover mission. The government offers him a chance to complete one last mission with the promise of reuniting with his wife, and he eagerly accepts the opportunity to right past wrongs.\r\n\r\nDirected by Gareth Edwards (Rogue One: A Star Wars Story), the film stars John David Washington (Tenet), Madeleine Yuna Voyles, Gemma Chan (Eternals), Ken Watanabe (The Last Samurai), Allison Janney (The Help) and Marc Menchaca (Ozark).\r\n\r\nWhile the storyline bore strong resemblances to Blade Runner, unfortunately, the plot of The Creator proved to be highly predictable, with only one noteworthy twist, and I didn't find myself as invested in the characters as I had hoped. On the positive side, the acting, special effects, cinematography and settings were magnificent, but throughout the entire film, there was a sense that something crucial was missing. Additionally, the film's extended runtime led to certain sections that dragged. However, it's worth noting that the ending was satisfying, and every action scene and shootout was executed exceptionally well, making this worth your time.\r\n\r\nIn conclusion, The Creator is a modernized take on the Blade Runner formula that falls short of greatness due to a missing element. I would give this a 6/10 rating and recommend seeing it once.", 7, "gfgfdl"));
            Reviews.Add(new Review("peter", "film", "The Creator seemed to promise so much through its trailer, and although it is still a very enjoyable film it feels a little too safe and run of the mill.\r\n\r\nIt's story follows the familiar tropes of humanity against AI, super weapons, chosen ones, and the reluctant guardian. None of these are overly original but they are decently executed in this film nonetheless.\r\n\r\nThe emotion of the piece is a bit hit and miss. Even though the performances are good, particularly Madeleine Yuna Voyles in the role of Alfie, I just didn't really connect to any of the characters. This lack of connection and emotion is one of the biggest things missing from this film.\r\n\r\nThere is no denying however that this film is beautiful. The natural landscapes are used well and the grainy camera style really adds a nice element. It also has a good score and the direction is very solid.\r\n\r\nUltimately The Creator is a really solid sci-fi film, but it just feels a little safe and derivative. Perhaps my expectations were too high as the trailer suggested this was potential a more high concept and emotionally gripping sci-fi, when I don't think that is the case. Still a good film though.", 5, "gfgfdl"));
            Reviews.Add(new Review("peter", "film", "With stunning visuals reminiscent of Blade Runner and the more recent Rouge One this movie is stunning to look at. Unfortunately the script falls far short of living up to its inspired cinematography. It's full of plot holes and cringe worthy moments from it's extremely one dimensional villains. The plot twist are telegraphed from miles away and there are no real surprises to be had. Everything plays out exactly like you would expect it to. Which is a shame. The look and mood of this film is almost enough to save it. But in the end it falls flat and its potential is wasted. See it for the visuals. Forget it for its script.", 1, "gfgfdl"));
        }
    }
}
