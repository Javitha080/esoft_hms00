using System;
using System.Data;
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.DAL
{
    /// <summary>
    /// AppointmentDAL (Data Access Layer) handles all database operations related to Appointments.
    /// This class provides methods for CRUD operations on the Appointments table.
    /// </summary>
    public class AppointmentDAL
    {
        /// <summary>
        /// Adds a new appointment to the database.
        /// </summary>
        /// <param name="appointment">Appointment object</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool AddAppointment(Appointment appointment)
        {
            try
            {
                string query = @"INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, AppointmentTime, Status, Reason, Notes, CreatedDate)
                                VALUES (@PatientID, @DoctorID, @AppointmentDate, @AppointmentTime, @Status, @Reason, @Notes, @CreatedDate)";

                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", appointment.PatientID),
                    new SqlParameter("@DoctorID", appointment.DoctorID),
                    new SqlParameter("@AppointmentDate", appointment.AppointmentDate),
                    new SqlParameter("@AppointmentTime", appointment.AppointmentTime),
                    new SqlParameter("@Status", appointment.Status),
                    new SqlParameter("@Reason", (object)appointment.Reason ?? DBNull.Value),
                    new SqlParameter("@Notes", (object)appointment.Notes ?? DBNull.Value),
                    new SqlParameter("@CreatedDate", appointment.CreatedDate)
                };

                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding appointment: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing appointment in the database.
        /// </summary>
        /// <param name="appointment">Appointment object with updated information</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool UpdateAppointment(Appointment appointment)
        {
            try
            {
                string query = @"UPDATE Appointments SET 
                                PatientID = @PatientID, 
                                DoctorID = @DoctorID, 
                                AppointmentDate = @AppointmentDate, 
                                AppointmentTime = @AppointmentTime, 
                                Status = @Status, 
                                Reason = @Reason, 
                                Notes = @Notes
                                WHERE AppointmentID = @AppointmentID";

                SqlParameter[] parameters = {
                    new SqlParameter("@AppointmentID", appointment.AppointmentID),
                    new SqlParameter("@PatientID", appointment.PatientID),
                    new SqlParameter("@DoctorID", appointment.DoctorID),
                    new SqlParameter("@AppointmentDate", appointment.AppointmentDate),
                    new SqlParameter("@AppointmentTime", appointment.AppointmentTime),
                    new SqlParameter("@Status", appointment.Status),
                    new SqlParameter("@Reason", (object)appointment.Reason ?? DBNull.Value),
                    new SqlParameter("@Notes", (object)appointment.Notes ?? DBNull.Value)
                };

                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating appointment: " + ex.Message);
            }
        }

        /// <summary>
        /// Cancels an appointment by updating its status to 'Cancelled'.
        /// </summary>
        /// <param name="appointmentID">Appointment ID</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool CancelAppointment(int appointmentID)
        {
            try
            {
                string query = "UPDATE Appointments SET Status = 'Cancelled' WHERE AppointmentID = @AppointmentID";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@AppointmentID", appointmentID)
                };

                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error cancelling appointment: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets all appointments from the database.
        /// </summary>
        /// <returns>DataTable containing all appointments</returns>
        public DataTable GetAllAppointments()
        {
            try
            {
                string query = @"SELECT 
                                a.AppointmentID,
                                a.AppointmentDate,
                                CONVERT(VARCHAR(5), a.AppointmentTime, 108) AS AppointmentTime,
                                p.FirstName + ' ' + p.LastName AS PatientName,
                                d.FirstName + ' ' + d.LastName AS DoctorName,
                                d.Specialization,
                                a.Status,
                                a.Reason
                                FROM Appointments a
                                INNER JOIN Patients p ON a.PatientID = p.PatientID
                                INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
                                ORDER BY a.AppointmentDate DESC, a.AppointmentTime DESC";
                
                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving appointments: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets appointments by date.
        /// </summary>
        /// <param name="date">Appointment date</param>
        /// <returns>DataTable containing appointments for the specified date</returns>
        public DataTable GetAppointmentsByDate(DateTime date)
        {
            try
            {
                string query = @"SELECT 
                                a.AppointmentID,
                                CONVERT(VARCHAR(5), a.AppointmentTime, 108) AS AppointmentTime,
                                p.FirstName + ' ' + p.LastName AS PatientName,
                                d.FirstName + ' ' + d.LastName AS DoctorName,
                                d.Specialization,
                                a.Status,
                                a.Reason
                                FROM Appointments a
                                INNER JOIN Patients p ON a.PatientID = p.PatientID
                                INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
                                WHERE a.AppointmentDate = @AppointmentDate
                                ORDER BY a.AppointmentTime";

                SqlParameter[] parameters = {
                    new SqlParameter("@AppointmentDate", date.Date)
                };

                return DatabaseConnection.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving appointments by date: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets appointments by patient ID.
        /// </summary>
        /// <param name="patientID">Patient ID</param>
        /// <returns>DataTable containing appointments for the specified patient</returns>
        public DataTable GetAppointmentsByPatient(int patientID)
        {
            try
            {
                string query = @"SELECT 
                                a.AppointmentID,
                                a.AppointmentDate,
                                CONVERT(VARCHAR(5), a.AppointmentTime, 108) AS AppointmentTime,
                                d.FirstName + ' ' + d.LastName AS DoctorName,
                                d.Specialization,
                                a.Status,
                                a.Reason
                                FROM Appointments a
                                INNER JOIN Doctors d ON a.DoctorID = d.DoctorID
                                WHERE a.PatientID = @PatientID
                                ORDER BY a.AppointmentDate DESC, a.AppointmentTime DESC";

                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", patientID)
                };

                return DatabaseConnection.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving appointments by patient: " + ex.Message);
            }
        }

        /// <summary>
        /// Checks if a doctor is available at a specific date and time.
        /// </summary>
        /// <param name="doctorID">Doctor ID</param>
        /// <param name="appointmentDate">Appointment date</param>
        /// <param name="appointmentTime">Appointment time</param>
        /// <returns>True if available, false otherwise</returns>
        public bool IsDoctorAvailable(int doctorID, DateTime appointmentDate, TimeSpan appointmentTime)
        {
            try
            {
                string query = @"SELECT COUNT(*) FROM Appointments 
                                WHERE DoctorID = @DoctorID 
                                AND AppointmentDate = @AppointmentDate 
                                AND AppointmentTime = @AppointmentTime 
                                AND Status != 'Cancelled'";

                SqlParameter[] parameters = {
                    new SqlParameter("@DoctorID", doctorID),
                    new SqlParameter("@AppointmentDate", appointmentDate.Date),
                    new SqlParameter("@AppointmentTime", appointmentTime)
                };

                int count = Convert.ToInt32(DatabaseConnection.ExecuteScalar(query, parameters));
                return count == 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking doctor availability: " + ex.Message);
            }
        }
    }
}

