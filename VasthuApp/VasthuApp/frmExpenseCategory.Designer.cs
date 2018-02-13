namespace VasthuApp
{
    partial class frmExpenseCategory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grdExpenseMaster = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expenses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdExpenseMaster)).BeginInit();
            this.SuspendLayout();
            // 
            // grdExpenseMaster
            // 
            this.grdExpenseMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdExpenseMaster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Expenses});
            this.grdExpenseMaster.Location = new System.Drawing.Point(12, 45);
            this.grdExpenseMaster.Name = "grdExpenseMaster";
            this.grdExpenseMaster.Size = new System.Drawing.Size(451, 403);
            this.grdExpenseMaster.TabIndex = 0;
            this.grdExpenseMaster.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdExpenseMaster_CellDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // Expenses
            // 
            this.Expenses.DataPropertyName = "Name";
            this.Expenses.HeaderText = "Expenses";
            this.Expenses.Name = "Expenses";
            this.Expenses.ReadOnly = true;
            this.Expenses.Width = 400;
            // 
            // btnNew
            // 
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Location = new System.Drawing.Point(388, 12);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // frmExpenseCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 460);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.grdExpenseMaster);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExpenseCategory";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Expenses";
            this.Load += new System.EventHandler(this.frmExpenseCategory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdExpenseMaster)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdExpenseMaster;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expenses;
    }
}