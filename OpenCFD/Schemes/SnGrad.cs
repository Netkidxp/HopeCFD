using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeCFD.OpenCFD.Schemes
{
    [Serializable]
    public class SnGrad:Scheme
    {
        public enum SnGradTypes
        {
            corrected,
            uncorrected,
            limited,
            bounded,
            fourth
        }
        SnGradTypes snGradType;
        string parameters;

        public SnGrad(SnGradTypes snGradType, string parameters)
        {
            this.snGradType = snGradType;
            this.parameters = parameters;
        }


        public SnGrad(SnGradTypes snGradType)
        {
            this.snGradType = snGradType;
            this.parameters = "";
        }

        public SnGradTypes SnGradType { get => snGradType; set => snGradType = value; }
        public string Parameters { get => parameters; set => parameters = value; }

        public override string Value
        {
            get
            {
                string s = "";
                if (parameters.Length > 0)
                    s = snGradType.ToString() + " " + parameters;
                else
                    s = snGradType.ToString();
                return s;
            }
        }

    }
}
