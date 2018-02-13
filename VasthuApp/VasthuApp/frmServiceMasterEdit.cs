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
    public partial class frmServiceMasterEdit : Form
    {
        VasthuDBEntities db = null;

        public frmServiceMasterEdit()
        {
            InitializeComponent();
            db = new VasthuDBEntities();
        }

        public EntryMode Mode { get; internal set; }
        public long ServiceId { get; internal set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmServiceMasterEdit_Load(object sender, EventArgs e)
        {
            if (Mode == EntryMode.Edit)
            {
                txtName.Text = db.ServiceMasters.FirstOrDefault(x => x.Id == ServiceId).Name;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateName())
            {
                if (Mode == EntryMode.New)
                {
                    ServiceMaster service = new ServiceMaster()
                    {
                        IsActive = true,
                        Name = txtName.Text.Trim()
                    };
                    db.ServiceMasters.Add(service);
                    db.SaveChanges();
                    MessageBox.Show("Saved", "Success", MessageBoxButtons.OK);
                    DialogResult = DialogResult.Yes;
                }
                else
                {

                    var service = db.ServiceMasters.FirstOrDefault(x => x.Id == ServiceId);
                    if (service != null)
                    {
                        service.Name = txtName.Text.Trim();
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
                    isNameExists = db.ServiceMasters.Any(x => x.Name.Equals(txtName.Text.Trim()) && x.IsActive == true);
                else
                    isNameExists = db.ServiceMasters.Any(x => x.Name.Equals(txtName.Text.Trim()) && x.IsActive == true && x.Id == ServiceId);

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.ServiceMasters.FirstOrDefault(x => x.Id == ServiceId).IsActive = false;
                db.SaveChanges();
                DialogResult = DialogResult.Yes;
            }
        }
    }

}
