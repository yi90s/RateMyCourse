using cRegis.IntegrationTest.Infrastructure;
using System.Collections.Generic;
namespace cRegis.IntegrationTest.Helper
{
    public static class Utilities
    {
        #region snippet1
        public static void InitializeDbForTests(DataContextTest db)
        {
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(DataContextTest db)
        {
            InitializeDbForTests(db);
        }

        #endregion
    }
}
