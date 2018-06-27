using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeCFD.OpenCFD.Dictionary
{
    [Serializable]
    public class DictItem
    {
        protected string _value;

        public virtual string Value
        {
            get => _value;
        }
        protected DictItem()
        {

        }
        public DictItem(string item)
        {
            _value = item;
        }
        public DictItem(object obj)
        {
            
            this._value = obj.ToString();
        }

        public override bool Equals(object obj)
        {
            return _value == obj.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("\t{0};",Value);
        }
    }
}
