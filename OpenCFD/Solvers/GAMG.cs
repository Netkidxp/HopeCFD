using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Db;
using HopeCFD.OpenCFD.Dictionary;

namespace HopeCFD.OpenCFD.Solvers
{
    [Serializable]
    public class GAMG : Solver
    {
        SmootherTypes smootherType;

        SwitchType cacheAgglomeration_;

        //- Number of pre-smoothing sweeps
        int nPreSweeps_;

        //- Lever multiplier for the number of pre-smoothing sweeps
        int preSweepsLevelMultiplier_;

        //- Maximum number of pre-smoothing sweeps
        int maxPreSweeps_;

        //- Number of post-smoothing sweeps
        int nPostSweeps_;

        //- Lever multiplier for the number of post-smoothing sweeps
        int postSweepsLevelMultiplier_;

        //- Maximum number of post-smoothing sweeps
        int maxPostSweeps_;

        //- Number of smoothing sweeps on finest mesh
        int nFinestSweeps_;

        //- Choose if the corrections should be interpolated after injection.
        //  By default corrections are not interpolated.
        SwitchType interpolateCorrection_;

        //- Choose if the corrections should be scaled.
        //  By default corrections for symmetric matrices are scaled
        //  but not for asymmetric matrices.
        //SwitchType scaleCorrection_;

        //- Direct or iteratively solve the coarsest level
        SwitchType directSolveCoarsest_;

        public GAMG(string fieldName, double tolerance, double relTol, SmootherTypes smootherType) :base(fieldName, tolerance, relTol)
        {
            this.solver = SolverTypes.GAMG;
            this.smoother = smootherType;
            cacheAgglomeration = SwitchType.on;
            nPreSweeps = 0;
            preSweepsLevelMultiplier = 1;
            maxPostSweeps = 4;
            nPostSweeps = 2;
            postSweepsLevelMultiplier = 1;
            maxPostSweeps = 4;
            nFinestSweeps = 2;
            interpolateCorrection = SwitchType.off;
            directSolveCoarsest = SwitchType.off;
        }

        public SmootherTypes smoother
        {
            get => smootherType;
            set
            {
                smootherType = value;
                this.NewChild("smoother", new DictItem(value));
            }
            
        }
        public SwitchType cacheAgglomeration
        {
            get => cacheAgglomeration_;
            set
            {
                cacheAgglomeration_ = value;
                this.NewChild("cacheAgglomeration", new DictItem(value));
            }
        }

        public int nPreSweeps
        {
            get => nPreSweeps_;
            set
            {
                nPreSweeps_ = value;
                this.NewChild("nPreSweeps", new DictItem(value));
            }
        }

        public int preSweepsLevelMultiplier
        {
            get => preSweepsLevelMultiplier_;
            set
            {
                preSweepsLevelMultiplier_ = value;
                this.NewChild("preSweepsLevelMultiplier", new DictItem(value));
            }
        }

        public int maxPreSweeps
        {
            get => maxPreSweeps_;
            set
            {

                maxPreSweeps_ = value;
                this.NewChild("maxPreSweeps", new DictItem(value));
            }
        }


        public int nPostSweeps
        {
            get => nPostSweeps_;
            set
            {
                nPostSweeps_ = value;
                this.NewChild("nPostSweeps", new DictItem(value));
            }
        }


        public int postSweepsLevelMultiplier
        {
            get => postSweepsLevelMultiplier_;
            set
            {
                postSweepsLevelMultiplier_ = value;
                this.NewChild("postSweepsLevelMultiplier", new DictItem(value));
            }
        }


        public int maxPostSweeps
        {
            get => maxPostSweeps_;
            set
            {
                maxPostSweeps_ = value;
                this.NewChild("maxPostSweeps", new DictItem(value));
            }
        }

        public int nFinestSweeps
        {
            get => nFinestSweeps_;
            set
            {
                nFinestSweeps_ = value;
                this.NewChild("nFinestSweeps", new DictItem(value));
            }
        }
        public SwitchType interpolateCorrection
        {
            get => interpolateCorrection_;
            set
            {
                interpolateCorrection_ = value;
                this.NewChild("interpolateCorrection", new DictItem(value));
            }
        }
        //public SwitchType scaleCorrection { get => scaleCorrection_; set => scaleCorrection_ = value; }
        public SwitchType directSolveCoarsest
        {
            get => directSolveCoarsest_;
            set
            {
                directSolveCoarsest_ = value;
                this.NewChild("directSolveCoarsest", new DictItem(value));
            }
        } 
    }
}
