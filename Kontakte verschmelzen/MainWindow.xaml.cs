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
            
        }

        private void Datei_Einlesen()
        {
            System.IO.StreamReader file = new StreamReader(@"E:\Eingang\dr.alles.vcf");

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
                    #region case N
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

                    #region case FN
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

                    #region case TEL
                    case "TEL":
                        Kontakt.TelNumber.Add(new Itm(Untertyp, Wert));
                        break;
                    #endregion

                    #region case EMail
                    case "EMAIL":
                        Kontakt.EMail.Add(new Itm(Untertyp, Wert));
                        break;
                    #endregion

                    #region Adr
                    case "ADR":
                        Kontakt.Adr.Add(new Itm(Untertyp, Wert));
                        break;
                    #endregion

                    #region Tit
                    case "TITLE":
                        Kontakt.Titel = Wert;
                        break;
                    #endregion

                    #region Web
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

                    #region BDay  
                    case "BDay":
                        Kontakt.Birthday = Wert;
                        break;
                        #endregion
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