using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VasthuApp
{
    public partial class MDIParent : Form
    {

        public MDIParent()
        {
            InitializeComponent();
            this.Text = System.Configuration.ConfigurationSettings.AppSettings["AppName"];
        }


        private void MDIParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void expenseCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExpenseCategory frm = new frmExpenseCategory() { MdiParent = this };
            frm.Show();
        }

        private void servicesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmServiceMasterList frm = new frmServiceMasterList() { MdiParent = this };
            frm.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void customerServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomerService frm = new frmCustomerService() { MdiParent = this };
            frm.Show();
        }

        private void receiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReceipt frm = new frmReceipt() { MdiParent = this };
            frm.Show();
        }

        private void daybookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.Daybook frm = new Reports.Daybook() { MdiParent = this };
            frm.Show();

        }

        private void expenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExpenses frm = new frmExpenses() { MdiParent = this };
            frm.Show();
        }
        List<string> keyCombination = new List<string>();
        private void MDIParent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode.ToString() != "ControlKey")
            {
                keyCombination.Add(e.KeyCode.ToString());
                if (keyCombination.Count == 3)
                {
                    if (string.Join("", keyCombination.ToArray()) == "EQD5")
                    {
                        Util.Config.IsSecure = true;
                        estimateToolStripMenuItem.Visible = true;
                        estimateReportToolStripMenuItem1.Visible = true;
                    }
                    else
                    {
                        Util.Config.IsSecure = false;
                        keyCombination = new List<string>();
                    }

                }
            }
        }

        private void estimateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEstimate frm = new frmEstimate() { MdiParent = this };
            frm.Show();
        }

        private void serviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmServiceReport frm = new Reports.frmServiceReport() { MdiParent = this };
            frm.Show();
        }

        private void expenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Reports.frmEstimateReport frm = new Reports.frmEstimateReport() { MdiParent = this };
            frm.Show();

        }

        private void MDIParent_Load(object sender, EventArgs e)
        {

        }

        private void receiptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmReceiptReport frm = new Reports.frmReceiptReport() { MdiParent = this };
            frm.Show();
        }
    }
}
