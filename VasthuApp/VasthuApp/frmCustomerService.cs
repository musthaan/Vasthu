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
using System.Data.Entity;
using Microsoft.Win32;

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
            btnPrint.Enabled = false;
            btnDelete.Enabled = false;
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
                    btnDelete.Enabled = true;
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

            // lblCGST.Text = "0.00";
            lblGrandTotal.Text = "0.00";
            // lblNetTotal.Text = "0.00";
            // lblSGST.Text = "0.00";

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
            //lblNetTotal.Text = total.ToString("0.00");
            //lblCGST.Text = (total * 0.09).ToString("0.00");
            //lblSGST.Text = (total * 0.09).ToString("0.00");
            lblGrandTotal.Text = total.ToString("0.00");// (total + (total * 0.09) + (total * 0.09)).ToString("0.00");
        }
        bool ValidateService()
        {
            bool status = true;

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Client Name is Required!");
                status = false;
            }

            if (string.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                MessageBox.Show("Client Phone No. is Required!");
                status = false;
            }

            decimal total = 0;
            decimal.TryParse(lblGrandTotal.Text.Trim(), out total);
            if (total <= 0)
            {
                MessageBox.Show("Invalid Entry!");
                status = false;
            }

            return status;
        }

        bool ValidateRowEntry()
        {
            decimal amount = 0;
            if (!decimal.TryParse(txtAmount.Text.Trim(), out amount))
            {
                MessageBox.Show("Invalid Amount");
                return false;

            }
            if (amount <= 0)
            {
                MessageBox.Show("Invalid Amount");
                return false;
            }
            return true;
        }

        void BindServiceCombo()
        {
            cmbService.DataSource = db.ServiceMasters.Where(x => x.IsActive == true).Select(x => new { Name = x.Name, Id = x.Id }).ToList();
            cmbService.ValueMember = "Id";
            cmbService.DisplayMember = "Name";

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateRowEntry())
                return;
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
            if (!ValidateService())
                return;
            if (Mode == Models.EntryMode.New)
            {
                var service = new Models.CustomerService();
                service.CGST = 0;// Convert.ToDecimal(lblCGST.Text.Trim());
                service.CustomerAddress = txtAddress.Text.Trim();
                service.CustomerName = txtName.Text.Trim();
                service.CustomerPhone = txtPhone.Text.Trim();
                service.Date = dtpServiceDate.Value;
                service.GrandTotal = Convert.ToDecimal(lblGrandTotal.Text.Trim());
                service.NetTotal = 0;// Convert.ToDecimal(lblNetTotal.Text.Trim());
                service.Note = "";
                service.SGST = 0;// Convert.ToDecimal(lblSGST.Text.Trim());


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

                    RegistryHandler objRegistryHandler = new RegistryHandler();
                    objRegistryHandler.TakeBackUp();
                    objRegistryHandler.SetPrintSetup();

                    print(service);

                    objRegistryHandler.Restore();
                }

                ClearForm();

            }
            else if (Mode == EntryMode.Edit)
            {
                var service = db.CustomerServices.Find(CustomerServiceId);
                if (service != null)
                {
                    service.CGST = 0;// Convert.ToDecimal(lblCGST.Text.Trim());
                    service.CustomerAddress = txtAddress.Text.Trim();
                    service.CustomerName = txtName.Text.Trim();
                    service.CustomerPhone = txtPhone.Text.Trim();
                    service.Date = dtpServiceDate.Value;
                    service.GrandTotal = Convert.ToDecimal(lblGrandTotal.Text.Trim());
                    service.NetTotal = 0;// Convert.ToDecimal(lblNetTotal.Text.Trim());
                    service.Note = "";
                    service.SGST = 0;// Convert.ToDecimal(lblSGST.Text.Trim());

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

            webBrowserForPrint.Url = new Uri(generateReport(service));
        }

        string generateReport(CustomerService service)
        {
            var folderPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            var file_path = System.IO.Path.Combine(folderPath, @"Reports\report-customer-service.html");
            string billContent = System.IO.File.ReadAllText(file_path);

            billContent = billContent.Replace("{{DATE}}", service.Date.ToString("dd MMM, yyyy"));
            billContent = billContent.Replace("{{BILLNO}}", service.Id.ToString().PadLeft(6, '0'));
            billContent = billContent.Replace("{{CUSTOMER_NAME}}", service.CustomerName);
            billContent = billContent.Replace("{{CUSTOMER_ADDRESS}}", service.CustomerAddress);
            billContent = billContent.Replace("{{CUSTOMER_PHONE}}", service.CustomerPhone);

            var strTableRows = string.Empty;
            var index = 1;
            foreach (var d in service.CustomerServiceDetails)
            {
                var serviceName = db.ServiceMasters.Find(d.ServiceId).Name;
                var _row = $@"  <tr>
                        <td style='text-align: center;'>{index++}</td>
                        <td style='text-align: left;'>{serviceName}</td>
                        <td style='text-align: right;'>{d.Amount.Value.ToString("0.00")}</td>
                    </tr> ";
                strTableRows += _row;
            }
            var _totalRow = $@" <tr>
                        <td colspan='2' style='text-align: right;'><b>Total:</b></td>
                        <td style='text-align: right;'>{service.GrandTotal.Value.ToString("0.00")}</td>
                    </tr>";

            billContent = billContent.Replace("{{SERVICE_ROW}}", strTableRows);
            billContent = billContent.Replace("{{SERVICE_TOTAL_ROW}}", _totalRow);



            var outputFilePath = System.IO.Path.Combine(folderPath, "customer-service.html");
            if (System.IO.File.Exists(outputFilePath))
                System.IO.File.Delete(outputFilePath);
            System.IO.File.WriteAllText(outputFilePath, billContent);
            return outputFilePath;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete ?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var model = db.CustomerServices.Find(CustomerServiceId);
                if (model != null)
                {
                    model.IsDeleted = true;
                    db.SaveChanges();
                    MessageBox.Show("Service Deleted!");
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error!");
                }
            }
        }
    }
}
