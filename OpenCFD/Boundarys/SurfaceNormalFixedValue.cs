using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HopeCFD.OpenCFD.Db;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.Boundarys
{
    [Serializable]
    public class SurfaceNormalFixedValue:Boundary
    {
        private ScalerVar _value;

        public SurfaceNormalFixedValue(string patchName, ScalerVar refValue) : base(patchName)
        {
            this._value = refValue;
            this.TypeName = "surfaceNormalFixedValue";
        }
        public ScalerVar Value { get => _value; set => _value = value; }
        public override string ToString()
        {
            this.NewChild("refValue", new DictItem(_value));
            return base.ToString();
        }
    }
}