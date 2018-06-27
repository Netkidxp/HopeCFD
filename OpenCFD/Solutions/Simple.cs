using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeCFD.OpenCFD.Solutions
{
    [Serializable]
    public class Simple : Solution
    {
        public Simple():base()
        {
            this.Key = "SIMPLE";
        }
    }
}
