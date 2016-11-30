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
using System.Windows.Shapes;
using Common.Settings;
using systemOrders.dbModule;
namespace systemOrders.Forms
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private SystemOrdersApp _systemOrdersApp;
        private SettingsForm _settingsForm;
        public AdminWindow()
        {
            InitializeComponent();
        }
        public AdminWindow(Firma konto)
        {
            InitializeComponent();
            try
            {
                _settingsForm = new SettingsForm();
                _systemOrdersApp = new SystemOrdersApp(konto);
                this.Background = _settingsForm.SettingsBackroungForm();

            }
            catch(Exception ex)
            {
                new MessageBoxSystemOrders("Błąd podczas ustawiania tła aplikacji. ", true);
                new LogDB("Błąd podczas ustawiania tła aplikacji -" + ex.ToString());
            }
        }

        private void MenuItemZardzanieTowarami_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new ZarzadzanieTowarami(_systemOrdersApp);
        }

        private void MenuItemZarzadzanieKlientami_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new ZarzadzanieKlientami(_systemOrdersApp);
        }

        private void MenuItem_ZarzadzanieZamowieniami_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new ZarzadzanieZamowieniami(_systemOrdersApp);
        }

        private void MenuItemKontakt_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new Kontakt();//_systemOrdersApp);
        }

        private void MenuItemWyloguj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItemPrzegladZamowien_Click(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new PrzegladZamowien(_systemOrdersApp);
        }

    }
}
