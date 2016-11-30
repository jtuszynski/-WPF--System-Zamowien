using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
namespace Common.Settings
{
    public class MessageBoxSystemOrders
    {
        private List<string> _errors;
        private string errorsShow;

        public MessageBoxSystemOrders(List<string> errors)
        {
            this._errors = errors;
        }
        public MessageBoxSystemOrders(string komunikat, bool error)
        {
            if(error)
                this.MessageBoxOneError(komunikat);
            else
                this.MessageBoxInformationSystemOrders(komunikat);
        }

        public MessageBoxResult MessageBoxErrorsShow(List<string> errors)
        {        
            foreach(string error in errors)
            {
                errorsShow += error + Environment.NewLine;
            }
            return System.Windows.MessageBox.Show(errorsShow, "Lista błędów walidacji:", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public MessageBoxResult MessageBoxOneError(string error)
        {
            return System.Windows.MessageBox.Show(error, "Błąd w systemie @tuszcom", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public MessageBoxResult MessageBoxInformationSystemOrders(string information)
        {
            return System.Windows.MessageBox.Show(information, "Komunikat systemu @tuszcom", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
    }
}
