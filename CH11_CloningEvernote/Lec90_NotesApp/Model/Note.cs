using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Lec90_NotesApp.Annotations;
using SQLite;

namespace Lec90_NotesApp.Model
{
    public class Note : INotifyPropertyChanged
    {
        private int id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private int notebookId;
        [Indexed]
        public int NotebookId
        {
            get { return notebookId; }
            set
            {
                notebookId = value;
                OnPropertyChanged("NotebookId");
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private DateTime createdTime;
        public DateTime CreatedTime
        {
            get { return createdTime; }
            set
            {
                createdTime = value;
                OnPropertyChanged("CreatedTime");
            }
        }

        private DateTime updatedTime;
        public DateTime UpdatedTime
        {
            get { return updatedTime; }
            set
            {
                updatedTime = value;
                OnPropertyChanged("UpdatedTime");
            }
        }

        private string fileLocation;
        public string FileLocation
        {
            get { return fileLocation; }
            set
            {
                fileLocation = value;
                OnPropertyChanged("FileLocation");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
