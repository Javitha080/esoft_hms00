using System;

namespace HospitalManagementSystem.Models
{
    /// <summary>
    /// User model class represents a user in the system.
    /// This class contains all properties related to user authentication and information.
    /// </summary>
    public class User
    {
        // Primary key
        public int UserID { get; set; }

        // Authentication credentials
        public string Username { get; set; }
        public string Password { get; set; }

        // User role (Admin, Receptionist, Doctor)
        public string Role { get; set; }

        // Personal information
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Status and tracking
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public User()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
        }

        /// <summary>
        /// Parameterized constructor for creating a new user
        /// </summary>
        public User(string username, string password, string role, string fullName)
        {
            Username = username;
            Password = password;
            Role = role;
            FullName = fullName;
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
    }
}

