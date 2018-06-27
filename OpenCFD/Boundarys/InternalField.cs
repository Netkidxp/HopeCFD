using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Db;
using HopeCFD.OpenCFD.IO;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.Boundarys
{
    [Serializable]
    public class InternalField:DictEntry
    {
        private Variable _value;

        public InternalField(Variable value):base("internalField")
        {
            this.Value = value;

        }
        public Variable Value { get => _value; set => this._value = value; }
        public override string ToString()
        {
            this.Item = new DictItem(_value);
            return base.ToString();
        }
    }
}
