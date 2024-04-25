using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11labbib
{
    public class UserContext : DbContext
	{
		public UserContext() : base("Dbb") { }
		public DbSet<Prep> Prep { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Rat> Rat { get; set; }



        static UserContext()
        {
/*            Database.SetInitializer<UserContext>(new DropCreateDatabaseAlways<UserContext>());
*/        }
    }

}
