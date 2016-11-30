using Common.Settings;
using System;
using System.Collections.Generic;
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
//using Common.Settings;
using systemOrders.Models;
using System.Globalization;
//using System.Windows;
using systemOrders.dbModule;
namespace systemOrders.Forms
{
    /// <summary>
    /// Interaction logic for ZarzadzanieKlientami.xaml
    /// </summary>
    public partial class ZarzadzanieKlientami : UserControl
    {
        private SystemOrdersApp _systemOrdersFormKlienci;
        private EnumEditCreate _CreateEnum;
        private Klienci _edytowanyKlient;
        public ZarzadzanieKlientami(SystemOrdersApp systemOrdersApp)
        {
            InitializeComponent();
            _systemOrdersFormKlienci = systemOrdersApp;
            _CreateEnum = EnumEditCreate.create;
            try
            {
                DataSourceKlienci(); 
            }
            catch(Exception ex)
            {
                new LogDB("Błąd podczas połączenia z bazą danych " + ex.ToString());
                new MessageBoxSystemOrders("Błąd podczas połączenia z bazą danych. " + Environment.NewLine +"Sprawdz połączenie z bazą danych. ",true);
            }
              
        }
        public List<string> WalidacjaDodajEdytujKlienta()
        {
            List<string> ListaWalidacji = new List<string>();

            // walidacja imienia
            if (txbImie.Text.Length > 50) { ListaWalidacji.Add("-Pole imie nie może przekraczać więcej niż 50 znaków."); }
            if (txbImie.Text.Length == 0) { ListaWalidacji.Add("-Pole imie nie może być puste."); }
            if (txbImie.Text.Length > 0 && txbImie.Text.Length < 50)
            if (!RegexExpression.regexImie.IsMatch(txbImie.Text)) { ListaWalidacji.Add("-Imie nie może zawierać znaków liczbowych."); }
           

            //walidacja nazwiska
            if (txbNazwisko.Text.Length > 50) { ListaWalidacji.Add("-Pole nazwisko nie może przekraczać więcej niż 50 znaków."); }
            if (txbNazwisko.Text.Length == 0) { ListaWalidacji.Add("-Pole nazwisko nie może być puste."); }
            if (txbNazwisko.Text.Length > 0 && txbNazwisko.Text.Length < 50)
            if (!RegexExpression.regexNazwisko.IsMatch(txbNazwisko.Text)) { ListaWalidacji.Add("-Imie nie może zawierać znaków liczbowych."); }
          
            //walidacja NIP = NIP moze byc nullem // zas jesli jest NIP to walidacja do 10 liczb
            if(txbNIP.Text!=string.Empty)
            {
                if (!RegexExpression.regexNip.IsMatch(txbNIP.Text))
                {
                    ListaWalidacji.Add("-Błędny kod NIP.");
                }
            }
            if (txbLogin.Text.Length > 20) { ListaWalidacji.Add("-Pole login nie może przekraczać więcej niż 20 znaków."); }          
            if (txbHaslo.Text.Length > 20) { ListaWalidacji.Add("-Pole hasło nie może przekraczać więcej niż 20 znaków."); }
            using (systemOrdersEntities session = new systemOrdersEntities())
            {
                List<string> listaLoginow = (from item in session.Kliencis select item.login).ToList();
                 if(txbLogin.Text != string.Empty && listaLoginow.Contains(txbLogin.Text) && txbLogin.Text != _edytowanyKlient.login)
                 {
                     ListaWalidacji.Add("-Dany login już istnieje w systemie.");
                 }
            }         
            if (CzySzczegoloweDane.IsChecked == true)
            {
                if (txbMiejscowosc.Text.Length > 50) { ListaWalidacji.Add("-Pole miejscowość nie może przekraczać więcej niż 50 znaków."); }
                if (txbMiejscowosc.Text.Length == 0) { ListaWalidacji.Add("-Pole miejscowość nie może być puste."); }
                if (txbMiejscowosc.Text.Length > 0 && txbMiejscowosc.Text.Length < 50)
                    if (!RegexExpression.regexMiejscowosc.IsMatch(txbMiejscowosc.Text)) { ListaWalidacji.Add("-Miejscowość nie może zawierać znaków liczbowych."); }
                if (txbUlica.Text.Length > 50) { ListaWalidacji.Add("-Pole ulica nie może przekraczać więcej niż 50 znaków."); }
                if (txbUlica.Text.Length == 0) { ListaWalidacji.Add("-Pole ulica nie może być puste."); }

                if (txbnrDomu.Text != string.Empty)
                {
                    if (!RegexExpression.regexNrDomu.IsMatch(txbnrDomu.Text)) { ListaWalidacji.Add("-Błąd walidacji nr domu."); } 
                }
                if (cbWojewodztwo.SelectedItem == null) { ListaWalidacji.Add("-Wybierz województwo dla danego klienta"); }
                if (txbPowiat.Text.Length > 50) { ListaWalidacji.Add("-Pole powiat nie może przekraczać więcej niż 50 znaków."); }
                if (txbPowiat.Text.Length == 0) { ListaWalidacji.Add("-Pole powiat nie może być puste."); }
                if(txbPowiat.Text.Length < 50 && txbPowiat.Text.Length>0)
                {
                    if (!RegexExpression.regexPowiat.IsMatch(txbPowiat.Text)) { ListaWalidacji.Add("-Pole powiat nie może zawierać liczb."); }
                }
                if (txbGmina.Text.Length > 50) { ListaWalidacji.Add("-Pole gmina nie może przekraczać więcej niż 50 znaków."); }
                if (txbGmina.Text.Length < 50 && txbGmina.Text.Length>0)
                {
                    if (!RegexExpression.regexGmina.IsMatch(txbGmina.Text)) { ListaWalidacji.Add("-Pole gmina nie może zawierać liczb."); } 
                }
                //if (txbGmina.Text.Length == 0) { ListaWalidacji.Add("-Pole gmina nie może być pusta."); }
                if (txbKodPocztowy.Text.Length > 0)
                    if (!RegexExpression.regexKodPocztowy.IsMatch(txbKodPocztowy.Text)) { ListaWalidacji.Add("-Błędy kod pocztowy. Dostosuj kot pocztowy do formatu 00-001"); }
            }      
            return ListaWalidacji;
        }

        private void btnDodajEdytujKlienta_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<string> errors = WalidacjaDodajEdytujKlienta();
            if (errors != null && errors.Count > 0)
            {
                MessageBoxSystemOrders errorsMessage = new MessageBoxSystemOrders(errors);
                MessageBoxResult result = errorsMessage.MessageBoxErrorsShow(errors);
            }
            else
            {
                if (_CreateEnum == EnumEditCreate.create)
                {
                    try
                    {
                        if (CzySzczegoloweDane.IsChecked == false) // sam klient bez adresu
                        {
                            using (systemOrdersEntities session = new systemOrdersEntities()) // TODO metoda Insert do bazy
                            {
                                Klienci nowyKlient = new Klienci();
                                nowyKlient.imie = txbImie.Text;
                                nowyKlient.nazwisko = txbNazwisko.Text;
                                nowyKlient.login = txbLogin.Text == string.Empty ? null : txbLogin.Text;
                                nowyKlient.haslo = txbHaslo.Text;
                                nowyKlient.nip = txbNIP.Text;
                                session.Kliencis.Add(nowyKlient);
                                session.SaveChanges();
                                new MessageBoxSystemOrders("Klient został dodany do systemu zamówień @tuszcom. ", false);
                                RefreshDataSource();
                            }
                        }
                        else
                        {
                            using (systemOrdersEntities session = new systemOrdersEntities()) // TODO metoda Insert do bazy
                            {
                                int nrDomu;
                                int.TryParse(txbnrDomu.Text, out nrDomu);
                                Adre nowyAdres = new Adre();
                                nowyAdres.miejscowosc = txbMiejscowosc.Text;
                                nowyAdres.ulica = txbUlica.Text;
                                nowyAdres.numer_domu = nrDomu;
                                nowyAdres.wojewodztwo = cbWojewodztwo.SelectionBoxItem.ToString();
                                nowyAdres.powiat = txbPowiat.Text;
                                nowyAdres.gmina = txbGmina.Text;
                                nowyAdres.kod_pocztowy = txbKodPocztowy.Text;
                                session.Adres.Add(nowyAdres);
                                session.SaveChanges();
                                Klienci nowyKlient = new Klienci();
                                nowyKlient.id_adresu = nowyAdres.id_adresu;
                                nowyKlient.imie = txbImie.Text;
                                nowyKlient.nazwisko = txbNazwisko.Text;
                                nowyKlient.login = txbLogin.Text == string.Empty ? null : txbLogin.Text;
                                nowyKlient.haslo = txbHaslo.Text;
                                nowyKlient.nip = txbNIP.Text;
                                session.Kliencis.Add(nowyKlient);
                                session.SaveChanges();
                                RefreshDataSource();
                                new MessageBoxSystemOrders("Klient wraz z adresem został dodany do systemu zamówień @tuszcom. ", false);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        new MessageBoxSystemOrders("Wystąpił błąd podczas zapisu klienta do systemu zamówień @tuszcom. ", true);
                        new LogDB("Wystąpił błąd podczas zapisu klienta do systemu zamówień @tuszcom. " + ex.ToString());
                    }
                }
                else if (_CreateEnum == EnumEditCreate.edit) // klient edytowalny
                {
                    try
                    {
                        using (systemOrdersEntities session = new systemOrdersEntities())
                        {
                            Klienci edytowanyKlient = (from x in session.Kliencis
                                                       where x.id_klienta == _edytowanyKlient.id_klienta
                                                       select x).First();

                            edytowanyKlient.imie = txbImie.Text;
                            edytowanyKlient.nazwisko = txbNazwisko.Text;
                            edytowanyKlient.nip = txbNIP.Text;
                            if (txbLogin.Text != null && txbLogin.Text != _edytowanyKlient.login)
                                edytowanyKlient.login = txbLogin.Text;
                            edytowanyKlient.haslo = txbHaslo.Text;

                            if (edytowanyKlient.id_adresu != null) // klient edytowalny, ktory posiada adres
                            {

                                Adre edytowanyAdres = (from x in session.Adres
                                                       where x.id_adresu == edytowanyKlient.id_adresu
                                                       select x).First();
                                if (edytowanyAdres != null)
                                {
                                    int nrDomu;
                                    int.TryParse(txbnrDomu.Text, out nrDomu);
                                    edytowanyAdres.miejscowosc = txbMiejscowosc.Text;
                                    edytowanyAdres.ulica = txbUlica.Text;
                                    edytowanyAdres.numer_domu = nrDomu;
                                    edytowanyAdres.wojewodztwo = cbWojewodztwo.SelectionBoxItem.ToString();
                                    edytowanyAdres.powiat = txbPowiat.Text;
                                    edytowanyAdres.gmina = txbGmina.Text;
                                    edytowanyAdres.kod_pocztowy = txbKodPocztowy.Text;
                                    session.SaveChanges();
                                }
                                session.SaveChanges();
                                RefreshDataSource();
                                new MessageBoxSystemOrders("Klient został zaktualizowany w systemie zamówień @tuszcom", false);
                            }
                            else
                            {
                                if (CzySzczegoloweDane.IsChecked == true) // jesli klient jest edytowany a nie mial adresu i ma zaznaczona opcje 'dane szczegolowe'
                                {
                                    int nrDomu;
                                    int.TryParse(txbnrDomu.Text, out nrDomu);
                                    Adre nowyAdres = new Adre();
                                    nowyAdres.miejscowosc = txbMiejscowosc.Text;
                                    nowyAdres.ulica = txbUlica.Text;
                                    nowyAdres.numer_domu = nrDomu;
                                    nowyAdres.wojewodztwo = cbWojewodztwo.SelectionBoxItem.ToString();
                                    nowyAdres.powiat = txbPowiat.Text;
                                    nowyAdres.gmina = txbGmina.Text;
                                    nowyAdres.kod_pocztowy = txbKodPocztowy.Text;
                                    session.Adres.Add(nowyAdres);
                                    session.SaveChanges();
                                    edytowanyKlient.imie = txbImie.Text;
                                    edytowanyKlient.nazwisko = txbNazwisko.Text;
                                    edytowanyKlient.nip = txbNIP.Text;
                                    if (txbLogin.Text != null && txbLogin.Text != _edytowanyKlient.login)
                                        edytowanyKlient.login = txbLogin.Text;
                                    edytowanyKlient.haslo = txbHaslo.Text;
                                    edytowanyKlient.id_adresu = nowyAdres.id_adresu;


                                }
                                session.SaveChanges();
                                RefreshDataSource();
                                new MessageBoxSystemOrders("Klient został zaktualizowany w systemie zamówień @tuszcom", false);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        new MessageBoxSystemOrders("Wystąpił błąd podczas aktualizacji klienta w systemie zamówień @tuszcom. ", true);
                        new LogDB("Wystąpił błąd podczas aktualizacji klienta w systemu zamówień @tuszcom. " + ex.ToString());
                    }

                }


            }
        }

        private void btnDodajKlienta_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                _CreateEnum = EnumEditCreate.create;
                gbDodajEdytujKlienta.IsEnabled = true;
                SetUserControlToEditOrCreateKlient();
                ClearTextboxesPanelKlienta();
            }
            catch(Exception ex)
            {
                new MessageBoxSystemOrders("Błąd przy aktywacji panelu edycji. ", true);
                new LogDB("Błąd przy aktywacji panelu edycji. " + ex.ToString());
            }

        }
      
        private void btnEdytujKlienta_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(dataGridKlienci.SelectedItem!=null)
            {
                _CreateEnum = EnumEditCreate.edit;
                gbDodajEdytujKlienta.IsEnabled = true;
                SetUserControlToEditOrCreateKlient();
            }
            else
            {
                new MessageBoxSystemOrders("Wybierz klienta, którego chcesz edytować. ", false);
            }

        }

        private void btnUsunKlienta_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dataGridKlienci.SelectedItem != null)
                {
                    if (dataGridKlienci.SelectedItem is Klienci)
                    {
                        using (systemOrdersEntities session = new systemOrdersEntities())
                        {
                            Klienci zaznaczonyKlient = (Klienci)dataGridKlienci.SelectedItem;
                            if (zaznaczonyKlient != null)
                            {
                                MessageBoxResult dialogResult = MessageBox.Show("Czy na pewno chcesz usunąć klienta - " + zaznaczonyKlient.imie + " " + zaznaczonyKlient.nazwisko + " ?", "Komunikat systemu zamówień @tuszcom", MessageBoxButton.YesNo);
                                if (dialogResult == MessageBoxResult.Yes)
                                {
                                    Klienci usuwanyKlient = (from x in session.Kliencis
                                                             where x.id_klienta == zaznaczonyKlient.id_klienta
                                                             select x).First();
                                    if (usuwanyKlient.id_adresu != null)
                                    {
                                        Adre usuwanyAdres = (from x in session.Adres
                                                             where x.id_adresu == usuwanyKlient.id_adresu
                                                             select x).First();

                                      
                                        session.Adres.Remove(usuwanyAdres);
                                    }
                                    List<Zamowienie> usuwaneZamowienie = (from x in session.Zamowienies
                                                                          where x.id_klienta == usuwanyKlient.id_klienta
                                                                          select x).ToList();


                                    foreach (Zamowienie zamRemove in usuwaneZamowienie)
                                    {
                                        List<ZamowioneTowary> usuwanieTowarow = (from x in session.ZamowioneTowaries
                                                                                 where x.id_zamowienia == zamRemove.id_zamowienia
                                                                                 select x).ToList();
                                        foreach (ZamowioneTowary zamTowRemove in usuwanieTowarow)
                                        {
                                            session.ZamowioneTowaries.Remove(zamTowRemove);
                                        }
                                        session.Zamowienies.Remove(zamRemove);
                                    }
                                    session.Kliencis.Remove(usuwanyKlient);

                                    session.SaveChanges();
                                    new MessageBoxSystemOrders("Klient został usunięty z systemu @tuszcom. ", false);
                                    ClearTextboxesPanelKlienta();
                                    RefreshDataSource();
                                }
                            }
                        }
                    }
                    else
                    {
                        new MessageBoxSystemOrders("Wybierz klienta, a następnie towar do usunięcia", false);
                    }
                }
                else
                {
                    new MessageBoxSystemOrders("Wybierz towar do usunięcia.", true);
                }
            }
            catch(Exception ex)
            {
                new LogDB("Wystąpił błąd podczas usuwania klienta z systemu zamówień @tuszcom.  " + ex.ToString());
                new MessageBoxSystemOrders("Wystąpił błąd podczas usuwania klienta z systemu zamówień @tuszcom. ", true);
            }
        }
          
        public void SetUserControlToEditOrCreateKlient()
        {
            try
            {
                cbWojewodztwo.SelectedValue = null;
                if (_CreateEnum == EnumEditCreate.create)
                {
                    gbDodajEdytujKlienta.Header = "Panel dodawania klienta";
                    btnDodajEdytujKlienta.Content = "Dodaj klienta";
                }
                else if (_CreateEnum == EnumEditCreate.edit)
                {
                    gbDodajEdytujKlienta.Header = "Panel edytowania towaru";
                    btnDodajEdytujKlienta.Content = "Zatwierdź";

                    if (dataGridKlienci.SelectedItem is Klienci)
                    {
                        _edytowanyKlient = (Klienci)dataGridKlienci.SelectedItem;
                        if (_edytowanyKlient.id_adresu != null)
                        {
                            gbDodajEdytujKlienta.Height = 601;
                            CzySzczegoloweDane.IsChecked = true;
                            VisbileSzczegoloweDane(Visibility.Visible);
                            using (systemOrdersEntities session = new systemOrdersEntities())
                            {
                                Adre edytowanyAdres = (from x in session.Adres
                                                       where x.id_adresu == _edytowanyKlient.id_adresu
                                                       select x).First();
                                txbMiejscowosc.Text = edytowanyAdres.miejscowosc;
                                txbUlica.Text = edytowanyAdres.ulica;
                                txbnrDomu.Text = edytowanyAdres.numer_domu.ToString();
                                cbWojewodztwo.SelectedValue = edytowanyAdres.wojewodztwo;
                                foreach (var cmbi in
                                     cbWojewodztwo.Items.Cast<ComboBoxItem>().Where(cmbi => (string)cmbi.Tag == edytowanyAdres.wojewodztwo))
                                    cmbi.IsSelected = true;

                                txbPowiat.Text = edytowanyAdres.powiat;
                                txbGmina.Text = edytowanyAdres.gmina;
                                txbKodPocztowy.Text = edytowanyAdres.kod_pocztowy;
                            }
                        }
                        else
                        {
                            gbDodajEdytujKlienta.Height = 301;
                            CzySzczegoloweDane.IsChecked = false;
                            VisbileSzczegoloweDane(Visibility.Hidden);
                            txbMiejscowosc.Text = string.Empty;
                            txbUlica.Text = string.Empty;
                            txbnrDomu.Text = string.Empty;
                            cbWojewodztwo.SelectedValue = string.Empty;
                            txbPowiat.Text = string.Empty;
                            txbGmina.Text = string.Empty;
                            txbKodPocztowy.Text = string.Empty;
                        }
                        txbImie.Text = _edytowanyKlient.imie;
                        txbNazwisko.Text = _edytowanyKlient.nazwisko;
                        txbLogin.Text = _edytowanyKlient.login;
                        txbHaslo.Text = _edytowanyKlient.haslo;
                        txbNIP.Text = _edytowanyKlient.nip;
                    }
                }
            }
            catch(Exception ex)
            {
              new LogDB("Błąd podczas połączenia z bazą danych " + ex.ToString());
              new MessageBoxSystemOrders("Błąd podczas połączenia z bazą danych. " + Environment.NewLine+ "Sprawdz połączenie z bazą danych. ",true);
            }
        
        }
        public void RefreshDataSource()
        {
            dataGridKlienci.ItemsSource = null;
            DataSourceKlienci();
        }
        public void DataSourceKlienci()
        {
            try
            {
                using (systemOrdersEntities session = new systemOrdersEntities())
                {
                    dataGridKlienci.ItemsSource = (from item in session.Kliencis select item).ToList();
                }
            
            }
            catch(Exception ex)
            {
                new LogDB("Błąd podczas połączenia z bazą danych " + ex.ToString());
                new MessageBoxSystemOrders("Błąd podczas połączenia z bazą danych. " + Environment.NewLine +"Sprawdz połączenie z bazą danych. ",true);
            }
           
        }

     public void VisbileSzczegoloweDane(Visibility widocznosc)
        {
            lmiejscowosc.Visibility = widocznosc;
            txbMiejscowosc.Visibility = widocznosc;
            lulica.Visibility = widocznosc;
            txbUlica.Visibility = widocznosc;
            lnrdomu.Visibility = widocznosc;
            txbnrDomu.Visibility = widocznosc;
            lwojedzodztwo.Visibility = widocznosc;
            cbWojewodztwo.Visibility = widocznosc;
            lpowiat.Visibility = widocznosc;
            txbPowiat.Visibility = widocznosc;
            lgmina.Visibility = widocznosc;
            txbGmina.Visibility = widocznosc;
            lkodpocztowy.Visibility = widocznosc;
            txbKodPocztowy.Visibility = widocznosc;
        }
     public void ClearTextboxesPanelKlienta()
     {
         txbImie.Text = string.Empty;
         txbNazwisko.Text = string.Empty;
         txbLogin.Text = string.Empty;
         txbHaslo.Text = string.Empty;
         txbNIP.Text = string.Empty;
         txbMiejscowosc.Text = string.Empty;
         txbUlica.Text = string.Empty;
         txbnrDomu.Text = string.Empty;
         cbWojewodztwo.SelectedItem = string.Empty;
         txbPowiat.Text = string.Empty;
         txbGmina.Text = string.Empty;
         txbKodPocztowy.Text = string.Empty;
     }
        private void CzySzczegoloweDane_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (CzySzczegoloweDane.IsChecked == true)
            {
                gbDodajEdytujKlienta.Height = 601;
                VisbileSzczegoloweDane(Visibility.Visible); 
            }
            else
            {
                gbDodajEdytujKlienta.Height = 301;
                VisbileSzczegoloweDane(Visibility.Hidden);
            }
        }
    }
}
