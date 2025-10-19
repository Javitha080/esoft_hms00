using System;

namespace HospitalManagementSystem.Models
{
    /// <summary>
    /// Bill model class represents a billing record in the hospital.
    /// This class contains all properties related to billing information.
    /// </summary>
    public class Bill
    {
        // Primary key
        public int BillID { get; set; }

        // Foreign keys
        public int PatientID { get; set; }
        public int? AppointmentID { get; set; } // Nullable because not all bills are linked to appointments

        // Bill details
        public DateTime BillDate { get; set; }
        public decimal ConsultationFee { get; set; }
        public decimal MedicineFee { get; set; }
        public decimal TestFee { get; set; }
        public decimal OtherCharges { get; set; }

        // Payment details
        public string PaymentStatus { get; set; } // Pending, Paid, Partial
        public string PaymentMethod { get; set; } // Cash, Card, Insurance, Online
        public decimal PaidAmount { get; set; }

        // Additional properties for display purposes (not in database)
        public string PatientName { get; set; }

        /// <summary>
        /// Calculates and returns the total amount of the bill
        /// </summary>
        public decimal TotalAmount
        {
            get { return ConsultationFee + MedicineFee + TestFee + OtherCharges; }
        }

        /// <summary>
        /// Calculates and returns the balance amount (remaining to be paid)
        /// </summary>
        public decimal BalanceAmount
        {
            get { return TotalAmount - PaidAmount; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Bill()
        {
            BillDate = DateTime.Now;
            PaymentStatus = "Pending";
            ConsultationFee = 0;
            MedicineFee = 0;
            TestFee = 0;
            OtherCharges = 0;
            PaidAmount = 0;
        }

        /// <summary>
        /// Parameterized constructor for creating a new bill
        /// </summary>
        public Bill(int patientID, decimal consultationFee)
        {
            PatientID = patientID;
            ConsultationFee = consultationFee;
            BillDate = DateTime.Now;
            PaymentStatus = "Pending";
            MedicineFee = 0;
            TestFee = 0;
            OtherCharges = 0;
            PaidAmount = 0;
        }
    }
}

