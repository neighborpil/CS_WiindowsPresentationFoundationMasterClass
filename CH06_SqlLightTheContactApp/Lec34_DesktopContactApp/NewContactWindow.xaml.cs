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
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        public NewContactWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Todo: Save contact
            Contact contact = new Contact()
            {
                Name = NameTextBox.Text,
                Email = EmailTextBox.Text,
                Phone = PhoneNumberTextBox.Text
            };

            string databaseName = "Contacts.db";
            string forderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string databasePath = System.IO.Path.Combine(forderPath, databaseName);

            SQLiteConnection connection = new SQLiteConnection(databasePath);
            connection.CreateTable<Contact>();
            connection.Insert(contact);
            connection.Close();

            this.Close();
        }
    }
}
