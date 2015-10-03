using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;
using Model;

namespace BOL
{
    public class SPYSTKOPTRepo
    {
        #region Events
        public event EventHandler<TickerPrice> STKTickPriceEvent;
        public event EventHandler<TickerPrice> OPTTickPriceEvent;
        public event EventHandler<TickSize> STKTickSizeEvent;
        public event EventHandler<TickSize> OPTTickSizeEvent;
        public event EventHandler<RTVolume> STKRTVolumeEvent;
        public event EventHandler<RTVolume> OPTRTVolumeEvent;
        #endregion
    }
}
