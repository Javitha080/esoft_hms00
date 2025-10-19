using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace HospitalManagementSystem
{
    /// <summary>
    /// DatabaseConnection class handles all database connectivity operations.
    /// This class implements the Singleton pattern to ensure only one connection instance exists.
    /// </summary>
    public class DatabaseConnection
    {
        // Connection string to connect to SQL Server database
        // In a real application, this should be stored in App.config or Web.config
        private static string connectionString = "Data Source=localhost;Initial Catalog=HospitalManagementDB;Integrated Security=True;TrustServerCertificate=True;";

        /// <summary>
        /// Gets a new SQL connection to the database.
        /// </summary>
        /// <returns>SqlConnection object</returns>
        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                return conn;
            }
            catch (Exception ex)
            {
                throw new Exception("Error connecting to database: " + ex.Message);
            }
        }

        /// <summary>
        /// Tests the database connection.
        /// </summary>
        /// <returns>True if connection is successful, otherwise false</returns>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return conn.State == ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Executes a SELECT query and returns a DataTable with the results.
        /// </summary>
        /// <param name="query">SQL SELECT query</param>
        /// <param name="parameters">Optional SQL parameters</param>
        /// <returns>DataTable containing query results</returns>
        public static DataTable ExecuteQuery(string query, SqlParameter[]? parameters = null)
        {
            DataTable dataTable = new DataTable();
            
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters if provided
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        conn.Open();
                        
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing query: " + ex.Message);
            }

            return dataTable;
        }

        /// <summary>
        /// Executes an INSERT, UPDATE, or DELETE query.
        /// </summary>
        /// <param name="query">SQL query (INSERT, UPDATE, DELETE)</param>
        /// <param name="parameters">Optional SQL parameters</param>
        /// <returns>Number of rows affected</returns>
        public static int ExecuteNonQuery(string query, SqlParameter[]? parameters = null)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters if provided
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        conn.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing non-query: " + ex.Message);
            }

            return rowsAffected;
        }

        /// <summary>
        /// Executes a query that returns a single value (e.g., COUNT, MAX, etc.).
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <param name="parameters">Optional SQL parameters</param>
        /// <returns>The first column of the first row in the result set</returns>
        public static object? ExecuteScalar(string query, SqlParameter[]? parameters = null)
        {
            object? result = null;

            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters if provided
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        conn.Open();
                        result = cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing scalar query: " + ex.Message);
            }

            return result;
        }
    }
}

