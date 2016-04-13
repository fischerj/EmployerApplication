using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessObjects;

namespace EmployerApplication
{
    public partial class Form1 : Form
    {
        EmployeeList el;
        PhoneTypeList phoneTypes;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            phoneTypes = new PhoneTypeList();
            phoneTypes = phoneTypes.GetAll();
            dgvPhone.CellFormatting += DgvPhone_CellFormatting;
            dgvPhone.CellValueChanged += DgvPhone_CellValueChanged;
            dgvPhone.RowValidated += DgvPhone_RowValidated;
            dgvPhone.DataError += DgvPhone_DataError;
            dgvEmployee.AutoGenerateColumns = false;
            dgvPhone.AutoGenerateColumns = false;
            el = new EmployeeList();
            el.Savable += El_Savable;
            mnuSave.Enabled = false;
            dgvEmployee.RowHeaderMouseDoubleClick += DgvEmployee_RowHeaderMouseDoubleClick;
        }

        private void DgvPhone_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                PopulatePhoneTypes((DataGridViewComboBoxColumn)
                    dgvPhone.Columns[e.ColumnIndex]);
            }
        }

        private void DgvPhone_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void DgvPhone_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void PopulatePhoneTypes(DataGridViewComboBoxColumn column)
        {
            var c = column;
            {
                if (c.DataSource == null)
                {
                    c.DisplayMember = "Type";
                    c.ValueMember = "Id";
                    c.DataSource = phoneTypes.List;
                }
            };

        }

        private void DgvPhone_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //IGNORE THIS ERROR
        }

        private void DgvEmployee_RowHeaderMouseDoubleClick(object sender, 
            DataGridViewCellMouseEventArgs e)
        {
            Employee employee = (Employee)dgvEmployee.Rows[e.RowIndex].DataBoundItem;
            dgvPhone.DataSource= employee.Phones.List;
        }

        private void El_Savable(SavableEventArgs e)
        {
            this.mnuSave.Enabled = e.Savable;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            el.Save();
            mnuSave.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            el = el.GetAll();
            dgvEmployee.DataSource = el.List;
        }


    }
}
