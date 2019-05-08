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
using System.Windows.Shapes;
using Lec90_NotesApp.ViewModel;

namespace Lec90_NotesApp.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            LoginVM vm = new LoginVM();
            ContainerGrid.DataContext = vm;
            vm.HasLoggedIn += VM_HasLoggedIn;

        }

        private void VM_HasLoggedIn(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HaveAccountButton_OnClick(object sender, RoutedEventArgs e)
        {
            RegisterStackPanel.Visibility = Visibility.Collapsed;
            LoginStackPanel.Visibility = Visibility.Visible;
        }

        private void NoAccountButton_OnClick(object sender, RoutedEventArgs e)
        {
            LoginStackPanel.Visibility = Visibility.Collapsed;
            RegisterStackPanel.Visibility = Visibility.Visible;
        }
    }
}
