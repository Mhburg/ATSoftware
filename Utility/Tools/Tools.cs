using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;

namespace Utility
{
    public static class Tools
    {
        public static readonly long TicksFrom70 = new DateTime(1970, 1, 1).Ticks;

        public static Contract TGetStockForQuery(string symbol, string exchange = "SMART", string currency = "USD")
        {
            Contract contract = new Contract();
            contract.Symbol = symbol;
            contract.Exchange = exchange;
            contract.Currency = currency;
            contract.SecType = "STK";
            return contract;
        }

        public static Contract TGetOptionForQuery(string symbol, string expiry, string exchange = "SMART", string currency = "USD")
        {
            Contract contract = new Contract();
            contract.Symbol = symbol;
            contract.Exchange = exchange;
            contract.Currency = currency;
            contract.Expiry = expiry;
            contract.SecType = "OPT";
            return contract;
        }

        private static Random _reqIdSource = new Random(DateTime.Now.Second);

        public static Random ReqIdSouce
        {
            get
            {
                return _reqIdSource;
            }
        }
    }
}
