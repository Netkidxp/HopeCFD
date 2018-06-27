using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.Solutions
{
    [Serializable]
    public class RelaxationFactors : DictEntry
    {
        DictEntry fields;
        DictEntry equations;
        public RelaxationFactors()
        {
            fields = this.NewChild("fields");
            equations = this.NewChild("equations");
            this.Key = "relaxationFactors";
        }
        public void AddFieldFactor(string fieldName,double factor)
        {
            fields.NewChild(fieldName,new DictItem(factor));
        }
        public void AddEquationFactor(string equationName, double factor)
        {
            equations.NewChild(equationName, new DictItem(factor));
        }
    }
}
