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
    public partial class frmExpenses : Form
    {
        public frmExpenses()
        {
            InitializeComponent();
        }
        VasthuDBEntities db = new VasthuDBEntities();

        private void frmExpenses_Load(object sender, EventArgs e)
        {
            clearForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmExpenseEdit frm = new frmExpenseEdit();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                clearForm();
            }
        }
        void clearForm()
        {
            dtpSearchFrom.Value = DateTime.Now;
            dtpSearchTo.Value = DateTime.Now;
            bindCategory();
            bindGrid();
        }
        void bindGrid()
        {
            var list = db.Expenses
                .Where(x => x.IsDelete == false)
                .OrderByDescending(x => x.Date)
                .Select(x =>
                            new GridRowModel()
                            {
                                Id = x.Id,
                                Date = x.Date,
                                Category = x.ExpenseCategory.Name,
                                Remark = x.Note,
                                Amount = x.Amount
                            })
                            .ToList();
            var total = list.Sum(x => x.Amount);
            list.Add(new GridRowModel() { Id = -1, Remark = "Total", Amount = total });
            grdExpense.DataSource = list;
        }

        void bindCategory()
        {
            var list = db.ExpenseCategories.Where(x => x.IsActive == true).Select(x => new { Name = x.Name, Id = x.Id }).ToList();
            cmbCategory.DataSource = list;
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "Id";
        }

        private void grdExpense_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.RowIndex >= 0)
            {
                var id = Convert.ToInt64(senderGrid.Rows[e.RowIndex].Cells[0].Value);
                if (id > 0)
                {
                    frmExpenseEdit frm = new frmExpenseEdit() { Mode = Models.EntryMode.Edit, ExpenseId = id };
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        bindGrid();
                    }
                }
            }
        }





        class GridRowModel
        {
            public long Id { get; set; }
            public DateTime? Date { get; set; }
            public string Category { get; set; }
            public string Remark { get; set; }
            public decimal? Amount { get; set; }
        }
    }
}
