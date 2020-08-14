using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPlaces2020.Client.Models
{
    public class Place
    {

        public long Id { get; set; }

        public enum Type_PLA { Bar, NightClub, ConcertHall, StudentCircle }  // enum required

        public string Nom_PLA { get; set; }        // Max 50 - required

        public string Vat_PLA { get; set; }        // Validé par API externe n° format - required

        public string EmailPro_PLA { get; set; }        // required - 255 max - az10

        public string FreeText_PLA { get; set; }        // max 2000

        public string Logo_PLA { get; set; }        // Upload Image max 1MB

        public int AddrPostalC_PLA { get; set; }        // required - 20max

        public string AddrCity_PLA { get; set; }        // required - 100max

        public string AddrCountry_PLA { get; set; }        // required - listederoul

        public string AddrStreet_PLA { get; set; }        // required - 100max

        public string AddrPostBox_PLA { get; set; }        // required - 20max

        public string Phone_PLA { get; set; }        // Validé par LibPhoneNumber - 25max fixe/mobile

        public string EmailPlace_PLA { get; set; }        // 255max

        public string Site_PLA { get; set; }        //  url

        public string Instagram_PLA { get; set; }        // url

        public string Facebook_PLA { get; set; }        // url

        public string Linkedin_PLA { get; set; }         // url

        public string Hours_PLA { get; set; }        // Visualiser

        public string Picture_PLA { get; set; }        // max 5 et max 3MB
    }
}
