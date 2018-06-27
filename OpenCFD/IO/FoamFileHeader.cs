using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;
using HopeCFD.OpenCFD.Db;
namespace HopeCFD.OpenCFD.IO
{
    [Serializable]
    public class FoamFileHeader:DictEntry
    {
        private string version;
        private Format format;
        private string fclass;
        private string fobject;
        private string local;

        public static FoamFileHeader ControlDictHeader
        {
            get
            {
                return new FoamFileHeader("2.0", Db.Format.ascii, "dictionary", "controlDict", "system");
            }
        }


        public static FoamFileHeader FvSolutionHeader
        {
            get
            {
                return new FoamFileHeader("2.0", Db.Format.ascii, "dictionary", "fvSolution", "system");
            }
        }

        public static FoamFileHeader FvSchemesHeader
        {
            get
            {
                return new FoamFileHeader("2.0", Db.Format.ascii, "dictionary", "fvSchemes", "system");
            }
        }

        public static FoamFileHeader TransportPropertiesHeader
        {
            get
            {
                return new FoamFileHeader("2.0", Db.Format.ascii, "dictionary", "transportProperties", "constant");
            }
        }

        public static FoamFileHeader TurbulencePropertiesHeader
        {
            get
            {
                return new FoamFileHeader("2.0", Db.Format.ascii, "dictionary", "turbulenceProperties", "constant");
            }
        }

        public FoamFileHeader(string version, Format format, string fclass, string fobject, string local)
        {
            this.version = version;
            this.format = format;
            this.fclass = fclass;
            this.fobject = fobject;
            this.local = local;
            this.Key = "FoamFile";
            this.Item = new SubDictItem();
            this.Father = null;
        }

        public string Version { get => version; set => version = value; }
        public Format Format { get => format; set => format = value; }
        public string Fclass { get => fclass; set => fclass = value; }
        public string Fobject { get => fobject; set => fobject = value; }
        public string Local { get => local; set => local = value; }
        public override string ToString()
        {
            this.NewChild("version", new DictItem(version));
            this.NewChild("format", new DictItem(format));
            this.NewChild("class", new DictItem(fclass));
            this.NewChild("object", new DictItem(fobject));
            this.NewChild("localion", new DictItem("\""+local+"\""));
            return base.ToString();
        }
        
    }
}
