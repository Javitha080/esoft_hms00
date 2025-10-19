using System;
using System.Data;
using System.Data.SqlClient;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.DAL
{
    /// <summary>
    /// BillDAL (Data Access Layer) handles all database operations related to Bills.
    /// This class provides methods for CRUD operations on the Bills table.
    /// </summary>
    public class BillDAL
    {
        /// <summary>
        /// Adds a new bill to the database.
        /// </summary>
        /// <param name="bill">Bill object</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool AddBill(Bill bill)
        {
            try
            {
                string query = @"INSERT INTO Bills (PatientID, AppointmentID, BillDate, ConsultationFee, MedicineFee, TestFee, OtherCharges, PaymentStatus, PaymentMethod, PaidAmount)
                                VALUES (@PatientID, @AppointmentID, @BillDate, @ConsultationFee, @MedicineFee, @TestFee, @OtherCharges, @PaymentStatus, @PaymentMethod, @PaidAmount)";

                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", bill.PatientID),
                    new SqlParameter("@AppointmentID", (object)bill.AppointmentID ?? DBNull.Value),
                    new SqlParameter("@BillDate", bill.BillDate),
                    new SqlParameter("@ConsultationFee", bill.ConsultationFee),
                    new SqlParameter("@MedicineFee", bill.MedicineFee),
                    new SqlParameter("@TestFee", bill.TestFee),
                    new SqlParameter("@OtherCharges", bill.OtherCharges),
                    new SqlParameter("@PaymentStatus", bill.PaymentStatus),
                    new SqlParameter("@PaymentMethod", (object)bill.PaymentMethod ?? DBNull.Value),
                    new SqlParameter("@PaidAmount", bill.PaidAmount)
                };

                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding bill: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing bill in the database.
        /// </summary>
        /// <param name="bill">Bill object with updated information</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool UpdateBill(Bill bill)
        {
            try
            {
                string query = @"UPDATE Bills SET 
                                ConsultationFee = @ConsultationFee, 
                                MedicineFee = @MedicineFee, 
                                TestFee = @TestFee, 
                                OtherCharges = @OtherCharges, 
                                PaymentStatus = @PaymentStatus, 
                                PaymentMethod = @PaymentMethod, 
                                PaidAmount = @PaidAmount
                                WHERE BillID = @BillID";

                SqlParameter[] parameters = {
                    new SqlParameter("@BillID", bill.BillID),
                    new SqlParameter("@ConsultationFee", bill.ConsultationFee),
                    new SqlParameter("@MedicineFee", bill.MedicineFee),
                    new SqlParameter("@TestFee", bill.TestFee),
                    new SqlParameter("@OtherCharges", bill.OtherCharges),
                    new SqlParameter("@PaymentStatus", bill.PaymentStatus),
                    new SqlParameter("@PaymentMethod", (object)bill.PaymentMethod ?? DBNull.Value),
                    new SqlParameter("@PaidAmount", bill.PaidAmount)
                };

                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating bill: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets all bills from the database.
        /// </summary>
        /// <returns>DataTable containing all bills</returns>
        public DataTable GetAllBills()
        {
            try
            {
                string query = @"SELECT 
                                b.BillID,
                                b.BillDate,
                                p.FirstName + ' ' + p.LastName AS PatientName,
                                b.ConsultationFee,
                                b.MedicineFee,
                                b.TestFee,
                                b.OtherCharges,
                                b.TotalAmount,
                                b.PaidAmount,
                                b.TotalAmount - b.PaidAmount AS BalanceAmount,
                                b.PaymentStatus,
                                b.PaymentMethod
                                FROM Bills b
                                INNER JOIN Patients p ON b.PatientID = p.PatientID
                                ORDER BY b.BillDate DESC";
                
                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving bills: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets bills by patient ID.
        /// </summary>
        /// <param name="patientID">Patient ID</param>
        /// <returns>DataTable containing bills for the specified patient</returns>
        public DataTable GetBillsByPatient(int patientID)
        {
            try
            {
                string query = @"SELECT 
                                BillID,
                                BillDate,
                                ConsultationFee,
                                MedicineFee,
                                TestFee,
                                OtherCharges,
                                TotalAmount,
                                PaidAmount,
                                TotalAmount - PaidAmount AS BalanceAmount,
                                PaymentStatus,
                                PaymentMethod
                                FROM Bills
                                WHERE PatientID = @PatientID
                                ORDER BY BillDate DESC";

                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", patientID)
                };

                return DatabaseConnection.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving bills by patient: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a bill by ID.
        /// </summary>
        /// <param name="billID">Bill ID</param>
        /// <returns>Bill object</returns>
        public Bill GetBillByID(int billID)
        {
            try
            {
                string query = "SELECT * FROM Bills WHERE BillID = @BillID";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@BillID", billID)
                };

                DataTable dt = DatabaseConnection.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new Bill
                    {
                        BillID = Convert.ToInt32(row["BillID"]),
                        PatientID = Convert.ToInt32(row["PatientID"]),
                        AppointmentID = row["AppointmentID"] != DBNull.Value ? (int?)Convert.ToInt32(row["AppointmentID"]) : null,
                        BillDate = Convert.ToDateTime(row["BillDate"]),
                        ConsultationFee = Convert.ToDecimal(row["ConsultationFee"]),
                        MedicineFee = Convert.ToDecimal(row["MedicineFee"]),
                        TestFee = Convert.ToDecimal(row["TestFee"]),
                        OtherCharges = Convert.ToDecimal(row["OtherCharges"]),
                        PaymentStatus = row["PaymentStatus"].ToString(),
                        PaymentMethod = row["PaymentMethod"] != DBNull.Value ? row["PaymentMethod"].ToString() : "",
                        PaidAmount = Convert.ToDecimal(row["PaidAmount"])
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving bill: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets pending bills (bills with outstanding balance).
        /// </summary>
        /// <returns>DataTable containing pending bills</returns>
        public DataTable GetPendingBills()
        {
            try
            {
                string query = @"SELECT 
                                b.BillID,
                                b.BillDate,
                                p.FirstName + ' ' + p.LastName AS PatientName,
                                b.TotalAmount,
                                b.PaidAmount,
                                b.TotalAmount - b.PaidAmount AS BalanceAmount,
                                b.PaymentStatus
                                FROM Bills b
                                INNER JOIN Patients p ON b.PatientID = p.PatientID
                                WHERE b.PaymentStatus IN ('Pending', 'Partial')
                                ORDER BY b.BillDate DESC";
                
                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving pending bills: " + ex.Message);
            }
        }
    }
}

