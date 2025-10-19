using System;
using System.Data;
using System.Data.SqlClient;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.DAL
{
    /// <summary>
    /// UserDAL (Data Access Layer) handles all database operations related to Users.
    /// This class provides methods for CRUD operations on the Users table.
    /// </summary>
    public class UserDAL
    {
        /// <summary>
        /// Authenticates a user by username and password.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>User object if authentication is successful, null otherwise</returns>
        public User AuthenticateUser(string username, string password)
        {
            try
            {
                string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password AND IsActive = 1";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", password)
                };

                DataTable dt = DatabaseConnection.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    User user = new User
                    {
                        UserID = Convert.ToInt32(row["UserID"]),
                        Username = row["Username"].ToString(),
                        Password = row["Password"].ToString(),
                        Role = row["Role"].ToString(),
                        FullName = row["FullName"].ToString(),
                        Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : "",
                        PhoneNumber = row["PhoneNumber"] != DBNull.Value ? row["PhoneNumber"].ToString() : "",
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                    };

                    // Update last login time
                    UpdateLastLogin(user.UserID);

                    return user;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the last login time for a user.
        /// </summary>
        /// <param name="userID">User ID</param>
        private void UpdateLastLogin(int userID)
        {
            try
            {
                string query = "UPDATE Users SET LastLogin = @LastLogin WHERE UserID = @UserID";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@LastLogin", DateTime.Now),
                    new SqlParameter("@UserID", userID)
                };

                DatabaseConnection.ExecuteNonQuery(query, parameters);
            }
            catch
            {
                // Silently fail - last login update is not critical
            }
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>True if successful, false otherwise</returns>
        public bool AddUser(User user)
        {
            try
            {
                string query = @"INSERT INTO Users (Username, Password, Role, FullName, Email, PhoneNumber, IsActive, CreatedDate)
                                VALUES (@Username, @Password, @Role, @FullName, @Email, @PhoneNumber, @IsActive, @CreatedDate)";

                SqlParameter[] parameters = {
                    new SqlParameter("@Username", user.Username),
                    new SqlParameter("@Password", user.Password),
                    new SqlParameter("@Role", user.Role),
                    new SqlParameter("@FullName", user.FullName),
                    new SqlParameter("@Email", (object)user.Email ?? DBNull.Value),
                    new SqlParameter("@PhoneNumber", (object)user.PhoneNumber ?? DBNull.Value),
                    new SqlParameter("@IsActive", user.IsActive),
                    new SqlParameter("@CreatedDate", user.CreatedDate)
                };

                int result = DatabaseConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding user: " + ex.Message);
            }
        }

        /// <summary>
        /// Gets all users from the database.
        /// </summary>
        /// <returns>DataTable containing all users</returns>
        public DataTable GetAllUsers()
        {
            try
            {
                string query = "SELECT UserID, Username, Role, FullName, Email, PhoneNumber, IsActive, CreatedDate FROM Users ORDER BY CreatedDate DESC";
                return DatabaseConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users: " + ex.Message);
            }
        }

        /// <summary>
        /// Checks if a username already exists in the database.
        /// </summary>
        /// <param name="username">Username to check</param>
        /// <returns>True if username exists, false otherwise</returns>
        public bool UsernameExists(string username)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@Username", username)
                };

                int count = Convert.ToInt32(DatabaseConnection.ExecuteScalar(query, parameters));
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking username: " + ex.Message);
            }
        }
    }
}

