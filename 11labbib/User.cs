using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using _11labbib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace _11labbib
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Fam { get; set; }
        public string Otch { get; set; }
        public int Kurs { get; set; }
        public int PRId { get; set; }



        public User()
        { }
        public User(string Login, string Password, string Name, string Fam, string Otch, int Kurs, int PRId)
        {
            this.Login = Login;
            this.Password = Password;
            this.Name = Name;
            this.Fam = Fam;
            this.Otch = Otch;
            this.Kurs = Kurs;
            this.PRId = PRId;
        }
    }
}
