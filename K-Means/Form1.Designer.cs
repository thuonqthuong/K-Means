
namespace K_Means
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.cblColumns = new System.Windows.Forms.CheckedListBox();
            this.medicineBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.k = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.databasesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.masterDataSet = new K_Means.masterDataSet();
            this.databasesTableAdapter = new K_Means.masterDataSetTableAdapters.databasesTableAdapter();
            this.tableAdapterManager2 = new K_Means.masterDataSetTableAdapters.TableAdapterManager();
            this.databasesGridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.databasesBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bang = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.phancum = new DevExpress.XtraEditors.PanelControl();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.results = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.sz = new System.Windows.Forms.TextBox();
            this.a = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.medicineBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.databasesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.masterDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.databasesGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.databasesBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phancum)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cblColumns
            // 
            this.cblColumns.CheckOnClick = true;
            this.cblColumns.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cblColumns.FormattingEnabled = true;
            this.cblColumns.Location = new System.Drawing.Point(505, 28);
            this.cblColumns.Name = "cblColumns";
            this.cblColumns.Size = new System.Drawing.Size(181, 280);
            this.cblColumns.TabIndex = 5;
            this.cblColumns.SelectedIndexChanged += new System.EventHandler(this.cblColumns_SelectedIndexChanged);
            // 
            // medicineBindingSource
            // 
            this.medicineBindingSource.DataMember = "Medicine";
            // 
            // k
            // 
            this.k.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.k.Location = new System.Drawing.Point(550, 348);
            this.k.Name = "k";
            this.k.Size = new System.Drawing.Size(32, 28);
            this.k.TabIndex = 6;
            this.k.Text = "2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(402, 351);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nhập số cụm k:";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button3.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.DarkBlue;
            this.button3.Location = new System.Drawing.Point(619, 340);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(134, 43);
            this.button3.TabIndex = 10;
            this.button3.Text = "PHÂN CỤM";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_MouseClick);
            // 
            // databasesBindingSource
            // 
            this.databasesBindingSource.DataMember = "databases";
            this.databasesBindingSource.DataSource = this.masterDataSet;
            // 
            // masterDataSet
            // 
            this.masterDataSet.DataSetName = "masterDataSet";
            this.masterDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // databasesTableAdapter
            // 
            this.databasesTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager2
            // 
            this.tableAdapterManager2.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager2.Connection = null;
            this.tableAdapterManager2.UpdateOrder = K_Means.masterDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // databasesGridControl
            // 
            this.databasesGridControl.DataSource = this.databasesBindingSource;
            this.databasesGridControl.Location = new System.Drawing.Point(52, 28);
            this.databasesGridControl.MainView = this.gridView2;
            this.databasesGridControl.Name = "databasesGridControl";
            this.databasesGridControl.Size = new System.Drawing.Size(181, 292);
            this.databasesGridControl.TabIndex = 11;
            this.databasesGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colname});
            this.gridView2.GridControl = this.databasesGridControl;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView2_RowClick);
            // 
            // colname
            // 
            this.colname.Caption = "Danh sách CSDL";
            this.colname.FieldName = "name";
            this.colname.MinWidth = 25;
            this.colname.Name = "colname";
            this.colname.Visible = true;
            this.colname.VisibleIndex = 0;
            this.colname.Width = 94;
            // 
            // databasesBindingSource1
            // 
            this.databasesBindingSource1.DataMember = "databases";
            this.databasesBindingSource1.DataSource = this.masterDataSet;
            // 
            // bang
            // 
            this.bang.CheckOnClick = true;
            this.bang.FormattingEnabled = true;
            this.bang.Location = new System.Drawing.Point(278, 28);
            this.bang.Name = "bang";
            this.bang.Size = new System.Drawing.Size(180, 292);
            this.bang.TabIndex = 12;
            this.bang.SelectedIndexChanged += new System.EventHandler(this.bang_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(48, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 24);
            this.label2.TabIndex = 16;
            this.label2.Text = "1. CSDL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(501, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 24);
            this.label3.TabIndex = 17;
            this.label3.Text = "3. CỘT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(274, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 24);
            this.label4.TabIndex = 18;
            this.label4.Text = "2. BẢNG";
            // 
            // phancum
            // 
            this.phancum.Location = new System.Drawing.Point(51, 402);
            this.phancum.Name = "phancum";
            this.phancum.Size = new System.Drawing.Size(589, 535);
            this.phancum.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(901, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(210, 24);
            this.label5.TabIndex = 20;
            this.label5.Text = "DỮ LIỆU PHÂN CỤM";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.results);
            this.panel1.Location = new System.Drawing.Point(666, 402);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(596, 535);
            this.panel1.TabIndex = 21;
            // 
            // results
            // 
            this.results.Dock = System.Windows.Forms.DockStyle.Fill;
            this.results.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.results.Location = new System.Drawing.Point(0, 0);
            this.results.Multiline = true;
            this.results.Name = "results";
            this.results.Size = new System.Drawing.Size(596, 535);
            this.results.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.Location = new System.Drawing.Point(717, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(545, 280);
            this.dataGridView1.TabIndex = 24;
            // 
            // sz
            // 
            this.sz.Location = new System.Drawing.Point(52, 375);
            this.sz.Name = "sz";
            this.sz.Size = new System.Drawing.Size(119, 23);
            this.sz.TabIndex = 26;
            this.sz.Text = "1";
            // 
            // a
            // 
            this.a.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.a.Name = "a";
            this.a.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1306, 949);
            this.Controls.Add(this.sz);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.phancum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bang);
            this.Controls.Add(this.databasesGridControl);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.k);
            this.Controls.Add(this.cblColumns);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.medicineBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.databasesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.masterDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.databasesGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.databasesBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phancum)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckedListBox cblColumns;
        private System.Windows.Forms.BindingSource medicineBindingSource;
        private System.Windows.Forms.TextBox k;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private masterDataSet masterDataSet;
        private System.Windows.Forms.BindingSource databasesBindingSource;
        private masterDataSetTableAdapters.databasesTableAdapter databasesTableAdapter;
        private masterDataSetTableAdapters.TableAdapterManager tableAdapterManager2;
        private DevExpress.XtraGrid.GridControl databasesGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colname;
        private System.Windows.Forms.BindingSource databasesBindingSource1;
        private System.Windows.Forms.CheckedListBox bang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.PanelControl phancum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox results;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox sz;
        private System.Windows.Forms.ContextMenuStrip a;
    }
}

