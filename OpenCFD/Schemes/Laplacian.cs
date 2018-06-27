using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeCFD.OpenCFD.Schemes
{
    [Serializable]
    public class Laplacian:Scheme
    {
        public enum LaplacianTypes
        {
            Gauss
        }
        LaplacianTypes laplacianType;
        Interpolation interpolation;
        SnGrad snGrad;

        public Laplacian(LaplacianTypes laplacianType, Interpolation interpolation, SnGrad snGrad)
        {
            this.laplacianType = laplacianType;
            this.interpolation = interpolation;
            this.snGrad = snGrad;
        }

        internal LaplacianTypes LaplacianType { get => laplacianType; set => laplacianType = value; }
        public Interpolation Interpolation { get => interpolation; set => interpolation = value; }
        public SnGrad SnGrad { get => snGrad; set => snGrad = value; }

        public override string Value
        {
            get
            {
                return laplacianType.ToString() + " " + interpolation.Value + " " + snGrad.Value;
            }
        }
    }
}
