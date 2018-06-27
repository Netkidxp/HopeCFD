using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.IO
{
    public class DictionaryFile:IIo
    {
        private FoamFileAnotation foamAnotation;
        private FoamFileHeader foamHeader;
        private List<DictionaryItem> dictionaryItems;
        private string dictName;
        protected DictionaryFile()
        {

        }
        public DictionaryFile(string dictName,string local)
        {
            this.dictName = dictName;
            this.foamAnotation = FoamFileAnotation.Default;
            this.foamHeader = new FoamFileHeader("2.0", "ascii", "dictionary", dictName, local);
            this.dictionaryItems = new List<DictionaryItem>();
        }
        public List<DictionaryItem> DictionaryItems { get => dictionaryItems; set => dictionaryItems = value; }
        public string DictName { get => dictName;}
        public FoamFileHeader FoamHeader { get => foamHeader;}
        public FoamFileAnotation FoamAnotation { get => foamAnotation;}

        public bool Write(string root)
        {
            try
            {
                if (!Directory.Exists(root))
                    Directory.CreateDirectory(root);
                if (!Directory.Exists(root + Path.DirectorySeparatorChar + FoamHeader.Local))
                    Directory.CreateDirectory(root + Path.DirectorySeparatorChar + FoamHeader.Local);
                StreamWriter sw = new StreamWriter(root + Path.DirectorySeparatorChar + FoamHeader.Local + Path.DirectorySeparatorChar + DictName, false, Encoding.Unicode);
                sw.Write(FoamAnotation);
                sw.Write(FoamHeader);
                foreach(DictionaryItem di in DictionaryItems)
                {
                    sw.Write(di);
                }
                sw.Close();
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
