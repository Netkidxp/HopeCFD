using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.TurbulenceModels
{
    [Serializable]
    public abstract class TurbulenceModel:DictEntry
    {
        public static double CalRe(double rho,double velocity,double length,double viscosity)
        {
            return rho * velocity * length / viscosity;
        }
        public static double CalI(double Re)
        {
            return 0.16 * Math.Pow(Re, -1.0 / 8.0);
        }
    }
}
