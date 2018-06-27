using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HopeCFD.OpenCFD
{
    class SurfaceNormalFixedValue:Boundary
    {
        private double refValue;

        public SurfaceNormalFixedValue(string patchName, Field field, double refValue) : base(patchName, field)
        {
            this.RefValue = refValue;
            this.TypeName = "surfaceNormalFixedValue:Boundary";
        }
        public double RefValue { get => refValue; set => refValue = value; }

        protected override string DictionaryContent
        {
            get
            {
                return "refValue uniform "+ RefValue.ToString()+";\nvalue uniform (0 0 0);\n";
            }
        }
    }
}