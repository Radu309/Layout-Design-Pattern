using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Entities;

namespace WinFormsApp1.DAO
{
    internal class OrderDAO
    {
        private String connectionString =
            @"Data Source=DESKTOP-D2EKQK8\SQLEXPRESS;Initial Catalog=RestaurantApahida;Integrated Security=True";
        private SqlConnection conn = null;

        public OrderDAO()
        {
            this.conn = new SqlConnection(connectionString);
        }
        public Order GetOrder(int id)
        {
            Order o = null;
            string sql = "SELECT o.id, o.totalPrice, o.date, o.hour, o.status, f.name, f.price, f.stock " +
                         "FROM dbo.[Order] o INNER JOIN dbo.[Order_Food] ofd ON o.id = ofd.OrderId " +
                         "INNER JOIN dbo.[Food] f ON ofd.FoodName = f.name " +
                         "WHERE o.id=@id";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                Dictionary<int, Order> orders = new Dictionary<int, Order>();
                while (reader.Read())
                {
                    int orderId = int.Parse(reader["id"].ToString());
                    if (!orders.ContainsKey(orderId))
                    {
                        float totalPrice = float.Parse(reader["totalPrice"].ToString());
                        DateTime dateTime = DateTime.Parse(reader["date"].ToString());
                        int hour = int.Parse(reader["hour"].ToString());
                        string status = reader["status"].ToString();
                        Order order = new Order(orderId, totalPrice, dateTime, hour, status);
                        orders[orderId] = order;
                    }

                    string foodName = reader["name"].ToString();
                    float foodPrice = float.Parse(reader["price"].ToString());
                    int foodStock = int.Parse(reader["stock"].ToString());
                    Food food = new Food(foodName, foodPrice, foodStock);
                    orders[orderId].ListFood.Add(food);
                }

                if (orders.ContainsKey(id))
                {
                    o = orders[id];
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

            return o;
        }
        public void InsertNewOrder(Order order)
        {
            try
            {
                conn.Open();
                string sql = "INSERT INTO dbo.[Order] (id, totalPrice, date, hour, status)" +
                    " VALUES (@id, @totalPrice, @date, @hour, @status); SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", order.Id);
                cmd.Parameters.AddWithValue("@totalPrice", order.TotalPrice);
                cmd.Parameters.AddWithValue("@date", order.Date);
                cmd.Parameters.AddWithValue("@hour", order.Hour);
                cmd.Parameters.AddWithValue("@status", order.Status);
                int rowsAffected = cmd.ExecuteNonQuery();
                foreach (Food food in order.ListFood)
                {
                    cmd = new SqlCommand("INSERT INTO dbo.[Order_Food] (OrderId, FoodName)" +
                        " VALUES (@OrderId, @FoodId)", conn);
                    cmd.Parameters.AddWithValue("@OrderId", order.Id);
                    cmd.Parameters.AddWithValue("@FoodId", food.Name);
                    cmd.ExecuteNonQuery();
                }
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
        public List<Order> GetListOrder()
        {
            List<Order> orders = new List<Order>();
            string sql = "SELECT o.id, o.totalPrice, o.date, o.hour, o.status, f.name, f.price, f.stock " +
                         "FROM dbo.[Order] o INNER JOIN dbo.[Order_Food] ofd ON o.id = ofd.OrderId " +
                         "INNER JOIN dbo.[Food] f ON ofd.FoodName = f.name";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                Dictionary<int, Order> orderDict = new Dictionary<int, Order>();
                while (reader.Read())
                {
                    int orderId = int.Parse(reader["id"].ToString());
                    if (!orderDict.ContainsKey(orderId))
                    {
                        float totalPrice = float.Parse(reader["totalPrice"].ToString());
                        DateTime dateTime = DateTime.Parse(reader["date"].ToString());
                        int hour = int.Parse(reader["hour"].ToString());
                        string status = reader["status"].ToString();
                        Order order = new Order(orderId, totalPrice, dateTime, hour, status);
                        orderDict[orderId] = order;
                    }

                    string foodName = reader["name"].ToString();
                    float foodPrice = float.Parse(reader["price"].ToString());
                    int foodStock = int.Parse(reader["stock"].ToString());
                    Food food = new Food(foodName, foodPrice, foodStock);
                    orderDict[orderId].ListFood.Add(food);
                }

                orders = orderDict.Values.ToList();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }

            return orders;
        }

        public void DeleteOrder(Order o)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM dbo.[Order] WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", o.Id);
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

        public void EditStatus(int orderId, string status)
        {
            try
            {
                conn.Open();

                string sql = "UPDATE dbo.[Order] SET status = @status WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", orderId);
                cmd.Parameters.AddWithValue("@status", status);
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
        public List<Order> GetListOrderBetweenTwoDates(string date1, string date2)
        {
            List<Order> orders = new List<Order>();
            //string sql = "SELECT * FROM dbo.Orders WHERE Date BETWEEN '" + date1 + "' AND '"+ date2 + "'";
            
            string sql = "SELECT o.id, o.totalPrice, o.date, o.hour, o.status, f.name, f.price, f.stock " +
                         "FROM dbo.[Order] o INNER JOIN dbo.[Order_Food] ofd ON o.id = ofd.OrderId " +
                         "INNER JOIN dbo.[Food] f ON ofd.FoodName = f.name " +
                         "WHERE o.date BETWEEN '" + date1 + "' AND '" + date2 + "'";
           
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                Dictionary<int, Order> orderDict = new Dictionary<int, Order>();
                while (reader.Read())
                {
                    int orderId = int.Parse(reader["id"].ToString());
                    if (!orderDict.ContainsKey(orderId))
                    {
                        float totalPrice = float.Parse(reader["totalPrice"].ToString());
                        DateTime dateTime = DateTime.Parse(reader["date"].ToString());
                        int hour = int.Parse(reader["hour"].ToString());
                        string status = reader["status"].ToString();
                        Order order = new Order(orderId, totalPrice, dateTime, hour, status);
                        orderDict[orderId] = order;
                    }

                    string foodName = reader["name"].ToString();
                    float foodPrice = float.Parse(reader["price"].ToString());
                    int foodStock = int.Parse(reader["stock"].ToString());
                    Food food = new Food(foodName, foodPrice, foodStock);
                    orderDict[orderId].ListFood.Add(food);
                }

                orders = orderDict.Values.ToList();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }

            return orders;
        }
    }
}
