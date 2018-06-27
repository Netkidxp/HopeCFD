using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.IO;
namespace HopeCFD.OpenCFD
{
    [Serializable]
    public class FoamFileAnotation 
    {
        public string ToDictionaryItem()
        {
            return "/*--------------------------------*- C++ -*----------------------------------*\\\n" +
                   "| =========                |                                                  |\n" +
                   "| \\      / F ield          | OpenFOAM: The Open Source CFD Toolbox            |\n" +
                   "|  \\    / O peration       | Version:  5                                      |\n" +
                   "|   \\  / A nd              | Web:      www.OpenFOAM.org, www.netkidxp.cn      |\n" +
                   "|    \\/ M anipulation      |                                                  |\n" +
                   "\\*---------------------------------------------------------------------------*/\n";
        }

        public override string ToString()
        {
            return ToDictionaryItem();
        }
        public static FoamFileAnotation Default
        {
            get
            {
                return new FoamFileAnotation();
            }  
        }
    }
}
