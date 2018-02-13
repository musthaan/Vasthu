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
    public partial class frmExpenseEdit : Form
    {
        Models.VasthuDBEntities db = new Models.VasthuDBEntities();
        public EntryMode Mode { get; set; }
        public long ExpenseId { get; set; }

        public frmExpenseEdit()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                try
                {
                    if (Mode == EntryMode.New)
                    {
                        Expense model = new Expense()
                        {
                            Amount = Convert.ToDecimal(txtAmount.Text.Trim()),
                            CategoryId = Convert.ToInt64(cmbCategory.SelectedValue),
                            Date = dtpExpense.Value,
                            IsDelete = false,
                            Note = txtNote.Text.Trim()
                        };

                        db.Expenses.Add(model);
                    }
                    else
                    {
                        var model = db.Expenses.FirstOrDefault(x => x.Id == ExpenseId);
                        if (model == null)
                        {
                            MessageBox.Show("Invalid !");
                            this.Close();
                        }
                        else
                        {
                            model.Date = dtpExpense.Value;
                            model.CategoryId = Convert.ToInt64(cmbCategory.SelectedValue);
                            model.Amount = Convert.ToDecimal(txtAmount.Text.Trim());
                            model.Note = txtNote.Text.Trim();
                        }
                       
                    }
                    db.SaveChanges();
                    MessageBox.Show("Saved Successfully!");
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Mode == EntryMode.Edit)
                {
                    if (MessageBox.Show("Are you sure to delete ?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var item = db.Expenses.FirstOrDefault(x => x.Id == ExpenseId);
                        item.IsDelete = true;
                        db.SaveChanges();
                        MessageBox.Show("Deleted Successfully !");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void clearForm()
        {
            dtpExpense.Value = DateTime.Now;
            dtpExpense.MaxDate = DateTime.Now;
            bindCategory();
            txtAmount.Text = "0.00";
            txtNote.Text = string.Empty;
            btnDelete.Enabled = ExpenseId > 0;
        }

        private void frmExpenseEdit_Load(object sender, EventArgs e)
        {
            clearForm();
            if (Mode == EntryMode.Edit && ExpenseId > 0) {
                var item =db.Expenses.FirstOrDefault(x => x.Id == ExpenseId && x.IsDelete == false);
                if (item == null)
                {
                    MessageBox.Show("Invalid !");
                    DialogResult = DialogResult.Cancel;
                    this.Close();
                }
                else {
                    dtpExpense.Value = item.Date.Value;
                    cmbCategory.SelectedValue = item.CategoryId;
                    txtAmount.Text = item.Amount.ToString();
                    txtNote.Text = item.Note;
                    dtpExpense.Focus();
                }
            }
        }
        void bindCategory()
        {
            var list = db.ExpenseCategories.Where(x => x.IsActive == true).Select(x => new { Name = x.Name, Id = x.Id }).ToList();
            cmbCategory.DataSource = list;
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "Id";
        }

        bool validate()
        {
            var result = true;
            decimal value;
            if (!decimal.TryParse(txtAmount.Text.Trim(), out value))
            {
                MessageBox.Show("Enter a valid amount");
                result = false;
            }

            return result;
        }
    }
}
