using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace TIN_myself
{
    public partial class Form1 : Form
    {
        public Graphics g = null;
        public Pen pen = new Pen(Color.Red, 1);
        public ArrayList list = new ArrayList();
        public ArrayList linelist = new ArrayList();
        public ArrayList sjxlist = new ArrayList();
        ArrayList sjx = new ArrayList();
        Bitmap bitmap = new Bitmap(600, 500);
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)//窗体加载事件
        {
            g = Graphics.FromImage(bitmap);
            g.RotateTransform(-90);
            g.TranslateTransform(-800, 0);
            double[,] ar = new double[,] {
            #region 
             {414.28,421.88,39.555}
            ,{387.80,425.02,36.8774}
            ,{359.06,426.62,31.225}
            ,{348.04,425.53,27.416}
            ,{344.57,440.31,27.7945}        
            ,{352.89,454.84,28.4999}
            ,{402.88,442.45,37.951}
            ,{393.47,393.86,32.5395}
            ,{358.85,387.57,29.426}
            ,{358.59,376.62,29.223}
            ,{348.66,364.21,28.2538}
            ,{362.80,340.89,26.8212}
            ,{335.73,347.62,26.2299}
            ,{331.84,362.69,26.6612}
            ,{351.82,402.35,28.4848}
            ,{335.09,399.61,26.6922}
            ,{331.15,333.34,24.6894}
            ,{344.10,322.26,24.3684}
            ,{326.80,381.66,26.7581}
            ,{396.59,331.42,28.7137}
            ,{372.70,317.25,25.8215}
            ,{404.54,313.74,26.9055}
            ,{416.04,349.16,31.7509}
            ,{424.70,367.77,34.8919}
            ,{414.85,383.5,37.4818}
            ,{399.59,370.21,32.5866}
            ,{386.89,353.32,30.5459}
            ,{383.23,336.82,29.2504}
            ,{421.13,322.59,27.7593}
            ,{468.29,316.53,27.0276}
            ,{434.93,313.32,26.5662}
            ,{456.71,324.05,28.8742}
            ,{491.54,372.23,33.0459}
            ,{470.83,363.75,32.6194}
            ,{414.07,397.87,39.5041}
            ,{446.84,368.77,34.5865}
            ,{438,313.25,30.1398}
            ,{456.08,344.36,30.1871}
            ,{472.76,401.02,38.5963}
            ,{479.13,432.82,42.3405}
            ,{499.63,422.7,40.7577}
            ,{492.35,402.93,37.9286}
            ,{489.32,390.16,36.34}
            ,{449.70,383.25,36.9367}
            ,{444.56,406.78,41.6945}
            ,{464.50,427.23,43.5075}
            ,{503.60,439.93,41.0365}
            ,{515.85,437.89,39.9929}
            ,{505.89,493.01,33.8673}
            ,{485.63,490.18,36.4479}
            ,{493.83,472.49,39.8801}
            ,{482.52,450.67,42.3327}
            ,{508.10,461.98,40.2172}
            ,{513.7,382.32,36.919}
            ,{532.14,466.79,37.7726}
            ,{547.81,446.42,38.3385}
            ,{555.09,429.29,37.5484}
            ,{550.59,466.8,37.233}
            ,{542.39,490.08,33.8351}
            ,{562.32,481.02,34.4659}
            ,{582.49,461.23,34.1418}
            ,{585.43,474.09,32.5303}
            ,{596.28,464.84,31.7542}
            ,{573.76,494.1,31.3335}
            ,{567.67,505.61,29.5076}
            ,{435.85,465.51,40.6101}
            ,{446.92,438.86,43.5085}
            ,{451.21,462.16,42.1787}
            ,{425.14,442.84,42.2289}
            ,{465.83,471.64,41.4353}
            ,{444.54,478.39,39.4434}
            ,{436.92,489.73,35.15}
            ,{446.87,502.75,32.0204}
            ,{424.80,499.59,31.1902}
            ,{415.55,477.23,36.1728}
            ,{405.41,459.37,36.2749}
            ,{407.98,487.67,31.2808}
            ,{410.21,511.39,29.09}
            ,{392.51,495.92,29.1024}
            ,{434.96,524.73,28.8913}
            ,{415.92,523.57,28.6408}
            ,{381.47,504.81,28.3432}
            ,{365.02,495.85,27.6838}
            ,{352.99,500.58,26.1047}
            ,{348.82,486.82,26.2623}
            ,{350.47,471.08,27.5493}
            ,{357.64,521.57,25.8516}
            ,{387.14,526.14,25.9322}
            ,{418.40,538.09,25.2847}
            ,{448.49,533.77,25.8802}
            ,{465.16,494.46,34.603}
            ,{458.79,508.1,31.0874}
            ,{503.63,519.64,28.9581}
            ,{504.38,505.74,29.4945}
            ,{487.85,513.90,29.3574}
            ,{473.73,522.38,28.2401}
            ,{474.65,534.46,28.1753}
            ,{501.33,537.72,27.601}
            ,{526.89,530.08,29.1993}
            ,{538.63,519.63,29.2767}
            ,{327.38,431.95,26.6129}
            ,{326.22,419.39,26.2965}
            ,{591.41,400.14,31.3133}
            ,{580.18,375.51,30.2236}
            ,{603.18,381.36,28.8233}
            ,{529.15,341.71,27.0601}
            ,{583.86,408.9,33.6867}
            ,{565.07,419.84,36.2957}
            ,{544.34,402.71,36.3476}
            ,{568.31,403.82,34.7975}
            ,{517.51,388.7,35.1603}
            ,{521.11,366.48,31.672}
            ,{539.64,349.28,29.138}
            ,{506.62,335.71,26.9185}
            ,{505.15,350.97,29.6483}
            ,{494.79,379.74,33.5259}
            ,{511.44,374.64,33.1928}
            ,{493.14,354.75,30.8649}
            ,{549.66,508.67,30.3146}
            ,{369.18,500.28,27.7}
            ,{442.80,431.06,43.90}
            ,{455.78,444.08,43.460}
            ,{461.02,374.57,35.5}
            ,{437.30,388.9,38.2}
            ,{399.61,407.54,37}
            ,{514.12,450.64,40.1}}; 
            #endregion 
            double avg = 0;
            for (int q1 = 0; q1 < ar.Length/3; q1++)//length表示所有元素的总数
            {
                avg+=ar[q1,2];//求高程的平均值
            }
            avg/=ar.Length/3;
            textBox14.Text = avg.ToString();
            
            Pointl[] a = new Pointl[ar.Length / 3];//一次实例化多个相同类
            ArrayList S = new ArrayList();
            for (int i = 0; i < ar.Length / 3; i++)
            {
                a[i] = new Pointl();
                a[i].X = (float)ar[i, 0];
                a[i].Y = (float)ar[i, 1];
                a[i].Z = (float)ar[i, 2];
            }
            foreach (object ab in a)
            {
                list.Add(ab);//列表又存储一次数据
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double ang;
            ArrayList tinline = new ArrayList();
            double mindis = 1000000000000;
            double dis;
            int count = 0;
            Line tl = new Line();

            #region  取首条基线
            for (int i = 1; i < list.Count; i++)//离第一个点最近
            {
                dis = Distance(((Pointl)list[0]), (Pointl)list[i]);
                if (dis < mindis)
                {
                    mindis = dis;
                    count = i;
                }
            }
            tl.Begin = (Pointl)list[0];
            tl.End = (Pointl)list[count];
            //  tl.ID = 1;
            tinline.Add(tl);
            #endregion
            for (int j = 0; j < tinline.Count; j++)//对每一条边判断一次
            {
                //MessageBox.Show(tinline.Count.ToString());
                double minang = -1;
                bool OK;
                OK = false;
                Line tling1 = new Line();
                Line tling2 = new Line();
                #region 右侧最近点
                for (int i = 0; i < list.Count; i++)
                {
                    int youbian;
                    youbian = ZuoYou((Pointl)list[i], ((Line)tinline[j]).Begin, ((Line)tinline[j]).End);
                    if (youbian == 1)
                    {
                        ang = Angle((Pointl)list[i], ((Line)tinline[j]).Begin, ((Line)tinline[j]).End);//返回弧度
                        if (ang > minang)
                        {
                            minang = ang;
                            count = i;
                        }
                        OK = true;
                    }
                }
                #endregion
                #region 右侧存在点
                if (OK == true)
                {
                    #region //将新生成两条边添加入集合中
                    int t1 = 0;

                    int t2 = 0;

                    tling1.Begin = ((Line)tinline[j]).Begin;

                    tling1.End = (Pointl)list[count];

                    tling2.Begin = (Pointl)list[count];

                    tling2.End = ((Line)tinline[j]).End;

                    tinline.Add(tling1);

                    tinline.Add(tling2);

                    sjx sjx = new sjx();

                    sjx.firstp = tling2.Begin;
                    sjx.secondp = tling2.End;
                    sjx.thirdp = tling1.Begin;
                    sjxlist.Add(sjx);
                    #endregion
                    #region 判断三角形是否重合定点
                    for (int i = 0; i < sjxlist.Count - 1; i++)
                    {
                        if (
                                 sjx.firstp == ((sjx)sjxlist[i]).firstp
                                 && sjx.secondp == ((sjx)sjxlist[i]).secondp
                                 && sjx.thirdp == ((sjx)sjxlist[i]).thirdp
                             || sjx.firstp == ((sjx)sjxlist[i]).firstp
                                 && sjx.secondp == ((sjx)sjxlist[i]).thirdp
                                 && sjx.thirdp == ((sjx)sjxlist[i]).secondp
                             || sjx.firstp == ((sjx)sjxlist[i]).secondp
                                 && sjx.secondp == ((sjx)sjxlist[i]).thirdp
                                 && sjx.thirdp == ((sjx)sjxlist[i]).firstp
                              || sjx.firstp == ((sjx)sjxlist[i]).secondp
                                 && sjx.secondp == ((sjx)sjxlist[i]).firstp
                                 && sjx.thirdp == ((sjx)sjxlist[i]).thirdp
                              || sjx.firstp == ((sjx)sjxlist[i]).thirdp
                                 && sjx.secondp == ((sjx)sjxlist[i]).secondp
                                 && sjx.thirdp == ((sjx)sjxlist[i]).firstp
                              || sjx.firstp == ((sjx)sjxlist[i]).thirdp
                                 && sjx.secondp == ((sjx)sjxlist[i]).firstp
                                 && sjx.thirdp == ((sjx)sjxlist[i]).secondp
                            )
                        {
                            sjxlist.Remove(sjxlist[sjxlist.Count - 1]);
                            //MessageBox.Show("移除" + i);
                        }
                    }
                    #endregion
                    #region 判断新生成的两边是否与已生成边重合
                    for (int i = 0; i < tinline.Count - 2; i++)
                    {
                        if ((tling2.Begin == ((Line)tinline[i]).Begin &&
                            tling2.End == ((Line)tinline[i]).End)
                            || (tling2.Begin == ((Line)tinline[i]).End
                            && tling2.End == ((Line)tinline[i]).Begin))
                        {
                            t2 = 1;
                        }
                        if ((tling1.Begin == ((Line)tinline[i]).Begin && 
                            tling1.End == ((Line)tinline[i]).End) 
                            || (tling1.Begin == ((Line)tinline[i]).End 
                            && tling1.End == ((Line)tinline[i]).Begin))
                        {
                            t1 = 1;
                        }
                    }
                    //两条边都重合
                    if (t2 == 1 && t1 == 1)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            tinline.Remove(tinline[tinline.Count - 1]);
                        }
                    }
                    //第二条边重合
                    else if (t2 == 1)
                    {
                        tinline.Remove(tinline[tinline.Count - 1]);
                    }
                    //第一条边重合
                    else if (t1 == 1)
                    {
                        tinline.Remove(tinline[tinline.Count - 2]);
                    }
                }
                    #endregion
            }
                #endregion
            #region 绘图
            for (int i = 0; i <= tinline.Count - 1; i++)
            {
                PointF zh_begin = new PointF();
                PointF zh_end = new PointF();
                zh_begin.X = ((Line)tinline[i]).Begin.X;
                zh_begin.Y = ((Line)tinline[i]).Begin.Y;
                zh_end.X = ((Line)tinline[i]).End.X;
                zh_end.Y = ((Line)tinline[i]).End.Y;
                g.DrawLine(pen, zh_begin, zh_end);
            }
            pictureBox1.Image = bitmap ;
            #endregion
            #region 计算面积开始
            double s1, s2, s3, szc, averH = 0;//SZX 周长 averH 平均高度
            double[] ss = new double[sjxlist.Count];
            double[] vv = new double[sjxlist.Count];
            double hh_num = 0;             //平场标高
            double tianfang = 0, wafang = 0;
            hh_num = 33.301;
            textBox9.Text = Convert.ToString(hh_num);
            //double s11 = 0, s21 = 0, s31 = 0, h1, h2,h3,min;
            for (int i = 0; i < sjxlist.Count; i++)
            {
                s1 = Distance(((sjx)sjxlist[i]).firstp, ((sjx)sjxlist[i]).secondp);
                s2 = Distance(((sjx)sjxlist[i]).thirdp, ((sjx)sjxlist[i]).secondp);
                s3 = Distance(((sjx)sjxlist[i]).firstp, ((sjx)sjxlist[i]).thirdp);
                //min = ((sjx)sjxlist[i]).firstp.Z;
                //if (min > ((sjx)sjxlist[i]).secondp.Z)
                //    min = ((sjx)sjxlist[i]).secondp.Z;
                //if (min > ((sjx)sjxlist[i]).thirdp.Z)
                //    min = ((sjx)sjxlist[i]).thirdp.Z;
                //if (min == ((sjx)sjxlist[i]).firstp.Z)
                //{
                //    h1 = ((sjx)sjxlist[i]).secondp.Z - min;
                //    h2 = ((sjx)sjxlist[i]).thirdp.Z - min;
                //    h3 = ((sjx)sjxlist[i]).secondp.Z - ((sjx)sjxlist[i]).thirdp.Z;
                //    s11 = Math.Sqrt(s1 * s1 - h1 * h1);
                //    s21 = Math.Sqrt(s2 * s2 - h3 * h3);
                //    s31 = Math.Sqrt(s3 * s3 - h2 * h2);
                //}
                //if (min == ((sjx)sjxlist[i]).secondp.Z)
                //{
                //    h1 = ((sjx)sjxlist[i]).firstp.Z - min;
                //    h2 = ((sjx)sjxlist[i]).thirdp.Z - min;
                //    h3 = ((sjx)sjxlist[i]).firstp.Z - ((sjx)sjxlist[i]).thirdp.Z;
                //    s11 = Math.Sqrt(s1 * s1 - h1 * h1);
                //    s21 = Math.Sqrt(s2 * s2 - h2 * h2);
                //    s31 = Math.Sqrt(s3 * s3 - h3 * h3);
                //}
                //if (min == ((sjx)sjxlist[i]).thirdp.Z)
                //{
                //    h1 = ((sjx)sjxlist[i]).firstp.Z - min;
                //    h2 = ((sjx)sjxlist[i]).secondp.Z - min;
                //    h3 = ((sjx)sjxlist[i]).secondp.Z - ((sjx)sjxlist[i]).firstp.Z;
                //    s11 = Math.Sqrt(s1 * s1 - h3 * h3);
                //    s21 = Math.Sqrt(s2 * s2 - h2 * h2);
                //    s31 = Math.Sqrt(s3 * s3 - h1 * h1);
                //}
                szc = (s1 + s2 + s3) / 2;
                //MessageBox.Show(szc.ToString() + i);
                ss[i] = Math.Sqrt(szc * (szc - s1) * (szc - s2) * (szc - s3));
                averH = (((sjx)sjxlist[i]).firstp.Z + ((sjx)sjxlist[i]).secondp.Z + ((sjx)sjxlist[i]).thirdp.Z) / 3;
                vv[i] = ss[i] * (averH - hh_num);
                if (vv[i] > 0)
                    tianfang = tianfang + vv[i];
                else if (vv[i] < 0)
                    wafang = wafang - vv[i];
            }
            
            textBox12.Text = Convert.ToString(tianfang);   //计算填方和挖方
            textBox13.Text = Convert.ToString(wafang);
            #endregion
            #region 结果显示
            richTextBox1.Text = Convert.ToString(tinline.Count); //text 显示三角形边的个数
            textBox3.Text = Convert.ToString(sjxlist.Count);     //显示多少个三角形
            double ss_num = 0;
            double vv_num = 0;
            for (int i = 0; i < sjxlist.Count; i++)
            {
                ss_num = ss_num + ss[i];
                vv_num = vv_num + vv[i];
            }
            textBox4.Text = Convert.ToString(ss_num);//显示总面积
            textBox7.Text = Convert.ToString(vv_num);//显示体积填方+挖方
            #endregion
        }
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
        #region 判断最近点
        public double Angle(Pointl cen, Pointl first, Pointl second)//三个点的夹角CEN为顶点，算的是夹角
        {
            double dx1, dx2, dy1, dy2;
            double angle;
            dx1 = first.X - cen.X;
            dy1 = first.Y - cen.Y;
            dx2 = second.X - cen.X;
            dy2 = second.Y - cen.Y;
            float c = (float)Math.Sqrt(dx1 * dx1 + dy1 * dy1) * (float)Math.Sqrt(dx2 * dx2 + dy2 * dy2);
            if (c == 0)
            {
                return -1;
                MessageBox.Show("jinggao");
            }
            else
            {
                angle = (float)Math.Acos((dx1 * dx2 + dy1 * dy2) / c);
                return angle;
            }
        }
        #endregion
        #region 距离
        public double Distance(Pointl first, Pointl second)//两点距离
        {
            double dis;
            dis = Math.Sqrt((second.Y - first.Y) * (second.Y - first.Y) + (second.X - first.X) * (second.X - first.X));
            return dis;
        }
        #endregion
        #region 判断左右
        public int ZuoYou(Pointl cen, Pointl first, Pointl second)//判断左右
        {
            double s;
            s = (first.X - cen.X) * (second.Y - cen.Y) - (first.Y - cen.Y) * (second.X - cen.X);
            if (s > 0)
            {
                return 1;//zuoce
            }
            else

                return 2;
        }
        #endregion
        /*左右方向是相对前进方向的,只要指定了前进方向就可以知道左右(比如指定前进方向是从直线的起点到终点).
         * 判断点在直线的左侧还是右侧是计算几何里面的一个最基本算法.使用矢量来判断.

          定义：平面上的三点P1(x1,y1),P2(x2,y2),P3(x3,y3)的面积量：   
                    |x1   x2   x3|   
        S(P1,P2,P3)=|y1   y2   y3|= (x1-x3)*(y2-y3)-(y1-y3)(x2-x3)   
                    |1    1     1|   


          当P1P2P3逆时针时S为正的，当P1P2P3顺时针时S为负的。   
    
          令矢量的起点为A，终点为B，判断的点为C，   
          如果S（A，B，C）为正数，则C在矢量AB的左侧；   
          如果S（A，B，C）为负数，则C在矢量AB的右侧；   
          如果S（A，B，C）为0，则C在直线AB上。*/
        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(this.BackColor);
        }
    }
}
