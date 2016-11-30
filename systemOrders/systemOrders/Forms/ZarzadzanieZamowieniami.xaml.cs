using Common.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using systemOrders.dbModule;
using systemOrders.Models;

namespace systemOrders.Forms
{
    /// <summary>
    /// Interaction logic for ZarzadzanieZamowieniami.xaml
    /// </summary>
    public partial class ZarzadzanieZamowieniami : UserControl
    {
        private SystemOrdersApp _systemOrders;
        private ObservableCollection<ZamowionyTowarDB> _listaZamowionychTowarow;
        public ZarzadzanieZamowieniami(SystemOrdersApp _systemOrdersApp)
        {
            InitializeComponent();
            _systemOrders = _systemOrdersApp;
            _listaZamowionychTowarow = new ObservableCollection<ZamowionyTowarDB>();
            DataSourceKlienci();
            DataSourceTowary();
        }
        public void DataSourceKlienci()
        {
            try
            {
                using (systemOrdersEntities session = new systemOrdersEntities())
                {
                    dataGridKlienciZam.ItemsSource = (from item in session.Kliencis select item).ToList();
                }
            }
            catch(Exception ex)
            {
                new LogDB("Błąd podczas połączenia z bazą danych. " + ex.ToString());
                new MessageBoxSystemOrders("Błąd podczas połączenia z bazą danych. " + Environment.NewLine +"Sprawdz połączenie z bazą danych. ",true);
            }
        }
        public void DataSourceTowary() 
        {        
            try
            {
                using (systemOrdersEntities session = new systemOrdersEntities())
                {
                    dataGridTowaryInSystem.ItemsSource = (from item in session.Towars select item).ToList();
                }
            }
            catch(Exception ex)
            {
                new LogDB("Błąd podczas połączenia z bazą danych. " + ex.ToString());
                new MessageBoxSystemOrders("Błąd podczas połączenia z bazą danych. " + Environment.NewLine + "Sprawdz połączenie z bazą danych. ", true);
            }

        }
        private void DodajTowarDoZamowienia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridKlienciZam.SelectedItem != null && dataGridTowaryInSystem.SelectedItem != null)
                {
                    if (dataGridKlienciZam.SelectedItem != null)
                    {


                        if (dataGridKlienciZam.SelectedItem is Klienci)
                        {
                            if (dataGridTowaryInSystem.SelectedItem != null)
                            {
                                if (dataGridTowaryInSystem.SelectedItem is Towar)
                                {
                                    using (systemOrdersEntities session = new systemOrdersEntities())
                                    {
                                        Klienci zaznaczonyKlient = (Klienci)dataGridKlienciZam.SelectedItem;
                                        Towar zaznaczonyTowar = (Towar)dataGridTowaryInSystem.SelectedItem;
                                        List<ZamowionyTowarDB> IstniejeNaLiscie = _listaZamowionychTowarow.Where(x => x.Id_towaru == zaznaczonyTowar.id_towaru).ToList();
                                        if (IstniejeNaLiscie != null && IstniejeNaLiscie.Count == 0)
                                        {

                                            ZamowionyTowarDB zamawianyTowar = new ZamowionyTowarDB(zaznaczonyTowar.id_towaru,
                                                                   zaznaczonyTowar.nazwa, 1, zaznaczonyTowar.cena);
                                            _listaZamowionychTowarow.Add(zamawianyTowar);
                                            dataGridTowaryInZamowieniu.DataContext = _listaZamowionychTowarow;
                                        }
                                        else
                                        {
                                            foreach (ZamowionyTowarDB item in _listaZamowionychTowarow)
                                            {
                                                if (item.Id_towaru == zaznaczonyTowar.id_towaru)
                                                {
                                                    item.Ilosc += 1;
                                                    item.Kwota = (zaznaczonyTowar.cena * item.Ilosc);
                                                }
                                            }
                                            dataGridTowaryInZamowieniu.DataContext = null;
                                            dataGridTowaryInZamowieniu.DataContext = _listaZamowionychTowarow;
                                        }
                                    }
                                }
                                else
                                {

                                    new MessageBoxSystemOrders("Wybierz produkt, który chciałbyś dodać dla danego klienta. ", false);
                                }
                            }
                            else
                            {
                                new MessageBoxSystemOrders("Wybierz klienta, do którego chciałbyś dodać towar. ", false);
                            }

                        }
                        else
                        {
                            new MessageBoxSystemOrders("Wybierz klienta, do którego chciałbyś dodać towar. ", false);
                        }
                    }
                    else
                    {
                        new MessageBoxSystemOrders("Wybierz klienta, do którego chciałbyś dodać towar. ", false);
                    }
                }
                else
                {
                    new MessageBoxSystemOrders("Wybierz klienta oraz wybierz produkt, dla którego chcesz stworzyć zamówienie. ", false);
                }
            }
            catch(Exception ex)
            {
                new MessageBoxSystemOrders("Błąd podczas dodawania towaru do zamówienia.", true);
                new LogDB("Błąd podczas dodawania towaru do zamówienia." + ex.ToString());
            }
           
        }


        private void UsunTowarZzamowienia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridKlienciZam.SelectedItem != null && dataGridTowaryInSystem.SelectedItem != null)
                {
                    if (dataGridKlienciZam.SelectedItem != null)
                    {
                        if (dataGridKlienciZam.SelectedItem is Klienci)
                        {
                            if (dataGridTowaryInSystem.SelectedItem != null)
                            {
                                if (dataGridTowaryInSystem.SelectedItem is Towar)
                                {
                                    using (systemOrdersEntities session = new systemOrdersEntities())
                                    {
                                        Klienci zaznaczonyKlient = (Klienci)dataGridKlienciZam.SelectedItem;
                                        Towar zaznaczonyTowar = (Towar)dataGridTowaryInSystem.SelectedItem;
                                        ZamowionyTowarDB TowarDoUsuniecia = ZwrocUsunietyTowarzZamowienia(zaznaczonyTowar.id_towaru);
                                        _listaZamowionychTowarow.Remove(TowarDoUsuniecia);
                                        dataGridTowaryInZamowieniu.DataContext = _listaZamowionychTowarow;
                                    }
                                }
                                else
                                {
                                    new LogDB("Pozycja z widoku nie jest typu towar. ");
                                    new MessageBoxSystemOrders("Pozycja z widoku nie jest typu towar. ", false);
                                }
                            }
                            else
                            {
                                new MessageBoxSystemOrders("Wybierz towar, który chciałbyś usunąć. ", false);
                            }
                        }
                        else
                        {
                            new LogDB("Pozycja z widoku nie jest typu klient. ");
                            new MessageBoxSystemOrders("Pozycja z widoku nie jest typu klient. ", false);
                        }
                    }
                    else
                    {
                        new MessageBoxSystemOrders("Wybierz klienta, a następnie produkt, który chciałbyś usunąć. ", false);
                    }                 
                }
                else
                {
                    new MessageBoxSystemOrders("Wybierz klienta oraz wybierz towar, który ma zostać usunięty. ", false);
                }
            }
            catch(Exception ex)
            {
                new LogDB("Błąd podczas usuwania towaru z zamówienia." + ex.ToString());
                new MessageBoxSystemOrders("Błąd podczas usuwania towaru z zamówienia.", true);
            }
            
        }
        public ZamowionyTowarDB ZwrocUsunietyTowarzZamowienia(int id_towaru)
        {
            foreach(ZamowionyTowarDB item in _listaZamowionychTowarow)
            {
                if(item.Id_towaru == id_towaru)
                {
                    return item;
                }
            }
            return null;
        }
        private void WyslijZamowienie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridKlienciZam.SelectedItem != null)
                {
                    if (_listaZamowionychTowarow != null && _listaZamowionychTowarow.Count > 0)
                    {
                        using (systemOrdersEntities session = new systemOrdersEntities())
                        {
                            Klienci zaznaczonyKlient = (Klienci)dataGridKlienciZam.SelectedItem;
                            Zamowienie NoweZamowienie = new Zamowienie();
                            NoweZamowienie.data_zamowienia = DateTime.Now;
                            NoweZamowienie.data_otrzymania = DateTime.Now;
                            NoweZamowienie.id_firmy = _systemOrders.Konto.id_firmy;
                            NoweZamowienie.id_klienta = zaznaczonyKlient.id_klienta;
                            NoweZamowienie.kwota_zamowienia = _listaZamowionychTowarow.Sum(x => x.Kwota);
                            NoweZamowienie.id_statusu = (int)EnumStatusZamowienia.zamowiona;
                            session.Zamowienies.Add(NoweZamowienie);
                            session.SaveChanges();
                            foreach (ZamowionyTowarDB item in _listaZamowionychTowarow)
                            {
                                ZamowioneTowary nowyTowardoZamowienia = new ZamowioneTowary();
                                nowyTowardoZamowienia.kwota = item.Kwota;
                                nowyTowardoZamowienia.ilosc = item.Ilosc;
                                nowyTowardoZamowienia.id_zamowienia = NoweZamowienie.id_zamowienia;
                                nowyTowardoZamowienia.id_towaru = item.Id_towaru;
                                session.ZamowioneTowaries.Add(nowyTowardoZamowienia);
                            }
                            session.SaveChanges();
                            new MessageBoxSystemOrders("Zamówienie zostało przyjęte w systemie zamówień @tuszcom. ", false);
                            ClearGridViewZamowien();
                        }
                    }
                    else
                    {
                        new MessageBoxSystemOrders("Nie moża wykonać operacji wysłania zamówienia." + Environment.NewLine + "Lista towarów w zmaówieniu jest pusta. ", false);
                    }
                }
                else
                {
                    new MessageBoxSystemOrders("Nie można wykonac operacji wysłania zamówienia." + Environment.NewLine + " Wybierz klienta a następnie stwórz zamówienie. ", false);
                }
            }
            catch(Exception ex)
            {
                new LogDB("Błąd wysyłania zamówienia." + ex.ToString());
                new MessageBoxSystemOrders("Błąd wysyłania zamówienia.", true);
            }
           
        }

        private void dataGridKlienciZam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearGridViewZamowien();
        }
        public void ClearGridViewZamowien()
        {
            _listaZamowionychTowarow = null;
            _listaZamowionychTowarow = new ObservableCollection<ZamowionyTowarDB>();
            dataGridTowaryInZamowieniu.DataContext = _listaZamowionychTowarow;
        }
    }
}
