using SimpleHMS;
using System;
using System.Windows.Forms;

namespace HospitalManagementSystem.Forms
{
    // MainForm - This is the main menu of the application
    // It has 3 buttons to open different forms
    public partial class MainForm : Form
    {
        ToolTip tooltip; // Tooltip shows hints when you hover over controls

        // Constructor - runs when form is created
        public MainForm()
        {
            InitializeComponent(); // Create all the controls
        }

        // This method creates all the UI controls (buttons, labels, etc.)
        private void InitializeComponent()
        {
            // Set form properties
            Text = "Hospital Management System"; // Form title (shown in title bar)
            Size = new Size(400, 300); // Form size (width=400, height=300)
            StartPosition = FormStartPosition.CenterScreen; // Open in center of screen

            // Initialize ToolTip - shows helpful hints when hovering
            tooltip = new ToolTip();
            tooltip.AutoPopDelay = 5000; // How long tooltip stays visible (5 seconds)
            tooltip.InitialDelay = 500; // Delay before showing tooltip (0.5 seconds)

            // Title Label - Shows the system name
            Label lblTitle = new Label();
            lblTitle.Text = "Hospital Management System"; // Label text
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold); // Font style
            lblTitle.Location = new Point(50, 30); // Position (x=50, y=30)
            lblTitle.Size = new Size(300, 30); // Size (width=300, height=30)
            Controls.Add(lblTitle); // Add label to form

            // Patient Registration Button
            Button btnPatient = new Button();
            btnPatient.Text = "Patient Registration"; // Button text
            btnPatient.Location = new Point(100, 80); // Position (x=100, y=80)
            btnPatient.Size = new Size(200, 40); // Size (width=200, height=40)
            btnPatient.Click += (s, e) => { new PatientForm().ShowDialog(); }; // When clicked, open PatientForm
            tooltip.SetToolTip(btnPatient, "Click to manage patient records (Add, Update, Delete)"); // Tooltip
            Controls.Add(btnPatient); // Add button to form

            // Doctor Registration Button
            Button btnDoctor = new Button();
            btnDoctor.Text = "Doctor Registration"; // Button text
            btnDoctor.Location = new Point(100, 130); // Position (x=100, y=130)
            btnDoctor.Size = new Size(200, 40); // Size (width=200, height=40)
            btnDoctor.Click += (s, e) => { new DoctorForm().ShowDialog(); }; // When clicked, open DoctorForm
            tooltip.SetToolTip(btnDoctor, "Click to manage doctor records (Add, Update, Delete)"); // Tooltip
            Controls.Add(btnDoctor); // Add button to form

            // Appointment Button
            Button btnAppointment = new Button();
            btnAppointment.Text = "Appointments"; // Button text
            btnAppointment.Location = new Point(100, 180); // Position (x=100, y=180)
            btnAppointment.Size = new Size(200, 40); // Size (width=200, height=40)
            btnAppointment.Click += (s, e) => { new AppointmentForm().ShowDialog(); }; // When clicked, open AppointmentForm
            tooltip.SetToolTip(btnAppointment, "Click to manage appointments (Book, Update, Cancel)"); // Tooltip
            Controls.Add(btnAppointment); // Add button to form
        }
    }
}

