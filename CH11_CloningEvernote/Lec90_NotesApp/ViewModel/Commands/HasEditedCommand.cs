using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lec90_NotesApp.Model;

namespace Lec90_NotesApp.ViewModel.Commands
{
    public class HasEditedCommand : ICommand
    {
        public NotesVM Vm { get; set; }
        public event EventHandler CanExecuteChanged;

        public HasEditedCommand(NotesVM vm)
        {
            Vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is Notebook notebook)
            {
                Vm.HasReNamed(notebook);
            }

        }

    }
}
