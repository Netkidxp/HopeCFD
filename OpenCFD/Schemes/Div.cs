using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.Schemes
{
    [Serializable]
    public class Div:Scheme
    {
        public enum DivTypes
        {
            Gauss
        }
        DivTypes divType;
        Interpolation interpolation;
        bool bounded;
        
        public Div(DivTypes divType, Interpolation interpolation)
        {
            this.DivType = divType;
            this.Interpolation = interpolation;
            this.bounded = false;
        }

        public Div(bool bounded, DivTypes divType, Interpolation interpolation)
        {
            this.DivType = divType;
            this.Interpolation = interpolation;
            this.bounded = bounded;
        }

        public DivTypes DivType { get => divType; set => divType = value; }
        public Interpolation Interpolation { get => interpolation; set => interpolation = value; }
        public bool Bounded { get => bounded; set => bounded = value; }

        public override string Value
        {
            get
            {
                string s = divType.ToString() + " " + interpolation.Value;
                if (bounded)
                    s = "bounded " + s;
                return s;
            }
        }
    }
}
