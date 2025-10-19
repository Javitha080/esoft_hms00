using System;

namespace HospitalManagementSystem.Models
{
    /// <summary>
    /// Appointment model class represents an appointment in the hospital.
    /// This class contains all properties related to appointment information.
    /// </summary>
    public class Appointment
    {
        // Primary key
        public int AppointmentID { get; set; }

        // Foreign keys
        public int PatientID { get; set; }
        public int DoctorID { get; set; }

        // Appointment details
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string Status { get; set; } // Scheduled, Completed, Cancelled
        public string Reason { get; set; }
        public string Notes { get; set; }

        // Tracking
        public DateTime CreatedDate { get; set; }

        // Additional properties for display purposes (not in database)
        public string PatientName { get; set; }
        public string DoctorName { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Appointment()
        {
            Status = "Scheduled";
            CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// Parameterized constructor for creating a new appointment
        /// </summary>
        public Appointment(int patientID, int doctorID, DateTime appointmentDate, TimeSpan appointmentTime)
        {
            PatientID = patientID;
            DoctorID = doctorID;
            AppointmentDate = appointmentDate;
            AppointmentTime = appointmentTime;
            Status = "Scheduled";
            CreatedDate = DateTime.Now;
        }
    }
}

