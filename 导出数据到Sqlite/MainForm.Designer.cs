namespace com.echo.ios
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.sqlSvrDataSet = new com.echo.ios.SqlSvrDataSet();
            this.tbl_CompenyTableAdapter = new com.echo.ios.SqlSvrDataSetTableAdapters.tbl_CompenyTableAdapter();
            this.tbl_gbjmTableAdapter = new com.echo.ios.SqlSvrDataSetTableAdapters.tbl_gbjmTableAdapter();
            this.tbl_Relate_gbjmTableAdapter = new com.echo.ios.SqlSvrDataSetTableAdapters.tbl_Relate_gbjmTableAdapter();
            this.tbl_unitTableAdapter = new com.echo.ios.gbmcDSTableAdapters.tbl_unitTableAdapter();
            this.gbmcDS = new com.echo.ios.gbmcDS();
            this.tbl_gbmcTableAdapter = new com.echo.ios.gbmcDSTableAdapters.tbl_gbmcTableAdapter();
            this.tbl_RelateTableAdapter = new com.echo.ios.gbmcDSTableAdapters.tbl_RelateTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.sqlSvrDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbmcDS)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::com.echo.ios.Properties.Settings.Default, "STR_CHECKSQL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label1.Location = new System.Drawing.Point(43, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = global::com.echo.ios.Properties.Settings.Default.STR_CHECKSQL;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::com.echo.ios.Properties.Settings.Default, "STR_CHECKSQLITE", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label2.Location = new System.Drawing.Point(45, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = global::com.echo.ios.Properties.Settings.Default.STR_CHECKSQLITE;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(47, 146);
            this.progressBar1.Maximum = 4;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(490, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::com.echo.ios.Properties.Settings.Default, "STR_EXPORT", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(135, 240);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 43);
            this.button1.TabIndex = 3;
            this.button1.Text = global::com.echo.ios.Properties.Settings.Default.STR_EXPORT;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::com.echo.ios.Properties.Settings.Default, "STR_EXIT", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btn_Exit.Location = new System.Drawing.Point(319, 240);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(111, 43);
            this.btn_Exit.TabIndex = 4;
            this.btn_Exit.Text = global::com.echo.ios.Properties.Settings.Default.STR_EXIT;
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.OnExit);
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(47, 197);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(490, 23);
            this.progressBar2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 12);
            this.label4.TabIndex = 7;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "DB";
            this.saveFileDialog1.Filter = "数据库文件 (*.DB)|*.DB";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // sqlSvrDataSet
            // 
            this.sqlSvrDataSet.DataSetName = "SqlSvrDataSet";
            this.sqlSvrDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tbl_CompenyTableAdapter
            // 
            this.tbl_CompenyTableAdapter.ClearBeforeFill = true;
            // 
            // tbl_gbjmTableAdapter
            // 
            this.tbl_gbjmTableAdapter.ClearBeforeFill = true;
            // 
            // tbl_Relate_gbjmTableAdapter
            // 
            this.tbl_Relate_gbjmTableAdapter.ClearBeforeFill = true;
            // 
            // tbl_unitTableAdapter
            // 
            this.tbl_unitTableAdapter.ClearBeforeFill = true;
            // 
            // gbmcDS
            // 
            this.gbmcDS.DataSetName = "gbmcDS";
            this.gbmcDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tbl_gbmcTableAdapter
            // 
            this.tbl_gbmcTableAdapter.ClearBeforeFill = true;
            // 
            // tbl_RelateTableAdapter
            // 
            this.tbl_RelateTableAdapter.ClearBeforeFill = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 314);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::com.echo.ios.Properties.Settings.Default, "STR_TITLE", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = global::com.echo.ios.Properties.Settings.Default.STR_TITLE;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.sqlSvrDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbmcDS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_Exit;
        private com.echo.ios.SqlSvrDataSetTableAdapters.tbl_CompenyTableAdapter tbl_CompenyTableAdapter;
        private SqlSvrDataSet sqlSvrDataSet;
        private com.echo.ios.gbmcDSTableAdapters.tbl_unitTableAdapter tbl_unitTableAdapter;
        private gbmcDS gbmcDS;
        private com.echo.ios.SqlSvrDataSetTableAdapters.tbl_gbjmTableAdapter tbl_gbjmTableAdapter;
        private com.echo.ios.gbmcDSTableAdapters.tbl_gbmcTableAdapter tbl_gbmcTableAdapter;
        private com.echo.ios.SqlSvrDataSetTableAdapters.tbl_Relate_gbjmTableAdapter tbl_Relate_gbjmTableAdapter;
        private com.echo.ios.gbmcDSTableAdapters.tbl_RelateTableAdapter tbl_RelateTableAdapter;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer1;
    }
}

