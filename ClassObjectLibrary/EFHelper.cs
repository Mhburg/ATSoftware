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
        public static T AddEntry<T>(this DbSet<T> dbset, T entry, IBDbContext dbc) where T : class
        {
            ++dbc.EntryCount;
            return dbset.Add(entry);
        }
        public static int Save(this IBDbContext dbc)
        {
            if (dbc.EntryCount >= 3000)
            {
                dbc.EntryCount = 0;
                int result = dbc.SaveChanges();
                dbc.Dispose();
                dbc = dbc.GetInstance();
                return result;
            }
            return 0;
        }
        #endregion

    }
}
