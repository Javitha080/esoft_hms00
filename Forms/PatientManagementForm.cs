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
        private TextBox? txtFirstName, txtLastName, txtPhoneNumber, txtEmail, txtAddress, txtMedicalHistory, txtSearch;
        private DateTimePicker? dtpDateOfBirth;
        private ComboBox? cmbGender, cmbBloodGroup;
        private DataGridView? dgvPatients;
        private Button? btnAdd, btnUpdate, btnDelete, btnSearch, btnClear;


        public PatientManagementForm()
        {
            InitializeComponent();
            patientDAL = new PatientDAL();
        }
        private void InitializeComponent()
        {
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtMedicalHistory = new System.Windows.Forms.TextBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.cmbBloodGroup = new System.Windows.Forms.ComboBox();
            this.dgvPatients = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(12, 12);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(100, 20);
            this.txtFirstName.TabIndex = 0;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(118, 12);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(100, 20);
            this.txtLastName.TabIndex = 1;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(12, 38);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(100, 20);
            this.txtPhoneNumber.TabIndex = 2;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(118, 38);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 20);
            this.txtEmail.TabIndex = 3;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(12, 64);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(206, 20);
            this.txtAddress.TabIndex = 4;
            // 
            // txtMedicalHistory
            // 
            this.txtMedicalHistory.Location = new System.Drawing.Point(12, 90);
            this.txtMedicalHistory.Name = "txtMedicalHistory";
            this.txtMedicalHistory.Size = new System.Drawing.Size(206, 20);
            this.txtMedicalHistory.TabIndex = 5;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(12, 116);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 6;
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.Location = new System.Drawing.Point(224, 12);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Size = new System.Drawing.Size(200, 20);
            this.dtpDateOfBirth.TabIndex = 7;
            // 
            // cmbGender
            // 
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Location = new System.Drawing.Point(224, 38);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(121, 21);
            this.cmbGender.TabIndex = 8;
            // 
            // cmbBloodGroup
            // 
            this.cmbBloodGroup.FormattingEnabled = true;
            this.cmbBloodGroup.Location = new System.Drawing.Point(224, 65);
            this.cmbBloodGroup.Name = "cmbBloodGroup";
            this.cmbBloodGroup.Size = new System.Drawing.Size(121, 21);
            this.cmbBloodGroup.TabIndex = 9;
            // 
            // dgvPatients
            // 
            this.dgvPatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPatients.Location = new System.Drawing.Point(12, 142);
            this.dgvPatients.Name = "dgvPatients";
            this.dgvPatients.Size = new System.Drawing.Size(776, 296);
            this.dgvPatients.TabIndex = 10;
            this.dgvPatients.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPatients_CellClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(430, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(430, 39);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 12;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(430, 68);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(118, 114);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(430, 97);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // PatientManagementForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvPatients);
            this.Controls.Add(this.cmbBloodGroup);
            this.Controls.Add(this.cmbGender);
            this.Controls.Add(this.dtpDateOfBirth);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.txtMedicalHistory);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtFirstName);
            this.Name = "PatientManagementForm";
            this.Load += new System.EventHandler(this.PatientManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
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
                Patient patient = new()
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
                Patient patient = new()
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
                    Patient? patient = patientDAL.GetPatientByID(selectedPatientID);


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

