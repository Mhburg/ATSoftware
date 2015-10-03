using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL
{
    public static class EFHelper
    {
        #region Private Fields

        #endregion

        #region Public Methods
        public static T AddEntry<T>(this DbSet<T> dbset, T entry, ICount count) where T : class
        {
            ++count.EntryCount;
            return dbset.Add(entry);
        }
        public static int Save(this DbContext dbContext, ICount count)
        {
            if (count.EntryCount >= 1000)
            {
                count.EntryCount = 0;
                return dbContext.SaveChanges();
            }
            return 0;
        }
        #endregion

    }
}
