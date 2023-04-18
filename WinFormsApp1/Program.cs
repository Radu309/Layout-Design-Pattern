using WinFormsApp1.UI;

namespace WinFormsApp1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();

            Application.EnableVisualStyles(); 
            Application.SetCompatibleTextRenderingDefault(false);

            LogIn logIn = new LogIn();
            Application.Run(logIn);
        }
    }
}