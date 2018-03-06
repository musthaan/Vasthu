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
    public partial class Daybook : Form
    {
        public Daybook()
        {
            InitializeComponent();
        }
        Models.VasthuDBEntities db = new Models.VasthuDBEntities();

        private void Daybook_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddDays((DateTime.Today.Day - 1) * -1);
            dtpTo.Value = DateTime.Today;
            dtpFrom.MaxDate = DateTime.Today;
            dtpTo.MaxDate = DateTime.Today;

            BindGrid();
        }
        List<GridRowModel> getCustomerServices()
        {
            var list = db.CustomerServices
               .Where(x => x.IsDeleted == false &&
               (x.Date >= dtpFrom.Value.Date && x.Date <= dtpTo.Value.Date)
               )
               .Select(x => new GridRowModel()
               {
                   Date = x.Date,
                   Type = "Client Service",
                   Client = x.CustomerName,
                   Cr = x.GrandTotal,
                   Dr = 0
               }).ToList();
            return list;
        }

        List<GridRowModel> getReceipts()
        {
            var list = db.Receipts
              .Where(x => x.Date >= dtpFrom.Value.Date && x.Date <= dtpTo.Value.Date)
              .Select(x => new GridRowModel()
              {
                  Date = x.Date,
                  Type = "Income",
                  Client = x.CustomerName,
                  Cr = x.Total,
                  Dr = 0
              }).ToList();
            return list;
        }

        List<GridRowModel> getExpenses()
        {
            var expenses = db.Expenses
              .Where(x => x.IsDelete == false &&
               (x.Date.Value >= dtpFrom.Value.Date && x.Date.Value <= dtpTo.Value.Date))
              .Select(x => new GridRowModel()
              {
                  Cr = 0,
                  Client = x.ExpenseCategory.Name,
                  Date = x.Date,
                  Dr = x.Amount,
                  Type = "Expenditure"
              });
            return expenses.ToList();
        }
        List<GridRowModel> getEstimate()
        {
            var list = db.Estimates
             .Where(x => (x.Date >= dtpFrom.Value.Date && x.Date <= dtpTo.Value.Date))
             .Select(x => new GridRowModel()
             {
                 Date = x.Date,
                 Type = "Estimate",
                 Client = x.CustomerName,
                 Cr = x.NetTotal,
                 Dr = 0
             }).ToList();
            return list;
        }
        void BindGrid()
        {

            var masterList = new List<GridRowModel>();


            masterList.AddRange(getCustomerServices());
            masterList.AddRange(getExpenses());
            masterList.AddRange(getReceipts());
            if (Util.Config.IsSecure)
            {
                masterList.AddRange(getEstimate());
            }

            var total_Cr = masterList.Sum(x => x.Cr);
            var total_Dr = masterList.Sum(x => x.Dr);

            var total_Row = new GridRowModel()
            {
                Cr = total_Cr,
                Dr = total_Dr,
                Client = "Total",
                Type = ""
            };

            masterList = masterList.OrderBy(x => x.Date).ToList();
            masterList.Add(total_Row);
            dataGridView1.DataSource = masterList;
           // dataGridView1.Rows.Add("", "", "Total", total_Cr, total_Dr);
            
        }





        class GridRowModel
        {
            public DateTime? Date { get; set; }
            public string Type { get; set; }
            public string Client { get; set; }
            public decimal? Cr { get; set; }
            public decimal? Dr { get; set; }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}
