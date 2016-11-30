using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace systemOrders.dbModule
{
    [Serializable]
    public class ZamowionyTowarDB
    {
        private int _id_towaru;
        private string _nazwa_towaru;
        private int _ilosc;
        private decimal _kwota;

        public ZamowionyTowarDB(int id_towaru, string nazwa_towaru, int ilosc, decimal kwota)
        {
            this._id_towaru = id_towaru;
            this._nazwa_towaru = nazwa_towaru;
            this._ilosc = ilosc;
            this._kwota = kwota;
        }
        public ZamowionyTowarDB() { }
        public decimal Kwota
        {
            get { return _kwota; }
            set { _kwota = value; }
        }
        public int Ilosc
        {
            get { return _ilosc; }
            set { _ilosc = value; }
        }
        public string Nazwa_towaru
        {
            get { return _nazwa_towaru; }
            set { _nazwa_towaru = value; }
        }
        public int Id_towaru
        {
            get { return _id_towaru; }
            set { _id_towaru = value; }
        }
    }
}
