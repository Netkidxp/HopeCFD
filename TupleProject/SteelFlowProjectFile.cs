using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.IO;
using HopeCFD.OpenCFD.Boundarys;
using HopeCFD.OpenCFD.Db;
using HopeCFD.OpenCFD.Dictionary;
using HopeCFD.OpenCFD.Schemes;
using HopeCFD.OpenCFD.Solutions;
using HopeCFD.OpenCFD.Solvers;
using HopeCFD.OpenCFD.TurbulenceModels;
using HopeCFD.OpenCFD.TurbulenceModels.RAS;

namespace HopeCFD.Tuple
{
    [Serializable]
    public class SteelFlowProjectFile:ProjectFile
    {
        double inletVelocity;
        double kValue;
        double eValue;

        FieldFile U;
        FieldFile p;
        FieldFile k;
        FieldFile nut;
        FieldFile epsilon;


        List<string> inletNames;
        List<string> upfaceNames;
        List<string> symNames;
        List<string> outletNames;
        List<string> wallNames;

        private SteelFlowProjectFile():base()
        {
            inletNames = new List<string>();
            upfaceNames = new List<string>();
            symNames = new List<string>();
            outletNames = new List<string>();
            wallNames = new List<string>();

            U = new FieldFile("U", Dimension.U, new InternalField(new VectorVar(0.0,0.0,0.0,true)));
            p = new FieldFile("p", Dimension.P, new InternalField(new ScalerVar(0,true)));
            k = new FieldFile("k", Dimension.K, new InternalField(new ScalerVar(0.001,true)));
            nut = new FieldFile("nut", Dimension.NU, new InternalField(new ScalerVar(0.0,true)));
            epsilon = new FieldFile("epsilon", Dimension.EPSILON, new InternalField(new ScalerVar(0.1, true)));
            this.Fields.AddRange(new FieldFile[] { U,p,k,nut,epsilon});

            TransportProperties.AddPropertie("nu", new ScalerVar(Material.DynamicViscosity / Material.Density, Dimension.NU));
            TurbulenceProperties.Model = new KEpsilon();

            Ddt ddtSteady = new Ddt(Ddt.DdtTypes.steadyState);
            Interpolation ipLinear = new Interpolation(Interpolation.InterpolationTypes.linear);
            Interpolation ipUpwind = new Interpolation(Interpolation.InterpolationTypes.upwind);
            Grad gdGaussLinear = new Grad(Grad.GradTypes.Gauss, ipLinear);
            Grad gdGaussUpwind = new Grad(Grad.GradTypes.Gauss, ipUpwind);
            Div dvGaussUpwind = new Div(Div.DivTypes.Gauss, ipUpwind);
            Div dvGaussLinear = new Div(Div.DivTypes.Gauss, ipLinear);
            SnGrad sgLimitedCorrected = new SnGrad(SnGrad.SnGradTypes.limited, "corrected 0.33");
            Laplacian lpGaussLinear = new Laplacian(Laplacian.LaplacianTypes.Gauss, ipLinear, sgLimitedCorrected);
            FvSchemes.AddDdtSchemes(ddtSteady);
            FvSchemes.AddGradSchemes(gdGaussLinear);
            FvSchemes.AddDivSchemes(dvGaussLinear);
            FvSchemes.AddDivSchemes("div(phi,U)", dvGaussUpwind);
            FvSchemes.AddDivSchemes("div(phi,k)", dvGaussUpwind);
            FvSchemes.AddDivSchemes("div(phi,epsilon)", dvGaussUpwind);
            FvSchemes.AddLaplacianSchemes(lpGaussLinear);
            FvSchemes.AddInterpolationSchemes(ipLinear);
            FvSchemes.AddSnGradSchemes(sgLimitedCorrected);

            Simple simple = new Simple();
            simple.residual.SetResidual("p", 1e-4);
            simple.residual.SetResidual("U", 1e-4);
            simple.residual.SetResidual("\"(k|omega|epsilon)\"", 1e-4);
            FvSolution.Solution = simple;
            FvSolution.Relaxation.AddFieldFactor("p", 0.3);
            FvSolution.Relaxation.AddEquationFactor("U", 0.7);
            FvSolution.Relaxation.AddEquationFactor("\"(k|omega|epsilon).*\"", 0.7);
            GAMG gamgP = new GAMG("p", 1e-6, 0.1, Solver.SmootherTypes.GaussSeidel);
            SmootherSolver ssOther = new SmootherSolver("\"(U|k|omega|epsilon)\"", 1e-5, 0.1, Solver.SmootherTypes.GaussSeidel);
            GAMG gamgOther = new GAMG("\"(U|k|omega|epsilon)\"", 1e-6, 0.1, Solver.SmootherTypes.GaussSeidel);
            FvSolution.AddSolver(gamgP);
            FvSolution.AddSolver(ssOther);


        }
        public SteelFlowProjectFile(double inletVelocity,double k,double e,List<string> boundarys):this()
        {
            InletVelocity = inletVelocity;
            KValue = k;
            EValue = e;
            SetBoundaryNames(boundarys);
        }
        public virtual void SetBoundaryNames(List<string> names)
        {
            ClearBoundary();
            inletNames.Clear();
            upfaceNames.Clear();
            symNames.Clear();
            outletNames.Clear();
            wallNames.Clear();
            

            foreach(string name in names)
            {
                BoundaryNames.Add(name);
                if (name.StartsWith("INLET")||name.StartsWith("IN"))
                    SetInletName(name);
                else if (name.StartsWith("UPFACE") || name.StartsWith("UP"))
                    SetUpfaceName(name);
                else if (name.StartsWith("SYM"))
                    SetSymfaceName(name);
                else if (name.StartsWith("OUTLET") || name.StartsWith("OUT"))
                    SetOutletName(name);
                else
                    SetWallName(name);
            }
        }
        public virtual void SetInletName(params string[] bns)
        {
            foreach (string bn in bns)
            {
                MakeVelocityBoundaryByKandE(bn);
                if(inletNames.IndexOf(bn) < 0)
                    inletNames.Add(bn);
            }
               
        }
        public virtual void SetUpfaceName(params string[] bns)
        {
            
            foreach (string bn in bns)
            {
                MakeUpfaceBoundary(bn);
                if (upfaceNames.IndexOf(bn) < 0)
                    upfaceNames.Add(bn);
            }
                
        }
        public virtual void SetSymfaceName(params string[] bns)
        {
            
            foreach (string bn in bns)
            {
                MakeSymBoundary(bn);
                if (symNames.IndexOf(bn) < 0)
                    symNames.Clear();
            }
                
        }
        public virtual void SetOutletName(params string[] bns)
        {
            
            foreach (string bn in bns)
            {
                MakeOutletBoundary(bn);
                if (outletNames.IndexOf(bn) < 0)
                    outletNames.Add(bn);
            }
                
        }
        public virtual void SetWallName(params string[] bns)
        {
            
            foreach (string bn in bns)
            {
                MakeWallBoundary(bn);
                if (wallNames.IndexOf(bn) < 0)
                    wallNames.Add(bn);
            }
                
        }
        

        /*
        protected virtual void MakeVelocityBoundaryByIandL(string boundaryName,double velocity,double intensity,double length)
        {
            KEpsilon keModel = this.TurbulenceProperties.Model as KEpsilon;
            double vk = keModel.CalcuteK(velocity, intensity);
            double ve = keModel.CalcuteEpsilon(velocity, intensity, length);
            U.BoundaryField.AddBoundary(new SurfaceNormalFixedValue(boundaryName, new ScalerVar(velocity)));
            p.BoundaryField.AddBoundary(new ZeroGradient(boundaryName));
            k.BoundaryField.AddBoundary(new FixedValue(boundaryName, new ScalerVar(vk)));
            epsilon.BoundaryField.AddBoundary(new FixedValue(boundaryName, new ScalerVar(ve)));
            nut.BoundaryField.AddBoundary(new Calculated(boundaryName, new ScalerVar(0.0)));

        }
        */


        protected virtual void MakeVelocityBoundaryByKandE(string boundaryName)
        {
            U.BoundaryField.AddBoundary(new SurfaceNormalFixedValue(boundaryName, new ScalerVar(inletVelocity,true)));
            p.BoundaryField.AddBoundary(new ZeroGradient(boundaryName));
            this.k.BoundaryField.AddBoundary(new FixedValue(boundaryName, new ScalerVar(KValue, true)));
            epsilon.BoundaryField.AddBoundary(new FixedValue(boundaryName, new ScalerVar(EValue, true)));
            nut.BoundaryField.AddBoundary(new Calculated(boundaryName, new ScalerVar(0.0, true)));

        }

        protected virtual void MakeOutletBoundary(string boundaryName)
        {
            U.BoundaryField.AddBoundary(new ZeroGradient(boundaryName));
            p.BoundaryField.AddBoundary(new FixedValue(boundaryName, new ScalerVar(0, true)));
            k.BoundaryField.AddBoundary(new ZeroGradient(boundaryName));
            epsilon.BoundaryField.AddBoundary(new ZeroGradient(boundaryName));
            nut.BoundaryField.AddBoundary(new Calculated(boundaryName, new ScalerVar(0.0, true)));
        }

        protected virtual void MakeSymBoundary(string bn)
        {
            Boundary sym = new Symmetry(bn);
            U.BoundaryField.AddBoundary(sym);
            p.BoundaryField.AddBoundary(sym);
            k.BoundaryField.AddBoundary(sym);
            epsilon.BoundaryField.AddBoundary(sym);
            nut.BoundaryField.AddBoundary(sym);
        }

        protected virtual void MakeUpfaceBoundary(string bn)
        {
            U.BoundaryField.AddBoundary(new Slip(bn));
            p.BoundaryField.AddBoundary(new ZeroGradient(bn));
            k.BoundaryField.AddBoundary(new KqRWallFunction(bn, 0.001));
            epsilon.BoundaryField.AddBoundary(new EpsilonWallFunction(bn, 0.01));
            nut.BoundaryField.AddBoundary(new NutkWallFunction(bn, 0.0));
        }

        protected virtual void MakeWallBoundary(string bn)
        {
            U.BoundaryField.AddBoundary(new NoSlip(bn));
            p.BoundaryField.AddBoundary(new ZeroGradient(bn));
            k.BoundaryField.AddBoundary(new KqRWallFunction(bn, 0.001));
            epsilon.BoundaryField.AddBoundary(new EpsilonWallFunction(bn, 0.01));
            nut.BoundaryField.AddBoundary(new NutkWallFunction(bn, 0.0));
        }

        public double InletVelocity { get => inletVelocity; set => inletVelocity = value; }
        public double KValue { get => kValue; set => kValue = value; }
        public double EValue { get => eValue; set => eValue = value; }

        public static double CalInletVelocity(double width,double thickness,double pullVelocity,int outletCount,double inletRadius)
        {
            return width * thickness * pullVelocity / (Math.PI * inletRadius * inletRadius);
        }

    }

}
