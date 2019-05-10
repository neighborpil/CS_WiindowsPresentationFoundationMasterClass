using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CH16_RssFeed_DependencyInjection.ViewModel;
using Unity;

namespace CH16_RssFeed_DependencyInjection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public MainVM ViewModel
        {
            set { DataContext = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
