using CinnaBerry.JsonDTO;
using System.Reflection;

namespace API_Image_Collector
{
    public class Program
    {
        public enum MediaType
        {
            movie,
            series
        }
        static void Main(string[] args)
        {
            int index = 1;
            MediaType mediatype = MediaType.movie;


            Console.WriteLine("API Image Collector for CinnaBerry \n");
            Console.WriteLine("Select media type \n1 For movies \n2 for series");
            string SelectedMediaType = Console.ReadLine();

            if (SelectedMediaType == "1")
            {
                mediatype = MediaType.movie;
            }
            else if (SelectedMediaType == "2")
            {
                mediatype = MediaType.series;
            }

            Console.WriteLine("Select starting index");
            index = Convert.ToInt32(Console.ReadLine());
            int requestCount = 0;
            do
            {
                if (requestCount >= 100)
                {
                    Console.WriteLine("waiting 5 minutes to not get rate limited");
                    System.Threading.Thread.Sleep(300000);
                    requestCount = 0;
                }
                var watch = new System.Diagnostics.Stopwatch();

                watch.Start();
                ImgChecker.ApiImgCheck(index, mediatype);
                index++;

                watch.Stop();
                requestCount++;
                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms \n");
            } while (true);
        }
    }
}