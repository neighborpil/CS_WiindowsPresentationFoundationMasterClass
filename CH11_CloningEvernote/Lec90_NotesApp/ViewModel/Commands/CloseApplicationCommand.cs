using System;
using System.Windows.Input;

namespace Lec90_NotesApp.ViewModel.Commands
{
    public class CloseApplicationCommand : ICommand
    {
        public NotesVM VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public CloseApplicationCommand(NotesVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.CloseApplication();
        }
    }
}
