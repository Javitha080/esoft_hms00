using System;
using System.Windows.Forms;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Forms
{
    /// <summary>
    /// MainDashboard is the main interface after login.
    /// It provides navigation to all modules of the system.
    /// </summary>
    public partial class MainDashboard : Form
    {
        private User currentUser;

        public MainDashboard(User user)
        {
            InitializeComponent();
            currentUser = user;
        }

        /// <summary>
        /// Handles the form load event.
        /// Sets up the dashboard based on user role.
        /// </summary>
        private void MainDashboard_Load(object sender, EventArgs e)
        {
            // Display welcome message
            lblWelcome.Text = "Welcome, " + currentUser.FullName + " (" + currentUser.Role + ")";
            
            // Set up role-based access control
            ConfigureAccessByRole();
        }

        /// <summary>
        /// Configures menu access based on user role.
        /// </summary>
        private void ConfigureAccessByRole()
        {
            // Admin has access to all modules
            if (currentUser.Role == "Admin")
            {
                // All buttons enabled
                return;
            }
            
            // Receptionist has limited access
            if (currentUser.Role == "Receptionist")
            {
                // Disable user management for receptionist
                btnUserManagement.Enabled = false;
            }
            
            // Doctor has view-only access to some modules
            if (currentUser.Role == "Doctor")
            {
                // Doctors can only view appointments and patient records
                btnDoctorManagement.Enabled = false;
                btnUserManagement.Enabled = false;
            }
        }

        /// <summary>
        /// Opens the Patient Management form.
        /// </summary>
        private void btnPatientManagement_Click(object sender, EventArgs e)
        {
            PatientManagementForm patientForm = new PatientManagementForm();
            patientForm.ShowDialog();
        }

        /// <summary>
        /// Opens the Doctor Management form.
        /// </summary>
        private void btnDoctorManagement_Click(object sender, EventArgs e)
        {
            DoctorManagementForm doctorForm = new DoctorManagementForm();
            doctorForm.ShowDialog();
        }

        /// <summary>
        /// Opens the Appointment Management form.
        /// </summary>
        private void btnAppointmentManagement_Click(object sender, EventArgs e)
        {
            AppointmentManagementForm appointmentForm = new AppointmentManagementForm();
            appointmentForm.ShowDialog();
        }

        /// <summary>
        /// Opens the Billing Management form.
        /// </summary>
        private void btnBillingManagement_Click(object sender, EventArgs e)
        {
            BillingManagementForm billingForm = new BillingManagementForm();
            billingForm.ShowDialog();
        }

        /// <summary>
        /// Opens the User Management form.
        /// </summary>
        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            UserManagementForm userForm = new UserManagementForm();
            userForm.ShowDialog();
        }

        /// <summary>
        /// Handles the logout button click.
        /// Returns to login screen.
        /// </summary>
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                this.Close();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        /// <summary>
        /// Handles the form closing event.
        /// Ensures proper cleanup.
        /// </summary>
        private void MainDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If user closes the form without logging out, exit application
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}

