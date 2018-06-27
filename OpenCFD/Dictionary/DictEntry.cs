using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeCFD.OpenCFD.Dictionary
{
    [Serializable]
    public class DictEntry
    {
        public const char SeparatorChar = '>';

        string _key;
        DictEntry _father;
        private int level;
        DictItem _item;
        protected string prefix
        {
            get
            {
                if (_father == null)
                    return "";
                else
                    return "\t";
            }
        }

        public string Key { get => _key; set => _key = value; }
        public DictEntry Father
        {
            get => _father;
            set
            {
                _father = value;
                if (_father != null)
                {
                    level = _father.level + 1;
                    if (_item is SubDictItem)
                    {
                        SubDictItem cs = _item as SubDictItem;
                        foreach (DictEntry e in cs.Entrys)
                            e.Father = this;
                    }
                }
                else
                    level = 0;
            }
        }
        protected int Level { get => level;}
        public DictItem Item { get => _item; set => _item = value; }

        protected DictEntry(string key)
        {
            _key = key;
            _item = new SubDictItem();
            _father = null;
            level = 0;
        }

        protected DictEntry(string key, DictEntry father)
        {
            _key = key;
            _item = new SubDictItem();
            Father = father;
        }

        protected DictEntry(string key,DictEntry father,DictItem item)
        {
            _key = key;
            _item = item;
            _father = father;
            if (_father != null)
                level = _father.level + 1;
            else
                level = 0;
        }

        protected DictEntry()
        {
        }

        public static DictEntry Root(string key)
        {
            return new DictEntry(key);
        }
        public static DictEntry Root(string key,DictItem item)
        {
            return new DictEntry(key, null, item);
        }

        public void AddChild(DictEntry entry)
        {
            
            entry.Father = this;
            if (_item == null)
                _item = new SubDictItem();
            if (_item is SubDictItem)
            {
                SubDictItem cs = _item as SubDictItem;
                RemoveChild(entry.Key);
                cs.Add(entry);
            }
        }
        public DictEntry NewChild(string key)
        {
            if (_item == null)
                _item = new SubDictItem();
            if (_item is SubDictItem)
            {
                DictEntry de = new DictEntry(key, this);
                RemoveChild(key);
                AddChild(de);
                return de;
            }
            else
                return null;
        }
        public DictEntry NewChild(string key,DictItem item)
        {
            if (_item == null)
                _item = new SubDictItem();
            if (_item is SubDictItem)
            {
                DictEntry de = new DictEntry(key, this, item);
                RemoveChild(key);
                AddChild(de);
                return de;
            }
            else
                return null;
        }

        public DictEntry NewChild(string key, object o)
        {
            return NewChild(key, new DictItem(o));
        }

        public void ClearChild()
        {
            if (_item is SubDictItem)
            {
                SubDictItem sdi = _item as SubDictItem;
                sdi.Entrys.Clear();
            }
        }

        public void RemoveChild(string key)
        {
            if (_item is SubDictItem)
            {
                SubDictItem sdi = _item as SubDictItem;
                sdi.Remove(key);
            }
        }

        public DictEntry Lookup(string path)
        {
            if (!(_item is SubDictItem))
                return null;
            SubDictItem sde = _item as SubDictItem;

            string[] names = path.Split(SeparatorChar);
            List<string> ls = new List<string>();
            foreach (string cs in names)
            {
                if (cs.Length != 0)
                    ls.Add(cs);
            }
            DictEntry ci = null;
            foreach (DictEntry de in sde.Entrys)
            {
                if (de.Key == ls[0])
                    ci = de;
            }
            if (ci == null)
                return null;
            if (ls.Count == 1)
                return ci;
            else
            {

                string ns = ls[1];
                for (int i = 2; i < ls.Count; i++)
                {
                    ns += SeparatorChar + ls[i];
                }
                return ci.Lookup(ns);
            }
        }

        public override string ToString()
        {
            
            string s = string.Format("\n{0}{1}",_key,_item.ToString());
            
            return s.Replace("\n", "\n" + prefix);
        }
    }
}
