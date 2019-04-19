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

namespace Lec34_DesktopContactApp.Controls
{
    /// <summary>
    /// Interaction logic for ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {
        private Contact contact;

        public Contact Contact
        {
            get { return (Contact)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        private static readonly Contact newContact = new Contact
        {
            Name = "FirstName LastName",
            Email = "email@domain.com",
            Phone = "(123) 456 789"
        };

        public static readonly DependencyProperty ContactProperty =
            DependencyProperty.Register("Contact",
                                        typeof(Contact),
                                        typeof(ContactControl),
                                        new PropertyMetadata(newContact, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ContactControl control)
            {
                var contact = e.NewValue as Contact;
                control.NameTextBlock.Text = contact?.Name;
                control.EmailTextBlock.Text = contact?.Email;
                control.PhoneTextBlock.Text = contact?.Phone;
            }

            
        }


        public ContactControl()
        {
            InitializeComponent();
        }
    }
}
