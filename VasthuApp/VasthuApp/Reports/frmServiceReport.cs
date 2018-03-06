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
    public partial class frmServiceReport : Form
    {
        public frmServiceReport()
        {
            InitializeComponent();
        }

        Models.VasthuDBEntities db = new Models.VasthuDBEntities();
        void onSearchClick()
        {
            if (cmbCategory.SelectedValue == null || cmbCategory.SelectedValue == "0" || Convert.ToInt64(cmbCategory.SelectedValue) == 0)
            {
                Search(dtpFrom.Value, dtpTo.Value, null);
            }
            else
            {
                long serviceId;
                serviceId = Convert.ToInt32(cmbCategory.SelectedValue);
                Search(dtpFrom.Value, dtpTo.Value, serviceId);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            onSearchClick();

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
            var result = db.CustomerServices
                    .Where(x => x.IsDeleted == false
                    && (From.HasValue ? x.Date >= From.Value : true)
                    && (To.HasValue ? x.Date <= To.Value : true)
                    && (CategoryId.HasValue ? x.CustomerServiceDetails.Any(s => s.ServiceId == CategoryId.Value) : true)
                    )
                    .Select(x => new
                    {
                        Id = x.Id,
                        x.Date,
                        Client = x.CustomerName,
                        x.GrandTotal
                    });
            dgview.DataSource = result.ToList();
            dgview.Columns[0].Visible = false;
        }

        private void frmServiceReport_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddDays((DateTime.Today.Day - 1) * -1);
            Search(DateTime.Today.AddDays((DateTime.Today.Day - 1) * -1), DateTime.Now.Date, null);
            BindServiceCombo();
        }

        private void dgview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn &&
                e.RowIndex >= 0)
            {
                var id = Convert.ToInt64(senderGrid.Rows[e.RowIndex].Cells[0].Value);
                var frm = new frmCustomerService();
                frm.Mode = Models.EntryMode.Edit;
                frm.CustomerServiceId = id;
                frm.ShowDialog();
                onSearchClick();
            }

        }
    }
}
