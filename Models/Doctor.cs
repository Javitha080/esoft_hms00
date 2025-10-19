using System;

namespace HospitalManagementSystem.Models
{
    /// <summary>
    /// Doctor model class represents a doctor in the hospital.
    /// This class contains all properties related to doctor information.
    /// </summary>
    public class Doctor
    {
        // Primary key
        public int DoctorID { get; set; }

        // Personal information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string Qualification { get; set; }

        // Contact information
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        // Professional information
        public decimal ConsultationFee { get; set; }
        public string AvailableDays { get; set; }
        public TimeSpan AvailableTimeStart { get; set; }
        public TimeSpan AvailableTimeEnd { get; set; }

        // Status and tracking
        public DateTime JoiningDate { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets the full name of the doctor
        /// </summary>
        public string FullName
        {
            get { return "Dr. " + FirstName + " " + LastName; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Doctor()
        {
            JoiningDate = DateTime.Now;
            IsActive = true;
        }

        /// <summary>
        /// Parameterized constructor for creating a new doctor
        /// </summary>
        public Doctor(string firstName, string lastName, string specialization, string phoneNumber, decimal consultationFee)
        {
            FirstName = firstName;
            LastName = lastName;
            Specialization = specialization;
            PhoneNumber = phoneNumber;
            ConsultationFee = consultationFee;
            JoiningDate = DateTime.Now;
            IsActive = true;
        }
    }
}

