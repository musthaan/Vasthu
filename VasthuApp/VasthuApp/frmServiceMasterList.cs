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
    public partial class frmServiceMasterList : Form
    {

        VasthuDBEntities db = null;
        public frmServiceMasterList()
        {
            InitializeComponent();
            db = new VasthuDBEntities();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmServiceMasterEdit frm = new frmServiceMasterEdit() { Mode = Models.EntryMode.New, ServiceId = 0 };
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                BindGrid();
            }

        }



        private void frmServiceMasterList_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void BindGrid()
        {
            grdServices.DataSource = db.ServiceMasters.Where(x => x.IsActive == true).Select(x => new { Name = x.Name, Id = x.Id }).ToList();
        }


        private void grdServices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.RowIndex >= 0)
            {
                var id = Convert.ToInt64(senderGrid.Rows[e.RowIndex].Cells[1].Value);
                frmServiceMasterEdit frm = new frmServiceMasterEdit() { Mode = Models.EntryMode.Edit, ServiceId = id};
                if (frm.ShowDialog() == DialogResult.Yes)
                {
                    BindGrid();
                }
            }
        }
    }
}
