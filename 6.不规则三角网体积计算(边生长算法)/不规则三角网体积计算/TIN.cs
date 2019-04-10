using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 不规则三角网体积计算
{
    public class Line//用来存储线
    {
        public Pointl Begin;
        public Pointl End;
    }
    public class sjx//用来存储三角形的三个点
    {
        public Pointl firstp;
        public Pointl secondp;
        public Pointl thirdp;
    }
    public class Pointl//用来储存带点号的三维点
    {
        public string dianhao;//类的成员
        public double X;
        public double Y;
        public double Z;
    }
    class TIN
    {
        #region 距离
        public static double Distance(Pointl first, Pointl second)//两点距离
        {
            double dis;
            dis = Math.Sqrt((second.Y - first.Y) * (second.Y - first.Y) + (second.X - first.X) * (second.X - first.X));
            return dis;
        }
        #endregion
        #region 判断左右
        public static int ZuoYou(Pointl cen, Pointl first, Pointl second)//判断第三个点在1到2点连线的左右
        {
            double s;
            s = (first.X - cen.X) * (second.Y - cen.Y) - (first.Y - cen.Y) * (second.X - cen.X);
            if (s > 0)
            {
                return 1;//右边
            }
            else
            {
                return -1;//并不一定是左边，s = 0时还可能在直线上
            }
            /*           
                    |x1   x2   x3|   
        S(P1,P2,P3)=|y1   y2   y3|= (x1-x3)*(y2-y3)-(y1-y3)(x2-x3)   
                    |1    1     1|   
             S > 0;在测量坐标系下是在右边，在数学坐标系下是在左边
            */
        }
        #endregion
        #region 判断最近点
        public static double Angle(Pointl cen, Pointl first, Pointl second)//三个点的夹角CEN为顶点，算的是夹角,返回弧度值
        {
            double dx1, dx2, dy1, dy2;
            double angle;
            dx1 = first.X - cen.X;
            dy1 = first.Y - cen.Y;
            dx2 = second.X - cen.X;
            dy2 = second.Y - cen.Y;
            double c = Math.Sqrt(dx1 * dx1 + dy1 * dy1) * Math.Sqrt(dx2 * dx2 + dy2 * dy2);
            if (c == 0)
            {
                return -1;//表示在直线上，该代码不会被运行
            }
            else
            {
                angle = Math.Acos((dx1 * dx2 + dy1 * dy2) / c);//其实就是余弦定理的一种形式
                return angle;
            }
        }
        #endregion
        #region dxf绘制
        public static string shiqian()
        {
            string h;
            h = "0\nSECTION\n2\nTABLES\n0\nTABLE\n2\nLAYER\n0\nLAYER\n70\n0\n2\nshiti\n62\n10\n6\nCONTINUOUS\n";//shiti图层，红色
            h += "0\nLAYER\n70\n0\n2\nzhuji\n62\n50\n6\nCONTINUOUS\n0\nLAYER\n70\n0\n2\nqita\n62\n90\n6\nCONTINUOUS\n0\nENDTAB\n0\nENDSEC\n";//注记图层，黄色,其他图层，绿色
            return h;
        }
        public static string zhuji(double x1, double y1, string s)
        {
            string h;
            h = "0\nTEXT\n8\nzhuji\n10\n" + x1 + "\n20\n" + y1 + "\n40\n1\n1\n" + s + "\n";
            return h;
        }
        #endregion
    }
}
