using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Entities
{
    internal class Food
    {
        private String name;
        private float price;
        private int stock;

        public Food(string name, float price, int stock)
        {
            this.Name = name;
            this.Price = price;
            this.Stock = stock;
        }

        public string Name { get => name; set => name = value; }
        public float Price { get => price; set => price = value; }
        public int Stock { get => stock; set => stock = value; }

        public override string? ToString()
        {
            return Name + " ... " + Price;
        }
    }
}
