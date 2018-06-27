using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;

namespace HopeCFD.OpenCFD.Solvers
{
    [Serializable]
    public class PCG : Solver
    {
        public enum Preconditioners
        {
            DIC,
            FDIC,
            DILU,
            diagonal,
            GAMG,
            none
        }
        Preconditioners preconditioner;

        internal Preconditioners Preconditioner
        {
            get => preconditioner;
            set
            {
                preconditioner = value;
                this.NewChild("preconditioner", new DictItem(value));
            }
        }
        public PCG(string fieldName, double tolerance, double relTol, Preconditioners preconditioner):base(fieldName, tolerance, relTol)
        {
            solver = SolverTypes.PCG;
            this.Preconditioner = preconditioner;
        }
    }
}
