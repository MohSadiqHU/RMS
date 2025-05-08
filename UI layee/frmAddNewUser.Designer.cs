namespace ResturantManagmentSystemUILayer
{
    partial class frmAddNewUser
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
            this.dgvEmployee = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPostion = new System.Windows.Forms.ComboBox();
            this.btnChoose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEmployee
            // 
            this.dgvEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployee.Location = new System.Drawing.Point(83, 231);
            this.dgvEmployee.Name = "dgvEmployee";
            this.dgvEmployee.Size = new System.Drawing.Size(824, 239);
            this.dgvEmployee.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Eras Medium ITC", 20.25F);
            this.label1.Location = new System.Drawing.Point(399, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Chose Employee";
            // 
            // cbPostion
            // 
            this.cbPostion.AllowDrop = true;
            this.cbPostion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cbPostion.Font = new System.Drawing.Font("Eras Medium ITC", 20.25F);
            this.cbPostion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cbPostion.FormattingEnabled = true;
            this.cbPostion.Items.AddRange(new object[] {
            "All",
            "Maneger",
            "Cashier",
            "Chef",
            "Purchasing Manager",
            "Waiter",
            "Cleaner"});
            this.cbPostion.Location = new System.Drawing.Point(606, 111);
            this.cbPostion.Name = "cbPostion";
            this.cbPostion.Size = new System.Drawing.Size(209, 39);
            this.cbPostion.TabIndex = 16;
            this.cbPostion.SelectedIndexChanged += new System.EventHandler(this.cbPostion_SelectedIndexChanged);
            // 
            // btnChoose
            // 
            this.btnChoose.Font = new System.Drawing.Font("Eras Medium ITC", 20.25F);
            this.btnChoose.Location = new System.Drawing.Point(219, 111);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(131, 57);
            this.btnChoose.TabIndex = 17;
            this.btnChoose.Text = "Choose";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // frmAddNewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 509);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.cbPostion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvEmployee);
            this.Name = "frmAddNewUser";
            this.Text = "frmAddNewUser";
            this.Load += new System.EventHandler(this.frmAddNewUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEmployee;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPostion;
        private System.Windows.Forms.Button btnChoose;
    }
}