using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public interface IRepo
    {
        void RegisterStrategy(IStrategy strt, int conId, bool snapshot);
        void ReadTickPrice(int tickerId, int field, double price, int canAutoExecute);
        void ReadtickSize(int tickerId, int field, int size);
        void ReadtickString(int tickerId, int tickType, string value);
        void ReadtickGeneric(int tickerId, int field, double value);
    }
}
