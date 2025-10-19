using System;
using System.Windows.Forms;
using HospitalManagementSystem.DAL;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Forms
{
    /// <summary>
    /// LoginForm handles user authentication.
    /// This is the entry point of the application.
    /// </summary>
    public partial class LoginForm : Form
    {
        private UserDAL userDAL;

        public LoginForm()
        {
            InitializeComponent();
            userDAL = new UserDAL();
        }

        /// <summary>
        /// Handles the login button click event.
        /// Authenticates the user and opens the main dashboard.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Please enter username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please enter password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                // Authenticate user
                User user = userDAL.AuthenticateUser(txtUsername.Text.Trim(), txtPassword.Text);

                if (user != null)
                {
                    MessageBox.Show("Login successful! Welcome " + user.FullName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Open main dashboard and pass user information
                    MainDashboard dashboard = new MainDashboard(user);
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during login: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the exit button click event.
        /// Closes the application.
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Handles the form load event.
        /// Tests database connection on startup.
        /// </summary>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!DatabaseConnection.TestConnection())
                {
                    MessageBox.Show("Unable to connect to database. Please check your connection settings.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Enter key press in password textbox.
        /// Triggers login when Enter is pressed.
        /// </summary>
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}

