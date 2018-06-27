using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.IO;
using HopeCFD.OpenCFD.TurbulenceModels.RAS;
using HopeCFD.OpenCFD.Boundarys;
using System.Runtime.Serialization.Formatters.Soap;

namespace HopeCFD.Tuple
{
    [Serializable]
    public abstract class ProjectFile
    {
        string remark;
        Material material;
        List<string> boundaryNames;
        List<FieldFile> fields;
        ControlDict controlDict;
        FvSchemes fvSchemes;
        FvSolution fvSolution;
        TransportProperties transportProperties;
        TurbulenceProperties turbulenceProperties;
        List<FoamFile> otherFiles;
        public ProjectFile()
        {
            fields = new List<FieldFile>();
            boundaryNames = new List<string>();
            otherFiles = new List<FoamFile>();
            controlDict = new ControlDict();
            transportProperties = new TransportProperties();
            turbulenceProperties = new TurbulenceProperties(new KEpsilon());
            material = new Material("Steel");
            material.Density = 6940.0;
            material.DynamicViscosity = 0.006293;
            fvSchemes = new FvSchemes();
            fvSolution = new FvSolution(new OpenCFD.Solutions.Simple());
        }

        public string Remark { get => remark; set => remark = value; }
        public Material Material { get => material; set => material = value; }
        public ControlDict ControlDict { get => controlDict; }
        public FvSchemes FvSchemes { get => fvSchemes; }
        public FvSolution FvSolution { get => fvSolution;  }
        public TransportProperties TransportProperties { get => transportProperties; }
        public TurbulenceProperties TurbulenceProperties { get => turbulenceProperties;  }
        public List<string> BoundaryNames { get => boundaryNames; }
        public List<FieldFile> Fields { get => fields;}

        public virtual void WriteXmlProjectFile(string fileName)
        {
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            SoapFormatter soapFormat = new SoapFormatter();
            soapFormat.Serialize(fStream, this);
            fStream.Close();
        }
        public static object ReadXmlProjectFile(string fileName)
        {
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            SoapFormatter soapFormat = new SoapFormatter();
            object o = soapFormat.Deserialize(fStream);
            fStream.Close();
            return o;
        }
        public virtual void WriteBinProjectFile(string fileName)
        {
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter binFormat = new BinaryFormatter();
            binFormat.Serialize(fStream, this);
            fStream.Close();
        }
        public static object ReadBinProjectFile(string fileName)
        {
            Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
            BinaryFormatter binFormat = new BinaryFormatter();
            object o = binFormat.Deserialize(fStream);
            fStream.Close();
            return o;
        }
        public virtual void WriteFoamFiles(string root)
        {
            ControlDict.Write(root);
            foreach (FieldFile f in Fields)
                f.Write(root);
            TransportProperties.Write(root);
            TurbulenceProperties.Write(root);
            FvSchemes.Write(root);
            FvSolution.Write(root);
            foreach(FoamFile f in otherFiles)
            {
                f.Write(root);
            }

        }
        public void AddFoamFile(ref FoamFile file)
        {
            otherFiles.Add(file);

        }
        public void AddFoamFiles(params FoamFile[] files)
        {
            foreach (FoamFile f in files)
                otherFiles.Add(f);
        }
        public void RemoveFoamFile(ref FoamFile file)
        {
            otherFiles.Remove(file);
        }
        public void ClearFoamFiles()
        {
            otherFiles.Clear();
        }
        
        public void Write(string fileName)
        {
            WriteBinProjectFile(fileName);
            string root = Path.GetDirectoryName(fileName) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(fileName) + "_files";
            Directory.CreateDirectory(root);
            WriteFoamFiles(root);
        }

        public virtual void RemoveBoundary(string boundaryName)
        {
            foreach (FieldFile f in Fields)
            {
                f.BoundaryField.RemoveBoundary(boundaryName);
            }
        }
        public virtual void ClearBoundary()
        {
            foreach (FieldFile f in Fields)
            {
                f.BoundaryField.ClearBoundary();
            }
            BoundaryNames.Clear();
        }
        public virtual Boundary AddBoundary(string fieldName,Boundary boundary)
        {
            FieldFile f = null;
            foreach (FieldFile ff in Fields)
                if (ff.FieldName == fieldName)
                    f = ff;
            if (f != null)
                f.BoundaryField.AddBoundary(boundary);
            return boundary;
        }
    }
}
