using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;

namespace HopeCFD.OpenCFD.Schemes
{

    [Serializable]
    public class Interpolation : Scheme
    {
        public enum InterpolationTypes
        {
            linear,
            cubicCorrection,
            midpoint,
            upwind,
            linearUpwind,
            skewLinear,
            filteredLinear2,
            limtiedLinear,
            vanLeer,
            MUSCL,
            limitedCubic,
            SFCD,
            Gamma
        }
        InterpolationTypes interpolationType;
        string parameters;

        public InterpolationTypes InterpolationType { get => interpolationType; set => interpolationType = value; }
        public string Parameters { get => parameters; set => parameters = value; }

        public Interpolation(InterpolationTypes interpolationType, string parameters)
        {
            this.interpolationType = interpolationType;
            this.parameters = parameters;
        }

        public Interpolation(InterpolationTypes interpolationType)
        {
            this.interpolationType = interpolationType;
            this.parameters = "";
        }

        public override string Value
        {
            get
            {
                string s = "";
                if (parameters.Length > 0)
                    s = interpolationType.ToString() + " " + parameters;
                else
                    s = interpolationType.ToString();
                return s;
            }
        }

    }
}
