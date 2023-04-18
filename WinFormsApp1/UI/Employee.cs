using WinFormsApp1.BL;
using WinFormsApp1.DAO;
using WinFormsApp1.Entities;

namespace WinFormsApp1.UI
{
    public partial class Employee : Form
    {
        FoodDAO foodDAO = new FoodDAO();
        OrderDAO orderDAO = new OrderDAO();
        public Employee()
        {
            InitializeComponent();
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            foreach (Food food in foodDAO.GetListFood())
            {   
                if(food.Stock > 0)
                    checkedListBox1.Items.Add(food);
            }
            foreach(Order order in orderDAO.GetListOrder())
                checkedListBox2.Items.Add(order);
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Add(checkedListBox1.SelectedItem);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox2.Refresh();
        }
        public void showCheckedListBox()
        {
            checkedListBox2.Items.Clear();
            checkedListBox2.Refresh();
            foreach (Order order in orderDAO.GetListOrder())
                checkedListBox2.Items.Add(order);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<Food> foodList = new List<Food>();
            foreach (Food food in listBox2.Items)
                foodList.Add(food);
            if(foodList.Count > 0)
            {
                OrderService.CreateOrder(foodList);
                button2_Click(sender, e);
                showCheckedListBox();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (checkedListBox2.SelectedItem != null)
            {
                Order order = (Order)checkedListBox2.SelectedItem;
                OrderService.SetNewStatus(order.Id, "Pending");
                showCheckedListBox();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (checkedListBox2.SelectedItem != null)
            {
                Order order = (Order)checkedListBox2.SelectedItem;
                OrderService.SetNewStatus(order.Id, "Finish");
                showCheckedListBox();
            }
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (Order order in orderDAO.GetListOrder())
            {
                OrderService.SetNewStatus(order.Id, "New Order");
                showCheckedListBox();
            }
        }
    }
}
