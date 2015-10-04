using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using IBApi;
using Model;

namespace DAL
{
    class SPYSTKDBC : IBDbContext
    {
        #region Constructor
        public SPYSTKDBC()
            :base()
        {
        }
        #endregion

        #region Public Properties
        public int EntryCount { get; set; }
        public DbSet<TickerPrice> SPY_STK_TICK_PRICES { get; set; }
        public DbSet<TickSize> SPY_STK_TICK_SIZES { get; set; }
        public DbSet<RTVolume> SPY_STK_RTVOLUMNS { get; set; }
        public DbSet<Contract> CONTRACTS { get; set; }
        public DbSet<ContractDetails> CONTRACT_DETAILS { get; set; }
        public DbSet<Order> SPY_STK_ORDERS { get; set; }

        public DbContext GetInstance()
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
