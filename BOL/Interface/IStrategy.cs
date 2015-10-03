using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BOL
{
    public interface IStrategy
    {
        /*** Used to receive Tick Price from Repo ***/
        void GetTickPrice(TickerPrice tp);
        /*** Used to receive Tick Size from Repo ***/
        void GetTickSize(TickSize ts);
        /*** Used to receive Tick String from Repo ***/
        void GetTickstring(TickString ts);
        /*** Used to receive Tick Generic from Repo ***/
        void GetTickGeneric(TickGeneric tg);
        /*** Used to receive RTVolume from Repo ***/
        void GetRTVolume(RTVolume rtvolume);
    }
}
