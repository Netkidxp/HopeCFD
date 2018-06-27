using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HopeCFD.OpenCFD.Db
{
    [Serializable]
    public class ScalerVar:Variable
    {
        private double _value;

        public ScalerVar(double value)
        {
            this._value = value;
            this.Dimension = null;
            this.Uniform = false;
        }

        public ScalerVar(double value,bool uniform)
        {
            this._value = value;
            this.Dimension = null;
            this.Uniform = uniform;
        }

        public ScalerVar(double value,Dimension dim)
        {
            this._value = value;
            this.Dimension = dim;
            this.Uniform = false;
        }

        public ScalerVar(double value, Dimension dim,bool uniform)
        {
            this._value = value;
            this.Dimension = dim;
            this.Uniform = uniform;
        }

        public double Value { get => Value; set => this.Value = value; }
        
        public override string ToString()
        {
            string s = _value.ToString();
            if (this.Dimension != null)
                s = this.Dimension.ToString() + " " + s;
            if (this.Uniform)
                s = "uniform " + s;
            return s;
        }

        public static ScalerVar Parase(string str)
        {
            return new ScalerVar(double.Parse(str));
        }

        public static bool TryParase(string str)
        {
            double td = 0;
            return double.TryParse(str,out td);
        }

    }
}
