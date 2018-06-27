using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.IO;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.Boundarys
{
    [Serializable]
    public class BoundaryField:DictEntry
    {
        private const string includeEtc = "#includeEtc \"caseDicts/setConstraintTypes\"\n";
        public BoundaryField():base("boundaryField")
        {
        }

        public void AddBoundarys(List<Boundary> bs)
        {
            foreach(Boundary b in bs)
            {
                AddBoundary(b);
            }
        }
        public void AddBoundary(Boundary b)
        {
            this.AddChild(b);
        }
        public void ClearBoundary()
        {
            this.ClearChild();
        }
        public void RemoveBoundary(string patchName)
        {
            this.RemoveChild(patchName);
        } 
    }
}
