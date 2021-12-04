using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FillPizzaShop.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserAccounts> Users { get; set; }
        public Role()
        {
            Users = new List<UserAccounts>();
        }
    }
}
