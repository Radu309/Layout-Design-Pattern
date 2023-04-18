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
using WinFormsApp1.DAO;
using WinFormsApp1.Entities;

namespace WinFormsApp1.UI
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserService.CreateNewEmployee(textBox1.Text, textBox2.Text, textBox3.Text);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            MenuService.AddNewFood(dishName.Text, dishPrice.Text, dishStock.Text);
            dishName.Text = "";
            dishPrice .Text = "";
            dishStock.Text = "";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            MenuService.DeleteFoodFromMenu(dishName.Text);
            dishName.Text = "";
            dishPrice.Text = "";
            dishStock.Text = "";
        }
        private void editFoodFromMenu_Click(object sender, EventArgs e)
        {
            MenuService.EditFoodFromMenu(dishName.Text, dishPrice.Text, dishStock.Text);
            dishName.Text = "";
            dishPrice.Text = "";
            dishStock.Text = "";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string firstDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string lastDay = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            ReportOrders report = new ReportOrders(firstDate, lastDay);
            report.Show();
        }
        private void button6_Click(object sender, EventArgs e)
        {

        }
        private void Admin_Load(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dishName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dishPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void dishStock_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
