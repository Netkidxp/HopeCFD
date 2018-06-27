using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Db;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.Boundarys
{
    [Serializable]
    public class EpsilonWallFunction:Boundary
    {
        private double _value;

        public EpsilonWallFunction(string patchName,double value) : base(patchName)
        {
            this.TypeName = "epsilonWallFunction";
            this.Value = value;
        }
        public double Value { get => _value; set => this._value = value; }

        public override string ToString()
        {
            this.NewChild("value", new DictItem(new ScalerVar(_value,true)));
            return base.ToString();
        }
    }
}
