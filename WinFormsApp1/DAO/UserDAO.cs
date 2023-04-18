using System.Data.SqlClient;
using WinFormsApp1.Entities;

namespace WinFormsApp1.DAO
{
    internal class UserDAO
    {
        private String connectionString = 
            @"Data Source=DESKTOP-D2EKQK8\SQLEXPRESS;Initial Catalog=RestaurantApahida;Integrated Security=True";
        private SqlConnection conn = null;

        public UserDAO()
        {
            this.conn = new SqlConnection(connectionString);
        }
        public User getUser(string username, string password)
        {
            User u = null;
            string sql = "SELECT * FROM dbo.[User] WHERE username=@username AND password=@password";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    u = new User(
                        int.Parse(reader["id"].ToString()),
                        reader["name"].ToString(),
                        reader["role"].ToString(),
                        reader["username"].ToString(),
                        reader["password"].ToString()
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
            return u;
        }
        public void InsertNewUser(User u)
        {
            try
            {
                conn.Open();
                string sql = "INSERT INTO dbo.[User] (id, name, role, username, password) " +
                    "VALUES (@id, @name, @role, @username, @password)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", u.Id);
                cmd.Parameters.AddWithValue("@name", u.Name);
                cmd.Parameters.AddWithValue("@role", u.Role);
                cmd.Parameters.AddWithValue("@username", u.Username);
                cmd.Parameters.AddWithValue("@password", u.Password);
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
        public List<User> GetListUsers()
        {
            List<User> users = new List<User>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[User]", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string username = reader["username"].ToString();
                    string password = reader["password"].ToString();
                    string name = reader["name"].ToString();
                    string role = reader["role"].ToString();
                    User u = new User(id, name, role, username, password);
                    users.Add(u);
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

            return users;
        }

        public void DeleteUser(User u)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM dbo.[User] WHERE username = @username", conn);
                cmd.Parameters.AddWithValue("@username", u.Username);
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

        public User EditUser(User u)
        {
            return u;
        }
        public void Dispose()
        {
            if (conn != null)
            {
                conn.Dispose();
                conn = null;
            }
        }

    }
}
