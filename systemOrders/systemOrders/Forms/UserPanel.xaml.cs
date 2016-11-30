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
    /// Interaction logic for UserPanel.xaml
    /// </summary>
    public partial class UserPanel : UserControl
    {
        private Klienci _kontoUser;
        private ObservableCollection<ZamowionyTowarDB> _listaZamowionychTowarow;
        public UserPanel(Klienci konto)
        {

            InitializeComponent();
            this._kontoUser = konto;
            _listaZamowionychTowarow = new ObservableCollection<ZamowionyTowarDB>();
            DataSourceTowary();
        }

        
        private void DodajTowarDoZamowienia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridTowaryInSystem.SelectedItem != null)
                {
                    if (dataGridTowaryInSystem.SelectedItem is Towar)
                    {
                        using (systemOrdersEntities session = new systemOrdersEntities())
                        {
                            //Klienci zaznaczonyKlient = (Klienci)dataGridKlienciZam.SelectedItem;
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
                    new MessageBoxSystemOrders("Wybierz produkt, który chciałbyś dodać dla danego klienta. ", false);
                }
            }
            catch (Exception ex)
            {
                new MessageBoxSystemOrders("Błąd podczas dodawania towaru do zamówienia.", true);
                new LogDB("Błąd podczas dodawania towaru do zamówienia." + ex.ToString());
            }
        }


        private void UsunTowarZzamowienia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridTowaryInSystem.SelectedItem!=null)
                {
                    if (dataGridTowaryInSystem.SelectedItem is Towar)
                    {
                        using (systemOrdersEntities session = new systemOrdersEntities())
                        {
                            // Klienci zaznaczonyKlient = (Klienci)dataGridKlienciZam.SelectedItem;
                            Towar zaznaczonyTowar = (Towar)dataGridTowaryInSystem.SelectedItem;
                            ZamowionyTowarDB TowarDoUsuniecia = ZwrocUsunietyTowarzZamowienia(zaznaczonyTowar.id_towaru);
                            _listaZamowionychTowarow.Remove(TowarDoUsuniecia);
                            dataGridTowaryInZamowieniu.DataContext = _listaZamowionychTowarow;
                        }
                    }
                    else
                    {
                        new MessageBoxSystemOrders("Wybierz towar, który chciałbyś usunąć. ", false);
                    }
                    
                }
                else
                {
                    new MessageBoxSystemOrders("Wybierz towar, który chciałbyś usunąć. ", false);
                }
                      

            }
            catch (Exception ex)
            {
                new LogDB("Błąd podczas usuwania towaru z zamówienia." + ex.ToString());
                new MessageBoxSystemOrders("Błąd podczas usuwania towaru z zamówienia.", true);
            }

        }
        public ZamowionyTowarDB ZwrocUsunietyTowarzZamowienia(int id_towaru)
        {
            foreach (ZamowionyTowarDB item in _listaZamowionychTowarow)
            {
                if (item.Id_towaru == id_towaru)
                {
                    return item;
                }
            }
            return null;
        }
        public void ClearGridViewZamowien()
        {
            _listaZamowionychTowarow = null;
            _listaZamowionychTowarow = new ObservableCollection<ZamowionyTowarDB>();
            dataGridTowaryInZamowieniu.DataContext = _listaZamowionychTowarow;
        }
        private void WyslijZamowienie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    if (_listaZamowionychTowarow != null && _listaZamowionychTowarow.Count > 0)
                    {
                        using (systemOrdersEntities session = new systemOrdersEntities())
                        {
                            //Klienci zaznaczonyKlient = (Klienci)dataGridKlienciZam.SelectedItem;
                            Zamowienie NoweZamowienie = new Zamowienie();
                            NoweZamowienie.data_zamowienia = DateTime.Now;
                            NoweZamowienie.data_otrzymania = DateTime.Now;
                            NoweZamowienie.id_firmy = 1;
                            NoweZamowienie.id_klienta = _kontoUser.id_klienta;
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
            catch (Exception ex)
            {
                new LogDB("Błąd wysyłania zamówienia." + ex.ToString());
                new MessageBoxSystemOrders("Błąd wysyłania zamówienia.", true);
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
            catch (Exception ex)
            {
                new LogDB("Błąd podczas połączenia z bazą danych. " + ex.ToString());
                new MessageBoxSystemOrders("Błąd podczas połączenia z bazą danych. " + Environment.NewLine + "Sprawdz połączenie z bazą danych. ", true);
            }

        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }
    }
}
