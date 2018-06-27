using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Db;
using HopeCFD.OpenCFD.IO;
using HopeCFD.OpenCFD.Dictionary;
namespace HopeCFD.OpenCFD.IO
{
    [Serializable]
    public class TransportProperties:FoamFile
    {

        SubDictItem _properties;
        public enum TransportModels
        {
            Newtonian
        }
        TransportModels transportModel;
        public TransportProperties()
        {
            _properties = new SubDictItem();
            transportModel = TransportModels.Newtonian;
        }

        public void AddPropertie(string name,ScalerVar propertie)
        {
            _properties.Remove(name);
            _properties.Add(DictEntry.Root(name,new DictItem(propertie)));
        }
        public void RemovePropertie(string name)
        {
            _properties.Remove(name);
        }
        public override void Write(string root)
        {
            this.header = FoamFileHeader.TransportPropertiesHeader;
            this.ClearContent();
            this.AddContent("transportModel", transportModel);
            foreach (DictEntry e in _properties.Entrys)
                this.AddContent(e);
            base.Write(root);
        }
    }
}
