using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Data.SQLite;

using System.Drawing.Imaging;



namespace com.echo.ios
{

    public partial class MainForm : Form
    {
        bool canDo = false;
        DateTime startTime;
        string beginTime = "";
        int second = 0;
        
        public bool CanDo
        {
            get { return canDo; }
            set { canDo = value; }
        }

        bool sqlOK = false;
        public bool SqlOK
        {
            get { return sqlOK; }
            set { sqlOK = value; }
        }

        bool sdfOK = false;
        public bool SdfOK
        {
            get { return sdfOK; }
            set { sdfOK = value; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void OnExit(object sender, EventArgs e)
        {
            Close();
        }

        private void ChkSql()
        {
            try
            {
                tbl_CompenyTableAdapter.Fill(sqlSvrDataSet.tbl_Compeny);
                label1.Text = Properties.Settings.Default.STR_CHKSQLOK;
                sqlOK = true;
            }
            catch
            {
                label1.Text = Properties.Settings.Default.STR_CHKSQLERROR;
            }
        }

        private void ChkSqlite()
        {
            try
            {
                tbl_unitTableAdapter.Fill(gbmcDS.tbl_unit);
                label2.Text = Properties.Settings.Default.STR_CHKSQLITEOK;
                sdfOK = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                label2.Text = Properties.Settings.Default.STR_CHKSQLITEERROR;
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            ChkSql();
            ChkSqlite();

            if (sqlOK && sdfOK)
            {
                canDo = true;
                button1.Enabled = true;
            }
            else
            {
                canDo = false;
                button1.Enabled = false;
            }

            tbl_gbjmTableAdapter.Fill(sqlSvrDataSet.tbl_gbjm);
            tbl_Relate_gbjmTableAdapter.Fill(sqlSvrDataSet.tbl_Relate_gbjm);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            timer1.Enabled = true;
            Thread runTaskThread = new Thread(new ThreadStart(RunTask));
            runTaskThread.Start();
        }



        delegate void SaveDBDelegate();

        private void SaveDB()
        {
            MessageBox.Show(Properties.Settings.Default.STR_SYNC_OK);
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.Copy(Application.StartupPath+ @"\" +Properties.Settings.Default.STR_DB_FILE, saveFileDialog1.FileName,true);
                MessageBox.Show("保存成功！");
            }
        }

        private void RunTask()
        {

            startTime = DateTime.Now;
            beginTime = DateTime.Now.ToString("HH:mm:ss");

            Cursor.Current = Cursors.WaitCursor;
            SetLabel(label3, Properties.Settings.Default.STR_SYNC_UNIT);
            ShowProgress(progressBar1, sqlSvrDataSet.tbl_Compeny.Rows.Count + sqlSvrDataSet.tbl_gbjm.Rows.Count * 2 + sqlSvrDataSet.tbl_Relate_gbjm.Rows.Count, 0);

            sync_unit();
            sqlSvrDataSet.tbl_Compeny.Dispose();
            gbmcDS.tbl_unit.Dispose();

            SetLabel(label3, Properties.Settings.Default.STR_SYNC_GBMC);

            sync_gbmc();
            sqlSvrDataSet.tbl_gbjm.Dispose();

            try
            {
                getPhoto();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            sqlSvrDataSet.tbl_gbjm.Dispose();
            gbmcDS.tbl_gbmc.Dispose();

            SetLabel(label3, Properties.Settings.Default.STR_SYNC_RELATE);


            sync_Relate();

            SetLabel(label3, Properties.Settings.Default.STR_SYNC_OVER);
            Cursor.Current = Cursors.Default;
            SaveDBDelegate saveDB = new SaveDBDelegate(SaveDB);
            this.BeginInvoke(saveDB);
            timer1.Enabled = false;
        }

        //同步单位表
        private void sync_unit()
        {
            gbmcDS.tbl_unit.Clear();
            tbl_unitTableAdapter.DeleteAll();

            int i = 1;
            ShowProgress(progressBar2,sqlSvrDataSet.tbl_Compeny.Rows.Count,0);

            int id = 1;
            int pid = 0;
            string unitName;
            string expression;
            DataRow[] rows;

            foreach (DataRow row in sqlSvrDataSet.tbl_Compeny)
            {
                SetLabel(label4, String.Format(Properties.Settings.Default.STR_SYNC_PROCESS, "单位树表", i.ToString(), progressBar2.Maximum.ToString()));
                ShowProgress(progressBar2, progressBar2.Maximum, i);
                ShowProgress(progressBar1, progressBar1.Maximum, progressBar1.Value + 1);

                i++;

                pid = 0;
                unitName = row["二级"].ToString();
                expression = "name='" + unitName + "' and pid=" + pid.ToString();

                rows = gbmcDS.tbl_unit.Select(expression);
                if (rows.Count() == 0)
                {
                    gbmcDS.tbl_unit.Rows.Add(id++, unitName, pid); //加入一级单位        
                }

                unitName = row["四级"].ToString();
                pid = getPidByUnitName(row["二级"].ToString());

                expression = "name='" + unitName + "' and pid=" + pid.ToString();

                rows = gbmcDS.tbl_unit.Select(expression);
                if (rows.Count() == 0)
                {
                    gbmcDS.tbl_unit.Rows.Add(id++, unitName, pid); //加入四级单位
                }

                tbl_unitTableAdapter.Update(gbmcDS.tbl_unit);
            }

            //sqlSvrDataSet.tbl_Compeny.Clear();
            //gbmcDS.tbl_unit.Clear();
        }

        //取得上级单位id
        private int getPidByUnitName(string unitName)
        {
            string exprssion = "name='" + unitName + "'";
            DataRow[] rows = gbmcDS.tbl_unit.Select(exprssion);
            if (rows.Count() > 0)
            {
                DataRow row = rows[0];
                return int.Parse(row[0].ToString());
            }
            else
            {
                return 0;
            }
        }

        //同步干部表
        private void sync_gbmc()
        {
            gbmcDS.tbl_gbmc.Clear();
            tbl_gbmcTableAdapter.DeleteAll();

            int i = 1;
            ShowProgress(progressBar2, sqlSvrDataSet.tbl_gbjm.Rows.Count, 0);

            DataRow gRow;

            tbl_gbjmTableAdapter.Fill(sqlSvrDataSet.tbl_gbjm);
            foreach (DataRow row in sqlSvrDataSet.tbl_gbjm)
            {
                SetLabel(label4, String.Format(Properties.Settings.Default.STR_SYNC_PROCESS, "领导干部表", i.ToString(), progressBar2.Maximum.ToString()));
                ShowProgress(progressBar2, progressBar2.Maximum, i);
                ShowProgress(progressBar1, progressBar1.Maximum, progressBar1.Value+1);

                gRow = gbmcDS.tbl_gbmc.NewRow();
                gRow["id"] = row["id"];
                gRow["姓名"] = row["姓名"];
                gRow["单位代码"] = getUnitIdByName(row["单位代码"].ToString());
                gRow["职务"] = row["职务"];
                gRow["性别"] = row["性别"];
                gRow["出生年月"] = row["出生年月"];
                gRow["年龄"] = row["年龄"];
                gRow["入党时间"] = row["入党时间"];
                gRow["参加工作时间"] = row["参加工作时间"];
                gRow["文化程度"] = row["文化程度"];
                gRow["毕业院校"] = row["毕业院校"];
                gRow["籍贯"] = row["籍贯"];
                gRow["民族"] = row["民族"];
                gRow["出生地"] = row["出生地"];
                gRow["职级"] = row["职级"];
                gRow["在职教育"] = row["在职教育"];
                gRow["在职毕业院校"] = row["在职毕业院校"];
                gRow["简历"] = row["简历"];
                gRow["奖惩情况"] = row["奖惩情况"];
                gRow["年度考核"] = row["年度考核"];
                gRow["党校培训"] = row["党校培训"];
                gRow["呈报单位"] = row["呈报单位"];
                gRow["审批机关"] = row["审批机关"];
                gRow["行政机关"] = row["行政机关"];
                gRow["序号"] = row["序号"];

                gbmcDS.tbl_gbmc.Rows.Add(gRow);
                tbl_gbmcTableAdapter.Update(gbmcDS.tbl_gbmc);
                i++;
                gRow = null;
              }

        }

        //取得单位代码
        private int getUnitIdByName(string unitName)
        {
            string expression = "name='" + unitName + "'";
            DataRow[] rows = gbmcDS.tbl_unit.Select(expression);
            if (rows.Count() > 0)
            {
                return int.Parse(rows[0][0].ToString());
            }
            else
            {
                return 999;
            }
        }

        //取得照片
        private void getPhoto()
        {
            int i = 1;
            ShowProgress(progressBar2, gbmcDS.tbl_gbmc.Rows.Count, 0);
            SetLabel(label3, "同步照片表数据");

            DataRow gRow;
            foreach (DataRow row in gbmcDS.tbl_gbmc)
            {
                SetLabel(label4, String.Format(Properties.Settings.Default.STR_SYNC_PROCESS, "照片表", i.ToString(), progressBar2.Maximum.ToString()));
                ShowProgress(progressBar2, progressBar2.Maximum, i);
                ShowProgress(progressBar1, progressBar1.Maximum, progressBar1.Value + 1);

                i++;

                gRow = getPhontoRowById(int.Parse(row["id"].ToString()));
                Console.WriteLine("id=" + int.Parse(row["id"].ToString()));

                if (!DBNull.Value.Equals(gRow["相片"]))
                {
                    byte[] imagebytes = null;
                    imagebytes = (byte[])gRow["相片"];
                    if (imagebytes.Length > 1024000)
                    {
                        row["相片"] = GetCompressImage(imagebytes);
                    }
                    else
                    {
                        row["相片"] = gRow["相片"];
                    }
                }
                
                tbl_gbmcTableAdapter.Update(gbmcDS.tbl_gbmc);
                gRow = null;
            }
           // sqlSvrDataSet.tbl_gbjm.Clear();
            //gbmcDS.tbl_gbmc.Clear();
        }

        private DataRow getPhontoRowById(int id)
        {
            tbl_gbjmTableAdapter.FillPhotoByID(sqlSvrDataSet.tbl_gbjm, id);
            return sqlSvrDataSet.tbl_gbjm.Rows[0];
        }

        //同步亲属关系
        private void sync_Relate()
        {
            gbmcDS.tbl_Relate.Clear();
            tbl_RelateTableAdapter.DeleteAll();

            int i = 1;
            ShowProgress(progressBar2, sqlSvrDataSet.tbl_Relate_gbjm.Rows.Count, 0);

            foreach (DataRow row in sqlSvrDataSet.tbl_Relate_gbjm)
            {
                SetLabel(label4, String.Format(Properties.Settings.Default.STR_SYNC_PROCESS, "亲属关系表", i.ToString(), progressBar2.Maximum.ToString()));

                ShowProgress(progressBar2, progressBar2.Maximum, i++);
                ShowProgress(progressBar1, progressBar1.Maximum, progressBar1.Value + 1);

                gbmcDS.tbl_Relate.Rows.Add(row.ItemArray);
                tbl_RelateTableAdapter.Update(gbmcDS.tbl_Relate);
            }
           // sqlSvrDataSet.tbl_Relate_gbjm.Clear();
           //gbmcDS.tbl_Relate.Clear();
        }

        // 显示进度条的委托声明
        delegate void ShowProgressDelegate(ProgressBar bar, int totalStep, int currentStep);
                
        // 显示进度条
        void ShowProgress(ProgressBar bar,int totalStep, int currentStep)
        {
            if (bar.InvokeRequired)
            {
                ShowProgressDelegate showProgress = new ShowProgressDelegate(ShowProgress);
                this.BeginInvoke(showProgress,new object[]{bar,totalStep,currentStep});
            }
            else
            {
                bar.Maximum = totalStep;
                bar.Value = currentStep;
            }
        }
        
        //显示内容委托声明
        delegate void SetLabelDelegate(Label lab, string t);

        void SetLabel(Label lab, string t)
        {
            if (lab.InvokeRequired)
            {
                SetLabelDelegate setLabel = new SetLabelDelegate(SetLabel);
                this.BeginInvoke(setLabel, lab, t);
            }
            else
            {
                lab.Text = t;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (startTime != null)
            {
                second++;

                string r = "开始时间：" + beginTime + "，已运行" + second.ToString() +"秒";

                SetLabel(label5, r);
            }
        }

        //照片处理
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private byte[] GetCompressImage(byte[] sourceImg)
        {
            // Get a bitmap.
            //Bitmap bmp1 = new Bitmap(@"c:\1.jpg");
            //ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            //System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            //EncoderParameters myEncoderParameters = new EncoderParameters(1);
            //EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            //myEncoderParameters.Param[0] = myEncoderParameter;
            //bmp1.Save(@"d:\1.jpg", jgpEncoder, myEncoderParameters);

            //改变大小
            //System.Drawing.Image b = new System.Drawing.Bitmap(@"c:\1.jpg");
            MemoryStream msSource = new MemoryStream(sourceImg);
            Bitmap sImage = new Bitmap(msSource);
            Bitmap tImage = new Bitmap(sImage, 210, 280);


            MemoryStream msTarget = new MemoryStream();
            tImage.Save(msTarget, ImageFormat.Jpeg);
            byte[] byteImage = new Byte[msTarget.Length];
            byteImage = msTarget.ToArray();
            return byteImage;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


    }
}
