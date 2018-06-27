using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.IO;
namespace HopeCFD.OpenCFD.Db
{
    [Serializable]
    public abstract class Variable
    {
        Dimension dimension;
        bool uniform;

        protected Variable()
        {
        }

        protected Variable(Dimension dimension)
        {
            this.dimension = dimension;
        }

        protected Variable(bool uniform)
        {
            this.uniform = uniform;
        }

        protected Variable(Dimension dimension, bool uniform)
        {
            this.dimension = dimension;
            this.uniform = uniform;
        }

        public Dimension Dimension { get => dimension; set => dimension = value; }
        public bool Uniform { get => uniform; set => uniform = value; }
        
    }
}
