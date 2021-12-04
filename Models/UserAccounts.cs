using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FillPizzaShop.Models
{
    public enum UserType
    {
        regular,
        golden
    }
    public class UserAccounts
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
