using System;
using System.Data;
using System.Windows.Forms;
using HospitalManagementSystem.DAL;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Forms
{
    public partial class BillingManagementForm : Form
    {
        public BillingManagementForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Billing Management";
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
