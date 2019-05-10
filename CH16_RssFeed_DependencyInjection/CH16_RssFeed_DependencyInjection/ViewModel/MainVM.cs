using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH16_RssFeed_DependencyInjection.Model;

namespace CH16_RssFeed_DependencyInjection.ViewModel
{
    public class MainVM
    {
        private readonly IRssHelper rssHelper;

        public ObservableCollection<Item> Items { get; set; }

        public MainVM(IRssHelper rssHelper)
        {
            this.rssHelper = rssHelper;
            Items = new ObservableCollection<Item>();

            ReadRss();
        }

        private void ReadRss()
        {
            var posts = rssHelper.GetPosts();

            Items.Clear();
            foreach (var post in posts)
            {
                Items.Add(post);
            }
        }
    }
}
