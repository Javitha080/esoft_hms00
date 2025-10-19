using System;
using System.Data;
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.DAL
{
    /// <summary>
    /// DoctorDAL (Data Access Layer) handles all database operations related to Doctors.
    /// This class provides methods for CRUD operations on the Doctors table.
    /// </summary>
    public class DoctorDAL
    {
        /// <summary>
        /// Adds a new doctor to the database.
        /// </summary>
        /// <param name="doctor">Doctor object</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool AddDoctor(Doctor doctor)
        {
            try
            {
                string query = @"INSERT INTO Doctors (FirstName, LastName, Specialization, Qualification, PhoneNumber, Email, ConsultationFee, AvailableDays, AvailableTimeStart, AvailableTimeEnd, JoiningDate, IsActive)
                                VALUES (@FirstName, @LastName, @Specialization, @Qualification, @PhoneNumber, @Email, @ConsultationFee, @AvailableDays, @AvailableTimeStart, @AvailableTimeEnd, @JoiningDate, @IsActive)";

                SqlParameter[] parameters = {
                    new SqlParameter("@FirstName", doctor.FirstName),
                    new SqlParameter("@LastName", doctor.LastName),
                    new SqlParameter("@Specialization", doctor.Specialization),
                    new SqlParameter("@Qualification", (object)doctor.Qualification ?? DBNull.Value),
                    new SqlParameter("@PhoneNumber", doctor.PhoneNumber),
                    new SqlParameter("@Email", (object)doctor.Email ?? DBNull.Value),
                    new SqlParameter("@ConsultationFee", doctor.ConsultationFee),
                    new SqlParameter("@AvailableDays", (object)doctor.AvailableDays ?? DBNull.Value),
                    new SqlParameter("@AvailableTimeStart", doctor.AvailableTimeStart),
                    new SqlParameter("@AvailableTimeEnd", doctor.AvailableTimeEnd),
                    new SqlParameter("@JoiningDate", doctor.JoiningDate),
                    new SqlParameter("@IsActive", doctor.IsActive)
                };

                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding doctor: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing doctor in the database.
        /// </summary>
        /// <param name="doctor">Doctor object with updated information</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool UpdateDoctor(Doctor doctor)
        {
            try
            {
                string query = @"UPDATE Doctors SET 
                                FirstName = @FirstName, 
                                LastName = @LastName, 
                                Specialization = @Specialization, 
                                Qualification = @Qualification, 
                                PhoneNumber = @PhoneNumber, 
                                Email = @Email, 
                                ConsultationFee = @ConsultationFee, 
                                AvailableDays = @AvailableDays, 
                                AvailableTimeStart = @AvailableTimeStart, 
                                AvailableTimeEnd = @AvailableTimeEnd,
                                IsActive = @IsActive
                                WHERE DoctorID = @DoctorID";

                SqlParameter[] parameters = {
                    new SqlParameter("@DoctorID", doctor.DoctorID),
                    new SqlParameter("@FirstName", doctor.FirstName),
                    new SqlParameter("@LastName", doctor.LastName),
                    new SqlParameter("@Specialization", doctor.Specialization),
                    new SqlParameter("@Qualification", (object)doctor.Qualification ?? DBNull.Value),
                    new SqlParameter("@PhoneNumber", doctor.PhoneNumber),
                    new SqlParameter("@Email", (object)doctor.Email ?? DBNull.Value),
                    new SqlParameter("@ConsultationFee", doctor.ConsultationFee),
                    new SqlParameter("@AvailableDays", (object)doctor.AvailableDays ?? DBNull.Value),
                    new SqlParameter("@AvailableTimeStart", doctor.AvailableTimeStart),
                    new SqlParameter("@AvailableTimeEnd", doctor.AvailableTimeEnd),
                    new SqlParameter("@IsActive", doctor.IsActive)
                };

                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating doctor: " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes a doctor from the database (soft delete by setting IsActive to false).
        /// </summary>
        /// <param name="doctorID">Doctor ID</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool DeleteDoctor(int doctorID)
        {
            try
            {
                string query = "UPDATE Doctors SET IsActive = 0 WHERE DoctorID = @DoctorID";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@DoctorID", doctorID)
                };

                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting doctor: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets all active doctors from the database.
        /// </summary>
        /// <returns>DataTable containing all active doctors</returns>
        public DataTable GetAllDoctors()
        {
            try
            {
                string query = @"SELECT DoctorID, FirstName, LastName, Specialization, Qualification, 
                                PhoneNumber, Email, ConsultationFee, AvailableDays
                                FROM Doctors 
                                WHERE IsActive = 1 
                                ORDER BY FirstName, LastName";
                
                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving doctors: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a doctor by ID.
        /// </summary>
        /// <param name="doctorID">Doctor ID</param>
        /// <returns>Doctor object</returns>
        public Doctor GetDoctorByID(int doctorID)
        {
            try
            {
                string query = "SELECT * FROM Doctors WHERE DoctorID = @DoctorID";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@DoctorID", doctorID)
                };

                DataTable dt = DatabaseConnection.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new Doctor
                    {
                        DoctorID = Convert.ToInt32(row["DoctorID"]),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        Specialization = row["Specialization"].ToString(),
                        Qualification = row["Qualification"] != DBNull.Value ? row["Qualification"].ToString() : "",
                        PhoneNumber = row["PhoneNumber"].ToString(),
                        Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : "",
                        ConsultationFee = Convert.ToDecimal(row["ConsultationFee"]),
                        AvailableDays = row["AvailableDays"] != DBNull.Value ? row["AvailableDays"].ToString() : "",
                        AvailableTimeStart = row["AvailableTimeStart"] != DBNull.Value ? (TimeSpan)row["AvailableTimeStart"] : TimeSpan.Zero,
                        AvailableTimeEnd = row["AvailableTimeEnd"] != DBNull.Value ? (TimeSpan)row["AvailableTimeEnd"] : TimeSpan.Zero,
                        JoiningDate = Convert.ToDateTime(row["JoiningDate"]),
                        IsActive = Convert.ToBoolean(row["IsActive"])
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving doctor: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets doctors by specialization.
        /// </summary>
        /// <param name="specialization">Specialization</param>
        /// <returns>DataTable containing doctors with the specified specialization</returns>
        public DataTable GetDoctorsBySpecialization(string specialization)
        {
            try
            {
                string query = @"SELECT DoctorID, FirstName, LastName, Specialization, ConsultationFee 
                                FROM Doctors 
                                WHERE IsActive = 1 AND Specialization = @Specialization
                                ORDER BY FirstName, LastName";

                SqlParameter[] parameters = {
                    new SqlParameter("@Specialization", specialization)
                };

                return DatabaseConnection.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving doctors by specialization: " + ex.Message);
            }
        }
    }
}

