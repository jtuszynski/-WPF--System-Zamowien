using Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace systemOrders.dbModule
{
    public class LogDB
    {
        public LogDB(string ex)
        {
            try
            {
   
                using (systemOrdersEntities session = new systemOrdersEntities())
                {
                    Log log = new Log();
                    log.data = DateTime.Now;
                    if (ex.Length < 800)
                        log.tresc_loga = ex;
                    else
                        log.tresc_loga = ex.Substring(0, 799); 
              
                    session.Logs.Add(log);
                    session.SaveChanges();
                }
            }
            catch
            {
                new MessageBoxSystemOrders("Problem z zapisem loga do bazy danych. ", true);
            }

        }
    }
}
