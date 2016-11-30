using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using systemOrders;
namespace SystemOrders
{
    public class LogIn
    {
        public LogIn() { }

        public Firma ZnajdzKontoAdmin(string login, string haslo)
        {
            using (systemOrdersEntities session = new systemOrdersEntities())
            {
                List<Firma> Firmy = session.Firmas.Where(x => x.login == login && x.haslo == haslo).ToList();
                if (Firmy != null && Firmy.Count > 0)
                    return Firmy[0];
                else
                    return null;
            }
        }
        public Klienci ZnajdzKontoKlienta(string login, string haslo)
        {
            using (systemOrdersEntities session = new systemOrdersEntities())
            {
                
                List<Klienci> Klienci = session.Kliencis.Where(x => x.login == login && x.haslo == haslo).ToList();
                if (Klienci != null && Klienci.Count > 0)
                    return Klienci[0];
                else
                    return null;
            }
        }
        public object Zaloguj(string login, string haslo)
        {

            if (login != string.Empty && haslo != string.Empty)
            {
                Firma kontoFirma = ZnajdzKontoAdmin(login, haslo);
                Klienci kontoKlienta = ZnajdzKontoKlienta(login, haslo);
               

                if (kontoFirma != null)
                    return kontoFirma;
                 else if(kontoKlienta != null)
                  return kontoKlienta;
            }
            return null;
        }
    }
}
