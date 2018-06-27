using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeCFD.OpenCFD.Dictionary
{
    public class DictionaryItem
    {
        
        private string key;
        private List<object> items;
        private DictionaryItem father;
        private int level = 0;
        public const char SeparatorChar = '|';
        public const string WildcardChar = "*";
        protected DictionaryItem(string key, DictionaryItem father)
        {
            this.key = key;
            items = new List<object>();
            this.father = father;
        }
        public void AddItem(object item)
        {
            items.Add(item);
        }
        public DictionaryItem NewChild(string key, object item)
        {
            //return new DictionaryItem(name, value, this);
            
            if (_value == null)
                _value = new List<DictionaryItem>();
            if (!(_value is List<DictionaryItem>))
                return null;
            else
            {
                DictionaryItem di = new DictionaryItem(name, value, this);
                List<DictionaryItem> ldi = _value as List<DictionaryItem>;
                ldi.Add(di);
                di.level = this.level + 1;
                return di;
            }
            
            DictionaryItem di = new DictionaryItem(key, item, this);


        }
        public static DictionaryItem Root(string name, object value)
        {
            return new DictionaryItem(name, value, null);
        }
        public string Name { get => name; set => name = value; }
        public object Value { get => _value; set => this._value = value; }
        public DictionaryItem Father { get => father;}

        public override bool Equals(object obj)
        {
            if(obj is DictionaryItem)
            {
                DictionaryItem di = obj as DictionaryItem;
                return di.Name == name && _value.Equals(di.Value);
            }
            else
                return _value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            string pre = "";
            for (int i =0;i<level;i++)
            {
                pre += "\t";
            }
            if (_value is List<DictionaryItem>)
            {
                string s = pre + this.name + "\n";
                s += pre +"{\n";
                foreach (DictionaryItem di in _value as List<DictionaryItem>)
                {
                    s += di.ToString();
                }
                s += pre + "}\n";
                return s;
            }
            else
                return pre + name + "\t" + _value.ToString() + ";\n";
        }

        public bool RemoveItem(string path)
        {
            DictionaryItem di = Lookup(path);
            if (di == null)
                return false;
            else
            {
                if (di.father != null)
                {
                    List<DictionaryItem> lis = di.father.Value as List<DictionaryItem>;
                    return lis.Remove(di);
                }
                else
                    return false;
            }
        }
        public DictionaryItem Lookup(string path)
        {
            
            if (!(_value is List<DictionaryItem>))
                return null;
            string[] names = path.Split(SeparatorChar);
            List<string> ls = new List<string>();
            foreach(string cs in names)
            {
                if (cs.Length != 0)
                    ls.Add(cs);
            }
            DictionaryItem ci = null;
            foreach (DictionaryItem di in _value as List<DictionaryItem>)
            {
                if (di.Name == ls[0]||ls[0]==WildcardChar)
                    ci = di;
            }
            if (ci == null)
                return null;
            if (ls.Count ==1)
                return ci;
            else
            {

                string ns = ls[1];
                for(int i=2;i<ls.Count;i++)
                {
                    ns += "|" + ls[i];
                }
                return ci.Lookup(ns);
            }
        }
    }
}
