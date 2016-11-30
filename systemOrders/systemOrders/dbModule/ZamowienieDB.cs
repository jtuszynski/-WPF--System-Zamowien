using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace systemOrders.dbModule
{
    [Serializable]
    public class ZamowienieDB
    {
        public int id_zamowienia { get; set; }
        public int id_klienta { get; set; }
        public int id_firmy { get; set; }
        public int id_statusu { get; set; }
        public System.DateTime data_zamowienia { get; set; }
        public System.DateTime data_otrzymania { get; set; }
        public Nullable<decimal> kwota_zamowienia { get; set; }
        public List<ZamowionyTowarDB> zamowione_towary { get; set; }
    }
}
