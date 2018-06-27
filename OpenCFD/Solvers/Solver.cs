using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.Solvers
{
    [Serializable]
    abstract public class Solver:DictEntry
    {
        public enum SolverTypes
        {
            PCG,
            PBiCG,
            smoothSolver,
            GAMG,
            diagonal
        }
        public enum SmootherTypes
        {
            GaussSeidel,
            DIC,
            DICGaussSeidel
        }
        protected SolverTypes _solver;
        double _tolerance;
        double _relTol;

        public Solver(string fieldName, double tolerance, double relTol)
        {
            this.Key = fieldName;
            this.tolerance = tolerance;
            this.relTol = relTol;
        }

        protected SolverTypes solver
        {
            get => _solver;
            set
            {
                _solver = value;
                NewChild("solver", new DictItem(value));
            }
        }
        public double tolerance
        {
            get => _tolerance;
            set
            {
                _tolerance = value;
                NewChild("tolerance", new DictItem(value));
            }
        }

        public double relTol
        {
            get => _relTol;
            set
            {
                _relTol = value;
                NewChild("relTol", new DictItem(value));
            }
        }

    }

}
