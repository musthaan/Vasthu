using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VasthuApp.Models;

namespace VasthuApp
{
    public partial class frmReceipt : Form
    {
        public Models.EntryMode Mode { get; set; }
        public long CustomerServiceId { get; set; }

        Models.VasthuDBEntities db = new Models.VasthuDBEntities();
        public frmReceipt()
        {
            InitializeComponent();
            InitializeTemplate();
        }

        private void frmReceipt_Load(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnNameSearch_Click(object sender, EventArgs e)
        {
            frmSearchCustomer frm = new frmSearchCustomer();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtName.Text = frm.SelectedCustomer.Name;
                txtPhone.Text = frm.SelectedCustomer.Phone;
                txtAddress.Text = frm.SelectedCustomer.Address;
            }
        }

        private void btnReferance_Click(object sender, EventArgs e)
        {

        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Mode == EntryMode.New)
            {
                var receipt = new Models.Receipt(); 
                receipt.CustomerAddress = txtAddress.Text.Trim();
                receipt.CustomerName = txtName.Text.Trim();
                receipt.CustomerPhone = txtPhone.Text.Trim();
                receipt.Date = dtpServiceDate.Value;
                receipt.Remark = txtNote.Text.Trim();
                receipt.Total= Convert.ToDecimal(txtAmount.Text.Trim()); 


                
                db.Receipts.Add(receipt);
                db.SaveChanges();
                if (MessageBox.Show("Saved Successfully ! Do you want to print?", "Success", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    print(receipt);
                }

                ClearForm();

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        void InitializeTemplate()
        {
            Color color = ColorTranslator.FromHtml(Util.Config.PrimaryColor);
            btnSave.BackColor = color;
        }
        void ClearForm()
        {
            dtpServiceDate.Value = DateTime.Now;
            txtName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtNote.Text = string.Empty;
            
            BindServiceCombo();
            txtNote.Text = string.Empty;
            txtAmount.Text = string.Empty;
              
        }

       

        void BindServiceCombo()
        {
            cmbService.DataSource = db.ServiceMasters.Where(x => x.IsActive == true).Select(x => new { Name = x.Name, Id = x.Id }).ToList();
            cmbService.ValueMember = "Id";
            cmbService.DisplayMember = "Name";

        }
        private void print(Receipt   receipt)
        {

        }
    }
}
