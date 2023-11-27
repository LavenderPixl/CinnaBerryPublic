using CinnaBerry.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls.Mixins;
using CinnaBerry.Data.API;
using System.Collections.ObjectModel;

namespace CinnaBerry.Data.Database
{
    public class Database
    {
        public static Database Instance { get; private set; }
        public SqlConnection sqlConnection;
        internal string ConnectionString = "********";
        static Database()
        {
            Instance = new Database();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns> Returns a string. </returns>
        public string ExecScalar(string command)
        {
            string val = "";
            sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(command, con);
                con.Open();
                val = (string)cmd.ExecuteScalar().ToString();
            }
            sqlConnection.Close();
            return val;
        }

        /// <summary>
        /// Send Query. Returns nothing.
        /// </summary>
        /// <param name="command"></param>
        public void ExecNonQuery(string command)
        {
            sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(command, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            sqlConnection.Close();
        }

        /// <summary>
        /// Gets logged on user, from UserId.
        /// </summary>
        /// <returns></returns>
        public static void GetUser(int userId)
        {
            SqlConnection con = new(Database.Instance.ConnectionString);
            using (con)
            {
                con.Open();
                try
                {
                    SqlCommand command = new SqlCommand($"EXEC GetUser {userId}", con);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User(userId, reader.GetString(1), reader.GetString(2), reader.GetString(3));
                            User.SetUser(user);
                        }
                    }
                }
                catch (Exception exc)
                {
                    Debug.WriteLine(exc);
                }
                //TODO: Check if user gets set! 
            }
        }

        /// <summary>
        /// Give userId of the reviewee.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Returns an observableCollection of Reviews, written by the user. (Type: Review.)</returns>
        public static ObservableCollection<Review> GetReviews(int userId)
        {
            SqlConnection con = new(Database.Instance.ConnectionString);
            using (con)
            {
                con.Open();
                //try
                {
                    SqlCommand command = new SqlCommand($"EXEC GetReviews {userId}", con);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ObservableCollection<Review> reviews = new ObservableCollection<Review>();
                        while (reader.Read())
                        {
                            //TODO: Delete or keep? :
                            //In case we add releaseDate again to review obj: (uint)(API.API.GetMovie(reader.GetString(0))).ReleaseDate
                            Review newReview = new Review(reader.GetString(3), (API.API.GetMovie(reader.GetString(0)).Title), reader.GetString(1), (double)reader.GetInt32(2), reader.GetString(0));
                            reviews.Add(newReview);
                            return reviews;

                        }
                    }
                }
                //catch (Exception exc)
                //{
                //    Debug.WriteLine(exc);
                //    return null;
                //}
                Debug.WriteLine("GetUser() method failed - Can't get user from UserId");
                return null;
            }
        }

        /// <summary>
        /// Gets reviews from the Movie, that do NOT belong to the userId.
        /// Is used for MovieInfoView to display the reviews to the Movie.
        /// </summary>
        /// <param name="guId"></param>
        /// <param name="userId"></param>
        /// <returns>ObservableCollection of Reviews, not belonging to logged in User. </returns>
        public static ObservableCollection<Review> GetReviewsForMovie(string guId, int userId)
        {
            SqlConnection connection = new(Database.Instance.ConnectionString);
            using (connection)
            {
                connection.Open();
                {
                    SqlCommand command = new SqlCommand($"EXEC GetReviewsForMovie '{guId}', {userId}", connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ObservableCollection<Review> reviews = new ObservableCollection<Review>();
                        while (reader.Read())
                        {
                            Review newReview = new Review(reader.GetString(5), (API.API.GetMovie(reader.GetString(1)).Title), reader.GetString(3), (double)reader.GetInt32(4), reader.GetString(1));
                            reviews.Add(newReview);
                        }
                        return reviews;
                    }
                }
            }
            Debug.WriteLine("GetReviewsForMovie() method failed - Connection?");
            return null;
        }


        /// <summary>
        /// Logged in user - Displays above.
        /// </summary>
        /// <param name="guId"></param>
        /// <param name="userId"></param>
        /// <returns>Returns one Review, belonging to the logged in user. </returns>
        public static Review GetUserReviewForMovie(string guId, int userId)
        {
            SqlConnection connection = new(Database.Instance.ConnectionString);
            using (connection)
            {
                connection.Open();
                {
                    SqlCommand command = new SqlCommand($"EXEC GetUserReview '{guId}', {userId}", connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Review review = new Review(reader.GetString(5), (API.API.GetMovie(reader.GetString(1)).Title), reader.GetString(3), (double)reader.GetInt32(4), reader.GetString(1));
                            return review;
                        }
                    }
                }
            }
            Debug.WriteLine("GetUserReview() - No review for user Found.");
            return null;
        }
    }
}

