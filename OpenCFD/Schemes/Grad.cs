using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeCFD.OpenCFD.Schemes
{
    [Serializable]
    public class Grad:Scheme
    {
        public enum LimitedTypes
        {
            cellLimited,
            faceLimited,
            none
        }
        public enum GradTypes
        {
            Gauss,
            leastSquares,
            fourth
        }
        LimitedTypes limitedType;
        GradTypes gradType;
        Interpolation interpolation;
        string parameters;

        public Grad(GradTypes gradType)
        {
            this.gradType = gradType;
            limitedType = LimitedTypes.none;
            interpolation = null;
            parameters = "";
        }

        public Grad(GradTypes gradType, Interpolation interpolation)
        {
            this.gradType = gradType;
            this.interpolation = interpolation;
            this.limitedType = LimitedTypes.none;
            parameters = "";
        }

        public Grad(GradTypes gradType, Interpolation interpolation, LimitedTypes limitedType, string parameters)
        {
            this.limitedType = limitedType;
            this.gradType = gradType;
            this.interpolation = interpolation;
            this.parameters = parameters;
        }

        public LimitedTypes LimitedType1 { get => limitedType; set => limitedType = value; }
        public GradTypes GradType { get => gradType; set => gradType = value; }
        public Interpolation Interpolation { get => interpolation; set => interpolation = value; }
        public string Parameters { get => parameters; set => parameters = value; }

        public override string Value
        {
            get
            {
                string sf = gradType.ToString();
                if (interpolation != null)
                    sf += " " + interpolation.Value;
                if (limitedType != LimitedTypes.none)
                    sf = limitedType.ToString() + " " + parameters;
                return sf;
            }
        }
    }
}
