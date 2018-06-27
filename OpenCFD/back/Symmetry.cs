using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeCFD.OpenCFD
{
    class Symmetry : Boundary
    {
        public Symmetry(string patchName, Field field) : base(patchName, field)
        {
            this.TypeName = "symmetry";
        }
    }
}
