using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HopeCFD.OpenCFD.Db
{
    [Serializable]
    public class VectorVar : Variable
    {
        private double v1, v2, v3;

        public VectorVar(double v1, double v2, double v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
        public VectorVar(double v1, double v2, double v3, Dimension dim):base(dim)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        public VectorVar(double v1, double v2, double v3, bool uniform):base(uniform)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
        public VectorVar(double v1, double v2, double v3,Dimension dim, bool uniform):base(dim,uniform)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
        public double V1 { get => v1; set => v1 = value; }
        public double V2 { get => v2; set => v2 = value; }
        public double V3 { get => v3; set => v3 = value; }

        public override bool Equals(object obj)
        {
            if (obj is VectorVar)
            {
                VectorVar v = obj as VectorVar;
                return v.v1 == v1 && v.v2 == v2 && v.v3 == v3;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            string s = "(" + v1.ToString() + " " + v2.ToString() + " " + v3.ToString() + ")";

            if (this.Dimension != null)
                s = this.Dimension.ToString() + " " + s;
            if (this.Uniform)
                s = "uniform " + s;
            return s;
        }
        public static VectorVar Parase(string str)
        {
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            string[] ss = str.Split(' ');
            ArrayList al = new ArrayList();
            foreach (string s in ss)
            {
                if (s != "")
                    al.Add(s);
            }
            if (al.Count != 3)
                return null;
            else
            {
                double dv1, dv2, dv3;
                bool bv1, bv2, bv3;
                bv1 = double.TryParse((string)al[0], out dv1);
                bv2 = double.TryParse((string)al[1], out dv2);
                bv3 = double.TryParse((string)al[2], out dv3);
                if (bv1 && bv2 && bv3)
                    return new VectorVar(dv1, dv2, dv3);
                else
                    return null;
            }
        }
        public static bool TryParase(string str)
        {
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            string[] ss = str.Split(' ');
            ArrayList al = new ArrayList();
            foreach (string s in ss)
            {
                if (s != "")
                    al.Add(s);
            }
            if (al.Count != 3)
                return false;
            else
            {
                double dv1, dv2, dv3;
                bool bv1, bv2, bv3;
                bv1 = double.TryParse((string)al[0], out dv1);
                bv2 = double.TryParse((string)al[1], out dv2);
                bv3 = double.TryParse((string)al[2], out dv3);
                if (bv1 && bv2 && bv3)
                    return true;
                else
                    return false;
            }
        }
    }
}
