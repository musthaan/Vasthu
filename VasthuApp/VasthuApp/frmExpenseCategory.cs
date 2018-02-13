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
    public partial class frmExpenseCategory : Form
    {
        VasthuDBEntities db = null;
        public frmExpenseCategory()
        {
            InitializeComponent();
            db = new VasthuDBEntities();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmExpenseCategoryEdit frm = new frmExpenseCategoryEdit() { Mode = Models.EntryMode.New, expenseId = 0 };
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                BindGrid();
            }
        }

        private void grdExpenseMaster_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                var id = Convert.ToInt64(senderGrid.Rows[e.RowIndex].Cells[0].Value);
                if (e.ColumnIndex == 2)
                {
                    //EDIT

                }
                else if (e.ColumnIndex == 3)
                {
                    //DELETE

                }
            }
        }

        private void frmExpenseCategory_Load(object sender, EventArgs e)
        {
            BindGrid();

        }

        private void BindGrid()
        {
            grdExpenseMaster.DataSource = db.ExpenseCategories.Where(x => x.IsActive == true).Select(x => new { Name = x.Name, Id = x.Id }).ToList();
        }


        private void grdExpenseMaster_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.RowIndex >= 0)
            {
                var id = Convert.ToInt64(senderGrid.Rows[e.RowIndex].Cells[1].Value);

                frmExpenseCategoryEdit frm = new frmExpenseCategoryEdit() { Mode = Models.EntryMode.Edit, expenseId = id };
                if (frm.ShowDialog() == DialogResult.Yes)
                {
                    BindGrid();
                }
            }
        }
    }
}
