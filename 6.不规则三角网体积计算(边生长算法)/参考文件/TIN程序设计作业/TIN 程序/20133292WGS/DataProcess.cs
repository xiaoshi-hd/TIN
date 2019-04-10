using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20133292WGS
{
    class DataProcess
    {
        public int Judge(DataStucture.Triangle[] A ,int p,DataStucture.Line B,DataStucture.Line C, DataStucture.Line D)
        {
            int k1 = 0,k2=0,k3=0;
            for (int d = 0; d < p;d++ )
           {
               for (int f = 0; f < 3;f++ )
               {
                   if ((A[d].Line[f].Point[0] == B.Point[0] && A[d].Line[f].Point[1] == B.Point[1]) || (A[d].Line[f].Point[0] == B.Point[1] && A[d].Line[f].Point[1] == B.Point[0]))
                   {
                       k1++;
                       if (k1 == 2)
                       {
                           return 0;
                       }
                   }
                   if ((A[d].Line[f].Point[0] == C.Point[0] && A[d].Line[f].Point[1] == C.Point[1]) || (A[d].Line[f].Point[0] == C.Point[1] && A[d].Line[f].Point[1] == C.Point[0]))
                   {
                       k2++;
                       if (k2 == 2)
                       {
                           return 0;
                       }
                   }

                   if ((A[d].Line[f].Point[0] == D.Point[0] && A[d].Line[f].Point[1] == D.Point[1]) || (A[d].Line[f].Point[0] == D.Point[1] && A[d].Line[f].Point[1] == D.Point[0]))
                   {
                       k3++;
                       if (k3 == 2)
                       {
                           return 0;
                       }
                   }
               }
                   
                ;
           }
            return 1;

        }
        public int Search(DataStucture.Triangle[] Tri,int K,int j,int k ,double[,] Value1)
        {
            int m=0, n=0;
            if(j==0)
            {
                m = 1; n = 2;
            }
            else if(j==1)
            {
                m = 2; n = 0;
            }
            else if(j==2)
            {
                m = 0; n =1;
            }
            
            int b=1,c;
            double fxy1, fxy2,cosc,Min=0;
            c = 1;
            fxy1 = Value1[Tri[K].Peak[j], 2] - ((Value1[Tri[K].Peak[m], 2] - Value1[Tri[K].Peak[n], 2]) / (Value1[Tri[K].Peak[m], 1] - Value1[Tri[K].Peak[n], 1]) *
                Value1[Tri[K].Peak[j], 1] - (Value1[Tri[K].Peak[n], 1] * Value1[Tri[K].Peak[m], 2] - Value1[Tri[K].Peak[m], 1] * Value1[Tri[K].Peak[n], 2]) /
                (Value1[Tri[K].Peak[m], 1] - Value1[Tri[K].Peak[n], 1]));
            for (int i = 0; i < k ; i++)
            {
                if (i != Tri[K].Peak[m] && i != Tri[K].Peak[n])
                {
                    fxy2 = Value1[i, 2] - ((Value1[Tri[K].Peak[m], 2] - Value1[Tri[K].Peak[n], 2]) / (Value1[Tri[K].Peak[m], 1] - Value1[Tri[K].Peak[n], 1]) *
                       Value1[i, 1] - (Value1[Tri[K].Peak[n], 1] * Value1[Tri[K].Peak[m], 2] - Value1[Tri[K].Peak[m], 1] * Value1[Tri[K].Peak[n], 2]) /
                       (Value1[Tri[K].Peak[m], 1] - Value1[Tri[K].Peak[n], 1]));
                    if (fxy1 * fxy2 < 0)
                    {
                       
                        cosc = ((Value1[i, 1] - Value1[Tri[K].Peak[m], 1]) * (Value1[i, 1] - Value1[Tri[K].Peak[m], 1]) + (Value1[i, 2] - Value1[Tri[K].Peak[m], 2]) * (Value1[i, 2] - Value1[Tri[K].Peak[m], 2])
                            + (Value1[i, 1] - Value1[Tri[K].Peak[n], 1]) * (Value1[i, 1] - Value1[Tri[K].Peak[n], 1]) + (Value1[i, 2] - Value1[Tri[K].Peak[n], 2]) * (Value1[i, 2] - Value1[Tri[K].Peak[n], 2])
                            - ((Value1[Tri[K].Peak[m], 1] - Value1[Tri[K].Peak[n], 1]) * (Value1[Tri[K].Peak[m], 1] - Value1[Tri[K].Peak[n], 1]) + (Value1[Tri[K].Peak[m], 2] - Value1[Tri[K].Peak[n], 2]) *
                            (Value1[Tri[K].Peak[m], 2] - Value1[Tri[K].Peak[n], 2]))) / (2 * Math.Sqrt((Value1[i, 1] - Value1[Tri[K].Peak[m], 1]) * (Value1[i, 1] - Value1[Tri[K].Peak[m], 1]) + (Value1[i, 2] -
                            Value1[Tri[K].Peak[m], 2]) * (Value1[i, 2] - Value1[Tri[K].Peak[m], 2])) * Math.Sqrt((Value1[i, 1] - Value1[Tri[K].Peak[n], 1]) * (Value1[i, 1] - Value1[Tri[K].Peak[n], 1]) +
                            (Value1[i, 2] - Value1[Tri[K].Peak[n], 2]) * (Value1[i, 2] - Value1[Tri[K].Peak[n], 2])));
                        if (c == 1)
                        {
                            Min = cosc;
                            b = i;
                            c++;
                        }
                        else
                        {
                            if (cosc < Min)
                            {
                                Min = cosc;
                                b = i;
                            }
                        }
                    }
                }
            }
            if (c != 1)
                return b;
            else
                return -1;
            
        }
        public int SS(DataProcess BB, DataStucture.Triangle[] Tri, int K, int k, ref int L, double[,] Value1,DataStucture.Line[] Line,ref int o)
        {
            int temp, jud=0, re = 0,k1=0,k2=0,c=0,d=0 ;
            DataStucture.Line[] Linetemp = new DataStucture.Line[2];
            Linetemp[0] = new DataStucture.Line();
            Linetemp[1] = new DataStucture.Line();
            temp = BB.Search(Tri, K, 2, k, Value1);
            if (temp != -1)
            {
                Linetemp[0].Point[0] = Tri[K].Peak[0];
                Linetemp[0].Point[1] = temp;
                Linetemp[1].Point[0] = temp;
                Linetemp[1].Point[1] = Tri[K].Peak[1];
                jud = BB.Judge(Tri, L, Linetemp[0], Linetemp[1], Tri[K].Line[0]);
                if (jud == 1)
                { 
                    Line[o] = new DataStucture.Line();
                    Tri[L] = new DataStucture.Triangle();
                    Tri[L].Line[0] = new DataStucture.Line();
                    Tri[L].Line[1] = new DataStucture.Line();
                    Tri[L].Line[2] = new DataStucture.Line();
                    Line[Tri[K].Line[0].ID - 1].Bor[1] = L + 1;
                    Tri[L].ID = L + 1;
                    Tri[L].Peak[0] = Tri[K].Peak[1];
                    Tri[L].Peak[1] = Tri[K].Peak[0];
                    Tri[L].Peak[2] = temp;

                    k1 = Judge2(Tri, L, Linetemp[1],ref c,ref d);
                    Line[o].ID = o + 1;
                    Line[o].Point[0] = Linetemp[1].Point[0];
                    Line[o].Point[1] = Linetemp[1].Point[1];
                    Line[o].Bor[0] = L + 1;
                    Tri[L].Line[2] = Line[o];
                    
                    if(k1!=-1)
                    {
                        Line[k1].Bor[1] = L + 1;
                        Line[o].Bor[1] = c+1;
                        Tri[c].Bor[d] = L + 1;
                        Tri[L].Bor[2] = c+1;
                    }
                    o++;

                    k2 = Judge2(Tri, L, Linetemp[0],ref c,ref d);
                    Line[o] = new DataStucture.Line();
                    Line[o].ID = o + 1;
                    Line[o].Point[0] = Linetemp[0].Point[0];
                    Line[o].Point[1] = Linetemp[0].Point[1];
                    Line[o].Bor[0] = L + 1;
                    Tri[L].Line[1] = Line[o];
                    
                    if(k2!=-1)
                    {
                        Line[k2].Bor[1] = L + 1;
                        Line[o].Bor[1] = c + 1;
                        Tri[c].Bor[d] = L + 1;
                        Tri[L].Bor[1] = c + 1;
                    }
                    o++;

                    Line[o] = new DataStucture.Line();
                    Line[o].ID = o + 1;
                    Line[o].Point[0] = Tri[L].Peak[0];
                    Line[o].Point[1] = Tri[L].Peak[1];
                    Line[o].Bor[0] = L + 1;
                    Line[o].Bor[1] = K+1;
                    Tri[L].Line[0] = Line[o];
                    o++;

                    Tri[K].Bor[0] = L + 1;
                    Tri[L].Bor[0] = K+1;
                    L++;
                    re++;
                }
                else
                {
                    //if (K+1 == Line[Tri[K].Line[0].ID-1].Bor[0] && Line[Tri[K].Line[0].ID-1].Bor[1] != 0)
                    //    Tri[K].Bor[0] = Line[Tri[K].Line[0].ID-1].Bor[1];
                    //else if (K+1 == Line[Tri[K].Line[0].ID-1].Bor[0] && Line[Tri[K].Line[0].ID-1].Bor[1] == 0)
                    //    Tri[K].Bor[0] = -1;
                    //else if (K+1 != Line[Tri[K].Line[0].ID-1].Bor[0] && Line[Tri[K].Line[0].ID-1].Bor[0] == 0)
                    //    Tri[K].Bor[0] = -1;
                    //else if (K+1 != Line[Tri[K].Line[0].ID-1].Bor[0] && Line[Tri[K].Line[0].ID-1].Bor[0] != 0)
                    //    Tri[K].Bor[0] = Line[Tri[K].Line[0].ID-1].Bor[0];

                }
            }
            else
            {
                Tri[K].Bor[0] = -1;
                Tri[K].Line[0].Bor[1] = -1;
            }
                
            
            temp = BB.Search(Tri, K, 1, k, Value1);
            if (temp != -1)
            {
                Linetemp[0].Point[0] = temp;
                Linetemp[0].Point[1] = Tri[K].Peak[0];
                Linetemp[1].Point[0] = Tri[K].Peak[2];
                Linetemp[1].Point[1] = temp;
                jud = BB.Judge(Tri, L, Linetemp[0], Linetemp[1], Tri[K].Line[2]);
                if (jud == 1)
                {
                    Tri[L] = new DataStucture.Triangle();
                    Line[o] = new DataStucture.Line();
                    Tri[L].Line[0] = new DataStucture.Line();
                    Tri[L].Line[1] = new DataStucture.Line();
                    Tri[L].Line[2] = new DataStucture.Line();
                    Line[Tri[K].Line[2].ID - 1].Bor[1] = L + 1;
                    Tri[L].ID = L + 1;
                    Tri[L].Peak[0] = Tri[K].Peak[0];
                    Tri[L].Peak[1] = Tri[K].Peak[2];
                    Tri[L].Peak[2] = temp;

                    k1 = Judge2(Tri, L, Linetemp[0],ref c,ref d);
                    Line[o] = new DataStucture.Line();
                    Line[o].ID = o + 1;
                    Line[o].Point[0] = Linetemp[0].Point[0];
                    Line[o].Point[1] = Linetemp[0].Point[1];
                    Line[o].Bor[0] = L + 1;
                    Tri[L].Line[2] = Line[o];
                    
                    if (k1 != -1)
                    {
                        Line[k1].Bor[1] = L + 1;
                        Line[o].Bor[1] = c + 1;
                        Tri[c].Bor[d] = L + 1;
                        Tri[L].Bor[2] = c + 1;
                    }
                    o++;

                    k2 = Judge2(Tri, L, Linetemp[1],ref c,ref d);
                    Line[o] = new DataStucture.Line();
                    Line[o].ID = o + 1;
                    Line[o].Point[0] = Linetemp[1].Point[0];
                    Line[o].Point[1] = Linetemp[1].Point[1];
                    Line[o].Bor[0] = L + 1;
                    Tri[L].Line[1] = Line[o];
                    
                    if (k2 != -1)
                    {
                        Line[k2].Bor[1] = L + 1;
                        Line[o].Bor[1] = c + 1;
                        Tri[c].Bor[d] = L + 1;
                        Tri[L].Bor[1] = c + 1;
                    }
                    o++;

                    Line[o] = new DataStucture.Line();
                    Line[o].ID = o + 1;
                    Line[o].Point[0] = Tri[L].Peak[0];
                    Line[o].Point[1] = Tri[L].Peak[1];
                    Line[o].Bor[0] = L + 1;
                    Line[o].Bor[1] = K + 1;
                    Tri[L].Line[0] = Line[o];
                    o++;

                    Tri[K].Bor[2] = L + 1;
                    Tri[L].Bor[0] = K + 1;
                    L++;
                    re++;   
                }
                else
                {
                    //if (K == Line[Tri[K].Line[2].ID].Bor[0] &&  Line[Tri[K].Line[2].ID].Bor[1]!=0)
                    //    Tri[K].Bor[2] = Line[Tri[K].Line[2].ID].Bor[1];
                    //else if (K == Line[Tri[K].Line[2].ID].Bor[0] && Line[Tri[K].Line[2].ID].Bor[1] == 0)
                    //    Tri[K].Bor[2] = -1;
                    //else if(K != Line[Tri[K].Line[2].ID].Bor[0] &&  Line[Tri[K].Line[2].ID].Bor[0]==0)
                    //    Tri[K].Bor[2] = -1;
                    //else if(K != Line[Tri[K].Line[2].ID].Bor[0] &&  Line[Tri[K].Line[2].ID].Bor[0]!=0)
                    //    Tri[K].Bor[2] = Line[Tri[K].Line[2].ID].Bor[0];

                }
            }
            else
            {
                Tri[K].Line[2].Bor[1] = -1;
                Tri[K].Bor[2] = -1;
            }
                            
            temp = BB.Search(Tri, K, 0, k, Value1);
            if(temp!=-1)
            {
                Linetemp[0].Point[0] = temp;
                Linetemp[0].Point[1] = Tri[K].Peak[2];
                Linetemp[1].Point[0] = Tri[K].Peak[1];
                Linetemp[1].Point[1] = temp;
                jud = BB.Judge(Tri, L, Linetemp[0], Linetemp[1], Tri[K].Line[1]);
                if (jud == 1)
                {
                    Tri[L] = new DataStucture.Triangle();
                    Line[o] = new DataStucture.Line();
                    Tri[L].Line[0] = new DataStucture.Line();
                    Tri[L].Line[1] = new DataStucture.Line();
                    Tri[L].Line[2] = new DataStucture.Line();
                    Line[Tri[K].Line[1].ID - 1].Bor[1] = L + 1;
                    Tri[L].ID = L + 1;
                    Tri[L].Peak[0] = Tri[K].Peak[2];
                    Tri[L].Peak[1] = Tri[K].Peak[1];
                    Tri[L].Peak[2] = temp;

                    k1 = Judge2(Tri, L, Linetemp[0],ref c,ref d);
                    Line[o].ID = o + 1;
                    Line[o].Point[0] = Linetemp[0].Point[0];
                    Line[o].Point[1] = Linetemp[0].Point[1];
                    Line[o].Bor[0] = L + 1;
                    Tri[L].Line[2] = Line[o]; 
                    if (k1 != -1)
                    {
                        Line[k1].Bor[1] = L + 1;
                        Line[o].Bor[1] = c + 1;
                        Tri[c].Bor[d] = L + 1;
                        Tri[L].Bor[2] = c + 1;
                    }
                    o++;

                    k2 = Judge2(Tri, L, Linetemp[1],ref c,ref d);
                    Line[o] = new DataStucture.Line();
                    Line[o].ID = o + 1;
                    Line[o].Point[0] = Linetemp[1].Point[0];
                    Line[o].Point[1] = Linetemp[1].Point[1];
                    Line[o].Bor[0] = L + 1;
                    Tri[L].Line[1] = Line[o];
                    
                    if (k2 != -1)
                    {
                        Line[k2].Bor[1] = L + 1;
                        Line[o].Bor[1] = c + 1;
                        Tri[c].Bor[d] = L + 1;
                        Tri[L].Bor[1] = c + 1;
                    }
                    o++;

                    Line[o] = new DataStucture.Line();
                    Line[o].ID = o + 1;
                    Line[o].Point[0] = Tri[L].Peak[0];
                    Line[o].Point[1] = Tri[L].Peak[1];
                    Line[o].Bor[0] = L + 1;
                    Line[o].Bor[1] = K + 1;
                    Tri[L].Line[0] = Line[o];
                    o++;

                    Tri[K].Bor[1] = L + 1;
                    Tri[L].Bor[0] = K + 1;
                    L++;
                    re++;

                    
                }
                else
                {
                    //if (K == Line[Tri[K].Line[1].ID].Bor[0] && Line[Tri[K].Line[1].ID].Bor[1] != 0)
                    //    Tri[K].Bor[1] = Line[Tri[K].Line[1].ID].Bor[1];
                    //else if (K == Line[Tri[K].Line[1].ID].Bor[0] && Line[Tri[K].Line[1].ID].Bor[1] == 0)
                    //    Tri[K].Bor[1] = -1;
                    //else if (K != Line[Tri[K].Line[1].ID].Bor[0] && Line[Tri[K].Line[1].ID].Bor[0] == 0)
                    //    Tri[K].Bor[1] = -1;
                    //else if (K != Line[Tri[K].Line[1].ID].Bor[0] && Line[Tri[K].Line[1].ID].Bor[0] != 0)
                    //    Tri[K].Bor[1] = Line[Tri[K].Line[1].ID].Bor[0];

                }
            }
            else
            {
                Tri[K].Line[1].Bor[1] = -1;
                Tri[K].Bor[1] = -1;
            }
            return re;
        }
        public int Judge2(DataStucture.Triangle[] Tri,int L,DataStucture.Line B ,ref int c,ref int d)
        {
            //for(int i=0;i<o;i++)
            //{
            //    if ((A[i].Point[1] == B.Point[0] && A[i].Point[0] == B.Point[1]))
            //        return i;      
            //}
            for (int i = 0; i < L; i++)
            {
                for(int j=0;j<3;j++)
                {
                    if(Tri[i].Line[j].Point[1]==B.Point[0]&&Tri[i].Line[j].Point[0]==B.Point[1])
                    {
                        c = i;
                        d = j;
                        return (Tri[i].Line[j].ID-1);
                        
                    }
                }
            }
            return -1;
        }

    }
}
