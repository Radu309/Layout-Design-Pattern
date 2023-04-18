using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Entities
{
    internal class Order
    {
        private int id;
        private float totalPrice;
        private DateTime date;
        private int hour;
        private String status;
        private List<Food> listFood = new List<Food>();

        public Order(){}

        public Order(int id, float totalPrice, DateTime date, int hour, string status)
        {
            this.Id = id;
            this.TotalPrice = totalPrice;
            this.Date = DateTime.Now.Date;
            this.Hour = hour;
            this.Status = status;
        }
        public override string? ToString()
        {
            return "Id = " + Id +
                "; Total Price =" + TotalPrice +
                " lei; Date: " + Date.Day + "/" + Date.Month + "/" + Date.Year +
                "; Hour: " + Hour +
                "; Status = " + Status + ".";
        }

        public int Id { get => id; set => id = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Hour { get => hour; set => hour = value; }
        public string Status { get => status; set => status = value; }
        internal List<Food> ListFood { get => listFood; set => listFood = value; }
    }
}
