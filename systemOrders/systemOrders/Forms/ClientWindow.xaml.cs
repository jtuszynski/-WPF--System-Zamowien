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
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {

        private SettingsForm _settingsForm;
        public ClientWindow(Klienci konto)
        {
            InitializeComponent();
             try
            {
                _settingsForm = new SettingsForm();
                this.Background = _settingsForm.SettingsBackroungForm();
                this.contentControlUser.Content = new UserPanel(konto);

            }
            catch(Exception ex)
            {
                new MessageBoxSystemOrders("Błąd podczas ustawiania tła aplikacji. ", true);
                new LogDB("Błąd podczas ustawiania tła aplikacji -" + ex.ToString());
            }
        }

     


        
    }
}
