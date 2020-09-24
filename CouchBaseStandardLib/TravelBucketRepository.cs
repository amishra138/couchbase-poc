using Couchbase;
using Couchbase.Linq;
using System.Collections.Generic;
using System.Linq;

namespace CouchBaseStandardLib
{
    public class TravelBucketRepository : ITravelBucketRepository
    {
        private readonly IBucketContext _bucketContext;

        public TravelBucketRepository(string bucketName)
        {
            _bucketContext = new BucketContext(ClusterHelper.GetBucket(bucketName));
        }

        public void Delete(string id)
        {
            var airline = _bucketContext
                            .Query<Airline>()
                            .FirstOrDefault(x => N1QlFunctions.Meta(x).Id == id);
            if (airline == null)
                return;

            _bucketContext.Remove(airline);
        }

        public IEnumerable<Airline> GetAirlines()
        {
            var query = _bucketContext.Query<Airline>()
                .Where(x => x.Country == "United Kingdom")
                .Select(x => new Airline()
                {
                    Id = N1QlFunctions.Meta(x).Id,
                    Name = x.Name,
                    Callsign = x.Callsign,
                    Country = x.Country,
                    Iata = x.Iata
                })
                .Take(10);

            return query;
        }

        public Airline GetById(string id)
        {
            var airline = _bucketContext
                .Query<Airline>()
                .Select(x => new Airline()
                {
                    Id = N1QlFunctions.Meta(x).Id,
                    Name = x.Name,
                    Callsign = x.Callsign,
                    Country = x.Country,
                    Iata = x.Iata
                })
                .FirstOrDefault(x => x.Id == id);

            return airline;

        }

        public void Insert(Airline airline)
        {
            _bucketContext.Save(airline);
        }

        public void Update(string id, Airline airline)
        {
            airline.Id = id;
            _bucketContext.Save(airline);
        }
    }
}
