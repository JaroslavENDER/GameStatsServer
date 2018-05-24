using Microsoft.AspNetCore.Mvc;

namespace GameStatsServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Servers")]
    public class ServersController : Controller
    {
        //GET api/servers/info
        [HttpGet("info")]
        public string AllInfo()
        {
            return "all info";
        }

        //GET api/servers/{endpoint}/info
        [HttpGet("{endpoint}/info")]
        public object Info(string endpoint)
        {
            return new { Info = "get info", Endpoint = endpoint };
        }

        //PUT api/servers/{endpoint}/info
        [HttpPut("{endpoint}/info")]
        public void Info(string endpoint, [FromBody]string value)
        {

        }

        //GET api/servers/{endpoint}/stats
        [HttpGet("{endpoint}/stats")]
        public string Stats(string endpoint)
        {
            return "stats";
        }

        //GET api/servers/{endpoint}/matches/{timestamp}
        [HttpGet("{endpoint}/matches/{timestamp}")]
        public object Matches(string endpoint, string timestamp)
        {
            return new { Info = "get matches", Endpoint = endpoint, Timestamp = timestamp };
        }

        //PUT api/servers/{endpoint}/matches/{timestamp}
        [HttpPut("{endpoint}/matches/{timestamp}")]
        public void Matches(string endpoint, string timestamp, [FromBody]string value)
        {

        }
    }
}
