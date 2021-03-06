﻿using System;
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
    public partial class frmEstimateReport : Form
    {
        public frmEstimateReport()
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
                long serviceId;
                serviceId = Convert.ToInt32(cmbCategory.SelectedValue);
                Search(dtpFrom.Value, dtpTo.Value, serviceId);
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
            var result = db.Estimates
                    .Where(x => x.IsDeleted == false
                    && (From.HasValue ? x.Date >= From.Value : true)
                    && (To.HasValue ? x.Date <= To.Value : true)
                    && (CategoryId.HasValue ? x.EstimateDetails.Any(s => s.ServiceId == CategoryId.Value) : true)
                    )
                    .Select(x => new EstimateReportViewModel()
                    {
                        Id = x.Id,
                        Date = x.Date,
                        Client = x.CustomerName,
                        Amount = x.GrandTotal.Value
                    }).ToList();
            var total = result.Sum(x => x.Amount);
            result.Add(new EstimateReportViewModel() { Id = -1, Client = "Total", Amount = total });
            dgview.DataSource = result;
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
                var frm = new frmEstimate();
                frm.Mode = Models.EntryMode.Edit;
                frm.EstimateId = id;
                frm.ShowDialog();
                btnSearch_Click(null, null);
            }

        }
    }

    class EstimateReportViewModel
    {
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public string Client { get; set; }
        public decimal Amount { get; set; }
    }
}
