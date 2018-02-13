namespace VasthuApp
{
    partial class frmReceipt
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
            this.btnNameSearch = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.cmbService = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpServiceDate = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReferance = new System.Windows.Forms.Button();
            this.txtRef = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnNameSearch
            // 
            this.btnNameSearch.BackColor = System.Drawing.Color.White;
            this.btnNameSearch.BackgroundImage = global::VasthuApp.Properties.Resources.icon_search_grey_1x;
            this.btnNameSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNameSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNameSearch.Location = new System.Drawing.Point(302, 39);
            this.btnNameSearch.Name = "btnNameSearch";
            this.btnNameSearch.Size = new System.Drawing.Size(21, 19);
            this.btnNameSearch.TabIndex = 41;
            this.btnNameSearch.UseVisualStyleBackColor = false;
            this.btnNameSearch.Click += new System.EventHandler(this.btnNameSearch_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 277);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "Amount";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(95, 274);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(122, 20);
            this.txtAmount.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 215);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Note";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(95, 218);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(385, 50);
            this.txtNote.TabIndex = 33;
            // 
            // cmbService
            // 
            this.cmbService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbService.FormattingEnabled = true;
            this.cmbService.Location = new System.Drawing.Point(95, 191);
            this.cmbService.Name = "cmbService";
            this.cmbService.Size = new System.Drawing.Size(187, 21);
            this.cmbService.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Service";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(4, 181);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(727, 3);
            this.panel1.TabIndex = 37;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(95, 91);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(385, 84);
            this.txtAddress.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Address";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(95, 65);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 20);
            this.txtPhone.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Phone";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(95, 39);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 20);
            this.txtName.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Date";
            // 
            // dtpServiceDate
            // 
            this.dtpServiceDate.Location = new System.Drawing.Point(95, 12);
            this.dtpServiceDate.Name = "dtpServiceDate";
            this.dtpServiceDate.Size = new System.Drawing.Size(200, 20);
            this.dtpServiceDate.TabIndex = 25;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(176, 305);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Location = new System.Drawing.Point(95, 305);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 43;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(15, 305);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 42;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReferance
            // 
            this.btnReferance.BackColor = System.Drawing.Color.White;
            this.btnReferance.BackgroundImage = global::VasthuApp.Properties.Resources.icon_search_grey_1x;
            this.btnReferance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReferance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReferance.Location = new System.Drawing.Point(711, 28);
            this.btnReferance.Name = "btnReferance";
            this.btnReferance.Size = new System.Drawing.Size(21, 19);
            this.btnReferance.TabIndex = 52;
            this.btnReferance.UseVisualStyleBackColor = false;
            this.btnReferance.Click += new System.EventHandler(this.btnReferance_Click);
            // 
            // txtRef
            // 
            this.txtRef.Location = new System.Drawing.Point(603, 28);
            this.txtRef.Name = "txtRef";
            this.txtRef.Size = new System.Drawing.Size(106, 20);
            this.txtRef.TabIndex = 50;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(600, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 13);
            this.label8.TabIndex = 51;
            this.label8.Text = "Ref";
            // 
            // frmReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 335);
            this.Controls.Add(this.btnReferance);
            this.Controls.Add(this.txtRef);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNameSearch);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.cmbService);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpServiceDate);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReceipt";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Receipt";
            this.Load += new System.EventHandler(this.frmReceipt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNameSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.ComboBox cmbService;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpServiceDate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReferance;
        private System.Windows.Forms.TextBox txtRef;
        private System.Windows.Forms.Label label8;
    }
}