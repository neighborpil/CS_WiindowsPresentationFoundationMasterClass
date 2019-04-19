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
using Lec34_DesktopContactApp.Classes;
using SQLite;

namespace Lec34_DesktopContactApp
{
    /// <summary>
    /// Interaction logic for ContactDetailsWindow.xaml
    /// </summary>
    public partial class ContactDetailsWindow : Window
    {
        private readonly Contact _contact;

        public event EventHandler ItemChangedEvent;

        public ContactDetailsWindow(Contact contact)
        {
            InitializeComponent();
            _contact = contact;
            NameTextBox.Text = contact.Name;
            EmailTextBox.Text = contact.Email;
            PhoneNumberTextBox.Text = contact.Phone;
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            _contact.Name = NameTextBox.Text;
            _contact.Email = EmailTextBox.Text;
            _contact.Phone = PhoneNumberTextBox.Text;

            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<Contact>();
                connection.Update(_contact);

                ItemChangedEvent?.Invoke(this, new EventArgs());
            }
            this.Close();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<Contact>();
                connection.Delete(_contact);

                ItemChangedEvent?.Invoke(this, new EventArgs());
            }
            this.Close();
        }
    }
}
