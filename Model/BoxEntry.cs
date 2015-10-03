namespace Model
{
    public class BoxEntry
    {
        public double Strike { get; set; }
        public double CallBid { get; set; }
        public double CallAsk { get; set; }
        public double PutBid { get; set; }
        public double PutAsk { get; set; }
        public int OrdersInvolved { get; set; }
    }
}
