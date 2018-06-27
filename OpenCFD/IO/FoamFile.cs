using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.IO
{
    [Serializable]
    public class FoamFile
    {
        static FoamFileAnotation anotation = FoamFileAnotation.Default;
        protected FoamFileHeader header;
        static protected List<DictEntry> content=new List<DictEntry>();

        public virtual void AddContent(string key,object obj)
        {
            RemoveContent(key);
            content.Add(DictEntry.Root(key, new DictItem(obj)));
        }
        public virtual void AddContent(DictEntry entry)
        {
            RemoveContent(entry.Key);
            content.Add(entry);
        }
        public virtual void RemoveContent(string key)
        {
            DictEntry de = GetEntry(key);
            if (de != null)
                content.Remove(de);
        }
        public virtual void ClearContent()
        {
            content.Clear();
        }
        public DictEntry GetEntry(string key)
        {
            foreach(DictEntry de in content)
            {
                if (de.Key == key)
                    return de;
            }
            return null;
        }
        public virtual void Write(string root)
        {
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);
            if (!Directory.Exists(root + Path.DirectorySeparatorChar + header.Local))
                Directory.CreateDirectory(root + Path.DirectorySeparatorChar + header.Local);
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);
            StreamWriter sw = new StreamWriter(root + Path.DirectorySeparatorChar + header.Local + Path.DirectorySeparatorChar + header.Fobject, false, utf8WithoutBom);
            sw.Write(anotation);
            sw.Write(header);
            sw.Write("\n\n"+@"// * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * //"+"\n");
            foreach (DictEntry de in content)
            {
                sw.Write(de);
            }
            sw.Write("\n\n"+@"// * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * //"+"\n");
            sw.Flush();
            sw.Close();
        }
    }
}
