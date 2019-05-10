using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CH16_RssFeed_DependencyInjection.Model
{
    public class Item
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
        private string pubDate;
        public string PubDate
        {
            get => pubDate;
            set
            {
                pubDate = value;
                PublishedDate = DateTime.ParseExact(pubDate, "ddd, dd MMM yyyy HH:mm:ss GMT",
                    CultureInfo.InvariantCulture);
            }
        }
    }
}
