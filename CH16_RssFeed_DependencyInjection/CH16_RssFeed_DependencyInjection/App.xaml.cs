using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using CH16_RssFeed_DependencyInjection.ViewModel;

namespace CH16_RssFeed_DependencyInjection
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<RssHelper>().As<IRssHelper>();
            builder.Register(new MainWindow());

            //DependencyInjector.Register<IRssHelper, RssHelper>();
            //MainWindow = DependencyInjector.Retrive<MainWindow>();
            //MainWindow.Show();

        }
    }
}
