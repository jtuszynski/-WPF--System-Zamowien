using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using systemOrders;
using systemOrders.dbModule;

namespace Common.Settings
{
    public class SystemOrdersApp
    {
        #region Private Members
        private Firma _konto;
        #endregion

        #region Constructors and public methods

        public SystemOrdersApp(Firma konto)
        {
            this.Konto = konto;
        }
        public Firma Konto
        {
            get { return _konto; }
            set { _konto = value; }
        }
        public String RandomSixDigitalNumber()
        {
            Random generator = new Random();
            return generator.Next(0, 1000000).ToString("D6");
        }
        public void SerializacjaZamowieniaXML(List<Zamowienie> zamowienia)
        {
            List<ZamowienieDB> zamowieniaXML = XmlMappingObjects(zamowienia);
            XmlSerializer xsSubmit = new XmlSerializer(typeof(List<ZamowienieDB>));
            StringWriter sww = new StringWriter();
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, zamowieniaXML);
                var xml = sww.ToString(); // xml

                if(xml.Length>0)
                {
                    // Create the XmlDocument.
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml); //Your string here

                    // Save the document to a file and auto-indent the output.
                    SaveFileDialog saveXMLfile = new SaveFileDialog();
                    saveXMLfile.FileName = "Zamowienie" + DateTime.Now.ToShortDateString() + RandomSixDigitalNumber() ; // Default file name
                    saveXMLfile.DefaultExt = ".xml"; // Default file extension
                    saveXMLfile.Filter = "Text documents (.xml)|*.xml"; // Filter files by extension
                    if (saveXMLfile.ShowDialog() == true)
                     {
                         XmlTextWriter writerXML = new XmlTextWriter(saveXMLfile.FileName, null);
                         writerXML.Formatting = Formatting.Indented;
                         doc.Save(writerXML);
                         new MessageBoxSystemOrders("Plik " + saveXMLfile.FileName + " (XML) został utworzony. ",false);
                     }
                    else
                    {
                        new MessageBoxSystemOrders("Nie udało się stworzyć pliku XML zamówień. ",true);
                    }
                    
                }
                else
                {
                     new MessageBoxSystemOrders("Nie udało się stworzyć pliku XML zamówień. ",true);
                }
            }
        }
        public List<ZamowienieDB> XmlMappingObjects(List<Zamowienie> zamowienia)
        {
            try
            {
                if (zamowienia != null && zamowienia.Count > 0)
                {
                    List<ZamowienieDB> _listZamowien = new List<ZamowienieDB>();
                    foreach (Zamowienie item in zamowienia)
                    {
                        ZamowienieDB ZamowienieDB = new ZamowienieDB();
                        ZamowienieDB.id_zamowienia = item.id_zamowienia;
                        ZamowienieDB.id_firmy = item.id_firmy;
                        ZamowienieDB.id_klienta = item.id_klienta;
                        ZamowienieDB.id_statusu = item.id_statusu;
                        ZamowienieDB.data_otrzymania = item.data_otrzymania;
                        ZamowienieDB.data_otrzymania = item.data_zamowienia;
                        using (systemOrdersEntities session = new systemOrdersEntities())
                        {
                            List<ZamowioneTowary> zamowioneTowary = (from itemTow in session.ZamowioneTowaries
                                                                     where itemTow.id_zamowienia == ZamowienieDB.id_zamowienia
                                                                     select itemTow).ToList();
                            if (zamowioneTowary != null && zamowioneTowary.Count > 0)
                            {
                                ZamowienieDB.zamowione_towary = new List<ZamowionyTowarDB>();
                                foreach (ZamowioneTowary itemZamTow in zamowioneTowary)
                                {
                                    ZamowionyTowarDB zamTowDB = new ZamowionyTowarDB();
                                    zamTowDB.Id_towaru = itemZamTow.id_towaru;
                                    zamTowDB.Ilosc = itemZamTow.ilosc;
                                    zamTowDB.Kwota = itemZamTow.kwota;
                                    zamTowDB.Nazwa_towaru = itemZamTow.Towar.nazwa;
                                    ZamowienieDB.zamowione_towary.Add(zamTowDB);
                                }
                            }
                        }
                        _listZamowien.Add(ZamowienieDB);
                    }
                    return _listZamowien;
                }
                else
                    return null;
            }
            catch(Exception ex)
            {
                new MessageBoxSystemOrders("Błąd podczas mapowania obiektów. ", true);
                new LogDB("Błąd podczas mapowania obiektów. " + ex.ToString());
                return null;
            }
           
        }
        #endregion
    }
}
