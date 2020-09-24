using CouchBaseStandardLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace SampleCouchbaseAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AirlineController : ControllerBase
    {
        private readonly ILogger<AirlineController> _logger;
        private readonly ITravelBucketRepository _travelBucketRepository;

        public AirlineController(ILogger<AirlineController> logger, ITravelBucketRepository travelBucketRepository)
        {
            _logger = logger;
            _travelBucketRepository = travelBucketRepository;
        }

        [HttpGet(Name = "getall")]
        public IEnumerable<Airline> Get()
        {
            try
            {
                var result = _travelBucketRepository.GetAirlines();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("{id}", Name = "get")]
        public Airline Get(string id)
        {
            try
            {
                var result = _travelBucketRepository.GetById(id);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost(Name = "create")]
        public IActionResult Create(Airline airline)
        {
            try
            {
                _travelBucketRepository.Insert(airline);
                return Ok("success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}", Name = "update")]
        public IActionResult Update(string id, Airline airline)
        {
            try
            {
                _travelBucketRepository.Update(id, airline);
                return Ok("success");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}", Name = "delete")]
        public IActionResult Delete(string id)
        {
            try
            {
                _travelBucketRepository.Delete(id);
                return Ok("deleted");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
