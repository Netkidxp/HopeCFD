using HopeCFD.OpenCFD.Db;
using HopeCFD.OpenCFD.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeCFD.OpenCFD.Boundarys
{
    [Serializable]
    public class Calculated:Boundary
    {
        private Variable _value;

        public Calculated(string patchName, Variable value) : base(patchName)
        {
            this.TypeName = "calculated";
            this._value = value;
        }

        public Variable Value { get => _value; set => this._value = value; }

        public override string ToString()
        {
            this.NewChild("value", new DictItem(_value));
            return base.ToString();
        }
    }
}
