namespace KAHA.TravelBot.NETCoreReactApp.Models
{
    public class CountryModel
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public int Population { get; set; }
        public double? Latitude { get; set; } // Use double? for nullable values
        public double? Longitude { get; set; } // Use double? for nullable values
    }
}