using System;
using System.Data;
using System.Windows.Forms;
using HospitalManagementSystem.DAL;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Forms
{
    /// <summary>
    /// PatientManagementForm handles all patient-related operations.
    /// Allows adding, updating, viewing, and searching patients.
    /// </summary>
    public partial class PatientManagementForm : Form
    {
        private PatientDAL patientDAL;
        private int selectedPatientID = 0;

        public PatientManagementForm()
        {
            InitializeComponent();
            patientDAL = new PatientDAL();
        }

        /// <summary>
        /// Handles the form load event.
        /// Loads all patients into the data grid.
        /// </summary>
        private void PatientManagementForm_Load(object sender, EventArgs e)
        {
            LoadPatients();
            SetupGenderComboBox();
            SetupBloodGroupComboBox();
        }

        /// <summary>
        /// Sets up the gender combo box with predefined values.
        /// </summary>
        private void SetupGenderComboBox()
        {
            cmbGender.Items.Clear();
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            cmbGender.Items.Add("Other");
        }

        /// <summary>
        /// Sets up the blood group combo box with predefined values.
        /// </summary>
        private void SetupBloodGroupComboBox()
        {
            cmbBloodGroup.Items.Clear();
            cmbBloodGroup.Items.Add("A+");
            cmbBloodGroup.Items.Add("A-");
            cmbBloodGroup.Items.Add("B+");
            cmbBloodGroup.Items.Add("B-");
            cmbBloodGroup.Items.Add("AB+");
            cmbBloodGroup.Items.Add("AB-");
            cmbBloodGroup.Items.Add("O+");
            cmbBloodGroup.Items.Add("O-");
        }

        /// <summary>
        /// Loads all patients from database and displays in DataGridView.
        /// </summary>
        private void LoadPatients()
        {
            try
            {
                DataTable dt = patientDAL.GetAllPatients();
                dgvPatients.DataSource = dt;
                
                // Hide PatientID column
                if (dgvPatients.Columns.Contains("PatientID"))
                {
                    dgvPatients.Columns["PatientID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading patients: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Add button click event.
        /// Adds a new patient to the database.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (!ValidateInput())
                    return;

                // Create patient object
                Patient patient = new Patient
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    DateOfBirth = dtpDateOfBirth.Value,
                    Gender = cmbGender.SelectedItem.ToString(),
                    PhoneNumber = txtPhoneNumber.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    BloodGroup = cmbBloodGroup.SelectedItem?.ToString(),
                    MedicalHistory = txtMedicalHistory.Text.Trim()
                };

                // Add to database
                if (patientDAL.AddPatient(patient))
                {
                    MessageBox.Show("Patient added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadPatients();
                }
                else
                {
                    MessageBox.Show("Failed to add patient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding patient: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Update button click event.
        /// Updates an existing patient in the database.
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedPatientID == 0)
                {
                    MessageBox.Show("Please select a patient to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate input
                if (!ValidateInput())
                    return;

                // Create patient object
                Patient patient = new Patient
                {
                    PatientID = selectedPatientID,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    DateOfBirth = dtpDateOfBirth.Value,
                    Gender = cmbGender.SelectedItem.ToString(),
                    PhoneNumber = txtPhoneNumber.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    BloodGroup = cmbBloodGroup.SelectedItem?.ToString(),
                    MedicalHistory = txtMedicalHistory.Text.Trim(),
                    IsActive = true
                };

                // Update in database
                if (patientDAL.UpdatePatient(patient))
                {
                    MessageBox.Show("Patient updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadPatients();
                }
                else
                {
                    MessageBox.Show("Failed to update patient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating patient: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Delete button click event.
        /// Deletes a patient from the database (soft delete).
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedPatientID == 0)
                {
                    MessageBox.Show("Please select a patient to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to delete this patient?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    if (patientDAL.DeletePatient(selectedPatientID))
                    {
                        MessageBox.Show("Patient deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadPatients();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete patient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting patient: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Search button click event.
        /// Searches for patients by name or phone number.
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    LoadPatients();
                    return;
                }

                DataTable dt = patientDAL.SearchPatients(txtSearch.Text.Trim());
                dgvPatients.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching patients: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the DataGridView cell click event.
        /// Loads selected patient data into form fields.
        /// </summary>
        private void dgvPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvPatients.Rows[e.RowIndex];
                    
                    selectedPatientID = Convert.ToInt32(row.Cells["PatientID"].Value);
                    
                    // Load patient details
                    Patient patient = patientDAL.GetPatientByID(selectedPatientID);
                    
                    if (patient != null)
                    {
                        txtFirstName.Text = patient.FirstName;
                        txtLastName.Text = patient.LastName;
                        dtpDateOfBirth.Value = patient.DateOfBirth;
                        cmbGender.SelectedItem = patient.Gender;
                        txtPhoneNumber.Text = patient.PhoneNumber;
                        txtEmail.Text = patient.Email;
                        txtAddress.Text = patient.Address;
                        cmbBloodGroup.SelectedItem = patient.BloodGroup;
                        txtMedicalHistory.Text = patient.MedicalHistory;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading patient details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Validates form input before saving.
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter first name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter last name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            if (cmbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGender.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                MessageBox.Show("Please enter phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhoneNumber.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Clears all form fields.
        /// </summary>
        private void ClearForm()
        {
            selectedPatientID = 0;
            txtFirstName.Clear();
            txtLastName.Clear();
            dtpDateOfBirth.Value = DateTime.Now;
            cmbGender.SelectedIndex = -1;
            txtPhoneNumber.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            cmbBloodGroup.SelectedIndex = -1;
            txtMedicalHistory.Clear();
        }

        /// <summary>
        /// Handles the Clear button click event.
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}

