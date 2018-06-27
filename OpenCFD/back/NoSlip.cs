using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeCFD.OpenCFD
{
    class NoSlip : Boundary
    {
        public NoSlip(string patchName, Field field) : base(patchName, field)
        {
            this.TypeName = "noSlip";
        }
    }
}
