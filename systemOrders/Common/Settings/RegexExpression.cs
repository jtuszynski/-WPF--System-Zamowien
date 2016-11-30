using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Settings
{
   public static class RegexExpression
   {
        #region regexy Klient
       public static Regex regexImie = new Regex(@"^([a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ\s]{2,50})$");
        public static Regex regexNazwisko = new Regex(@"^([a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ\s]{2,50})$");
        public static Regex regexNip = new Regex(@"^(\(d{3}-\d{3}-\d{2}-\d{2})|(d{3}-\d{2}-\d{2}-\d{3})$");
        public static Regex regexPowiat = new Regex(@"^([a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ\s]{2,50})$");
        public static Regex regexGmina = new Regex(@"^([a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ\s]{2,50})$");
        public static Regex regexMiejscowosc = new Regex(@"^([a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ\s]{2,50})$");
       // public static Regex regexUlica = new Regex("^([a-zA-Z0-9ąćęłńóśźżĄĆĘŁŃÓŚŹŻ\\s\\.\\-]{3,50})$");
        public static Regex regexTelefon = new Regex("^([0-9]{9})$");
        public static Regex regexNrDomu = new Regex("^([a-zA-Z0-9]{1,8})$");
        public static Regex regexKodPocztowy = new Regex(@"^[0-9]{2}\-[0-9]{3}$");
        public static Regex regexLogin = new Regex("^([a-zA-Z0-9]{4,12})$");
        #endregion 
   }
}
