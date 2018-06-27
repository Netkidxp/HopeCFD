using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopeCFD.OpenCFD.Db
{
    [Serializable]
    public enum WriteControl
    {
        timeStep,
        runTime,
        adjustableRunTime,
        clockTime,
        cpuTime
    }

    [Serializable]
    public enum StopAtControl
    {
        endTime,    //!< stop when Time reaches the prescribed endTime
        noWriteNow, //!< set endTime to stop immediately w/o writing
        writeNow,   //!< set endTime to stop immediately w/ writing
        nextWrite   //!< stop the next time data are written
    }

    [Serializable]
    public enum TimeFormat
    {
        general,
        Fixed,
        scientific
    }
    public enum StartFrom
    {
        startTime,
        firstTime,
        latestTime
    }
}
