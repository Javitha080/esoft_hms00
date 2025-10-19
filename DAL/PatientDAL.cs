using System;
using System.Data;
using System.Data.SqlClient;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.DAL
{
    /// <summary>
    /// PatientDAL (Data Access Layer) handles all database operations related to Patients.
    /// This class provides methods for CRUD operations on the Patients table.
    /// </summary>
    public class PatientDAL
    {
        /// <summary>
        /// Adds a new patient to the database.
        /// </summary>
        /// <param name="patient">Patient object</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool AddPatient(Patient patient)
        {
            try
            {
                string query = @"INSERT INTO Patients (FirstName, LastName, DateOfBirth, Gender, PhoneNumber, Email, Address, BloodGroup, MedicalHistory, RegistrationDate, IsActive)
                                VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @PhoneNumber, @Email, @Address, @BloodGroup, @MedicalHistory, @RegistrationDate, @IsActive)";

                SqlParameter[] parameters = {
                    new SqlParameter("@FirstName", patient.FirstName),
                    new SqlParameter("@LastName", patient.LastName),
                    new SqlParameter("@DateOfBirth", patient.DateOfBirth),
                    new SqlParameter("@Gender", patient.Gender),
                    new SqlParameter("@PhoneNumber", patient.PhoneNumber),
                    new SqlParameter("@Email", (object)patient.Email ?? DBNull.Value),
                    new SqlParameter("@Address", (object)patient.Address ?? DBNull.Value),
                    new SqlParameter("@BloodGroup", (object)patient.BloodGroup ?? DBNull.Value),
                    new SqlParameter("@MedicalHistory", (object)patient.MedicalHistory ?? DBNull.Value),
                    new SqlParameter("@RegistrationDate", patient.RegistrationDate),
                    new SqlParameter("@IsActive", patient.IsActive)
                };

                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding patient: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing patient in the database.
        /// </summary>
        /// <param name="patient">Patient object with updated information</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool UpdatePatient(Patient patient)
        {
            try
            {
                string query = @"UPDATE Patients SET 
                                FirstName = @FirstName, 
                                LastName = @LastName, 
                                DateOfBirth = @DateOfBirth, 
                                Gender = @Gender, 
                                PhoneNumber = @PhoneNumber, 
                                Email = @Email, 
                                Address = @Address, 
                                BloodGroup = @BloodGroup, 
                                MedicalHistory = @MedicalHistory,
                                IsActive = @IsActive
                                WHERE PatientID = @PatientID";

                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", patient.PatientID),
                    new SqlParameter("@FirstName", patient.FirstName),
                    new SqlParameter("@LastName", patient.LastName),
                    new SqlParameter("@DateOfBirth", patient.DateOfBirth),
                    new SqlParameter("@Gender", patient.Gender),
                    new SqlParameter("@PhoneNumber", patient.PhoneNumber),
                    new SqlParameter("@Email", (object)patient.Email ?? DBNull.Value),
                    new SqlParameter("@Address", (object)patient.Address ?? DBNull.Value),
                    new SqlParameter("@BloodGroup", (object)patient.BloodGroup ?? DBNull.Value),
                    new SqlParameter("@MedicalHistory", (object)patient.MedicalHistory ?? DBNull.Value),
                    new SqlParameter("@IsActive", patient.IsActive)
                };

                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating patient: " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes a patient from the database (soft delete by setting IsActive to false).
        /// </summary>
        /// <param name="patientID">Patient ID</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool DeletePatient(int patientID)
        {
            try
            {
                string query = "UPDATE Patients SET IsActive = 0 WHERE PatientID = @PatientID";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", patientID)
                };

                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting patient: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets all active patients from the database.
        /// </summary>
        /// <returns>DataTable containing all active patients</returns>
        public DataTable GetAllPatients()
        {
            try
            {
                string query = @"SELECT PatientID, FirstName, LastName, 
                                CONVERT(VARCHAR(10), DateOfBirth, 103) AS DateOfBirth, 
                                Gender, PhoneNumber, Email, Address, BloodGroup, 
                                DATEDIFF(YEAR, DateOfBirth, GETDATE()) AS Age
                                FROM Patients 
                                WHERE IsActive = 1 
                                ORDER BY RegistrationDate DESC";
                
                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving patients: " + ex.Message);
            }
        }

        /// <summary>
        /// Searches for patients by name or phone number.
        /// </summary>
        /// <param name="searchText">Search text</param>
        /// <returns>DataTable containing matching patients</returns>
        public DataTable SearchPatients(string searchText)
        {
            try
            {
                string query = @"SELECT PatientID, FirstName, LastName, 
                                CONVERT(VARCHAR(10), DateOfBirth, 103) AS DateOfBirth, 
                                Gender, PhoneNumber, Email, Address, BloodGroup,
                                DATEDIFF(YEAR, DateOfBirth, GETDATE()) AS Age
                                FROM Patients 
                                WHERE IsActive = 1 
                                AND (FirstName LIKE @SearchText 
                                OR LastName LIKE @SearchText 
                                OR PhoneNumber LIKE @SearchText)
                                ORDER BY FirstName, LastName";

                SqlParameter[] parameters = {
                    new SqlParameter("@SearchText", "%" + searchText + "%")
                };

                return DatabaseConnection.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching patients: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a patient by ID.
        /// </summary>
        /// <param name="patientID">Patient ID</param>
        /// <returns>Patient object</returns>
        public Patient GetPatientByID(int patientID)
        {
            try
            {
                string query = "SELECT * FROM Patients WHERE PatientID = @PatientID";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@PatientID", patientID)
                };

                DataTable dt = DatabaseConnection.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new Patient
                    {
                        PatientID = Convert.ToInt32(row["PatientID"]),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]),
                        Gender = row["Gender"].ToString(),
                        PhoneNumber = row["PhoneNumber"].ToString(),
                        Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : "",
                        Address = row["Address"] != DBNull.Value ? row["Address"].ToString() : "",
                        BloodGroup = row["BloodGroup"] != DBNull.Value ? row["BloodGroup"].ToString() : "",
                        MedicalHistory = row["MedicalHistory"] != DBNull.Value ? row["MedicalHistory"].ToString() : "",
                        RegistrationDate = Convert.ToDateTime(row["RegistrationDate"]),
                        IsActive = Convert.ToBoolean(row["IsActive"])
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving patient: " + ex.Message);
            }
        }
    }
}

