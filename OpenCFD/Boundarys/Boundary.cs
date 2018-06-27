using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.IO;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.Boundarys
{
    [Serializable]
    public abstract class Boundary:DictEntry
    {
        private string patchName;
        private string typeName;

        public Boundary(string patchName):base(patchName)
        {
            this.PatchName = patchName;
            this.TypeName = "";
        }
        public string PatchName
        {
            get => patchName;
            set
            {
                this.Key = value;
                patchName = value;
            }
        }
        public string TypeName
        {
            get => typeName;
            set
            {
                typeName = value;
                this.NewChild("type", new DictItem(typeName));
            }
        }
    }
}
