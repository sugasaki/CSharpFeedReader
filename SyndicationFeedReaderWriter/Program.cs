using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;


namespace SyndicationFeedReaderWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start loading feed");

            string url = "https://news.yahoo.co.jp/pickup/computer/rss.xml";
            var t = ReadAtomFeed(url);
            t.Wait();

            Console.WriteLine("Comleted loading feed");
            Console.ReadKey();
        }


        /// <summary>
        /// Feed Reader
        /// https://github.com/dotnet/SyndicationFeedReaderWriter/blob/master/examples/ReadRssItemWithCustomFieldsExample.cs
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task ReadAtomFeed(string filepath)
        {
            try
            {
                //
                // Create an XmlReader from file
                using (var xmlReader = XmlReader.Create(filepath, new XmlReaderSettings() { Async = true }))
                {
                    var parser = new RssParser();
                    var feedReader = new RssFeedReader(xmlReader, parser);

                    //
                    // Read the feed
                    while (await feedReader.Read())
                    {
                        if (feedReader.ElementType == SyndicationElementType.Item)
                        {
                            //
                            // Read the item as generic content
                            ISyndicationContent content = await feedReader.ReadContent();

                            //
                            // Parse the item if needed (unrecognized tags aren't available)
                            // Utilize the existing parser
                            ISyndicationItem item = parser.CreateItem(content);

                            Console.WriteLine($"Item: {item.Title}");

                            //
                            // Get <example:customElement> field
                            ISyndicationContent customElement = content.Fields.FirstOrDefault(f => f.Name == "example:customElement");

                            if (customElement != null)
                            {
                                Console.WriteLine($"{customElement.Name}: {customElement.Value}");
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.InnerException.Message}{Environment.NewLine}{ex.InnerException.ToString()}");
            }

        }


    }
}
