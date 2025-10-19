using System;
using System.Windows.Forms;
using HospitalManagementSystem.Forms;

namespace HospitalManagementSystem
{
    /// <summary>
    /// Program class contains the main entry point of the application.
    /// This is where the application starts execution.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// This method is called when the application starts.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Enable visual styles for modern UI appearance
            Application.EnableVisualStyles();
            
            // Set text rendering to use GDI+ for better quality
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Run the application with LoginForm as the startup form
            Application.Run(new LoginForm());
        }
    }
}

