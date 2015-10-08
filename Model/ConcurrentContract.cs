using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Model
{
    public class ConcurrentContract<T>
    {
        #region Private Variables

        private T _contract;
        private double _bidPrice;
        private double _askPrice;
        private ReaderWriterLockSlim _bidLock = new ReaderWriterLockSlim();
        private ReaderWriterLockSlim _askLock = new ReaderWriterLockSlim();

        #endregion

        #region Public Properties

        public T Contract
        {
            get
            {
                return _contract;
            }
            set
            {
                _contract = value;
            }
        }

        public double BidPrice
        {
            get
            {
                _bidLock.EnterReadLock();
                double bid = _bidPrice;
                _bidLock.ExitReadLock();
                return bid; 
            }
            set
            {
                _bidLock.EnterWriteLock();
                _bidPrice = value;
                _bidLock.ExitWriteLock();
            }
        }

        public double AskPrice
        {
            get
            {
                _askLock.EnterReadLock();
                double ask = _askPrice;
                _askLock.ExitReadLock();
                return ask;
            }
            set
            {
                _askLock.EnterWriteLock();
                _askPrice = value;
                _askLock.ExitWriteLock();
            }
        }

        #endregion

        public ConcurrentContract(T contract)
        {
            _contract = contract;
        }
    }
}
