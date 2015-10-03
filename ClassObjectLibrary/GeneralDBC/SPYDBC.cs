using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Model;
using IBApi;

namespace DAL
{
    public class SPYDBC : DbContext, ICount
    {
        #region Constructors
        public SPYDBC()
            : base(System.Configuration.ConfigurationManager.ConnectionStrings["Sample_Stocks_V3"].ConnectionString)
        {

        }
        #endregion




        #region Public Properties
        public int EntryCount { get; set; }
        public DbSet<TickerPrice> SPY_TICK_PRICES { get; set; }
        public DbSet<TickSize> SPY_TICK_SIZES { get; set; }
        public DbSet<RTVolume> SPY_RTVOLUME { get; set; }
        public DbSet<Contract> CONTRACTS { get; set; }
        public DbSet<ContractDetails> CONTRACT_DETAILS { get; set; }
        public DbSet<Order> SPY_ORDERS { get; set; }
        #endregion
    }
}
