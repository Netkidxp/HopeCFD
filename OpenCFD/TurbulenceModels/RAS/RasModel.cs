using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;
using HopeCFD.OpenCFD.Db;
namespace HopeCFD.OpenCFD.TurbulenceModels.RAS
{
    [Serializable]
    public abstract class RasModel : TurbulenceModel
    {
        SwitchType _turbulence;

        SwitchType _printCoeffs;

        double _kMin;

        //- Lower limit of epsilon
        double _epsilonMin;

        //- Lower limit for omega
        double _omegaMin;

        DictEntry coeffs;

        string _RASModel;

        public void SetCoeffes(string name,double coeff)
        {
            coeffs.NewChild(name, new DictItem(coeff));
        }
        public void RemoveCoeffes(string name)
        {
            coeffs.RemoveChild(name);
        }
        public void ClearCoeefes()
        {
            coeffs.ClearChild();
        }
        protected RasModel()
        {
            this.Key = "RAS";
            turbulence = SwitchType.on;
            printCoeffs = SwitchType.on;
            coeffs = DictEntry.Root("coeffs", new SubDictItem());
        }

        
        public SwitchType turbulence { get => _turbulence; set => _turbulence = value; }
        public SwitchType printCoeffs { get => _printCoeffs; set => _printCoeffs = value; }
        public double kMin { get => _kMin; set => _kMin = value; }
        public double epsilonMin { get => _epsilonMin; set => _epsilonMin = value; }
        public double omegaMin { get => _omegaMin; set => _omegaMin = value; }
        public string RASModel { get => _RASModel; set => _RASModel = value; }

        public override string ToString()
        {
            this.ClearChild();
            this.NewChild("RASModel", RASModel);
            this.NewChild("turbulence", turbulence);
            this.NewChild("printCoeffs", printCoeffs);
            this.NewChild("kMin", kMin);
            this.NewChild("epsilonMin", epsilonMin);
            this.NewChild("omegaMin", omegaMin);

            coeffs.Key = this.RASModel + "Coeffs";
            SubDictItem scs = coeffs.Item as SubDictItem;
            if(scs.Entrys.Count>0)
                this.AddChild(coeffs);
            return base.ToString();
        }
    }
}
