using System.Collections.Generic;

namespace CouchBaseStandardLib
{
    public interface ITravelBucketRepository
    {
        IEnumerable<Airline> GetAirlines();

        Airline GetById(string id);

        void Insert(Airline airline);

        void Update(string id, Airline airline);

        void Delete(string id);
    }
}
