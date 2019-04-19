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
using Lec34_DesktopContactApp.Classes;
using SQLite;

namespace Lec34_DesktopContactApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;


        public MainWindow()
        {
            InitializeComponent();
            contacts = new List<Contact>();
            ReadDatabase();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();

            ReadDatabase(); // This opens after NewContactWindow closes.
        }

        void ReadDatabase()
        {
            using (SQLite.SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                contacts = new List<Contact>();
                conn.CreateTable<Contact>();
                var orderedContacts = conn.Table<Contact>().OrderBy(c => c.Name);
                contacts.AddRange(orderedContacts);  // retrieve the table

                //var variable = from c2 in contacts
                //               orderby c2.Name
                //               select c2;
            }

            //foreach (var contact in contacts)
            //{
            //    contactsListView.Items.Add(new ListViewItem
            //    {
            //        Content = contact
            //    });
            //}
            ContactsListView.ItemsSource = contacts;
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchTextBox = sender as TextBox;
            string searchText = searchTextBox.Text;
            var filteredList = contacts.Where(c => c.Name.ToLower().Contains(searchText.ToLower()));
            //var filteredList2 = from c2 in contacts
            //                    where c2.Name.ToLower().Contains(searchText.ToLower())
            //                    orderby c2.Email
            //                    select c2.Id;

            ContactsListView.ItemsSource = filteredList;

        }

        private void ContactsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContactsListView.SelectedItem is Contact selectedContact)
            {
                ContactDetailsWindow window = new ContactDetailsWindow(selectedContact);
                window.ItemChangedEvent += (o, args) => ReadDatabase();
                window.ShowDialog();
                ReadDatabase();
            }
        }
    }
}
