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
    public partial class frmCustomerService : Form
    {
        public Models.EntryMode Mode { get; set; }
        public long CustomerServiceId { get; set; }

        Models.VasthuDBEntities db = new Models.VasthuDBEntities();
        public frmCustomerService()
        {
            InitializeComponent();
            InitializeTemplate();
        }

        void InitializeTemplate()
        {
            Color color = ColorTranslator.FromHtml(Util.Config.PrimaryColor);
            btnAdd.BackColor = color;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCustomerService_Load(object sender, EventArgs e)
        {
            btnPrint.Enabled = true;
            ClearForm();

            if (Mode == EntryMode.Edit && CustomerServiceId > 0)
            {
                try
                {
                    var service = db.CustomerServices.Where(x => x.Id == CustomerServiceId).FirstOrDefault();
                    dtpServiceDate.Value = service.Date;
                    txtName.Text = service.CustomerName;
                    txtPhone.Text = service.CustomerPhone;
                    txtAddress.Text = service.CustomerAddress;
                    grdService.Rows.Clear();

                    foreach (var detail in service.CustomerServiceDetails)
                    {
                        string[] row = new string[] { detail.ServiceId.ToString(), db.ServiceMasters.Find(detail.ServiceId).Name, detail.Note, detail.Amount.ToString() };
                        grdService.Rows.Add(row);
                    }
                    CalculateTotal();
                }
                catch (Exception ex)
                {
                }
            }
        }


        void ClearForm()
        {
            dtpServiceDate.Value = DateTime.Now;
            txtName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;

            BindServiceCombo();
            txtNote.Text = string.Empty;
            txtAmount.Text = string.Empty;

            lblCGST.Text = "0.00";
            lblGrandTotal.Text = "0.00";
            lblNetTotal.Text = "0.00";
            lblSGST.Text = "0.00";

            grdService.Rows.Clear();
            CalculateTotal();
        }

        void CalculateTotal()
        {
            double total = 0;
            foreach (var r in grdService.Rows)
            {
                var row = (DataGridViewRow)r;
                var value = Convert.ToDouble(row.Cells[3].Value);
                total = total + value;
            }
            lblNetTotal.Text = total.ToString("0.00");
            lblCGST.Text = (total * 0.09).ToString("0.00");
            lblSGST.Text = (total * 0.09).ToString("0.00");
            lblGrandTotal.Text = (total + (total * 0.09) + (total * 0.09)).ToString("0.00");
        }

        void BindServiceCombo()
        {
            cmbService.DataSource = db.ServiceMasters.Where(x => x.IsActive == true).Select(x => new { Name = x.Name, Id = x.Id }).ToList();
            cmbService.ValueMember = "Id";
            cmbService.DisplayMember = "Name";

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string[] row = new string[] { cmbService.SelectedValue.ToString(), cmbService.Text, txtNote.Text.Trim(), txtAmount.Text.Trim() };
            grdService.Rows.Add(row);

            cmbService.SelectedIndex = 0;
            txtNote.Text = string.Empty;
            txtAmount.Text = string.Empty;

            cmbService.Focus();
            CalculateTotal();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Mode == Models.EntryMode.New)
            {
                var service = new Models.CustomerService();
                service.CGST = Convert.ToDecimal(lblCGST.Text.Trim());
                service.CustomerAddress = txtAddress.Text.Trim();
                service.CustomerName = txtName.Text.Trim();
                service.CustomerPhone = txtPhone.Text.Trim();
                service.Date = dtpServiceDate.Value;
                service.GrandTotal = Convert.ToDecimal(lblGrandTotal.Text.Trim());
                service.NetTotal = Convert.ToDecimal(lblNetTotal.Text.Trim());
                service.Note = "";
                service.SGST = Convert.ToDecimal(lblSGST.Text.Trim());


                foreach (var r in grdService.Rows)
                {
                    var row = (DataGridViewRow)r;

                    var serviceId = Convert.ToInt32(row.Cells[0].Value);
                    var note = row.Cells[2].Value.ToString();
                    var amount = Convert.ToInt32(row.Cells[3].Value);

                    var serviceDetails = new Models.CustomerServiceDetail()
                    {
                        Amount = amount,
                        Note = note,
                        ServiceId = serviceId
                    };
                    service.CustomerServiceDetails.Add(serviceDetails);

                }
                db.CustomerServices.Add(service);
                db.SaveChanges();
                if (MessageBox.Show("Saved Successfully ! Do you want to print?", "Success", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    print(service);
                }

                ClearForm();

            }
            else if (Mode == EntryMode.Edit)
            {
                var service = db.CustomerServices.Find(CustomerServiceId);
                if (service != null)
                {
                    service.CGST = Convert.ToDecimal(lblCGST.Text.Trim());
                    service.CustomerAddress = txtAddress.Text.Trim();
                    service.CustomerName = txtName.Text.Trim();
                    service.CustomerPhone = txtPhone.Text.Trim();
                    service.Date = dtpServiceDate.Value;
                    service.GrandTotal = Convert.ToDecimal(lblGrandTotal.Text.Trim());
                    service.NetTotal = Convert.ToDecimal(lblNetTotal.Text.Trim());
                    service.Note = "";
                    service.SGST = Convert.ToDecimal(lblSGST.Text.Trim());

                    service.CustomerServiceDetails.Clear();
                    foreach (var r in grdService.Rows)
                    {
                        var row = (DataGridViewRow)r;

                        var serviceId = Convert.ToInt32(row.Cells[0].Value);
                        var note = row.Cells[2].Value.ToString();
                        var amount = Convert.ToDouble(row.Cells[3].Value);

                        var serviceDetails = new Models.CustomerServiceDetail()
                        {
                            Amount = Convert.ToDecimal(amount),
                            Note = note,
                            ServiceId = serviceId
                        };
                        service.CustomerServiceDetails.Add(serviceDetails);

                    }
                    db.SaveChanges();
                    MessageBox.Show("Saved Successfully !");
                }
            }
            DialogResult = DialogResult.OK;
        }

        private void print(CustomerService service)
        {
            WebBrowser webBrowserForPrint = new WebBrowser();
            webBrowserForPrint.DocumentCompleted +=
                new WebBrowserDocumentCompletedEventHandler(printDocument);

            webBrowserForPrint.Url = new Uri($@"{ Application.StartupPath}\report.html");
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            WebBrowser webBrowserForPrint = new WebBrowser();
            webBrowserForPrint.DocumentCompleted +=
                new WebBrowserDocumentCompletedEventHandler(printDocument);

            webBrowserForPrint.Url = new Uri($@"{ Application.StartupPath}\report.html");
        }

        void printDocument(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ((WebBrowser)sender).Print();
            ((WebBrowser)sender).Dispose();
        }
    }
}
