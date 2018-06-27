using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeCFD.OpenCFD
{
    class Boundary
    {
        private string patchName;
        private string typeName;
        private Field field;

        public Boundary(string patchName, Field field)
        {
            this.PatchName = patchName;
            this.TypeName = "";
            this.Field = field;
        }

        private string DictionaryHead { get => (TypeName +"\n" + "{\ntype\t"+TypeName+";\n"); }
        private string DictionaryTail { get => ("}\n"); }
        protected virtual string DictionaryContent{ get => "\n"; }
        public string PatchName { get => patchName; set => patchName = value; }
        public string TypeName { get => typeName; set => typeName = value; }
        public Field Field { get => field; set => field = value; }

        public string ToDictionaryItem()
        {
            return DictionaryHead + DictionaryContent + DictionaryTail;
        }
    }
}
