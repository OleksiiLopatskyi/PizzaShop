using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FillPizzaShop.Models
{
    public enum ProductType
    {
        Pizza,
        Salat,
        Drink
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public ProductType Type { get; set; }
        public bool Salt { get; set; }
        public bool Cheese { get; set; }

    }
}
