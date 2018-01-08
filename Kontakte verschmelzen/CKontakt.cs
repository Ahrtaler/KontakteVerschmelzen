using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Kontakte_verschmelzen
{
    class CKontakt
    {
        public string version = "";
        public _Name Name = new _Name();
        public string FullName = "";
        public Phonetic_Name PhoneticName = new Phonetic_Name();
        public List<Itm> TelNumber = new List<Itm>();
        public List<Itm> EMail = new List<Itm>();
        public List<Itm> Adr = new List<Itm>();
        public string Organisation = "";
        public string Titel = "";
        public List<string> Website = new List<string>();
        public string Note = "";
        public string Birthday = "";
        public string img = "";
        public List<string> ev = new List<string>();
        public string nickname = "";
        public List<Itm> MessegersAndOthers = new List<Itm>();

        public System.Drawing.Image getImage()
        {
            byte[] buffer = Convert.FromBase64String(img);

            if (buffer != null)
            {
                ImageConverter ic = new ImageConverter();
                return ic.ConvertFrom(buffer) as Image;
            }
            else
                return null;
        }

    }

    class _Name
    {
        public string Praefix = "";
        public string VorName = "";
        public string ZweitName = "";
        public string NachName = "";
        public string Suffix = "";
    }

    class Phonetic_Name
    {
        public string FirstName = "";
        public string MiddleName = "";
        public string LastName = "";
    }

    class Itm
    {
        public string Name = "";
        public string Wert = "";

        public Itm(string name, string wert)
        {
            Name = name;
            Wert = wert;
        }
    }
}
