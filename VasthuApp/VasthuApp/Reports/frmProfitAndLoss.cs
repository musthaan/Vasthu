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
    public partial class frmProfitAndLoss : Form
    {
        public enum Mode {
            WithEstimate,
            WithService

        }
        public Mode FormMode { get; set; }
        public frmProfitAndLoss()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillGrid();
        }

        private void frmProfitAndLoss_Load(object sender, EventArgs e)
        {
            fillMonths();
            fillGrid();
        }

        private void fillMonths()
        {
            string[] months = { "All", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            foreach (var m in months)
            {
                cmbMonth.Items.Add(m);
            }
            cmbMonth.SelectedIndex = 0;
        }

        private void fillGrid()
        {
            var db = new Models.VasthuDBEntities();
            grdPL.Columns.Clear();
            grdPL.Columns.Add("DrParticulars", "Particulars");
            grdPL.Columns.Add("DrAmount", "Amount");
            grdPL.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdPL.Columns.Add("CrParticulars", "Particulars");
            grdPL.Columns.Add("CrAmount", "Amount");
            grdPL.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            var _e_list = new List<ProfitAndLossReportModel>();
            var tempList = db.ExpenseCategories.Select(x => new ProfitAndLossReportModel()
            {
                DrParticular = x.Name,
                DrAmount = x.Expenses.Where(e => e.IsDelete == false).Sum(e => e.Amount)
            });
            _e_list.Add(new ProfitAndLossReportModel() { DrParticular = "Expenses", IsDrHeader = true, DrAmount = tempList.Sum(x => x.DrAmount) });
            _e_list.AddRange(tempList);

            var cr_list = new List<ProfitAndLossReportModel>();
            if (FormMode == Mode.WithService)
            {
                tempList = db.ServiceMasters
                     .Where(x => x.IsActive == true)
                     .Select(x => new ProfitAndLossReportModel()
                     {
                         CrParticular = x.Name,
                         CrAmount = x.CustomerServiceDetails.Where(c => c.CustomerService.IsDeleted == false).Sum(c => c.Amount)
                     });
                cr_list.Add(new ProfitAndLossReportModel() { CrParticular = "Client Service", CrAmount = tempList.Sum(x => x.CrAmount), IsCrHeader = true });
                cr_list.AddRange(tempList);
            }
            tempList = db.ServiceMasters
                .Where(x => x.IsActive == true)
                .Select(x => new ProfitAndLossReportModel()
                {
                    CrParticular = x.Name,
                    CrAmount = x.Receipts.Sum(r => r.Total)
                });
            cr_list.Add(new ProfitAndLossReportModel() { CrParticular = "Income", CrAmount = tempList.Sum(x => x.CrAmount), IsCrHeader = true });
            cr_list.AddRange(tempList);

            List<ProfitAndLossReportModel> modelList = new List<ProfitAndLossReportModel>();
            var debitCount = _e_list.Count();

            if (Util.Config.IsSecure && FormMode == Mode.WithEstimate)
            {
                tempList = db.ServiceMasters
                           .Where(x => x.IsActive == true)
                           .Select(x => new ProfitAndLossReportModel()
                           {
                               CrParticular = x.Name,
                               CrAmount = x.EstimateDetails.Where(e=> e.Estimate.IsDeleted==false).Sum(c => c.Amount)
                           });

                cr_list.Add(new ProfitAndLossReportModel() { CrParticular = "Estimate", CrAmount = tempList.Sum(x => x.CrAmount), IsCrHeader = true });
                cr_list.AddRange(tempList);

            }
            var creditCount = cr_list.Count();
            var rowCount = (debitCount > creditCount) ? debitCount : creditCount;
            decimal crTotal = 0;
            decimal drTotal = 0;
            for (int i = 0; i < rowCount; i++)
            {
                var m = new ProfitAndLossReportModel();
                if (i < _e_list.Count())
                {
                    m.DrParticular = (_e_list[i].IsDrHeader ? "" : "   ") + _e_list[i].DrParticular;
                    m.DrAmount = _e_list[i].DrAmount.HasValue ? _e_list[i].DrAmount : 0;
                    m.IsDrHeader = _e_list[i].IsDrHeader;
                    drTotal += m.IsDrHeader ? 0 : m.DrAmount.Value;

                }
                if (i < cr_list.Count())
                {
                    m.CrParticular = (cr_list[i].IsCrHeader ? "" : "   ") + cr_list[i].CrParticular;
                    m.CrAmount = cr_list[i].CrAmount.HasValue ? cr_list[i].CrAmount : 0;

                    m.IsCrHeader = cr_list[i].IsCrHeader;
                    crTotal += m.IsCrHeader ? 0 : m.CrAmount.Value;
                }
                grdPL.Rows.Add(m.DrParticular, m.DrAmount, m.CrParticular, m.CrAmount);
                grdPL.Rows[grdPL.Rows.Count - 1].Cells[2].Style.ForeColor = m.IsCrHeader ? Color.DarkGreen : Color.Black;
                grdPL.Rows[grdPL.Rows.Count - 1].Cells[2].Style.Font = new Font(FontFamily.GenericSansSerif, m.IsCrHeader ? 9 : 8);
                grdPL.Rows[grdPL.Rows.Count - 1].Cells[0].Style.ForeColor = m.IsDrHeader ? Color.DarkGreen : Color.Black;
                grdPL.Rows[grdPL.Rows.Count - 1].Cells[0].Style.Font = new Font(FontFamily.GenericSansSerif, m.IsDrHeader ? 9 : 8);
            }
            grdPL.Rows.Add("Total", drTotal, "Total", crTotal);
            grdPL.Rows[grdPL.Rows.Count - 1].DefaultCellStyle.Font = new Font(FontFamily.GenericSansSerif, 10);

            decimal finalDr = 0, finalCr = 0;
            if (drTotal > crTotal)
            {
                finalDr = drTotal - crTotal;
            }
            else if (drTotal < crTotal) {
                finalCr = crTotal - drTotal;
            }
            grdPL.Rows.Add("Balance", finalDr, "", finalCr);
            grdPL.Rows[grdPL.Rows.Count - 1].Cells[0].Style.ForeColor = Color.Black;
            grdPL.Rows[grdPL.Rows.Count - 1].Cells[0].Style.Font = new Font(FontFamily.GenericSansSerif, 10);

            grdPL.Rows[grdPL.Rows.Count - 1].Cells[1].Style.ForeColor =  Color.DarkRed;
            grdPL.Rows[grdPL.Rows.Count - 1].Cells[2].Style.Font = new Font(FontFamily.GenericSansSerif, 10);

            grdPL.Rows[grdPL.Rows.Count - 1].Cells[3].Style.ForeColor = Color.DarkGreen;
            grdPL.Rows[grdPL.Rows.Count - 1].Cells[3].Style.Font = new Font(FontFamily.GenericSansSerif, 10);
        }
    }
    class ProfitAndLossReportModel
    {
        public string DrParticular { get; set; }
        public string CrParticular { get; set; }

        public decimal? DrAmount { get; set; }
        public decimal? CrAmount { get; set; }

        public bool IsCrHeader { get; set; }
        public bool IsDrHeader { get; set; }
    }
}
