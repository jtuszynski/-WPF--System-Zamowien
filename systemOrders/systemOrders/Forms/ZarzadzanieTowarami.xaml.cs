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
    /// Interaction logic for ZarzadzanieTowarami.xaml
    /// </summary>
    public partial class ZarzadzanieTowarami : UserControl
    {
        private SystemOrdersApp _systemOrdersFormTow;
        private EnumEditCreate _CreateEnum;
        private Towar _edytowanyTowar;
        public ZarzadzanieTowarami(SystemOrdersApp systemOrders)
        {
            InitializeComponent();
            _systemOrdersFormTow = systemOrders;
            _CreateEnum = EnumEditCreate.create;
            DataSourceTowary();

        }

   
        public List<string> WalidacjaDodajEdytujTowar()
        {
            try
            {
                List<string> ListaWalidacji = new List<string>();

                // walidacja nazwy towaru
                if (txbNazwa.Text.Length > 50) { ListaWalidacji.Add("-Pole nazwa towaru nie może przekraczać więcej niż 50 znaków."); }
                if (txbNazwa.Text.Length == 0) { ListaWalidacji.Add("-Pole nazwa towaru nie może być pusta."); }
                //if(txbNazwa.Text.Length>0 && txbNazwa.Text.Length<50)
                //  if (!Common.Settings.RegexExpression.regexNazwaTowar.IsMatch(txbNazwa.Text)) { ListaWalidacji.Add("-Nazwa towaru nie może zawierać liczb."); }

                //walidacja ceny towaru
                if (txbCena.Text == string.Empty) { ListaWalidacji.Add("-Pole cena powinno zostać wypełnione wartością."); }
                try
                {
                    if (txbCena.Text.Length > 0 && txbCena.Text.Length < 21)
                    {
                        decimal decValue;
                        decimal number;
                        if (txbCena.Text.Contains(","))
                        {
                            var ci = CultureInfo.InvariantCulture.Clone() as CultureInfo;
                            ci.NumberFormat.NumberDecimalSeparator = ",";
                            number = decimal.Parse(txbCena.Text, ci); // 1.1
                        }
                        else
                        {
                            var ci = CultureInfo.InvariantCulture.Clone() as CultureInfo;
                            ci.NumberFormat.NumberDecimalSeparator = ".";
                            number = decimal.Parse(txbCena.Text, ci); // 1.1
                        }

                        if (!decimal.TryParse(number.ToString(), out decValue)) { ListaWalidacji.Add("-Wartość pola 'cena' jest niezgodna z formatem liczbowym."); }
                    }
                }
                catch
                {
                    ListaWalidacji.Add("-Wartość pola 'cena' jest niezgodna z formatem liczbowym." + Environment.NewLine + "Wartość liczbowa powinna być napisana ciągiem, bez pustych znaków.");
                }


                // if (!Common.Settings.RegexExpression.regexCenaTowar.IsMatch(txbCena.Text)) { ListaWalidacji.Add("-Pole cena nie może zawierać liter."); }
                if (txbCena.Text.Length > 21) { ListaWalidacji.Add("-Wartość pola 'cena' jest za długa."); };

                //walidacja opisu towaru
                if (txbOpis.Text == string.Empty) { ListaWalidacji.Add("-Pole opis nie może być puste"); };
                if (txbOpis.Text.Length > 50) { ListaWalidacji.Add("-Pole opis nie może przekraczać więcej niż 50 znaków."); }

                return ListaWalidacji;
            }
            catch(Exception ex)
            {
                new MessageBoxSystemOrders("Błąd poczas walidacji danych klienta. ", true);
                new LogDB("Błąd poczas walidacji danych klienta.  " + ex.ToString());
                return null;
            }
        }
        private void btDodajTowar_Click(object sender, RoutedEventArgs e)
        {
            List<string> errors = WalidacjaDodajEdytujTowar();
            if (errors != null && errors.Count > 0)
            {
                MessageBoxSystemOrders errorsMessage = new MessageBoxSystemOrders(errors);
                MessageBoxResult result = errorsMessage.MessageBoxErrorsShow(errors);
            }
            else
            {
               if(_CreateEnum == EnumEditCreate.create)
               {
                   try
                   {

                       using (systemOrdersEntities session = new systemOrdersEntities()) // TODO metoda Insert do bazy
                       {
                           decimal cenaTowaru;
                           string cena;
                           if (txbCena.Text.Contains("."))
                           {
                               cena = txbCena.Text.Replace(".", ",");
                           }
                           else
                               cena = txbCena.Text;
                           decimal.TryParse(cena, out cenaTowaru);
                           Towar nowyTowar = new Towar();
                           nowyTowar.id_firmy = _systemOrdersFormTow.Konto.id_firmy;
                           nowyTowar.nazwa = txbNazwa.Text;
                           nowyTowar.cena = cenaTowaru;
                           nowyTowar.opis = txbOpis.Text;
                           session.Towars.Add(nowyTowar);
                           session.SaveChanges();
                           RefreshDataSource();
                       }
                   }      
                   catch(Exception ex)
                   {
                       new MessageBoxSystemOrders("Błąd podczas tworzenia nowego towaru. ", true);
                       new LogDB("Błąd podczas tworzenia nowego towaru. " + ex.ToString());
                   }
               }
               else if(_CreateEnum == EnumEditCreate.edit)
               {
                   try
                   {
                       using (systemOrdersEntities session = new systemOrdersEntities())   //TODO metoda Update do Bazy
                       {
                           decimal cenaTowaru;
                           string cena;
                           if (txbCena.Text.Contains("."))
                           {
                               cena = txbCena.Text.Replace(".", ",");
                           }
                           else
                               cena = txbCena.Text;
                           decimal.TryParse(cena, out cenaTowaru);

                           Towar edytowanyTowar = (from x in session.Towars
                                                   where x.id_towaru == _edytowanyTowar.id_towaru
                                                   select x).First();
                           edytowanyTowar.nazwa = txbNazwa.Text;
                           edytowanyTowar.cena = cenaTowaru;
                           edytowanyTowar.opis = txbOpis.Text;
                           session.SaveChanges();
                           RefreshDataSource();
                       }
                   }
                   catch(Exception ex)
                   {
                       new MessageBoxSystemOrders("Błąd podczas edycji towaru. ", true);
                       new LogDB("Błąd podczas edycji towaru. " + ex.ToString());
                   }
                 
               }
               ClearTextboxesPanelTowar();
               panelTowaru.IsEnabled = false;
            }
        }
        public void ClearTextboxesPanelTowar()
        {
            txbCena.Text = string.Empty;
            txbNazwa.Text = string.Empty;
            txbOpis.Text = string.Empty;
        }
        private void DodajTowar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _CreateEnum = EnumEditCreate.create;
                panelTowaru.IsEnabled = true;
                SetUserControlToEditOrCreateTowar();
            }
            catch(Exception ex)
            {
                new MessageBoxSystemOrders("Błąd podczas aktywacji panelu dodawania towaru.", true);
                new LogDB("Błąd podczas aktywacji panelu dodawania towaru." + ex.ToString());
            }

            
        }
        private void EdytujTowar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridTowary.SelectedItem != null)
                {
                    _CreateEnum = EnumEditCreate.edit;
                    panelTowaru.IsEnabled = true;
                    SetUserControlToEditOrCreateTowar();

                }
                else
                {
                    new MessageBoxSystemOrders("Wybierz towar do edycji.", false);
                }
            }
            catch(Exception ex)
            {
                new MessageBoxSystemOrders("Błąd podczas aktywacji panelu edycji towaru.", true);
                new LogDB("Błąd podczas aktywacji panelu edycji towaru." + ex.ToString());
            }
        

        }
        public void SetUserControlToEditOrCreateTowar()
        {
            if (_CreateEnum == EnumEditCreate.create)
            {
                panelTowaru.Header = "Panel dodawania towaru";
                btDodajTowar.Content = "Dodaj towar";
            }
            else if (_CreateEnum == EnumEditCreate.edit)
            {
                panelTowaru.Header = "Panel edytowania towaru";
                btDodajTowar.Content = "Edytuj towar";
                
                if (dataGridTowary.SelectedItem is Towar)
                {
                    _edytowanyTowar = (Towar)dataGridTowary.SelectedItem;
                    txbCena.Text = _edytowanyTowar.cena.ToString();
                    txbNazwa.Text = _edytowanyTowar.nazwa.ToString();
                    txbOpis.Text = _edytowanyTowar.opis.ToString();
                }
            }

        }
        private void UsunTowar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridTowary.SelectedItem != null)
                {
                    if (dataGridTowary.SelectedItem is Towar)
                    {
                        using (systemOrdersEntities session = new systemOrdersEntities())
                        {
                            Towar zaznaczonyTowar = (Towar)dataGridTowary.SelectedItem;
                            MessageBoxResult dialogResult = MessageBox.Show("Czy na pewno chcesz usunąć towar - " + zaznaczonyTowar.nazwa + " ?", "Komunikat systemu zamówień @tuszcom", MessageBoxButton.YesNo);

                            if (dialogResult == MessageBoxResult.Yes)
                            {
                                Towar usuwanyTowar = (from x in session.Towars
                                                      where x.id_towaru == zaznaczonyTowar.id_towaru
                                                      select x).First();
                                session.Towars.Remove(usuwanyTowar);
                                session.SaveChanges();
                                new MessageBoxSystemOrders("Towar został usunięty z systemu zamówień @tuszcom.", false);
                                RefreshDataSource();
                            }

                        }
                    }
                }
                else
                {
                    new MessageBoxSystemOrders("Wybierz towar do usunięcia.", false);
                }
            }
            catch(Exception ex)
            {
                new MessageBoxSystemOrders("Błąd podczas usuwania towaru.", true);
                new LogDB("Błąd podczas usuwania towaru." + ex.ToString());
            }
           
           
        }
        public void DataSourceTowary()
        {
            try
            {
                using (systemOrdersEntities session = new systemOrdersEntities())
                {
                    dataGridTowary.ItemsSource = (from item in session.Towars select item).ToList();
                }

            }
            catch(Exception ex)
            {
                new MessageBoxSystemOrders("Błąd podczas połączenia z bazą danych. " + Environment.NewLine +"Sprawdz połączenie z bazą danych. ",true);
                new LogDB("Błąd podczas połączenia z bazą danych. " + ex.ToString());
            }
        }
        public void RefreshDataSource()
        {
            dataGridTowary.ItemsSource = null;
            DataSourceTowary();
        }
    }
}
