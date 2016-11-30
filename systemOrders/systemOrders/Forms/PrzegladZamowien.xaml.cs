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
    /// Interaction logic for PrzegladZamowien.xaml
    /// </summary>
    public partial class PrzegladZamowien : UserControl
    {
        private SystemOrdersApp _systemOrders;
        private ObservableCollection<ZamowionyTowarDB> _listaZamowionychTowarow;
        public DbSet<Zamowienie> zamowieniaInGridZamowien;

        public PrzegladZamowien(SystemOrdersApp _systemOrdersApp)
        {
            InitializeComponent();
            this._systemOrders = _systemOrdersApp;
            _listaZamowionychTowarow = new ObservableCollection<ZamowionyTowarDB>();
            DataSourceKlienci();
            DataSourceWszystkieTowary();
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
                new MessageBoxSystemOrders("Błąd podczas połączenia z bazą danych. " + Environment.NewLine + "Sprawdz połączenie z bazą danych. ", true);
                new LogDB("Błąd podczas połączenia z bazą danych. " + ex.ToString());
            }

        }
        public void DataSourceWszystkieTowary() // zastosowane by lepiej wyświetlało się w DataGrid
        {
            try
            {
                using (systemOrdersEntities session = new systemOrdersEntities())
                {
                    dataGridZamowienia.ItemsSource = (from item in session.Zamowienies select item).ToList();
                }
            }
            catch(Exception ex)
            {
                new MessageBoxSystemOrders("Błąd podczas połączenia z bazą danych. " + Environment.NewLine + "Sprawdz połączenie z bazą danych. ", true);
                new LogDB("Błąd podczas połączenia z bazą danych. " + ex.ToString());
            }

        }

        private void btnWyswietlWszystkieZamowienia_Click(object sender, RoutedEventArgs e)
        {
            DataSourceWszystkieTowary();
            dataGridProduktyWzamowieniu.ItemsSource = new ObservableCollection<ZamowionyTowarDB>();
        }

        private void dataGridZamowienia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dataGridZamowienia.SelectedItem != null)
                {
                    if (dataGridZamowienia.SelectedItem is Zamowienie)
                    {
                        using (systemOrdersEntities session = new systemOrdersEntities())
                        {
                            Zamowienie zamowienie = (Zamowienie)dataGridZamowienia.SelectedItem;
                            var TowaryZamowienia = (from item in session.ZamowioneTowaries
                                                    where item.id_zamowienia == zamowienie.id_zamowienia
                                                    select item).ToList();
                            dataGridProduktyWzamowieniu.ItemsSource = TowaryZamowienia;

                        }

                    }
                }
                //else
                //{
                //    new MessageBoxSystemOrders("Wybierz zamówienie by móc przejrzeć dostępne towary w zamówieniu. ", false);
                //}
            }
            catch(Exception ex)
            {
                new MessageBoxSystemOrders("Błąd aplikacji @tuszcom. ", true);
                new LogDB("Błąd aplikacji @tuszcom. " + ex.ToString());
            }

        }

        private void dataGridKlienciZam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dataGridKlienciZam.SelectedItem != null)
                {
                    if (dataGridKlienciZam.SelectedItem is Klienci)
                    {
                        Klienci zaznaczonyKlient = (Klienci)dataGridKlienciZam.SelectedItem;

                        using (systemOrdersEntities sesssion = new systemOrdersEntities())
                        {
                            List<Zamowienie> zamowieniaDlaKlienta = (from item in sesssion.Zamowienies
                                                                     where item.id_klienta == zaznaczonyKlient.id_klienta
                                                                     select item).ToList();
                            dataGridZamowienia.ItemsSource = zamowieniaDlaKlienta;
                            dataGridProduktyWzamowieniu.ItemsSource = new ObservableCollection<ZamowionyTowarDB>();
                        }
                    }
                }
                else
                {
                    new MessageBoxSystemOrders("Nie wybrano klienta. ", false);
                }
            }
            catch(Exception ex)
            {
                new MessageBoxSystemOrders("Błąd aplikacji @tuszcom. ", true);
                new LogDB("Błąd aplikacji @tuszcom. " + ex.ToString());
            }

        }

        private void btnEksportZamowienXML_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridZamowienia.SelectedItem != null) // seralizacja wybranego zamówienia dla klienta
                {
                    if (dataGridZamowienia.SelectedItem is Zamowienie)
                    {
                        using (systemOrdersEntities session = new systemOrdersEntities())
                        {
                            List<Zamowienie> listZamowien = (from item in session.Zamowienies
                                                             where item.id_zamowienia == ((Zamowienie)(dataGridZamowienia.SelectedItem)).id_zamowienia
                                                             select item).ToList();
                            _systemOrders.SerializacjaZamowieniaXML(listZamowien);
                        }
                    }
                }
                else // serializacja wszystkich zamówień z dataGridZamówień
                {
                    List<Zamowienie> listaZamowien = new List<Zamowienie>();
                    var listZamowien = (from row in dataGridZamowienia.ItemsSource.Cast<Zamowienie>()
                                        select new
                                        {
                                            row.id_zamowienia,
                                            row.id_klienta,
                                            row.id_firmy,
                                            row.id_statusu,
                                            row.kwota_zamowienia,
                                            row.data_otrzymania,
                                            row.data_zamowienia,
                                        }).ToList();
                    foreach (var item in listZamowien)
                    {
                        Zamowienie zamowienie = new Zamowienie();
                        zamowienie.id_zamowienia = item.id_zamowienia;
                        zamowienie.id_klienta = item.id_klienta;
                        zamowienie.id_firmy = item.id_firmy;
                        zamowienie.id_statusu = item.id_statusu;
                        zamowienie.data_zamowienia = item.data_zamowienia;
                        zamowienie.data_otrzymania = item.data_otrzymania;
                        zamowienie.kwota_zamowienia = item.kwota_zamowienia;
                        listaZamowien.Add(zamowienie);
                    }

                    if (listaZamowien != null && listaZamowien.Count > 0)
                    {
                        _systemOrders.SerializacjaZamowieniaXML(listaZamowien);
                    }
                }
            }
            catch(Exception ex)
            {
                new MessageBoxSystemOrders("Błąd serializacji pliku zamówień. ", true);
                new LogDB("Błąd serializacji pliku zamówień." + ex.ToString());
            }

        }
    }
}
