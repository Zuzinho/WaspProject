using System;


namespace WaspProject.Parser.Kinoafisha
{
    class KinoafishaSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://msk.kinoafisha.info/cinema/";
        public string Prefix { get; set; } = "page{CurrentID}";
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
