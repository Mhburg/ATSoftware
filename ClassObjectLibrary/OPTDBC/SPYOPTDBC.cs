using System.Data.Entity;
using Model;
using IBApi;
using System;

namespace DAL
{
    public class SPYOPTDBC : IBDbContext
    {
        #region Constructor
        public SPYOPTDBC()
            :base()
        {
        }
        #endregion

        #region Public Properties
        public int EntryCount { get; set; }
        public DbSet<TickerPrice> SPY_OPT_TICK_PRICES { get; set; }
        public DbSet<TickSize> SPY_OPT_TICK_SIZES { get; set; }
        public DbSet<RTVolume> SPY_OPT_RTVOLUMNS { get; set; }
        public DbSet<Contract> CONTRACTS { get; set; }
        public DbSet<ContractDetails> CONTRACT_DETAILS { get; set; }
        public DbSet<Order> SPY_OPT_ORDERS { get; set; }

        #endregion
    }

    
}
