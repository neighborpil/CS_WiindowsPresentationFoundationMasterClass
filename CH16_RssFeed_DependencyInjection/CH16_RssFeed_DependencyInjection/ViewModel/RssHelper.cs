using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using CH16_RssFeed_DependencyInjection.Model;

namespace CH16_RssFeed_DependencyInjection.ViewModel
{
    public interface IRssHelper
    {
        List<Item> GetPosts();
    }

    public class RssHelper : IRssHelper
    {
        public List<Item> GetPosts()
        {
            XDocument feedXML = XDocument.Load("https://www.finzen.mx/blog-feed.xml");

            var items = feedXML.Descendants("item")
                .Select(i => new Item
                {
                    Title = i.Element("title")?.Value ?? "",
                    Link = i.Element("link")?.Value ?? "",
                    Description = i.Element("description")?.Value ?? "",
                    PubDate = i.Element("pubDate")?.Value
                });

            return items.ToList();
        }
    }
}
