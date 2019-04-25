using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Lec90_NotesApp.Model;
using Lec90_NotesApp.ViewModel.Commands;

namespace Lec90_NotesApp.ViewModel
{
    public class NotesVM
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }
        private Notebook selectedNotebook;

        

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                //todo: get the notes
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public CloseApplicationCommand CloseApplicationCommand { get; set; }

        public NotesVM()
        {
            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            CloseApplicationCommand = new CloseApplicationCommand(this);

            ReadNotebooks();
        }

        public void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        public void CreateNotebook()
        {
            Notebook newNotebook = new Notebook
            {
                Name = "New notebook"
            };
            DatabaseHelper.Insert(newNotebook);
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
        }

        public void ReadNotebooks()
        {
            var notebooks = DatabaseHelper.Table<Notebook>();

            Notebooks.Clear();
            foreach (var notebook in notebooks)
                Notebooks.Add(notebook);
        }

        public void ReadNotes(int selectedNotebookId)
        {
            if (SelectedNotebook == null)
                return;

            using (var conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
            {
                var notes = conn.Table<Note>().Where(n => n.NotebookId ==SelectedNotebook.Id);
                Notes.Clear();
                foreach (var note in notes)
                    Notes.Add(note);
            }
        }
    }
}
