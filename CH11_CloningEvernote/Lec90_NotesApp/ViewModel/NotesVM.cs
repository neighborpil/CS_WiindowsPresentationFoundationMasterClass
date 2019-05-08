using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Lec90_NotesApp.Annotations;
using Lec90_NotesApp.Model;
using Lec90_NotesApp.ViewModel.Commands;
using SQLite;

namespace Lec90_NotesApp.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {
        private bool isEditing;

        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                isEditing = value;
                OnPropertyChanged("IsEditing");
            }
        }


        public ObservableCollection<Notebook> Notebooks { get; set; }
        private Notebook selectedNotebook;
        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                ReadNotes();
            }
        }

        private Note selectedNote;

        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                SelectedNoteChanged(this, new EventArgs());
            }
        }


        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public CloseApplicationCommand CloseApplicationCommand { get; set; }
        public BeginEditCommand BeginEditCommand { get; set; }
        public HasEditedCommand HasEditedCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SelectedNoteChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NotesVM()
        {
            IsEditing = false;
            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            CloseApplicationCommand = new CloseApplicationCommand(this);
            BeginEditCommand = new BeginEditCommand(this);
            HasEditedCommand = new HasEditedCommand(this);

            ReadNotebooks();
            ReadNotes();
        }

        public void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        public void CreateNotebook()
        {
            Notebook newNotebook = new Notebook
            {
                Name = "New notebook",
                UserId = int.Parse(App.UserId)
            };
            DatabaseHelper.Insert(newNotebook);

            ReadNotebooks();
        }

        public void CreateNote(int notebookId)
        {
            Note newNote = new Note
            {
                NotebookId = notebookId,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                Title = "New note"
            };

            DatabaseHelper.Insert(newNote);

            ReadNotes();
        }

        public void ReadNotebooks()
        {
            var notebooks = DatabaseHelper.Table<Notebook>();

            Notebooks.Clear();
            foreach (var notebook in notebooks)
                Notebooks.Add(notebook);
        }

        public void ReadNotes()
        {
            if (SelectedNotebook == null)
                return;

            using (var conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
            {
                conn.CreateTable<Note>();
                var notes = conn.Table<Note>().Where(n => n.NotebookId == SelectedNotebook.Id);
                Notes.Clear();
                foreach (var note in notes)
                    Notes.Add(note);
            }
        }

        public void StartEditing()
        {
            IsEditing = true;
        }

        public void HasReNamed(Notebook notebook)
        {
            if (notebook != null)
            {
                DatabaseHelper.Update(notebook);
                IsEditing = false;
                ReadNotebooks();
            }
        }

        public void UpdateSelectedNote()
        {
            DatabaseHelper.Update(SelectedNote);
        }

    }
}
