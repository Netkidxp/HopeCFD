using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace HopeCFD.Tuple
{
    [Serializable]
    public class Material
    {
        string name;
        double density;
        double dynamicViscosity;//dynamic viscosity
        double thermalConductivity;
        double specificHeat;
        double tenthermalExpansivity;
        double refTemperature;

        public Material(string name)
        {
            this.name = name;
        }

        public Material()
        {
        }

        public string Name { get => name; set => name = value; }
        public double Density { get => density; set => density = value; }
        public double DynamicViscosity { get => dynamicViscosity; set => dynamicViscosity = value; }
        public double ThermalConductivity { get => thermalConductivity; set => thermalConductivity = value; }
        public double SpecificHeat { get => specificHeat; set => specificHeat = value; }
        public double TenthermalExpansivity { get => tenthermalExpansivity; set => tenthermalExpansivity = value; }
        public double RefTemperature { get => refTemperature; set => refTemperature = value; }

        public void Write(string fileName)
        {
            //Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            //SoapFormatter soapFormat = new SoapFormatter();
            //soapFormat.Serialize(fStream, this);
            //fStream.Close();
            XmlSerializer xs = new XmlSerializer(typeof(Material));
            Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read);
            xs.Serialize(stream, this);
            stream.Close();
        }

        public static Material Read(string fileName)
        {
            //Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            //SoapFormatter soapFormat = new SoapFormatter();
            //object o = soapFormat.Deserialize(fStream);
            //return o as Material;
            XmlSerializer xs = new XmlSerializer(typeof(Material));
            Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            Material m = xs.Deserialize(stream) as Material;
            return m;
        }
    }


}
