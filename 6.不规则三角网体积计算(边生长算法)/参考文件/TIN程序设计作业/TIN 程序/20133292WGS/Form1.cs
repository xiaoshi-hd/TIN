using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace _20133292WGS
{
    public partial class Form1 : Form
    {
        private string curFileName;
        private string TempStr1;
        private string[] TempStr2;
        private double[,] Value1,Value2,Value3;
        private _20133292WGS.DataStucture.Triangle[] Tri;
        private DataStucture.Line[] Line;
        Image img1;
        int o,K,wid,hig,k;
        private System.Drawing.Bitmap curBitmap;
        public Form1()
        {
            InitializeComponent();            
        }
        #region 文件打开
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.InitialDirectory = "E:\\sixth\\数字高程模型";
            openDlg.Filter = "所有文件(*.*)|*.*";
            openDlg.Title = "选择已知点文件";
            openDlg.ShowHelp = true;
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    curFileName = openDlg.FileName;
                    StreamReader myStreamReader = new StreamReader(curFileName, true);//创建文件流
                    TempStr1 = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    string[] a = new string[] { " ", ",", "\r" };//多号分隔先创建字符串
                    TempStr2 = TempStr1.Split(a, StringSplitOptions.RemoveEmptyEntries);//元素个数
                    Value1 = new double[TempStr2.Length/3,4];
                    Value3 = new double[TempStr2.Length/3,3];
                    int j = 0;
                    double max1=0, max2=0, min1=0, min2=0;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("X", typeof(String));
                    dt.Columns.Add("Y", typeof(String));
                    dt.Columns.Add("Z", typeof(String));
                    this.dataGridView1.DataSource = dt;                 //将datatable绑定到datagridview上显示结果  
                    for (int i = 0; i < TempStr2.Length; i=i+3)
                    {
                        Value1[j, 0] = j+1;
                        Value1[j, 1] = double.Parse(TempStr2[i]);
                        Value1[j, 2] = double.Parse(TempStr2[i + 1]);
                        Value1[j, 3] = double.Parse(TempStr2[i + 2]);
                        if(j==0)
                        {
                            max1 = Value1[j, 1];
                            min1 = Value1[j, 1];
                            max2 = Value1[j, 2];
                            min2 = Value1[j, 2];
                        }
                        else
                        {
                            if (max1 < Value1[j, 1])
                                max1 = Value1[j, 1];
                            if (max2 < Value1[j, 2])
                                max2 = Value1[j, 2];
                            if (min1 > Value1[j, 1])
                                min1 = Value1[j, 1];
                            if (min2 > Value1[j, 2])
                                min2 = Value1[j, 2];
                        }
                        
                       
                        DataRow dr = dt.NewRow();
                  
                        dr[0] = Value1[j,1];                         
                        dr[1] = Value1[j,2];
                        dr[2] = Value1[j,3];
                        dt.Rows.Add(dr);
                        j++;                                      
                    }
                    j = 0;
                    for (int i = 0; i < TempStr2.Length; i = i + 3)
                    {
                        Value3[j, 0] = j + 1;
                        Value3[j, 1] = 10 * (double.Parse(TempStr2[i]) - min1+5);
                        Value3[j, 2] = 10 * (double.Parse(TempStr2[i + 1]) - min2+5);
                        j++;
                    }
                    wid =(int)(10*( max2 - min2 + 10));
                    hig = (int)(10*(max1 - min1 + 10));
                    dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//单元格中文字对齐方式
                    dataGridView1.AllowUserToAddRows = false;
                    groupBox1.Text = "地形特征点坐标";
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }
        #endregion
        #region 行号生成
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)//每一行数据填充之后执行该语句
        {
            try
            {
                SolidBrush v_SolidBrush = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor);//设置笔刷的颜色
                int v_LineNo = 0;
                v_LineNo = e.RowIndex + 1;//单元格行索引加1
                string v_Line = v_LineNo.ToString();
                e.Graphics.DrawString(v_Line, e.InheritedRowStyle.Font, v_SolidBrush, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + 5);
                //绘制字符串（sting, font, brush, point）
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加行号时发生错误，错误信息：" + ex.Message, "操作失败");
            }
        }
        #endregion
        #region 三角网绘制
        private void 三角网生长算法ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.toolStripProgressBar1.Maximum = 20;//设置进度条的最大值
            this.toolStripProgressBar1.Value = 4;//设置进度条的值
            k=TempStr2.Length/3;//点个数
            Value2=new double[k,k];
            double Min=0,min2=0,midx,midy;
            int m=0, n=1,L=0,g=0,h=1;
            K = 0;
            int[] AA=new int[k];
            for(int i=0;i<k;i++)//计算两点间距离，找出距离最近的两个点
                for(int j=i+1;j<k;j++)
                {
                    Value2[i,j]=(Value1[i,1]-Value1[j,1])*(Value1[i,1]-Value1[j,1])+
                        (Value1[i,2]-Value1[j,2])*(Value1[i,2]-Value1[j,2]);
                    if (i == 0 && j == 1)
                        Min = Value2[0, 1];
                    else
                    {
                        if(Min>Value2[i,j])
                        {
                            m = i; n = j;
                            Min = Value2[i, j];//求一个最小值
                        }
                    }    
                }

            this.toolStripProgressBar1.Value = 6;
            Tri = new DataStucture.Triangle[k * (k -1)/2];
            Line = new DataStucture.Line[k * (k - 1) / 2];
            Tri[0] = new DataStucture.Triangle();
            Tri[0].Line[0] = new DataStucture.Line();
            Tri[0].Line[1] = new DataStucture.Line();
            Tri[0].Line[2] = new DataStucture.Line();
            
            Tri[0].ID = 1;
            Tri[0].Peak[0] = m;
            Tri[0].Peak[1] = n;
            
            Tri[0].Line[0].ID =1 ;
            Tri[0].Line[0].Point[0] = m;
            Tri[0].Line[0].Point[1] = n;

            Line[0] = new DataStucture.Line();
            Line[1] = new DataStucture.Line();
            Line[2] = new DataStucture.Line();
            Line[0].ID = 1;
            Line[0].Point[0]=m;
            Line[0].Point[1]=n;
            o = 1;
            this.toolStripProgressBar1.Value = 8;
            int q=0;
            midx = (Value1[m, 1] + Value1[n, 1]) / 2;
            midy = (Value1[m, 2] + Value1[n, 2]) / 2;

            for (int j = 0; j < k ; j++)
            {
                if ((j != m) && (j != n))
                {
                    Min = (midx - Value1[j, 1]) * (midx - Value1[j, 1]) + (midy - Value1[j, 2]) * (midy - Value1[j, 2]);
                    q = j;
                    break;
                }
            }
            for(int i=0;i<k;i++)
            {  
                if ((i != m) && (i != n) && ((midx - Value1[i, 1]) * (midx - Value1[i, 1]) + (midy - Value1[i, 2]) * (midy - Value1[i, 2]) < Min))
                {
                    Min = (midx - Value1[i, 1]) * (midx - Value1[i, 1]) + (midy - Value1[i, 2]) * (midy - Value1[i, 2]);
                    q = i;
                }   
            }
            Line[1].ID = 2;
            Line[1].Point[0] = Tri[0].Line[0].Point[1];
            Line[1].Point[1] = q;
            Line[2].ID = 3;
            Line[2].Point[0] = q;
            Line[2].Point[1] = Line[0].Point[0];
            Line[0].Bor[0] = Tri[0].ID;
            Line[1].Bor[0] = Tri[0].ID;
            Line[2].Bor[0] = Tri[0].ID;

            Tri[0].Peak[2] = q;
            Tri[0].Line[1] = Line[1];
            Tri[0].Line[2]=Line[2];
            
            this.toolStripProgressBar1.Value = 12;
            
            o = 3;
            L=1;
            K=0;
            ArrayList List1 = new ArrayList();
            ArrayList List2 = new ArrayList();
            DataStucture.Line[] Linetemp=new DataStucture.Line[2];
            DataProcess BB=new DataProcess();
            DataProcess CC=new DataProcess();
            while(K<L)
            {
                int d;
                d=CC.SS(BB, Tri, K, k, ref L, Value1,Line,ref o);
                K++;
            }

            this.toolStripProgressBar1.Value = 16;
            DataTable dt = new DataTable(); 
            dt.Columns.Add("顶点1", typeof(String));
            dt.Columns.Add("顶点2", typeof(String));
            dt.Columns.Add("顶点3", typeof(String));
            this.dataGridView1.DataSource = dt;                 //将datatable绑定到datagridview上显示结果  
            for (int i = 0; i < L; i++ )
            {     
                DataRow dr = dt.NewRow();
                dr[0] = Tri[i].Peak[0];
                dr[1] = Tri[i].Peak[1];
                dr[2] = Tri[i].Peak[2];
                dt.Rows.Add(dr);
                
            }
            dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AllowUserToAddRows = false;
            groupBox1.Text = "TIN三角形点号";

            curBitmap = new Bitmap(wid, hig);
            //myGraphics.Clear(Color.White); 
            for (int i = 0; i < K; i++)
            {
                Graphics myGraphics = Graphics.FromImage(curBitmap);    //创建Graphics对象
                myGraphics.DrawLine(new Pen(Color.Red, 1), new PointF((float)Value3[Tri[i].Peak[0], 2], (float)Value3[Tri[i].Peak[0], 1]), new PointF((float)Value3[Tri[i].Peak[1], 2], (float)Value3[Tri[i].Peak[1], 1]));
                myGraphics.DrawLine(new Pen(Color.Red, 1), new PointF((float)Value3[Tri[i].Peak[1], 2], (float)Value3[Tri[i].Peak[1], 1]), new PointF((float)Value3[Tri[i].Peak[2], 2], (float)Value3[Tri[i].Peak[2], 1]));
                myGraphics.DrawLine(new Pen(Color.Red, 1), new PointF((float)Value3[Tri[i].Peak[0], 2], (float)Value3[Tri[i].Peak[0], 1]), new PointF((float)Value3[Tri[i].Peak[2], 2], (float)Value3[Tri[i].Peak[2], 1]));                                                                                                
                myGraphics.Dispose();
                this.pictureBox1.Image = curBitmap;  
                
            }
            this.toolStripProgressBar1.Value = 20;
            this.toolStripProgressBar1.Value = 0;
        }
        #endregion
        #region 鼠标缩放事件
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//鼠标左键按下放大1.2倍
            {
                if (pictureBox1.Width < 5000)
                {
                    pictureBox1.Width = Convert.ToInt32(pictureBox1.Width * 1.2);
                    pictureBox1.Height = Convert.ToInt32(pictureBox1.Height * 1.2);
                }
            }
            if (e.Button == MouseButtons.Right)//鼠标右键按下缩小1.2倍
            {
                if (pictureBox1.Width > 100)
                {
                    pictureBox1.Width = Convert.ToInt32(pictureBox1.Width / 1.2);
                    pictureBox1.Height = Convert.ToInt32(pictureBox1.Height / 1.2);
                }
            }
        }
        #endregion
        #region 文件保存
        private void 拓扑关系ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog open = new SaveFileDialog();
            open.Filter = "文本文件（*.txt）|*.txt|所有（*.*）|*.*";//文件类型
            open.FilterIndex = 1;//默认值类型为第一项
            open.RestoreDirectory = true;
            open.InitialDirectory = "E:\\sixth\\数字高程模型";//默认输出路径
            string localFilePath = "";
            string fileNameExt = "";
            if (open.ShowDialog() == DialogResult.OK)
            {
                localFilePath = open.FileName.ToString(); //获得文件路径 
                //获取文件名，不带路径
                fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
            }
            if (localFilePath != "" && fileNameExt != "")
            {
                FileStream fs = new FileStream(localFilePath, FileMode.Create, FileAccess.Write);
                //通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));

                sw.WriteLine("                                   三角形信息");
                sw.WriteLine("三角形编号    顶点1   顶点2   顶点3   边1   边2   边3   相临三角形1 相临三角形2 相临三角形3");
                for (int i = 0; i < K; i++)
                {
                    sw.WriteLine("{0,6}{1,12}{2,8}{3,8}{4,6}{5,6}{6,6}  {7,8}    {8,8}    {9,8}", (i + 1).ToString(), Tri[i].Peak[0].ToString(),
                           Tri[i].Peak[1].ToString(), Tri[i].Peak[2].ToString(), Tri[i].Line[0].ID.ToString(), Tri[i].Line[1].ID.ToString(), Tri[i].Line[2].ID.ToString(),
                           Tri[i].Bor[0].ToString(), Tri[i].Bor[1].ToString(), Tri[i].Bor[2].ToString());
                }
                sw.WriteLine("\r\n");
                sw.WriteLine("*********************************************************************************************");
                sw.WriteLine("                  边信息");
                sw.WriteLine("边编号   顶点1   顶点2  相临三角形1 相临三角形2");
                for (int i = 0; i < o; i++)
                {
                    sw.WriteLine("{0,5}{1,8}{2,8}{3,8}      {4,6}", (i + 1).ToString(), Line[i].Point[0].ToString(), Line[i].Point[1].ToString(),
                        Line[i].Bor[0].ToString(), Line[i].Bor[1].ToString());
                }

                sw.WriteLine("\r\n");
                sw.WriteLine("*********************************************************************************************");
                sw.WriteLine("                  离散特征点信息");
                sw.WriteLine("  点号           X              Y               Z");
                for (int i = 0; i < k; i++)
                {
                    sw.WriteLine("{0,5}   {1,14}  {2,14}  {3,12}", (i + 1).ToString(), Value1[i, 1].ToString(),
                           Value1[i, 2].ToString(), Value1[i, 3].ToString());
                }

                sw.Close();
            }
        }
        #endregion
        #region 时间控件
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            this.timer1.Interval = 1000;
            this.timer1.Start();
        }
        #endregion
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
