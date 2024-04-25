using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11labbib
{
    public class Prep
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Fam { get; set; }
        public string Otch { get; set; }

        public Prep()
        { }
        public Prep(string Login, string Password, string Name, string Fam, string Otch)
        {
            this.Login = Login;
            this.Password = Password;
            this.Name = Name;
            this.Fam = Fam;
            this.Otch = Otch;

        }
    }
}
