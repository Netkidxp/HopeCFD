using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;

namespace HopeCFD.OpenCFD.Solvers
{
    [Serializable]
    public class SmootherSolver : Solver
    {

        SmootherTypes smootherType;
        public SmootherSolver(string fieldName, double tolerance, double relTol, SmootherTypes smootherType) :base(fieldName, tolerance, relTol)
        {
            solver = SolverTypes.smoothSolver;
            this.SmootherType = smootherType;
        }

        public SmootherTypes SmootherType
        {
            get => smootherType;
            set
            {
                smootherType = value;
                this.NewChild("smoother", new DictItem(value));
            }
        }
    }
}
