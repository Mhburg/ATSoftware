using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class GenericWrapper : EWrapperImpl
    {
        #region Private Fields
        IRepo _repo;
        #endregion

        #region Constructors
        public GenericWrapper(IRepo repo)
        {
            _repo = repo;
        }
        #endregion

        #region Public Methods
        public override void tickPrice(int tickerId, int field, double price, int canAutoExecute)
        {
            _repo.ReadTickPrice(tickerId, field, price, canAutoExecute);
        }

        public override void tickSize(int tickerId, int field, int size)
        {
            _repo.ReadtickSize(tickerId, field, size);
        }

        public override void tickString(int tickerId, int tickType, string value)
        {
            _repo.ReadtickString(tickerId, tickType, value);
        }

        public override void tickGeneric(int tickerId, int field, double value)
        {
            _repo.ReadtickGeneric(tickerId, field, value);
        }
        #endregion
    }
}
