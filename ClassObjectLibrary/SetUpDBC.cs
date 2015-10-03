using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Model;

namespace ClassObjectLibrary
{
    class SetUpDBC : DbContext
    {
        public SetUpDBC()
            : base(System.Configuration.ConfigurationManager.ConnectionStrings["Sample_Stocks_V3"].ConnectionString)
        {
            Database.SetInitializer(new DropCreateDatabaseAlwaysWithSeedData());
        }

        public DbSet<TickerIdRef> TickerId_Ref { get; set; }
    }

    internal class DropCreateDatabaseAlwaysWithSeedData : DropCreateDatabaseAlways<SetUpDBC>
    {
        protected override void Seed(SetUpDBC context)
        {
            context.TickerId_Ref.Add(new TickerIdRef { Symbol = "TWTR", TickerId = 1000001 });
            context.TickerId_Ref.Add(new TickerIdRef { Symbol = "SPY", TickerId = 1000002 });
            context.TickerId_Ref.Add(new TickerIdRef { Symbol = "QQQ", TickerId = 1000003 });
            base.Seed(context);
        }
    }
}
