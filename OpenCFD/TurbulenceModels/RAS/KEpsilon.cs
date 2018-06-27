using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.TurbulenceModels.RAS
{
    [Serializable]
    public class KEpsilon:RasModel
    {
        public KEpsilon():base()
        {
            this.RASModel = "kEpsilon";
        }
    }
}
