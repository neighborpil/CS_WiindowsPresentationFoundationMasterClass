using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lec90_NotesApp.Model;

namespace Lec90_NotesApp.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public LoginVM VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public RegisterCommand(LoginVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            var user = parameter as User;

            //if (user == null)
            //    return false;
            //if (string.IsNullOrWhiteSpace(user.Username))
            //    return false;
            //if (string.IsNullOrWhiteSpace(user.Password))
            //    return false;
            //if (string.IsNullOrWhiteSpace(user.Email))
            //    return false;
            //if (string.IsNullOrWhiteSpace(user.Lastname))
            //    return false;
            //if (string.IsNullOrWhiteSpace(user.Name))
            //    return false;

            return true;
        }

        public void Execute(object parameter)
        {
            VM.Register();

        }

    }
}
