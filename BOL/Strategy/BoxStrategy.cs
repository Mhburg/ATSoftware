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
        private ReaderWriterLockSlim _udlyContLock 
            = new ReaderWriterLockSlim();
        private ConcurrentDictionary<int, ConcurrentContract<Contract>> _contractCOL
            = new ConcurrentDictionary<int, ConcurrentContract<Contract>>();
        private ConcurrentDictionary<double, BoxEntry> _priceCOL
            = new ConcurrentDictionary<double, BoxEntry>();

        #endregion

        #region Public Properties

        public ContractDetails UdlyContract
        {
            get
            {
                _udlyContLock.EnterReadLock();
                var contract = _udlyContract;
                _udlyContLock.ExitReadLock();
                return contract;
            }
            private set
            {
                _udlyContLock.EnterWriteLock();
                _udlyContract = value;
                _udlyContLock.ExitWriteLock();
            }
        }

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
            ConcurrentContract<Contract> tmpContract = new ConcurrentContract<Contract>(contractdetails.Summary);
            _contractCOL.AddOrUpdate(contractdetails.Summary.ConId, tmpContract, (key, oldValue) => oldValue);
            if (contractdetails.Summary.SecType == SecTypRef.GetType(SecTypRef.OPT))
            {
                if (contractdetails.Summary.Right == SecRightRef.C || contractdetails.Summary.Right == SecRightRef.CALL)
                {
                    _repo.RequestMktData(this, contractdetails.Summary.ConId, contractdetails.Summary, "", false);
                    _priceCOL.AddOrUpdate(
                        contractdetails.Summary.Strike,
                        (key) => new BoxEntry() { CallContract = tmpContract },
                        (key, oldEntry) =>
                        {
                            oldEntry.CallContract = tmpContract; return oldEntry;
                        });
                }
                else
                {
                    _repo.RequestMktData(this, contractdetails.Summary.ConId, contractdetails.Summary, "", false);
                    _priceCOL.AddOrUpdate(
                        contractdetails.Summary.Strike,
                        (key) => new BoxEntry() { PutContract = tmpContract },
                        (key, oldEntry) =>
                        {
                            oldEntry.PutContract = tmpContract; return oldEntry;
                        });
                }
            }

            if (contractdetails.Summary.SecType == SecTypRef.GetType(SecTypRef.STK))
            {
                UdlyContract = contractdetails;
            }
        }

        public void GetContractDetailsEnd()
        {
            _contractLock.Set();
        }

        public void GetRTVolume(RTVolume rtvolume)
        {
            //throw new NotImplementedException();
        }

        public void GetTickGeneric(TickGeneric tg)
        {
            //throw new NotImplementedException();
        }

        public void GetTickPrice(TickerPrice tp)
        {
            if (tp.Field == TickType.ASK)
            {
                _contractCOL[tp.Ticker_Id].AskPrice = tp.Price;
            }
            else
            {
                _contractCOL[tp.Ticker_Id].BidPrice = tp.Price;
            }
        }

        public void GetTickSize(TickSize ts)
        {
            //throw new NotImplementedException();
        }

        public void GetTickSnapShotEnd()
        {
            _snapShotLock.Set();
        }

        public void GetTickstring(TickString ts)
        {
            //throw new NotImplementedException();
        }

        public void Setup()
        {
            _repo.RequestContractsForQuery(this, Tools.ReqIdSouce.Next(), Tools.TGetStockForQuery(_repo.UdlySymbol));
            _contractLock.Wait();
            _contractLock.Reset();
            while (UdlyContract == null) { }
            _repo.RequestMktData(this, UdlyContract.Summary.ConId, Tools.TGetStockForQuery(_repo.UdlySymbol), "", true);
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
