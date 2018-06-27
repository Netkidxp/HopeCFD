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
    public class FixedValue : Boundary
    {
        private Variable _value;

        public FixedValue(string patchName,Variable value) : base(patchName)
        {
            this.TypeName = "fixedValue";
            this._value = value;
        }

        public  Variable Value { get => _value; set => this._value = value; }

        public override string ToString()
        {
            this.NewChild("value", new DictItem(_value));
            return base.ToString();
        }
    }
}
