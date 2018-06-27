using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.IO;
using HopeCFD.OpenCFD.Dictionary;
using HopeCFD.OpenCFD.Db;
namespace HopeCFD.OpenCFD.IO
{
    [Serializable]
    public class ControlDict : FoamFile
    {
        string _application;
        StartFrom _startFrom;
        double _startTime;
        StopAtControl _stopAt;
        double _endTime;
        double _deltaT;
        WriteControl _writeControl;
        double _writeInterval;
        double _purgeWrite;
        Format _writeFormat;
        int _writePrecision;
        Compression _writeCompression;
        TimeFormat _timeFormat;
        int _timePrecision;
        SwitchType _runTimeModifiable;
        List<string> _libs;

        public ControlDict()
        {
            _application = "simpleFoam";
            _startFrom = StartFrom.latestTime;
            _startTime = 0.0;
            _stopAt = StopAtControl.endTime;
            _endTime = 100.0;
            _deltaT = 1.0;
            _writeControl = WriteControl.timeStep;
            _writeInterval = 20.0;
            _purgeWrite = 0.0;
            _writeFormat = Format.binary;
            _writePrecision = 6;
            _writeCompression = Compression.uncompressed;
            _timeFormat = TimeFormat.general;
            _timePrecision = 6;
            _runTimeModifiable = SwitchType.on;
            _libs = new List<string>();
        }

        public string application { get => _application; set => _application = value; }
        public StartFrom startFrom { get => _startFrom; set => _startFrom = value; }
        public double startTime { get => _startTime; set => _startTime = value; }
        public StopAtControl stopAt { get => _stopAt; set => _stopAt = value; }
        public double endTime { get => _endTime; set => _endTime = value; }
        public double deltaT { get => _deltaT; set => _deltaT = value; }
        public WriteControl writeControl { get => _writeControl; set => _writeControl = value; }
        public double writeInterval { get => _writeInterval; set => _writeInterval = value; }
        public double purgeWrite { get => _purgeWrite; set => _purgeWrite = value; }
        public Format writeFormat { get => _writeFormat; set => _writeFormat = value; }
        public int writePrecision { get => _writePrecision; set => _writePrecision = value; }
        public Compression writeCompression { get => _writeCompression; set => _writeCompression = value; }
        public TimeFormat timeFormat { get => _timeFormat; set => _timeFormat = value; }
        public int timePrecision { get => _timePrecision; set => _timePrecision = value; }
        public SwitchType runTimeModifiable { get => _runTimeModifiable; set => _runTimeModifiable = value; }
        private List<string> libs { get => _libs; set => _libs = value; }

        public void SetLibs(params string[] libNames)
        {
            libs.Clear();
            libs.AddRange(libNames);
        }

        public void ClearLibs()
        {
            libs.Clear();
        }

        public void RemoveLibs(params string[] libNames)
        {
            foreach (string l in libNames)
                libs.Remove(l);
        }
        public override void Write(string root)
        {
            header = FoamFileHeader.ControlDictHeader;
            content.Clear();
            
            AddContent("application", application);
            AddContent("startFrom", startFrom);
            AddContent("startTime", startTime);
            AddContent("stopAt", stopAt);
            AddContent("endTime", endTime);
            AddContent("deltaT", deltaT);
            AddContent("writeControl", writeControl);
            AddContent("writeInterval", writeInterval);
            AddContent("purgeWrite", purgeWrite);
            AddContent("writeFormat", writeFormat);
            AddContent("writePrecision", writePrecision);
            AddContent("writeCompression", writeCompression);
            AddContent("timeFormat", timeFormat);
            AddContent("timePrecision", timePrecision);
            AddContent("runTimeModifiable", runTimeModifiable);
            if (libs.Count > 0)
            {
                string slibs = "(";
                foreach (string lib in libs)
                {
                    slibs += "\"" + lib + "\" ";
                }
                slibs += ")";
                AddContent("libs", slibs);
            }
            base.Write(root);
        }
    }
}
