using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Db;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.Solutions
{
    [Serializable]
    public class Pimple:Solution
    {
        SwitchType _solveFlow;
        int _nOuterCorrectors;
        int _nCorrector;
        SwitchType _SIMPLErho;
        SwitchType _turbOnFinalIterOnly;

        public Pimple():base()
        {
            this.Key = "PIMPLE";
            solveFlow = SwitchType.on;
            nOuterCorrectors = 1;
            nCorrector = 1;
            SIMPLErho = SwitchType.off;
            turbOnFinalIterOnly = SwitchType.on;
        }

        public SwitchType solveFlow { get => _solveFlow; set => _solveFlow = value; }
        public int nOuterCorrectors { get => _nOuterCorrectors; set => _nOuterCorrectors = value; }
        public int nCorrector { get => _nCorrector; set => _nCorrector = value; }
        public SwitchType SIMPLErho { get => _SIMPLErho; set => _SIMPLErho = value; }
        public SwitchType turbOnFinalIterOnly { get => _turbOnFinalIterOnly; set => _turbOnFinalIterOnly = value; }

        public override string ToString()
        {
            this.NewChild("solveFlow",new DictItem(solveFlow));
            this.NewChild("nOuterCorrectors", new DictItem(nOuterCorrectors));
            this.NewChild("nCorrector", new DictItem(nCorrector));
            this.NewChild("SIMPLErho", new DictItem(SIMPLErho));
            this.NewChild("turbOnFinalIterOnly", new DictItem(turbOnFinalIterOnly));

            return base.ToString();
        }
    }
}
