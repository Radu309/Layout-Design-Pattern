using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.DAO;
using WinFormsApp1.Entities;

namespace WinFormsApp1.BL
{
    /*
    * add a dish            -check 1
    * edit a dish           -check 3
    * delete a dish         -check 2
    */
    internal class MenuService
    {
        public static void AddNewFood(string dishName, string dishPrice, string dishStock)
        {
            FoodDAO foodDAO = new FoodDAO();
            if (foodDAO.GetFood(dishName) == null)
            {
                Food newFood = new Food(dishName, float.Parse(dishPrice), int.Parse(dishStock));
                foodDAO.InsertNewFood(newFood);
            }
            else
                EditFoodFromMenu(dishName, dishPrice, dishStock);
        }

        public static void DeleteFoodFromMenu(string dishName)
        {
            FoodDAO foodDAO = new FoodDAO();
            foodDAO.DeleteFood(dishName);
        }
        
        public static void EditFoodFromMenu(string dishName, string newPrice, string newStock)
        {
            FoodDAO foodDAO = new FoodDAO();
            List<Food> foods = foodDAO.GetListFood();
            foreach(Food food in foods)
            {
                if(food.Name == dishName)
                {
                    food.Price = float.Parse(newPrice);
                    food.Stock = int.Parse(newStock);
                    Food newFood = food;
                    foodDAO.EditFood(newFood);
                }
            }
        }
    }
}
