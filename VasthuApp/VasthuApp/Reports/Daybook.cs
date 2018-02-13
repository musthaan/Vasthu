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
    public partial class Daybook : Form
    {
        public Daybook()
        {
            InitializeComponent();
        }
        Models.VasthuDBEntities db = new Models.VasthuDBEntities();

        private void Daybook_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
        List<GridRowModel> getCustomerServices()
        {
            var list = db.CustomerServices
               .Where(x => x.IsDeleted == false)
               .Select(x => new GridRowModel()
               {
                   Date = x.Date,
                   Type = "Customer Service",
                   Customer = x.CustomerName,
                   Cr = x.NetTotal,
                   Dr = 0
               }).ToList();
            return list;
        }

        List<GridRowModel> getReceipts()
        {
            var list = db.Receipts
              .Select(x => new GridRowModel()
              {
                  Date = x.Date,
                  Type = "Receipt",
                  Customer = x.CustomerName,
                  Cr = x.Total,
                  Dr = 0
              }).ToList();
            return list;
        }

        List<GridRowModel> getExpenses()
        {
            var expenses = db.Expenses
              .Where(x => x.IsDelete == false)
              .Select(x => new GridRowModel()
              {
                  Cr = 0,
                  Customer = x.ExpenseCategory.Name,
                  Date = x.Date,
                  Dr = x.Amount,
                  Type = "Expense"
              });
            return expenses.ToList();
        }
        List<GridRowModel> getEstimate()
        {
            var list = db.Estimates
             .Select(x => new GridRowModel()
             {
                 Date = x.Date,
                 Type = "Estimate",
                 Customer = x.CustomerName,
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
                Customer = "Total",
                Type = "",
                Date = DateTime.Today.AddDays(1)
            };
            masterList.Add(total_Row);

            dataGridView1.DataSource = masterList.OrderBy(x => x.Date).ToList();
        }





        class GridRowModel
        {
            public DateTime? Date { get; set; }
            public string Type { get; set; }
            public string Customer { get; set; }
            public decimal? Cr { get; set; }
            public decimal? Dr { get; set; }
        }
    }
}