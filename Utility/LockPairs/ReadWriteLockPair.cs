using System.Threading;

namespace Utility
{
    public class ReadWriteLockPair
    {
        private ManualResetEventSlim _readThreadLock = new ManualResetEventSlim(true);
        private ManualResetEventSlim _writeThreadLock = new ManualResetEventSlim(false);
        public ManualResetEventSlim ReadThreadLock
        {
            get
            {
                return _readThreadLock;
            }
        }
        public ManualResetEventSlim WriteThreadLock
        {
            get
            {
                return _writeThreadLock;
            }
        }
        public void Dispose()
        {
            _readThreadLock.Dispose();
            _writeThreadLock.Dispose();
        }
    }
}
