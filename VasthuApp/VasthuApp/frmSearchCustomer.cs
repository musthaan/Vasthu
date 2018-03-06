using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VasthuApp
{
    public partial class frmSearchCustomer : Form
    {
        public string From { get; set; }
        public frmSearchCustomer()
        {
            InitializeComponent();
        }
        Models.VasthuDBEntities db = new Models.VasthuDBEntities();
        public CustomerSearchGridRowModel SelectedCustomer { get; set; }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            List<CustomerSearchGridRowModel> masterList = new List<CustomerSearchGridRowModel>();
            masterList.AddRange(getCustomersInService());
            if (From == "estimate") masterList.AddRange(getCustomersInEstimate());
            masterList.AddRange(getCustomersInReceipt());

            grdCustomer.DataSource = masterList.OrderBy(x => x.Name).ToList();
        }

        List<CustomerSearchGridRowModel> getCustomersInService()
        {
            var result = db.CustomerServices
                .Where(x =>
                x.IsDeleted == false &&
                (x.CustomerName.Contains(txtSearch.Text.Trim())
                || x.CustomerPhone.Contains(txtSearch.Text.Trim())
                || x.CustomerAddress.Contains(txtSearch.Text.Trim())
                ))
                .Select(x => new CustomerSearchGridRowModel()
                {
                    Address = x.CustomerAddress,
                    Name = x.CustomerName,
                    Phone = x.CustomerPhone
                });
            return result.ToList();
        }
        List<CustomerSearchGridRowModel> getCustomersInReceipt()
        {
            var result = db.Receipts
                 .Where(x =>
                 (x.CustomerName.Contains(txtSearch.Text.Trim())
                 || x.CustomerPhone.Contains(txtSearch.Text.Trim())
                 || x.CustomerAddress.Contains(txtSearch.Text.Trim())
                 ))
                 .Select(x => new CustomerSearchGridRowModel()
                 {
                     Address = x.CustomerAddress,
                     Name = x.CustomerName,
                     Phone = x.CustomerPhone
                 });
            return result.ToList();
        }
        List<CustomerSearchGridRowModel> getCustomersInEstimate()
        {
            var result = db.Estimates
                 .Where(x =>
                 x.IsDeleted == false &&
                 (x.CustomerName.Contains(txtSearch.Text.Trim())
                 || x.CustomerPhone.Contains(txtSearch.Text.Trim())
                 || x.CustomerAddress.Contains(txtSearch.Text.Trim())
                 ))
                 .Select(x => new CustomerSearchGridRowModel()
                 {
                     Address = x.CustomerAddress,
                     Name = x.CustomerName,
                     Phone = x.CustomerPhone
                 });
            return result.ToList();
        }




        private void grdCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.RowIndex >= 0)
            {
                var id = Convert.ToInt64(senderGrid.Rows[e.RowIndex].Cells[1].Value);
                SelectedCustomer = new CustomerSearchGridRowModel();
                SelectedCustomer.Name = senderGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
                SelectedCustomer.Phone = senderGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                SelectedCustomer.Address = senderGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
    public class CustomerSearchGridRowModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
