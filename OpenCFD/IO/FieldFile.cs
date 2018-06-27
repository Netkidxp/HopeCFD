using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Db;
using HopeCFD.OpenCFD.Boundarys;
namespace HopeCFD.OpenCFD.IO
{
    [Serializable]
    public class FieldFile:FoamFile
    {
        private string fieldName;
        private InternalField internalField;
        private BoundaryField boundaryField;
        private Dimension dimensions;

        public string FieldName { get => fieldName;}
        public InternalField InternalField { get => internalField; set => internalField = value; }
        public BoundaryField BoundaryField { get => boundaryField; set => boundaryField = value; }
        public Dimension Dimensions { get => dimensions; set => dimensions = value; }

        public FieldFile(string fieldName, Dimension dimension, InternalField internalField, BoundaryField boundaryField)
        {
            this.fieldName = fieldName;
            this.dimensions = dimension;
            this.InternalField = internalField;
            this.BoundaryField = boundaryField;
            
        }
        public FieldFile(string fieldName)
        {
            this.fieldName = fieldName;
            this.dimensions = new Dimension(0, 0, 0, 0, 0, 0, 0);
            this.internalField = new InternalField(new ScalerVar(0, this.dimensions, true));
            this.boundaryField = new BoundaryField();
        }

        public FieldFile(string fieldName, Dimension dimensions) : this(fieldName)
        {
            this.dimensions = dimensions;
        }
        public FieldFile(string fieldName, Dimension dimensions,InternalField internalField):this(fieldName,dimensions)
        {
            this.internalField = internalField;
        }

        public override void Write(String root)
        {
            string sc = (internalField.Value is VectorVar) ? "volVectorField" : "volScalarField";
            this.header = new FoamFileHeader("2.0", Format.ascii, sc, fieldName, "0");
            content.Clear();
            AddContent("dimensions", dimensions);
            AddContent(internalField);
            AddContent(boundaryField);
            base.Write(root);
        }
    }
}
