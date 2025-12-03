using System;

namespace PublicHolidayTracker
{
    public class Holiday
    {
        // JSON'dan gelen 'date' alanı
        public string date { get; set; }

        // JSON'dan gelen 'localName' alanı
        public string localName { get; set; }

        // JSON'dan gelen 'name' alanı
        public string name { get; set; }

        // JSON'dan gelen 'countryCode' alanı
        public string countryCode { get; set; }

        // JSON'dan gelen 'fixed' alanı. C#'ta 'fixed' bir anahtar kelime olduğu için başına '@' koyuyoruz.
        public bool @fixed { get; set; }

        // JSON'dan gelen 'global' alanı
        public bool global { get; set; }
    }
}
