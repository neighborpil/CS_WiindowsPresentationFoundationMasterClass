using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lec90_NotesApp.Model;
using Lec90_NotesApp.ViewModel.Commands;

namespace Lec90_NotesApp.ViewModel
{
    public class LoginVM
    {
        private User user;
        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public RegisterCommand RegisterCommand { get; set; }
        public LoginCommand LoginCommand { get; set; }

        public event EventHandler HasLoggedIn;

        public LoginVM()
        {
            User = new User();
            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);

        }

        public void Login()
        {
            using (var conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
            {
                conn.CreateTable<User>();
                var user = conn.Table<User>().Where(u => u.Username == User.Username).FirstOrDefault();

                if(user.Password == User.Password)
                {
                    App.UserId = user.Id.ToString();
                    HasLoggedIn?.Invoke(this, new EventArgs());
                }
            }
        }

        public void Register()
        {
            var result = DatabaseHelper.Insert<User>(User);

            if(result)
            {
                App.UserId = User.Id.ToString();
                HasLoggedIn?.Invoke(this, new EventArgs());
            }
        }
    }
}
