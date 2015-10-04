namespace Utility
{
    public static class SecTypRef
    {
        public const int STK = 1;
        public const int OPT = 2;
        public const int FUT = 3;
        public const int IND = 4;
        public const int FOP = 5;
        public const int CASH = 6;
        public const int BAG = 7;
        public const int NEWS = 8;

        public static string GetType(int field)
        {
            switch (field)
            {
                case STK:
                    return "STK";
                case OPT:
                    return "OPT";
                case FUT:
                    return "FUT";
                case IND:
                    return "IND";
                case FOP:
                    return "FOP";
                case CASH:
                    return "CASE";
                case BAG:
                    return "BAG";
                case NEWS:
                    return "NEWS";
                default:
                    return "";
            }
        }
    }
}
