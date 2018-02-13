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
    public partial class frmExpenseCategoryEdit : Form
    {
        public EntryMode Mode { get; set; }
        public long expenseId { get; set; }
        VasthuDBEntities db = null;
        public frmExpenseCategoryEdit()
        {
            InitializeComponent();
            db = new VasthuDBEntities();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateName())
            {
                if (Mode == EntryMode.New)
                {
                    ExpenseCategory expense = new ExpenseCategory()
                    {
                        IsActive = true,
                        Name = txtName.Text.Trim()
                    };
                    db.ExpenseCategories.Add(expense);
                    db.SaveChanges();
                    MessageBox.Show("Saved", "Success", MessageBoxButtons.OK);
                    DialogResult = DialogResult.Yes;
                }
                else
                {

                    var expense = db.ExpenseCategories.FirstOrDefault(x => x.Id == expenseId);
                    if (expense != null)
                    {
                        expense.Name = txtName.Text.Trim();
                        db.SaveChanges();
                        MessageBox.Show("Saved", "Success", MessageBoxButtons.OK);
                        DialogResult = DialogResult.Yes;
                    }
                    else
                    {
                        MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.No;
                    }
                }
            }
        }

        private bool ValidateName()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Name required!");
                return false;
            }

            try
            {
                bool isNameExists;
                if (Mode == EntryMode.New)
                    isNameExists = db.ExpenseCategories.Any(x => x.Name.Equals(txtName.Text.Trim()) && x.IsActive == true);
                else
                    isNameExists = db.ExpenseCategories.Any(x => x.Name.Equals(txtName.Text.Trim()) && x.IsActive == true && x.Id == expenseId);

                if (isNameExists)
                {
                    MessageBox.Show("Name already exists !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                    return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
        }

        private void frmExpenseCategoryEdit_Load(object sender, EventArgs e)
        {
            if (Mode == EntryMode.Edit)
            {
                txtName.Text = db.ExpenseCategories.FirstOrDefault(x => x.Id == expenseId).Name;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.ExpenseCategories.FirstOrDefault(x => x.Id == expenseId).IsActive = false;
                db.SaveChanges();
                DialogResult = DialogResult.Yes;
            }
        }
    }
}
