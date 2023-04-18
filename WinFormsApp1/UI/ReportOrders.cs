using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.BL;
using WinFormsApp1.Entities;

namespace WinFormsApp1.UI
{
    public partial class ReportOrders : Form
    {
        private string firstDate;
        private string lastDate;
        public ReportOrders(string firstDate, string lastDate)
        {
            InitializeComponent();
            this.firstDate = firstDate; 
            this.lastDate = lastDate;
        }
        private void ReportOrders_Load(object sender, EventArgs e)
        {
            List<Order> orders = new List<Order>();
            orders = OrderService.CreateReport(firstDate, lastDate);
            foreach (Order order in orders)
                listBox1.Items.Add(order);

            Dictionary<string, int> data = new Dictionary<string, int>();
            foreach(Order order in orders)
            {
                foreach(Food food in order.ListFood)
                {
                    if(data.ContainsKey(food.Name))
                    {
                        data[food.Name]++;
                    }
                    else
                    {
                        data.Add(food.Name, 1);
                    }
                }
            }
            Dictionary<string, int> finalData = data.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            
            foreach(KeyValuePair<string, int> pair in finalData)
            {
                listBox2.Items.Add(pair.Key + ", " + pair.Value);
            }
        }
         private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            saveFileDialog.FileName = "All Orders.csv";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    writer.WriteLine("Id,TotalPrice,Date,Hour,Status");
                    foreach (Order order in listBox1.Items)
                    {
                        StringBuilder stb = new StringBuilder();
                        
                        stb.Append(order.Id + ",");
                        stb.Append(order.TotalPrice + ",");
                        stb.Append(order.Date + ",");
                        stb.Append(order.Hour + ",");
                        stb.Append(order.Status);
                        writer.WriteLine(stb.ToString());
                    }
                }
                MessageBox.Show("CSV file saved successfully.");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            saveFileDialog.FileName = "Top Dishes.csv";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Create a StreamWriter to write the data to the file
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    // Loop through the rows in the DataGridView control and write each row to the file as a comma-separated list of values
                    foreach (string key in listBox2.Items)
                    {
                        writer.WriteLine(key);
                    }
                }
                MessageBox.Show("CSV file saved successfully.");
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
