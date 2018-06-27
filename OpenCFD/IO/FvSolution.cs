using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;
using HopeCFD.OpenCFD.Solutions;
using HopeCFD.OpenCFD.Solvers;
namespace HopeCFD.OpenCFD.IO
{
    [Serializable]
    public class FvSolution : FoamFile
    {
        DictEntry _solvers;
        Solution _solution;
        RelaxationFactors _relaxation;
        public FvSolution(Solution solution)
        {
            _solvers = DictEntry.Root("solvers");
            _relaxation = new RelaxationFactors();
            _solution = solution;
        }

        public DictEntry Solvers { get => _solvers; }
        public Solution Solution { get => _solution; set => _solution = value; }
        public RelaxationFactors Relaxation { get => _relaxation; }

        public override void Write(string root)
        {
            this.header = FoamFileHeader.FvSolutionHeader;
            ClearContent();
            AddContent(Solvers);
            AddContent(Solution);
            AddContent(Relaxation);
            base.Write(root);
        }
        public void AddSolver(Solver solver)
        {
            _solvers.AddChild(solver);
        }
        public void RemoveSolver(string fieldName)
        {
            _solvers.RemoveChild(fieldName);
        }
        public void SetFieldRelaxtionFactors(string fieldName,double factor)
        {
            _relaxation.AddFieldFactor(fieldName, factor);
        }
        public void SetEquationRelaxtionFactors(string equationName, double factor)
        {
            _relaxation.AddEquationFactor(equationName, factor);
        }

    }
}
