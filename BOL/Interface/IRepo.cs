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
        string UdlySymbol { get; }

        void RequestMktData(IStrategy strt, int conId, Contract contract, string genericticklist, bool snapshot);
        /// <summary>
        /// Send request for contracts to API
        /// </summary>
        void RequestContractsForQuery(IStrategy strt, int reqId, Contract contract);
        /// <summary>
        /// Remove strategy from data subscription collection
        /// </summary>
        void UnRegisterDataSubs(IStrategy strt, int reqId);

        /*** Read incoming data from Wrapper Reader ***/
        void ReadTickPrice(int tickerId, int field, double price, int canAutoExecute);
        void ReadtickSize(int tickerId, int field, int size);
        void ReadtickString(int tickerId, int tickType, string value);
        void ReadtickGeneric(int tickerId, int field, double value);
        void ReadContractDetails(int reqId, ContractDetails contractDetails);
        void ReadContractDetailsEnd(int reqId);
        void ReadTickSnapShotEnd(int reqId);
    }
}
