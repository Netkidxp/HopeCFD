using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;
using HopeCFD.OpenCFD.Db;
namespace HopeCFD.OpenCFD.Solutions
{
    [Serializable]
    public abstract class Solution:DictEntry
    {
        int _nNonOrthogonalCorrectors;
        SwitchType _momentumPredictor;
        SwitchType _transonic;
        SwitchType _consistent;
        Residual _residual;
        int _pRefCell;
        int _pRefValue;

        protected Solution()
        {
            this.Key = "solution";
            _nNonOrthogonalCorrectors = 0;
            _momentumPredictor = SwitchType.on;
            _transonic = SwitchType.off;
            _consistent = SwitchType.off;
            _residual = new Residual();
            _pRefCell = 0;
            _pRefValue = 0;
        }

        public int nNonOrthogonalCorrectors { get => _nNonOrthogonalCorrectors; set => _nNonOrthogonalCorrectors = value; }
        public SwitchType momentumPredictor { get => _momentumPredictor; set => _momentumPredictor = value; }
        public SwitchType transonic { get => _transonic; set => _transonic = value; }
        public SwitchType consistent { get => _consistent; set => _consistent = value; }
        public Residual residual { get => _residual; }
        public int pRefCell { get => _pRefCell; set => _pRefCell = value; }
        public int pRefValue { get => _pRefValue; set => _pRefValue = value; }

        public override string ToString()
        {
            this.NewChild("nNonOrthogonalCorrectors", new DictItem(_nNonOrthogonalCorrectors));
            this.NewChild("momentumPredictor", new DictItem(_momentumPredictor));
            this.NewChild("transonic", new DictItem(_transonic));
            this.NewChild("consistent", new DictItem(_consistent));
            
            this.AddChild(_residual);
            this.NewChild("pRefCell", new DictItem(_pRefCell));
            this.NewChild("pRefValue", new DictItem(_pRefValue));
            return base.ToString();
        }
    }
}
