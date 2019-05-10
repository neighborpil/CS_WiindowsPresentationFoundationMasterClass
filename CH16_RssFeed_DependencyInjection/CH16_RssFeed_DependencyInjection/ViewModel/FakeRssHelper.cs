using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH16_RssFeed_DependencyInjection.Model;

namespace CH16_RssFeed_DependencyInjection.ViewModel
{
    public class FakeRssHelper : IRssHelper
    {
        public List<Item> GetPosts()
        {
            return new List<Item>()
            {
                new Item
                {
                    Title = "title1",
                    Description = "description1",
                    PubDate = "Fri, 10 May 2019 08:52:39 GMT"
                },
                new Item
                {
                    Title = "title2",
                    Description = "description2",
                    PubDate = "Fri, 10 May 2019 08:52:39 GMT"
                },
                new Item
                {
                    Title = "title3",
                    Description = "description3",
                    PubDate = "Fri, 10 May 2019 08:52:39 GMT"
                }
            };

        }
    }
}
