using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Entities;

namespace WinFormsApp1.DAO
{
    internal class FoodDAO
    {
        private String connectionString =
            @"Data Source=DESKTOP-D2EKQK8\SQLEXPRESS;Initial Catalog=RestaurantApahida;Integrated Security=True";
        private SqlConnection conn = null;

        public FoodDAO()
        {
            this.conn = new SqlConnection(connectionString);
        }
        public Food GetFood(string name)
        {
            Food f = null;
            string sql = "SELECT * FROM dbo.[Food] WHERE name=@name";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    f = new Food(
                        reader["name"].ToString(),
                        float.Parse(reader["price"].ToString()),
                        int.Parse(reader["stock"].ToString())
                    );
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
            return f;
        }
        public void InsertNewFood(Food f)
        {
            try
            {
                conn.Open();
                string sql = "INSERT INTO dbo.[Food] (name, price, stock) " +
                    "VALUES (@name, @price, @stock)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", f.Name);
                cmd.Parameters.AddWithValue("@price", f.Price);
                cmd.Parameters.AddWithValue("@stock", f.Stock);
                int rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public List<Food> GetListFood()
        {
            List<Food> foods = new List<Food>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Food]", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["name"].ToString();
                    float price = float.Parse(reader["price"].ToString());
                    int stock = int.Parse(reader["stock"].ToString());
                    Food f = new Food(name, price, stock);
                    foods.Add(f);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                conn.Close();
            }

            return foods;
        }

        public void DeleteFood(string dishName)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM dbo.[Food] WHERE name = @dishName", conn);
                cmd.Parameters.AddWithValue("@dishName", dishName);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException exc)
            {
                throw exc;
            }
            finally
            {
                conn.Close();
            }
        }

        public void EditFood(Food f)
        {
            try
            {
                conn.Open();
                string sql = "UPDATE dbo.[Food] SET price = @price, stock = @stock WHERE name = @name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", f.Name);
                cmd.Parameters.AddWithValue("@price", f.Price);
                cmd.Parameters.AddWithValue("@stock", f.Stock);
                int rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
