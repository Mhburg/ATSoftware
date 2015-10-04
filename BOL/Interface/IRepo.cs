using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;

namespace BOL
{
    public interface IRepo
    {
        /*** Register strategy for data broadcast ***/
        void RegisterStrategy(IStrategy strt, int conId, bool snapshot);
        /*** Send contract info to strategy ***/
        IQueryable<ContractDetails> ReturnContractsForQuery(string symbol);

        /*** Read incoming data from Wrapper Reader ***/
        void ReadTickPrice(int tickerId, int field, double price, int canAutoExecute);
        void ReadtickSize(int tickerId, int field, int size);
        void ReadtickString(int tickerId, int tickType, string value);
        void ReadtickGeneric(int tickerId, int field, double value);
        void ReadContractDetails(int reqId, ContractDetails contractDetails);
        void ReadContractDetailsEnd(int reqId);
    }
}
