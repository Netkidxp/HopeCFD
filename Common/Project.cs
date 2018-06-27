using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace HopeCFD.Common
{
    public class Project
    {
        private Version version;
        private String comment;

        public Version Version { get => version; set => version = value; }
        public string Comment { get => comment; set => comment = value; }

        public virtual Project Read(StreamReader sr)
        {
            return null;
        }
        public virtual bool Write(StreamWriter sw)
        {
            return true;
        }
    }
}
