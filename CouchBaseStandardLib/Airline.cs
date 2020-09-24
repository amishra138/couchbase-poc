using System.ComponentModel.DataAnnotations;

namespace CouchBaseStandardLib
{
    public class Airline
    {
        [Key]
        public string Id { get; set; }
        public string Callsign { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Iata { get; set; }
    }
}
