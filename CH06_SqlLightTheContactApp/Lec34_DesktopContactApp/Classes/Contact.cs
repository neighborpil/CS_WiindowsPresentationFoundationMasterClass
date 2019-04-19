using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Lec34_DesktopContactApp.Classes
{
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"[Name]: {Name}, [Email]: {Email}, [Phone]:{Phone}";
        }

        //[MaxLength(50)]
        //[Indexed]
        //[Unique]

        //[Ignore]
        //public string FullName { get; }

        //[Ignore]
        //public List<Contact> Inhabitats { get; set; }
    }
}
