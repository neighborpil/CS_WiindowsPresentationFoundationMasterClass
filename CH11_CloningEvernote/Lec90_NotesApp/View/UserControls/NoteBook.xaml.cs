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

namespace Lec90_NotesApp.View.UserControls
{
    /// <summary>
    /// Interaction logic for NoteBook.xaml
    /// </summary>
    public partial class NoteBook : UserControl
    {
        public Model.Notebook DisplayNotebook
        {
            get { return (Model.Notebook)GetValue(NotebookProperty); }
            set { SetValue(NotebookProperty, value); }
        }

        public static readonly DependencyProperty NotebookProperty = DependencyProperty.Register(
            "DisplayNotebook", typeof(Model.Notebook), typeof(NoteBook), new PropertyMetadata(null, SetValue));

        private static void SetValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NoteBook notebook = d as NoteBook;

            if (notebook != null)
            {
                notebook.NotebookNameTextBox.Text = (e.NewValue as Model.Notebook).Name;
            }
        }

        public NoteBook()
        {
            InitializeComponent();
        }
    }
}
