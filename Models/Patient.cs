using System;

namespace HospitalManagementSystem.Models
{
    /// <summary>
    /// Patient model class represents a patient in the hospital.
    /// This class contains all properties related to patient information.
    /// </summary>
    public class Patient
    {
        // Primary key
        public int PatientID { get; set; }

        // Personal information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        // Contact information
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Medical information
        public string BloodGroup { get; set; }
        public string MedicalHistory { get; set; }

        // Status and tracking
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets the full name of the patient
        /// </summary>
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        /// <summary>
        /// Calculates and returns the age of the patient
        /// </summary>
        public int Age
        {
            get
            {
                int age = DateTime.Now.Year - DateOfBirth.Year;
                if (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear)
                    age--;
                return age;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Patient()
        {
            RegistrationDate = DateTime.Now;
            IsActive = true;
        }

        /// <summary>
        /// Parameterized constructor for creating a new patient
        /// </summary>
        public Patient(string firstName, string lastName, DateTime dateOfBirth, string gender, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            PhoneNumber = phoneNumber;
            RegistrationDate = DateTime.Now;
            IsActive = true;
        }
    }
}

