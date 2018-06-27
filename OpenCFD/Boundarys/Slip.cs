using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeCFD.OpenCFD.Boundarys
{
    [Serializable]
    public class Slip : Boundary
    {
        public Slip(string patchName) : base(patchName)
        {
            this.TypeName = "slip";
        }
    }
}
