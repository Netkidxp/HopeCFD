using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.TurbulenceModels;
namespace HopeCFD.OpenCFD.IO
{
    [Serializable]
    public class TurbulenceProperties : FoamFile
    {
        TurbulenceModel model;

        public TurbulenceProperties(TurbulenceModel model)
        {
            this.model = model;
        }

        public TurbulenceModel Model { get => model; set => model = value; }

        public override void Write(string root)
        {
            this.ClearContent();
            this.AddContent("simulationType", model.Key);
            this.AddContent(model);
            this.header = FoamFileHeader.TurbulencePropertiesHeader;
            base.Write(root);
        }
    }
}
