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
        public float Price { get; set; }
        public ProductType Type { get; set; }

    }
}
