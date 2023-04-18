using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.DAO;
using WinFormsApp1.Entities;

namespace WinFormsApp1.BL
{
    internal class OrderService
    {
        public static void CreateOrder(List<Food> foods)
        {
            OrderDAO orderDAO = new OrderDAO();
            int newId = orderDAO.GetListOrder().Count + 1;
            Order order = new Order() 
                {Id = newId, 
                TotalPrice = 0, 
                Date = DateTime.Now.Date, 
                Hour = DateTime.Now.Hour, 
                Status = "New Order"
            };

            foreach (Food food in foods)
            {
                if (food.Stock > 0)
                {
                    order.ListFood.Add(food);
                    order.TotalPrice += food.Price;
                    MenuService.EditFoodFromMenu(food.Name, food.Price.ToString(), (food.Stock - 1).ToString());
                }
            }
            orderDAO.InsertNewOrder(order);
        }

        public static List<Order> GetOrderList()
        {
            OrderDAO orderDAO = new OrderDAO();
            List<Order> orderList = orderDAO.GetListOrder();
            return orderList;
        }

        public static void SetNewStatus(int orderID, string newStatus)
        {
            OrderDAO orderDAO = new OrderDAO();
            orderDAO.EditStatus(orderID, newStatus);
        }

        public static List<Order> CreateReport(string firstDate, string lastDate)
        {
            //"2023-04-18", "2023-04-19"
            OrderDAO orderDAO = new OrderDAO();
            return orderDAO.GetListOrderBetweenTwoDates(firstDate, lastDate);
        }
    }
}
