using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.IO;
using HopeCFD.OpenCFD.Boundarys;
using HopeCFD.OpenCFD.Dictionary;
using HopeCFD.OpenCFD.Db;
using HopeCFD.OpenCFD.Schemes;
using HopeCFD.OpenCFD.Solutions;
using HopeCFD.OpenCFD.Solvers;
using HopeCFD.Tuple;
using HopeCFD.OpenCFD.TurbulenceModels.RAS;
namespace Test
{
    class Program
    {
        static void test1()
        {
            FieldFile ff = new FieldFile("U");
            ff.Dimensions = Dimension.U;
            ff.InternalField = new InternalField(new VectorVar(1, 0, 0, true));
            ff.BoundaryField = new BoundaryField();
            ff.BoundaryField.AddBoundary(new FixedValue("in", new VectorVar(1, 0, 0, true)));
            ff.BoundaryField.AddBoundary(new Symmetry("sym"));
            //ff.Write("e:\\hope\\test");

            ControlDict cd = new ControlDict();
            //cd.Write("e:\\hope\\test");

            FvSchemes fs = new FvSchemes();
            Interpolation i = new Interpolation(Interpolation.InterpolationTypes.linear);
            Grad g = new Grad(Grad.GradTypes.Gauss, i);
            SnGrad s = new SnGrad(SnGrad.SnGradTypes.corrected);
            Laplacian l = new Laplacian(Laplacian.LaplacianTypes.Gauss, i, s);
            Div d = new Div(Div.DivTypes.Gauss, i);
            fs.AddDdtSchemes(new Ddt(Ddt.DdtTypes.steadyState));
            fs.AddGradSchemes(g);
            fs.AddDivSchemes(d);
            fs.AddDivSchemes("div(phi,U)", d);
            fs.AddLaplacianSchemes(l);
            fs.AddInterpolationSchemes(i);
            fs.AddSnGradSchemes(s);
            //fs.Write("e:\\hope\\test");

            FvSolution fsn = new FvSolution(new Simple());
            PBiCG psu = new PBiCG("U", 0.001, 0.001, PBiCG.Preconditioners.DIC);
            PBiCG psp = new PBiCG("p", 0.001, 0.001, PBiCG.Preconditioners.DIC);
            fsn.AddSolver(psu);
            fsn.AddSolver(psp);
            fsn.SetFieldRelaxtionFactors("U", 1);
            fsn.SetFieldRelaxtionFactors("U", 0.8);
            //fsn.Write("e:\\hope\\test");
            
        }

        static void test2()
        {
            //Material m = new Material("haha");
            //m.Density = 6940.0;
            //m.DynamicViscosity = 0.006293;
            //m.Write("e:\\hope\\test2\\q230.xml");
            Material m = Material.Read("e:\\hope\\test2\\q230.xml");

        }

        static void test3()
        {
            List<string> bs = new List<string>();
            bs.AddRange(new string[] {"IN","UP","SYM",
                "OUT1","OUT2","OUT3","OUT4",
                "SHELL" });
            SteelFlowProjectFile p =new SteelFlowProjectFile(-1.23,0.3312,6.3,bs);
            //p.ControlDict.SetLibs("hha1.so", "hah2.so");
            RasModel tm = p.TurbulenceProperties.Model as RasModel;
            //tm.SetCoeffes("cmu", 0.9);
            p.Write("e:\\hope\\test2\\test1.flp");
            p.ControlDict.ClearLibs();
        }
        static void test4()
        {
            SteelFlowProjectFile p = ProjectFile.ReadBinProjectFile("e:\\hope\\test2\\nb1.flp") as SteelFlowProjectFile;
            p.Write("e:\\hope\\test2\\nb1_bak.flp");
        }
        static void Main(string[] args)
        {

            test3();
        }
    }
}
