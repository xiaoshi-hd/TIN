using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;//文件操作
using System.Collections;//数据类型添加
using Excel = Microsoft.Office.Interop.Excel;//Excel操作

namespace 不规则三角网体积计算
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region 定义变量
        // ArrayList可以存储任意数据类型
        double hh;//平场标高
        double pingjunh;//平均高程
        double tianfang, wafang;//总填方，总挖方
        Pointl[] point1;//用来存储散点数据的数组
        ArrayList linelist;//用来存储线列表
        ArrayList sjxlist;//用来存储三角形的三个点的列表
        Bitmap bitmap;
        #endregion
        #region 初始化
        public void chushihua()
        {
            linelist = new ArrayList();
            sjxlist = new ArrayList();

            txt_bian.Text = "";
            txt_pingjun.Text = "";
            txt_sanjiao.Text = "";
            txt_tian.Text = "";
            txt_wa.Text = "";
        }
        #endregion
        #region 时间控件
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripProgressBar1.Maximum = 20;//设置进度条的最大值

            toolStripStatusLabel3.Text = DateTime.Now.ToString();
            timer1.Enabled = true;
            timer1.Interval = 1000;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = DateTime.Now.ToString();
        }
        #endregion
        #region 文件打开
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.Rows.Clear();

            openFileDialog1.Title = "散点数据打开";
            openFileDialog1.Filter = "文本文件(*.txt)|*.txt|Excel旧版本文件(*.xls)|*.xls|Excel新版本文件(*.xlsx)|*.xlsx";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                #region txt
                if (openFileDialog1.FilterIndex == 1)
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName,Encoding.Default);
                    int i = 0;
                    while (!sr.EndOfStream)
                    {
                        string[] arrstr = sr.ReadLine().Split(',');
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = i + 1;//行号
                        for (int j = 0; j < arrstr.Length; j++)
                        {
                            dataGridView1.Rows[i].Cells[j + 1].Value = arrstr[j];
                        }
                        i++;
                    }
                    sr.Close();
                }
                #endregion 
                #region excel
                else
                {
                    Excel.Application excel = new Excel.Application();
                    excel.Visible = false;
                    Excel.Workbook workbook = excel.Application.Workbooks.Open(openFileDialog1.FileName);
                    Excel.Worksheet worksheet = excel.Application.Workbooks[1].Worksheets[1];

                    int rows = worksheet.UsedRange.Rows.Count;
                    int cloumns = worksheet.UsedRange.Columns.Count;

                    for (int i = 0; i < rows; i++)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = i + 1;
                        for (int j = 0; j < cloumns; j++)
                        {
                            dataGridView1.Rows[i].Cells[j + 1].Value = worksheet.Cells[i + 1,j + 1].Value;
                        }
                    }
                    workbook.Close();
                }
                #endregion 
            }
        }
        #endregion
        #region 生成TIN
        private void 构三角网ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chushihua();
            toolStripProgressBar1.Value = 0;//设置进度条的值
            dataGridView1.AllowUserToAddRows = false;
            #region 数据导入
            point1 = new Pointl[dataGridView1.Rows.Count];//实例化一个点数组，用来存储散点数据
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("请输入散点数据！");
                return;
            }
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    point1[i] = new Pointl();
                    point1[i].dianhao = dataGridView1.Rows[i].Cells[0].Value.ToString().Replace(" ","");
                    point1[i].X = Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value.ToString().Replace(" ", ""));
                    point1[i].Y = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString().Replace(" ", ""));
                    point1[i].Z = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value.ToString().Replace(" ", ""));
                }
            }
            catch
            {
                MessageBox.Show("请输入正确的散点数据！");
                return;
            }
            toolStripProgressBar1.Value = 4;//设置进度条的值
            #endregion
            #region 取第一条基线
            double dis, mindis = 10000000000;//存储两点距离和距离的最小值
            int count = 0;//存储最近的点的点号
            double ang;//存储余弦角度值，求距离边最近点时用到
            Line t1 = new Line();//存储第一条基线
            for (int i = 1; i < dataGridView1.Rows.Count; i++)
            {
                dis = TIN.Distance(point1[0],point1[i]);
                if (mindis > dis)
                {
                    mindis = dis;
                    count = i;
                }
            }
            t1.Begin = point1[0];
            t1.End = point1[count];
            linelist.Add(t1);
            //MessageBox.Show(point1[count].X.ToString());
            //toolStripProgressBar1.Value = 8;//设置进度条的值
            #endregion
            #region 存储点线
            for (int i = 0; i < linelist.Count; i++)//对每条边都判断一次
            {
                double minang = -1;//最小的余弦角度值
                bool OK = false;//存储是否存在左边点的信息
                Line line1 = new Line();//存储三角形中添加的第一条线
                Line line2 = new Line();//存储三角形中添加的第二条线

                #region 右边最近点
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    int yuobian;
                    yuobian = TIN.ZuoYou(point1[j], ((Line)linelist[i]).Begin, ((Line)linelist[i]).End);//linelist是列表，必须声明（line）以表示其类型，sjxlist同理
                    if (yuobian == 1)//右边存在点
                    {
                        ang = TIN.Angle(point1[j], ((Line)linelist[i]).Begin, ((Line)linelist[i]).End);
                        if (ang > minang)//寻找右边最近的点，一定会执行一次
                        {
                            minang = ang;
                            count = j;//右边最近点的点号
                        }
                        OK = true;
                    }
                }
                #endregion
                #region 构成三角形
                if (OK)//右边存在点
                {
                    #region 将新生成两条边添加入集合中
                    int tt1 = 0;//用来存储边是否重合的信息
                    int tt2 = 0;
                    line1.Begin = ((Line)linelist[i]).Begin;//由已知线的起点开始
                    line1.End = point1[count];//到最近点结束，作为第一条线
                    line2.Begin = point1[count];//由最近点开始
                    line2.End = ((Line)linelist[i]).End;//到已知线的终点结束，作为第二条线
                    linelist.Add(line1);
                    linelist.Add(line2);

                    sjx sjx1 = new sjx();//存储三角形的三点坐标信息
                    sjx1.firstp = line2.Begin;//已知点作为第一个点
                    sjx1.secondp = line2.End;
                    sjx1.thirdp = line1.Begin;
                    sjxlist.Add(sjx1);
                    #endregion
                    #region 判断三角形是否重合定点
                    for (int j = 0; j < sjxlist.Count - 1; j++)//第一个生成的三角形不用判断
                    {//三个点一共6种排列组合
                        if ((sjx1.firstp == ((sjx)sjxlist[j]).firstp
                            && sjx1.secondp == ((sjx)sjxlist[j]).secondp
                            && sjx1.thirdp == ((sjx)sjxlist[j]).thirdp)
                        || (sjx1.firstp == ((sjx)sjxlist[j]).firstp
                            && sjx1.secondp == ((sjx)sjxlist[j]).thirdp
                            && sjx1.thirdp == ((sjx)sjxlist[j]).secondp)
                        || (sjx1.firstp == ((sjx)sjxlist[j]).secondp
                            && sjx1.secondp == ((sjx)sjxlist[j]).thirdp
                            && sjx1.thirdp == ((sjx)sjxlist[j]).firstp)
                        || (sjx1.firstp == ((sjx)sjxlist[j]).secondp
                            && sjx1.secondp == ((sjx)sjxlist[j]).firstp
                            && sjx1.thirdp == ((sjx)sjxlist[j]).thirdp)
                        || (sjx1.firstp == ((sjx)sjxlist[j]).thirdp
                            && sjx1.secondp == ((sjx)sjxlist[j]).secondp
                            && sjx1.thirdp == ((sjx)sjxlist[j]).firstp)
                        || (sjx1.firstp == ((sjx)sjxlist[j]).thirdp
                            && sjx1.secondp == ((sjx)sjxlist[j]).firstp
                            && sjx1.thirdp == ((sjx)sjxlist[j]).secondp))
                        {
                            sjxlist.Remove(sjxlist[sjxlist.Count - 1]);//当前判断的三角形是列表中最后一个三角形
                        }
                    }
                    #endregion
                    #region 判断新生成的两边是否与已生成边重合
                    for (int j = 0; j < linelist.Count - 2; j++)//减去新生成的两条边进行循环
                    {
                        if ((line1.Begin == ((Line)linelist[j]).Begin && 
                            line1.End == ((Line)linelist[j]).End) 
                            || (line1.Begin == ((Line)linelist[j]).End
                            && line1.End == ((Line)linelist[j]).Begin))//两种组合方式
                        {
                            tt1 = 1;
                        }
                        if ((line2.Begin == ((Line)linelist[j]).Begin &&
                            line2.End == ((Line)linelist[j]).End)
                            || (line2.Begin == ((Line)linelist[j]).End
                            && line2.End == ((Line)linelist[j]).Begin))
                        {
                            tt2 = 1;
                        }
                    }
                    //第一条边重合
                    if (tt1 == 1)
                    {
                        linelist.Remove(linelist[linelist.Count - 2]);//line1先添加，所以-2，但是第一条边重合移去和第二条边重合移去的顺序不能反
                    }
                    //第二条边重合
                    if (tt2 == 1)
                    {
                        linelist.Remove(linelist[linelist.Count - 1]);
                    }
                    #endregion
                }
                #endregion
            }
            toolStripProgressBar1.Value = 16;//设置进度条的值
            #endregion
            #region 绘图
            double xmax = 0, ymax= 0,xmin = 10000000000000, ymin = 100000000000000;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)//找X和Y坐标的最大值
            {
                if (xmax < point1[i].X)
                {
                    xmax = point1[i].X;
                }
                if (ymax < point1[i].Y)
                {
                    ymax = point1[i].Y;
                }
                if (xmin > point1[i].X)
                {
                    xmin = point1[i].X;
                }
                if (ymin > point1[i].Y)
                {
                    ymin = point1[i].Y;
                }
            }
            //MessageBox.Show(xmax.ToString());
            //MessageBox.Show(xmin.ToString());
            //MessageBox.Show(ymax.ToString());
            //MessageBox.Show(ymin.ToString());
            bitmap = new Bitmap((int)(ymax - ymin + 10) * 10, (int)(xmax - xmin + 10) * 10);//放大整个绘图区域10倍，为了弥补位图分辨率低的缺点
            Graphics g = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.Red, 2f);
            //g.RotateTransform(-90);//旋转为测量坐标系
            //g.TranslateTransform(-(int)(xmax), -(int)ymin);//移动原点坐标以显示图片
            for (int i = 0; i < linelist.Count; i++)
            {
                PointF begion = new PointF();
                PointF end = new PointF();
                //任意点的X和Y减去最小值归化到原点，再乘以10倍以适应位图大小
                begion.X = ((float)((Line)linelist[i]).Begin.Y - (int)ymin + 5) * 10; //X和Y调换，(数学坐标系转换到测量坐标系)
                begion.Y = - ((float)((Line)linelist[i]).Begin.X - (int)xmax - 5) * 10;//X减去max再变成正数(像素坐标系转换数学坐标系)
                end.X = ((float)((Line)linelist[i]).End.Y - (int)ymin + 5) * 10;
                end.Y = - ((float)((Line)linelist[i]).End.X - (int)xmax - 5) * 10;
                g.DrawLine(pen, begion, end);
                //MessageBox.Show(begion.X.ToString());
                //MessageBox.Show(begion.Y.ToString());
                //MessageBox.Show(end.X.ToString());
                //MessageBox.Show(end.Y.ToString());
            }
            pictureBox1.Image = bitmap;
            toolStripProgressBar1.Value = 20;//设置进度条的值
            #endregion
            #region 计算体积
            #region 数据导入
            try//放这里导入是因为就算不输入平场标高也能绘制三角网
            {
                hh = Convert.ToDouble(txt_gaocheng.Text);//平场标高
            }
            catch
            {
                MessageBox.Show("请输入正确的平场标高！");
                return;
            }
            #endregion
            toolStripProgressBar1.Value = 0;
            double s1, s2, s3, szc, aveH;//s1,2,3 表示三角形的三条边长,scz 三角形的周长，aveH平均高程
            double[] ss = new double[sjxlist.Count];//存储表面积
            double[] vv = new double[sjxlist.Count];//存储体积
            tianfang = 0;
            wafang = 0;
            #region 平均高程
            pingjunh = 0;//平均高程
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                pingjunh += point1[i].Z;
            }
            pingjunh = pingjunh / dataGridView1.Rows.Count;
            txt_pingjun.Text = Math.Round(pingjunh, 3).ToString();
            #endregion
            #region 其他计算
            for (int i = 0; i < sjxlist.Count; i++)
            {
                s1 = TIN.Distance(((sjx)sjxlist[i]).firstp, ((sjx)sjxlist[i]).secondp);
                s2 = TIN.Distance(((sjx)sjxlist[i]).thirdp, ((sjx)sjxlist[i]).secondp);
                s3 = TIN.Distance(((sjx)sjxlist[i]).firstp, ((sjx)sjxlist[i]).thirdp);
                szc = (s1 + s2 + s3) / 2;//用的是海伦公式，计算三角形面积
                //MessageBox.Show(szc.ToString() + i);
                ss[i] = Math.Sqrt(szc * (szc - s1) * (szc - s2) * (szc - s3));
                aveH = (((sjx)sjxlist[i]).firstp.Z + ((sjx)sjxlist[i]).secondp.Z + ((sjx)sjxlist[i]).thirdp.Z) / 3;
                vv[i] = ss[i] * (aveH - hh);
                if (vv[i] > 0)
                {
                    tianfang = tianfang + vv[i];
                }
                else if (vv[i] < 0)
                {
                    wafang = wafang - vv[i];
                }
            }
            txt_tian.Text = Math.Round(tianfang, 3).ToString();
            txt_wa.Text = Math.Round(wafang, 3).ToString();
            txt_bian.Text = Convert.ToString(linelist.Count); //显示三角形边的个数
            txt_sanjiao.Text = Convert.ToString(sjxlist.Count);     //显示多少个三角形
            #endregion
        }
            #endregion
        #endregion
        #region 文件保存
        #region 计算结果保存
        private void 计算结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Title = "计算成果保存";
                saveFileDialog1.Filter = "文本文件(*.txt)|*.txt|Excel旧版本文件(*.xls)|*.xls|Excel新版本文件(*.xlsx)|*.xlsx";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    #region txt文档
                    if (saveFileDialog1.FilterIndex == 1)
                    {
                        StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                        sw.WriteLine("平场标高" + hh);
                        sw.WriteLine("平均高程" + pingjunh);
                        sw.WriteLine("总边数" + linelist.Count);
                        sw.WriteLine("三角形个数" + sjxlist.Count);
                        sw.WriteLine("填方(m3)" + tianfang);
                        sw.WriteLine("挖方(m3)" + wafang);
                        sw.Close();
                    }
                    #endregion
                    #region xls或xlsx文件
                    else
                    {
                        Excel.Application excel1 = new Excel.Application();//创建一个excel对象
                        Excel.Workbook workbook1 = excel1.Workbooks.Add(true);//为该excel对象添加一个工作簿
                        Excel.Worksheet worksheet1 = excel1.Workbooks[1].Worksheets[1];//获取工作簿中的第一个工作表
                        //为工作表填充数据，以单元格为操作对象
                        worksheet1.Cells[1, 1].Value = "平场标高";
                        worksheet1.Cells[1, 2].Value = hh;
                        worksheet1.Cells[1, 4].Value = "平均高程";
                        worksheet1.Cells[1, 5].Value = pingjunh;
                        worksheet1.Cells[2, 1].Value = "总边数";
                        worksheet1.Cells[2, 2].Value = linelist.Count;
                        worksheet1.Cells[2, 4].Value = "三角形个数";
                        worksheet1.Cells[2, 5].Value = sjxlist.Count;
                        worksheet1.Cells[3, 1].Value = "填方(m3)";
                        worksheet1.Cells[3, 2].Value = tianfang;
                        worksheet1.Cells[3, 4].Value = "挖方(m3)";
                        worksheet1.Cells[3, 5].Value = wafang;

                        worksheet1.Columns.AutoFit();//自动调整列宽
                        worksheet1.SaveAs(saveFileDialog1.FileName);//保存工作表
                        workbook1.Close();
                    }
                    #endregion 
                }
                MessageBox.Show("保存成功！");
            }
            catch
            {
                MessageBox.Show("请先生成TIN再保存！");
                return;
            }
        }
        #endregion
        #region bmp保存
        private void bmp图形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog save1 = new SaveFileDialog();
                save1.Title = "bmp文件保存";
                save1.Filter = "图像文件(*.bmp)|*.bmp";
                if (save1.ShowDialog() == DialogResult.OK)
                {
                    bitmap.Save(save1.FileName);
                }
                MessageBox.Show("保存成功！");
            }
            catch
            {
                MessageBox.Show("请先生成TIN再保存！");
                return;
            }
        }
        #endregion
        #endregion
        #region dxf绘制
        #region 散点图绘制
        private void 散点图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Title = "保存dxf文件";
                saveFileDialog1.Filter = "AutoCAD dxf文件(*.dxf)|*.dxf";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    {
                        sw.Write(TIN.shiqian());//设置了两个图层
                        sw.Write("0\nSECTION\n");//第二段开始
                        sw.Write("2\nENTITIES\n");//实体段开始
                        #region 画三维点
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            sw.Write("0\nPOINT\n8\nshiti\n");
                            sw.Write("10\n" + point1[i].Y + "\n");
                            sw.Write("20\n" + point1[i].X + "\n");
                            sw.Write("30\n" + point1[i].Z + "\n");
                        }
                        #endregion
                        #region 绘制注记
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            sw.Write(TIN.zhuji(point1[i].Y, point1[i].X, point1[i].dianhao));
                        }
                        #endregion
                        sw.Write("0\nENDSEC\n");//第二段结束
                        sw.Write("0\nEOF\n");//文件结束
                    }
                    MessageBox.Show("保存成功！");
                }
            }
            catch
            {
                MessageBox.Show("请先生成TIN再绘制dxf！");
                return;
            }
        }
        #endregion
        #region 三角网
        private void 三角网图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Title = "保存dxf文件";
                saveFileDialog1.Filter = "AutoCAD dxf文件(*.dxf)|*.dxf";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    {
                        sw.Write(TIN.shiqian());
                        sw.Write("0\nSECTION\n");//第二段开始
                        sw.Write("2\nENTITIES\n");//实体段开始
                        #region 画三维点
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            sw.Write("0\nPOINT\n8\n0\n");
                            sw.Write("10\n" + point1[i].Y + "\n");
                            sw.Write("20\n" + point1[i].X + "\n");
                            sw.Write("30\n" + point1[i].Z + "\n");
                        }
                        #endregion
                        #region 绘制注记
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            sw.Write(TIN.zhuji(point1[i].Y, point1[i].X, point1[i].dianhao));
                        }
                        #endregion
                        #region 绘制三角
                        for (int i = 0; i < sjxlist.Count; i++)
                        {
                            sw.Write("0\nPOLYLINE\n8\nshiti\n66\n1\n");
                            sw.Write("0\nVERTEX\n8\nshiti\n");
                            sw.Write("10\n" + ((sjx)sjxlist[i]).firstp.Y + "\n");
                            sw.Write("20\n" + ((sjx)sjxlist[i]).firstp.X + "\n");
                            sw.Write("30\n" + ((sjx)sjxlist[i]).firstp.Z + "\n");
                            sw.Write("0\nVERTEX\n8\nshiti\n");
                            sw.Write("10\n" + ((sjx)sjxlist[i]).secondp.Y + "\n");
                            sw.Write("20\n" + ((sjx)sjxlist[i]).secondp.X + "\n");
                            sw.Write("30\n" + ((sjx)sjxlist[i]).secondp.Z + "\n"); 
                            sw.Write("0\nVERTEX\n8\nshiti\n");
                            sw.Write("10\n" + ((sjx)sjxlist[i]).thirdp.Y + "\n");
                            sw.Write("20\n" + ((sjx)sjxlist[i]).thirdp.X + "\n");
                            sw.Write("30\n" + ((sjx)sjxlist[i]).thirdp.Z + "\n");
                            sw.Write("0\nVERTEX\n8\nshiti\n");
                            sw.Write("10\n" + ((sjx)sjxlist[i]).firstp.Y + "\n");
                            sw.Write("20\n" + ((sjx)sjxlist[i]).firstp.X + "\n");
                            sw.Write("30\n" + ((sjx)sjxlist[i]).firstp.Z + "\n");
                            sw.Write("0\nSEQEND\n");
                        }
                        #endregion
                        sw.Write("0\nENDSEC\n");//第二段结束
                        sw.Write("0\nEOF\n");//文件结束
                    }
                    MessageBox.Show("保存成功！");
                }
            }
            catch
            {
                MessageBox.Show("请先生成TIN再绘制dxf！");
                return;
            }
        }
        #endregion
        #endregion
        #region 鼠标缩放事件
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {//修改picture的大小，在窗体上添加一个panel控件，设置autoscorl属性为true，就可以实现滚动条
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
        #region 刷新
        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();//清空窗体中的所有控件
            this.InitializeComponent();
        }
        #endregion
    }
}
