using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
using Utility;
using IBApi;

namespace BOL
{
    public class SPYRepo : IRepo
    {
        #region Private Fields
        private ConcurrentDictionary<int, List<IStrategy>> _strategyBacklog
            = new ConcurrentDictionary<int, List<IStrategy>>();
        private SPYDBC _spyDBC = new SPYDBC();
        private GenericWrapper _wrapper;
        #endregion

        #region Constructors
        public SPYRepo()
        {
            _wrapper = new GenericWrapper(this);
        }
        #endregion

        #region IRepo Implementation
        public void RegisterStrategy(IStrategy strt, int conId, bool snapshot)
        {
            if (!snapshot)
            {
                _strategyBacklog[conId].Add(strt);
            }
            else
            {
                Task.Run(() =>
                {
                    using (var tmpDBC = new SPYDBC())
                    {
                        strt.GetTickPrice(tmpDBC.SPY_TICK_PRICES.LastOrDefault(t => t.Ticker_Id == conId && t.Field == TickType.ASK));
                        strt.GetTickPrice(tmpDBC.SPY_TICK_PRICES.LastOrDefault(t => t.Ticker_Id == conId && t.Field == TickType.BID));
                        strt.GetTickPrice(tmpDBC.SPY_TICK_PRICES.LastOrDefault(t => t.Ticker_Id == conId && t.Field == TickType.LAST));
                        strt.GetTickSize(tmpDBC.SPY_TICK_SIZES.LastOrDefault(t => t.Ticker_Id == conId && t.Field == TickType.ASK_SIZE));
                        strt.GetTickSize(tmpDBC.SPY_TICK_SIZES.LastOrDefault(t => t.Ticker_Id == conId && t.Field == TickType.BID_SIZE));
                        strt.GetTickSize(tmpDBC.SPY_TICK_SIZES.LastOrDefault(t => t.Ticker_Id == conId && t.Field == TickType.LAST_SIZE));
                    }
                });
            }
        }

        public void ReadTickPrice(int tickerId, int field, double price, int canAutoExecute)
        {
            long timestamp = DateTime.Now.Ticks - Tools.TicksFrom70;
            _strategyBacklog[tickerId].ForEach(
                t => Task.Run(
                    () => t.GetTickPrice(
                        new TickerPrice()
                        {
                            Ticker_Id = tickerId,
                            Field = field,
                            Price = price,
                            Can_Auto_Execute = canAutoExecute,
                            TimeStamp = timestamp
                        })));
            _spyDBC.SPY_TICK_PRICES.AddEntry(
                new TickerPrice()
                {
                    Ticker_Id = tickerId,
                    Field = field,
                    Price = price,
                    Can_Auto_Execute = canAutoExecute,
                    TimeStamp = timestamp
                }, _spyDBC);
        }

        public void ReadtickSize(int tickerId, int field, int size)
        {
            TickSize ts = new TickSize() { Ticker_Id = tickerId, Field = field, Size = size };
            _strategyBacklog[tickerId].ForEach(t => Task.Run(() => t.GetTickSize(ts)));
        }

        public void ReadtickString(int tickerId, int tickType, string value)
        {
            if (tickType == TickType.RT_VOLUME)
            {
                _strategyBacklog[tickerId].ForEach(
                    t => Task.Run(
                        () => t.GetRTVolume(
                            new RTVolume(tickerId, value))));
                _spyDBC.SPY_RTVOLUME.AddEntry(new RTVolume(tickerId, value), _spyDBC);
            }
            else
            {
                _strategyBacklog[tickerId].ForEach(
                    t => Task.Run(
                        () => t.GetTickstring(new TickString()
                        {
                            Ticker_Id = tickerId,
                            Tick_Type = tickType,
                            Value = value
                        })));
            }
        }

        public void ReadtickGeneric(int tickerId, int field, double value)
        {
            TickGeneric tg = new TickGeneric() { Ticker_Id = tickerId, Field = field, Value = value };
            _strategyBacklog[tickerId].ForEach(t => Task.Run(() => t.GetTickGeneric(tg)));
        }
        #endregion
    }
}
