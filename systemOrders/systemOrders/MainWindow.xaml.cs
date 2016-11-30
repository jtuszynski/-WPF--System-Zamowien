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
using System.IO;
using System.Reflection;
using System.Windows.Interop;
using Common.Settings;
using systemOrders;
using systemOrders.Forms;
using systemOrders.dbModule;

namespace SystemOrders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SettingsForm _settingsForm;
        public MainWindow()
        {
            System.Diagnostics.Process[] tabProc = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)); //sprawdzza, czy istnieje inna instancja
            if (tabProc.Length > 1)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else
            {
                InitializeComponent();
                try
                {
                    _settingsForm = new SettingsForm();
                    this.Background = _settingsForm.SettingsBackroungForm();
                }
                catch
                {
                    MessageBox.Show("Błąd załadowania tła aplikacji. ");
                }

            }
        }

        /// <summary>
        ///  Zdarzenie logowania do systemu
        /// </summary>
        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            LogIn login = new LogIn();
            try
            {
                object konto = login.Zaloguj(tbLogin.Text, tbPassword.Password);  
                if (konto != null)
                {
                    if (konto is Firma)
                    {
                        AdminWindow win2 = new AdminWindow((Firma)konto);
                        win2.Show();
                        this.Close();
                    }
                    else if (konto is Klienci)
                    {
                        ClientWindow clientWindow = new ClientWindow((Klienci)konto);
                        clientWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        new MessageBoxSystemOrders("Konto użytkownika nie istnieje. ", false);
                    }
                }
                else
                {
                    new MessageBoxSystemOrders("Konto użytkownika nie istnieje. ", false);
                }
            }
            catch(Exception ex)
            {
                new LogDB("Błąd podczas logowania do systemu. " + ex.ToString());
                new MessageBoxSystemOrders("Błąd podczas logowania do systemu. ", true);
            }
        }
    }
}
