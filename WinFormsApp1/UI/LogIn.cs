using WinFormsApp1.BL;

namespace WinFormsApp1.UI
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            String username = textBox1.Text;
            String password = textBox2.Text;
            String logIn = UserService.logInToServer(username, password);
            if (logIn == "Admin")
            {
                Admin admin = new Admin();
                admin.Show();
            }else   
                if (logIn == "Employee")
            {
                Employee employee= new Employee();
                employee.Show(); 
            }
            else
            {
                label3.Text = logIn;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void LogIn_Load(object sender, EventArgs e)
        {

        }
    }
}
