using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Threading;

namespace DAL
{
    public class IBDbContext : DbContext
    {
        #region Private/Protected Fields
        protected int _entryCount = 0;
        private ReaderWriterLockSlim _entryCountLock
            = new ReaderWriterLockSlim();
        #endregion

        #region Constructor
        public IBDbContext()
            : base(System.Configuration.ConfigurationManager.ConnectionStrings["Sample_Stocks_V3"].ConnectionString)
        {

        }
        #endregion

        #region IIBDbContext Implementation
        public int EntryCount
        {
            get
            {
                int count = _entryCount;
                return count;
            }

            set
            {
                _entryCount = value;
            }
        }

        public virtual IBDbContext GetInstance()
        {
            return new IBDbContext();
        }
        #endregion
    }
}
