using HopeCFD.OpenCFD.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeCFD.OpenCFD.TurbulenceModels.RAS
{
    [Serializable]
    public class RNGkEpsilon:KEpsilon
    {
        public RNGkEpsilon():base()
        {
            this.RASModel = "RNGkEpsilon";
        }
    }
}
