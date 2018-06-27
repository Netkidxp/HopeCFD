using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.Schemes
{
    [Serializable]
    public class Ddt:Scheme
    {
        public enum DdtTypes
        {
            Euler,
            localEuler,
            CrankNicholson,
            backward,
            steadyState
        }
        DdtTypes ddtType;

        public override string Value => ddtType.ToString();

        public Ddt(DdtTypes ddtType)
        {
            this.DdtType = ddtType;
        }

        public DdtTypes DdtType
        {
            get => ddtType;
            set
            {
                ddtType = value;
            }
        }
        
    }
}
