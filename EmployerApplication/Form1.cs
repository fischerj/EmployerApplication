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
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dgvEmployee.AutoGenerateColumns = false;
            el = new EmployeeList();
            el.Savable += El_Savable;
            el = el.GetAll();
            mnuSave.Enabled = false;
            dgvEmployee.DataSource = el.List;
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
    }
}
