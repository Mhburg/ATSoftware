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
        private string _udlySymbol;

        
        #endregion

        #region Constructors
        public SPYRepo(string symbol)
        {
            _wrapper = new GenericWrapper(this);
            _udlySymbol = symbol;
        }
        #endregion

        #region IRepo Implementation

        public string UdlySymbol
        {
            get
            {
                return _udlySymbol;
            }
        }

        /// <summary>
        /// When snapshot flag is set, conId must be a unique key in the _strategyBackLog.
        /// </summary>
        public void RequestMktData(IStrategy strt, int conId, Contract contract, string genericticklist, bool snapshot)
        {
            _strategyBacklog.AddOrUpdate(conId, new List<IStrategy>() { strt },
                (key, oldlist) =>
                {
                    oldlist.Add(strt);
                    return oldlist;
                });
            /*** Any possible error if send multiple request for the same market data ***/
            _wrapper.ClientSocket.reqMktData(conId, contract, genericticklist, snapshot, new List<TagValue>());
        }

        public void RequestContractsForQuery(IStrategy strt, int reqId, Contract contract)
        {
            _strategyBacklog.TryAdd(reqId, new List<IStrategy>() { strt });
            _wrapper.ClientSocket.reqContractDetails(reqId, contract);
        }

        public void UnRegisterDataSubs(IStrategy strt, int reqId)
        {
            _strategyBacklog[reqId].RemoveAll(t => t.StrategyId == strt.StrategyId);
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
            _spyDBC.Save();
        }

        public void ReadtickSize(int tickerId, int field, int size)
        {
            long timestamp = DateTime.Now.Ticks - Tools.TicksFrom70;
            _strategyBacklog[tickerId].ForEach(t => Task.Run(
                () => t.GetTickSize(
                    new TickSize()
                    {
                        Ticker_Id = tickerId,
                        Field = field,
                        Size = size,
                        TimeStamp = timestamp
                    })));
            _spyDBC.SPY_TICK_SIZES.AddEntry(new TickSize()
            {
                Ticker_Id = tickerId,
                Field = field,
                Size = size,
                TimeStamp = timestamp
            }, _spyDBC);
            _spyDBC.Save();
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
            _spyDBC.Save();
        }

        public void ReadtickGeneric(int tickerId, int field, double value)
        {
            TickGeneric tg = new TickGeneric() { Ticker_Id = tickerId, Field = field, Value = value };
            _strategyBacklog[tickerId].ForEach(t => Task.Run(() => t.GetTickGeneric(tg)));
        }

        public void ReadContractDetails(int reqId, ContractDetails contractDetails)
        {
            if (!_spyDBC.CONTRACT_DETAILS.Any(t => t.Summary.ConId == contractDetails.Summary.ConId))
            {
                _spyDBC.CONTRACT_DETAILS.AddEntry(contractDetails, _spyDBC);
            }
            _strategyBacklog[reqId].ForEach(
                t => Task.Run(
                    () => t.GetContractDetails(contractDetails)
                    ));
            _spyDBC.Save();
        }

        public void ReadContractDetailsEnd(int reqid)
        {
            _strategyBacklog[reqid].ForEach(
                t => Task.Run(
                    () => t.GetContractDetailsEnd()
                    ));
        }

        public void ReadTickSnapShotEnd(int reqId)
        {
            _strategyBacklog[reqId].ForEach(
                t => Task.Run(
                    () => t.GetTickSnapShotEnd()
                    ));
        }

        #endregion
    }
}
