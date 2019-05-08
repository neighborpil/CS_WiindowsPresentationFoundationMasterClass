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
using Lec90_NotesApp.Model;

namespace Lec90_NotesApp.View.UserControls
{
    /// <summary>
    /// Interaction logic for NoteControl.xaml
    /// </summary>
    public partial class NoteControl : UserControl
    {
        public static readonly DependencyProperty NoteProperty = DependencyProperty.Register(
            "Note", typeof(Note), typeof(NoteControl), new PropertyMetadata(null, SetValue));

        private static void SetValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NoteControl note = d as NoteControl;
            if (note != null)
            {
                note.TitleTextBlock.Text = (e.NewValue as Note).Title;
                note.EditedTextBlock.Text = (e.NewValue as Note).UpdatedTime.ToShortDateString();
                note.ContentTextBlock.Text = (e.NewValue as Note).Title;
            }
        }

        public Note Note
        {
            get { return (Note) GetValue(NoteProperty); }
            set { SetValue(NoteProperty, value); }
        }

        public NoteControl()
        {
            InitializeComponent();
        }
    }
}
