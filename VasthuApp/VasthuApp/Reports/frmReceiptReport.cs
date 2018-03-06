using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VasthuApp.Reports
{
    public partial class frmReceiptReport : Form
    {
        public frmReceiptReport()
        {
            InitializeComponent();
        }

        Models.VasthuDBEntities db = new Models.VasthuDBEntities();

        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (cmbCategory.SelectedValue == null || cmbCategory.SelectedValue == "0" || Convert.ToInt64(cmbCategory.SelectedValue) == 0)
            {
                Search(dtpFrom.Value, dtpTo.Value, null);
            }
            else
            {
                long receiptId;
                receiptId = Convert.ToInt64(cmbCategory.SelectedValue);
                Search(dtpFrom.Value, dtpTo.Value, receiptId);
            }
        }
        class ComboDataSource
        {
            public long Id { get; set; }
            public string Name { get; set; }
        }
        void BindServiceCombo()
        {
            var list = new List<ComboDataSource>();
            list.Add(new ComboDataSource() { Id = 0, Name = "All" });
            list.AddRange(db.ServiceMasters.Where(x => x.IsActive == true).Select(x => new ComboDataSource { Name = x.Name, Id = x.Id }).ToList());

            cmbCategory.DataSource = list;
            cmbCategory.ValueMember = "Id";
            cmbCategory.DisplayMember = "Name";

        }

        void Search(DateTime? From, DateTime? To, long? CategoryId)
        {
            var result = db.Receipts
                    .Where(x => (From.HasValue ? x.Date >= From.Value : true)
                    && (To.HasValue ? x.Date <= To.Value : true)
                    && (CategoryId.HasValue ? (x.ServiceId == CategoryId.Value) : true)
                    )
                    .Select(x => new ReceiptReportViewModel()
                    {
                        Id = x.Id,
                        Date = x.Date,
                        Client = x.CustomerName,
                        Amount = x.Total.Value
                    }).ToList();
            var total = result.Sum(x => x.Amount);
            result.Add(new ReceiptReportViewModel() { Id = -1, Client = "Total", Amount = total });
            dgview.DataSource = result.ToList();
            dgview.Columns[0].Visible = false;
        }

        private void frmReceiptReport_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddDays((DateTime.Today.Day - 1) * -1);
            Search(DateTime.Today.AddDays((DateTime.Today.Day - 1) * -1), DateTime.Now.Date, null);
            BindServiceCombo();
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {

        }
    }

    class ReceiptReportViewModel
    {
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public string Client { get; set; }
        public decimal Amount { get; set; }
    }
}
