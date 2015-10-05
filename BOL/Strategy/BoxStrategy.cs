using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;
using Model;
using Utility;
using System.Threading;
using System.Collections.Concurrent;

namespace BOL
{
    public class BoxStrategy : IStrategy
    {
        #region Private Fields

        private int _strategyId;
        private IRepo _repo;
        private string _expiry;
        private ContractDetails _udlyContract;

        private ManualResetEventSlim _contractLock
            = new ManualResetEventSlim();
        private ManualResetEventSlim _snapShotLock
            = new ManualResetEventSlim();
        private ConcurrentDictionary<int, ContractDetails> _contractCOL 
            = new ConcurrentDictionary<int, ContractDetails>();
        private ConcurrentDictionary<int, BoxEntry> _priceCOL
            = new ConcurrentDictionary<int, BoxEntry>();

        #endregion

        #region Constructor
        public BoxStrategy(IRepo repo, int strategyId, string expiry)
        {
            _repo = repo;
            _strategyId = strategyId;
            _expiry = expiry;
        }
        #endregion


        #region IStrategy Implementation
        public int StrategyId
        {
            get
            {
                return _strategyId;
            }

            set
            {
                _strategyId = value;
            }
        }

        public void GetContractDetails(ContractDetails contractdetails)
        {
            _contractCOL.AddOrUpdate(contractdetails.Summary.ConId, contractdetails, (key, oldValue) => oldValue);
            if (contractdetails.Summary.SecType == SecTypRef.GetType(SecTypRef.STK))
            {
                _udlyContract = contractdetails;
            }
        }

        public void GetContractDetailsEnd()
        {
            _contractLock.Set();
        }

        public void GetRTVolume(RTVolume rtvolume)
        {
            throw new NotImplementedException();
        }

        public void GetTickGeneric(TickGeneric tg)
        {
            throw new NotImplementedException();
        }

        public void GetTickPrice(TickerPrice tp)
        {
            if (tp.Field == TickType.ASK)
            {
            }
        }

        public void GetTickSize(TickSize ts)
        {
            throw new NotImplementedException();
        }

        public void GetTickSnapShotEnd()
        {
            throw new NotImplementedException();
        }

        public void GetTickstring(TickString ts)
        {
            throw new NotImplementedException();
        }

        public void Setup()
        {
            _repo.RequestContractsForQuery(this, Tools.ReqIdSouce.Next(), Tools.TGetStockForQuery(_repo.UdlySymbol));
            _contractLock.Wait();
            _contractLock.Reset();
            _repo.RequestMktData(this, _udlyContract.Summary.ConId, Tools.TGetStockForQuery(_repo.UdlySymbol), "", true);
            _repo.RequestContractsForQuery(this, Tools.ReqIdSouce.Next(), Tools.TGetOptionForQuery(_repo.UdlySymbol, _expiry));
            _contractLock.Wait();
            _contractLock.Reset();
            _snapShotLock.Wait();
            _snapShotLock.Reset();

        }

        public void Run()
        {
            Setup();
        }
        #endregion

    }
}
