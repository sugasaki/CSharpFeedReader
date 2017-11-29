using System;
using System.Linq;
using System.Threading.Tasks;

using CodeHollow.FeedReader;


namespace CodehollowFeedReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start loading feed");

            string url = "https://news.yahoo.co.jp/pickup/computer/rss.xml";
            var result = ReadAtomFeed(url);
            result.Wait();

            Console.WriteLine("Comleted loading feed");
            Console.ReadKey();
        }

        /// <summary>
        /// Feed Reader
        /// https://github.com/codehollow/FeedReader/blob/master/FeedReader.ConsoleSample/Program.cs
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<Feed> ReadAtomFeed(string url)
        {
            try
            {
                var reader = await FeedReader.ReadAsync(url);

                foreach (var item in reader.Items)
                {
                    Console.WriteLine(item.Title + " - " + item.Link);
                }
                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.InnerException.Message}{Environment.NewLine}{ex.InnerException.ToString()}");
                return null;
            }
        }


    }
}