using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HopeCFD.OpenCFD.Db
{
    [Serializable]
    public class Dimension
    {
        public const int nDimension = 7;
        public enum DimensionType
        {
            MASS = 0,           // kilogram   kg
            LENGTH,             // metre      m
            TIME,               // second     s
            TEMPERATURE,        // Kelvin     K
            MOLES,              // mole       mol
            CURRENT,            // Ampere     A
            LUMINOUS_INTENSITY  // Candela    Cd
        };
        float[] dims=new float[7];
        public Dimension(float mass,
            float length,
            float time,
            float temperature,
            float moles,
            float current,
            float luminous_intensity
            )
        {
            dims[(int)DimensionType.MASS] = mass;
            dims[(int)DimensionType.LENGTH] = length;
            dims[(int)DimensionType.TEMPERATURE] = temperature;
            dims[(int)DimensionType.MOLES] = moles;
            dims[(int)DimensionType.CURRENT] = current;
            dims[(int)DimensionType.TIME] = time;
            dims[(int)DimensionType.LUMINOUS_INTENSITY] = luminous_intensity;
        }

        public override string ToString()
        {
            string s = "[" + dims[0].ToString();
            for(int i =1;i<nDimension;i++)
            {
                s += " " + dims[i].ToString();
            }
            s += "]";
            return s;
        }

        public static Dimension Parase(string ds)
        {
            if(ds.StartsWith("[")&&ds.EndsWith("]"))
            {
                ds = ds.Substring(1, ds.Length - 2);
                string[] dms = ds.Split(' ');
                if (dms.Length != 7)
                    return null;
                return new Dimension(float.Parse(dms[0]),
                    float.Parse(dms[0]),
                    float.Parse(dms[0]),
                    float.Parse(dms[0]),
                    float.Parse(dms[0]),
                    float.Parse(dms[0]),
                    float.Parse(dms[0])
                    );
            }
            else
                return null;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static Dimension U
        {
            get
            {
                return new Dimension(0,1,-1,0,0,0,0);
            }
        }

        public static Dimension MASS
        {
            get {
                return new Dimension(1, 0, 0, 0, 0, 0, 0);
            }
        }
        public static Dimension NU
        {
            get
            {
                return new Dimension(0, 2, -1, 0, 0, 0, 0);
            }
        }
        public static Dimension RHO
        {
            get
            {
                return new Dimension(1, -3, 0, 0, 0, 0, 0);
            }
        }
        public static Dimension CP
        {
            get
            {
                return new Dimension(0, 2, -2, -1, 0, 0, 0);
            }
        }
        public static Dimension PR
        {
            get
            {
                return new Dimension(0, 0, 0, 0, 0, 0, 0);
            }
        }
        public static Dimension PRT
        {
            get
            {
                return new Dimension(0, 0, 0, 0, 0, 0, 0);
            }
        }
        public static Dimension P
        {
            get
            {
                return new Dimension(0,2,-2,0,0,0,0);
            }
        }
        public static Dimension K
        {
            get
            {
                return new Dimension(0, 2, -2, 0, 0, 0, 0);
            }
        }
        public static Dimension EPSILON
        {
            get
            {
                return new Dimension(0, 2, -3, 0, 0, 0, 0);
            }
        }

    }
}