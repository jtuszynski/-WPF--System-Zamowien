using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace systemOrders.dbModule
{
    public class TowarDB
    {
        private int _id_towaru;
        private string _nazwa;
        private string _opis;
        private decimal _cena;
        private int? _id_firmy;

        public TowarDB(int id_towaru, string nazwa , string opis, int? id_firmy)
        {
            this._id_towaru = id_towaru;
            this._id_firmy = id_firmy;
            this._nazwa = nazwa;
            this._opis = opis;
        }

        public int Id_towaru
        {
            get { return _id_towaru; }
            set { _id_towaru = value; }
        }
        public int? Id_firmy
        {
            get { return _id_firmy; }
            set { _id_firmy = value; }
        }
        public string Nazwa
        {
            get { return _nazwa; }
            set { _nazwa = value; }
        }
        public string Opis
        {
            get { return _opis; }
            set { _opis = value; }
        }
        public decimal Cena
        {
            get { return _cena; }
            set { _cena = value; }
        }
    }
}
