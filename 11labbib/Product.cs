using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11labbib
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PRId { get; set; } 




        public Product()
        { }

        public Product(string Name, int PRId)
        {
            this.Name = Name;
            this.PRId = PRId;

        }
    }
}
