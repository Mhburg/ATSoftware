using IBApi;

namespace Model
{
    public class BoxEntry
    {
        public double Strike { get; set; }
        public ConcurrentContract<Contract> CallContract { get; set; }
        public ConcurrentContract<Contract> PutContract { get; set; }
        public int OrdersInvolved { get; set; }
    }
}
