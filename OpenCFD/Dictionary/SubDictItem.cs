using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeCFD.OpenCFD.Dictionary
{
    [Serializable]
    class SubDictItem : DictItem
    {
        List<DictEntry> _entrys;

        public List<DictEntry> Entrys { get => _entrys; /*set => _entrys = value;*/ }

        public SubDictItem()
        {
            _entrys = new List<DictEntry>();
        }
        public SubDictItem(params DictEntry[] entrys)
        {
            _entrys = new List<DictEntry>();
            Add(entrys);
        }
        public void Add(DictEntry entry)
        {
            Entrys.Add(entry);
        }
        public void Add(params DictEntry[] entrys)
        {
            foreach (DictEntry entry in entrys)
                Add(entry);
        }
        public void Remove(DictEntry entry)
        {
            Entrys.Remove(entry);
        }
        public void Remove(string key)
        {
            DictEntry ce = null;
            foreach (DictEntry e in _entrys)
            {
                if (e.Key == key)
                    ce = e ;
            }
            if (ce != null)
                _entrys.Remove(ce);
        }
        public void Remove(params string[] keys)
        {
            foreach (string k in keys)
                Remove(k);
        }
        public override string ToString()
        {
            string content = "";
            foreach(DictEntry de in _entrys)
            {
                content += de.ToString();
            }
            return "\n{"+content+"\n}";
        }
    }
}
