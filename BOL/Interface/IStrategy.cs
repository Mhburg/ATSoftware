using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using IBApi;

namespace BOL
{
    public interface IStrategy
    {
        /*** Identifier for the strategy ***/
        int StrategyId { get; set; }
        /*** Used to receive Tick Price from Repo ***/
        void GetTickPrice(TickerPrice tp);
        /*** Used to receive Tick Size from Repo ***/
        void GetTickSize(TickSize ts);
        /*** Used to receive Tick String from Repo ***/
        void GetTickstring(TickString ts);
        /*** Used to receive Tick Generic from Repo ***/
        void GetTickGeneric(TickGeneric tg);
        /*** Used to receive RTVolume from Repo ***/
        void GetRTVolume(RTVolume rtvolume);
        /*** Used to receive ContractDetails from Repo ***/
        void GetContractDetails(ContractDetails contractdetails);
        /*** Used to receive ContractDetailsEnd from Repo ***/
        void GetContractDetailsEnd();
        /*** Used to receive TickSnapShotEnd from Repo ***/
        void GetTickSnapShotEnd();
        /*** Set up what is needed for the strategy to run ***/
        void Setup();
        /*** Put the strategy in motion ***/
        void Run();
    }
}
