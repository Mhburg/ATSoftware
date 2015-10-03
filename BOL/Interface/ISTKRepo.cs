using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;

namespace BOL
{
    public interface ISTKRepo
    {
        void tickPrice(int tickerId, int field, double price, int canAutoExecute);
        void tickSize(int tickerId, int field, int size);
        void tickString(int tickerId, int tickType, string value);
        void tickGeneric(int tickerId, int field, double value);
        void tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureExpiry, double dividendImpact, double dividendsToExpiry);
        void tickSnapshotEnd(int tickerId);
        void nextValidId(int orderId);
        void deltaNeutralValidation(int reqId, UnderComp underComp);
        void tickOptionComputation(int tickerId, int field, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice);
        void updateAccountValue(string key, string value, string currency, string accountName);
        void updatePortfolio(Contract contract, int position, double marketPrice, double marketValue, double averageCost, double unrealisedPNL, double realisedPNL, string accountName);
        void updateAccountTime(string timestamp);
        void accountDownloadEnd(string account);
        void orderStatus(int orderId, string status, int filled, int remaining, double avgFillPrice, int permId, int parentId, double lastFillPrice, int clientId, string whyHeld);
        void openOrder(int orderId, Contract contract, Order order, OrderState orderState);
        void openOrderEnd();
        void contractDetails(int reqId, ContractDetails contractDetails);
        void contractDetailsEnd(int reqId);
        void execDetails(int reqId, Contract contract, Execution execution);
        void execDetailsEnd(int reqId);
        void commissionReport(CommissionReport commissionReport);
        void fundamentalData(int reqId, string data);
        void historicalData(int reqId, string date, double open, double high, double low, double close, int volume, int count, double WAP, bool hasGaps);
        void marketDataType(int reqId, int marketDataType);
        void updateMktDepth(int tickerId, int position, int operation, int side, double price, int size);
        void updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, int size);
        void updateNewsBulletin(int msgId, int msgType, String message, String origExchange);
        void position(string account, Contract contract, int pos, double avgCost);
        void positionEnd();
        void realtimeBar(int reqId, long time, double open, double high, double low, double close, long volume, double WAP, int count);
        void receiveFA(int faDataType, string faXmlData);
        void historicalDataEnd(int reqId, string startDate, string endDate);
        void verifyMessageAPI(string apiData);
        void verifyCompleted(bool isSuccessful, string errorText);
        void displayGroupList(int reqId, string groups);
        void displayGroupUpdated(int reqId, string contractInfo);
    }
}
