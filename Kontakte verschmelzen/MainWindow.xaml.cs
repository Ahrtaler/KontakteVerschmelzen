using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kontakte_verschmelzen
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<CKontakt> Kontakte = new List<CKontakt>();

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            Datei_Einlesen(@"E:\Eingang\dr.alles.vcf");
        }

        private void Datei_Einlesen(string path)
        {
            System.IO.StreamReader file = new StreamReader(path);

            string line;
            CKontakt Kontakt = new CKontakt();

            while ((line = file.ReadLine()) != null)
            {
                if (line == "BEGIN:VCARD")
                {
                    Kontakt = new CKontakt();
                }
                else if (line == "END:VCARD")
                {
                    Kontakte.Add(Kontakt);
                }
                else
                {
                    int i = 0;
                    string Typ = "";
                    string Untertyp = "";
                    string Wert = "";

                    while (line[i] != ':')
                    {
                        Typ += line[i];
                        i++;
                    }

                    i++;

                    Wert = line.Substring(i);

                    if (GetCountofChar(';', Typ) >= 1)
                    {
                        i = 0;
                        while (Typ[i] != ';')
                            i++;

                        Untertyp = Typ.Substring(i + 1);
                        Typ = Typ.Substring(0, i);
                    }

                    switch (Typ)
                    {
                        #region Version
                        case "VERSION":
                            Kontakt.version = Wert;
                            break;
                        #endregion

                        #region Name
                        case "N":
                            i = 0;
                            while (Wert[i] != ';')
                            {
                                Kontakt.Name.NachName += Wert[i];
                                i++;
                            }

                            i++;

                            while (Wert[i] != ';')
                            {
                                Kontakt.Name.VorName += Wert[i];
                                i++;
                            }

                            i++;

                            while (Wert[i] != ';')
                            {
                                Kontakt.Name.ZweitName += Wert[i];
                                i++;
                            }

                            i++;

                            while (Wert[i] != ';')
                            {
                                Kontakt.Name.Praefix += Wert[i];
                                i++;
                            }

                            i++;

                            Kontakt.Name.Suffix += Wert.Substring(i);
                            break;
                        #endregion

                        #region FN
                        case "FN":
                            Kontakt.FullName = Wert;
                            break;
                        #endregion

                        #region Phonetic Name
                        #region case X-PHONETIC-FIRST-NAME
                        case "X-PHONETIC-FIRST-NAME":
                            Kontakt.PhoneticName.FirstName = Wert;
                            break;
                        #endregion

                        #region case X-PHONETIC-MIDDLE-NAME
                        case "X-PHONETIC-MIDDLE-NAME":
                            Kontakt.PhoneticName.MiddleName = Wert;
                            break;
                        #endregion

                        #region case X-PHONETIC-LAST-NAME
                        case "X-PHONETIC-LAST-NAME":
                            Kontakt.PhoneticName.LastName = Wert;
                            break;
                        #endregion
                        #endregion

                        #region Telefon
                        case "TEL":
                            Kontakt.TelNumber.Add(new Itm(Untertyp, Wert));
                            break;
                        #endregion

                        #region EMail
                        case "EMAIL":
                            Kontakt.EMail.Add(new Itm(Untertyp, Wert));
                            break;
                        #endregion

                        #region Adresse
                        case "ADR":
                            Kontakt.Adr.Add(new Itm(Untertyp, Wert));
                            break;
                        #endregion

                        #region Titel
                        case "TITLE":
                            Kontakt.Titel = Wert;
                            break;
                        #endregion

                        #region Organisation
                        case "ORG":
                            Kontakt.Organisation = Wert;
                            break;
                        #endregion

                        #region Websites
                        case "URL":
                            Kontakt.Website.Add(Wert);
                            break;
                        #endregion

                        #region Photo  
                        case "PHOTO":
                            Kontakt.img = Wert;
                            break;
                        #endregion

                        #region Note  
                        case "NOTE":
                            Kontakt.Note = Wert;
                            break;
                        #endregion

                        #region Birthday  
                        case "BDAY":
                            Kontakt.Birthday = Wert;
                            break;
                        #endregion

                        #region Nickname & Event
                        case "X-ANDROID-CUSTOM":
                            if (Wert.Substring(23, 9) == "/nickname")
                                Kontakt.nickname = Wert;
                            else
                                Kontakt.ev.Add(Wert);
                            break;
                        #endregion

                        #region Rest Meist Messenger
                        default:
                            Kontakt.MessegersAndOthers.Add(new Itm(Typ, Wert));
                            break;
                            #endregion
                    }
                }
            }
        }

        private int GetCountofChar(char Char, string String)
        {
            int i = 0;

            foreach (char c in String)
            {
                if (c == Char)
                {
                    i++;
                }
            }

            return i;
        }
    }
}