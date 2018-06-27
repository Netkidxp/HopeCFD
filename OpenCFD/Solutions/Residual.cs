using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.Solutions
{
    [Serializable]
    public class Residual : DictEntry
    {
        public Residual()
        {
            this.Key = "residualControl";
            this.Item = new SubDictItem();
        }
        public void SetResidual(string fieldName,double residual)
        {
            this.NewChild(fieldName, new DictItem(residual));
        }
    }
}
