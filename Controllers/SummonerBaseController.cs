using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MingweiSamuel.Camille;
using MingweiSamuel.Camille.Enums;

namespace riotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummonerBaseController : ControllerBase
    {
        private readonly IConfiguration _config;
        private string riotApiKey = "";

        public SummonerBaseController(IConfiguration config)
        {
            _config = config;
            riotApiKey = _config.GetSection("AppSettings:Token").Value;
        }

        [HttpGet("GetBySummonerName/{name}/{region}")]
        public async Task<IActionResult> GetBySummonerName(string name, string region)
        {
            Region regionEnum = Region.Get(region);
            var riotApi = RiotApi.NewInstance(riotApiKey);
            try
            {
                var result = await riotApi.SummonerV4.GetBySummonerNameAsync(regionEnum, name);
                return Ok(result);
            }
            catch (System.Exception e)
            {
                var errorMessage = "Invalid riot api token.";
                return StatusCode(403, errorMessage);
            }
        }

        [HttpGet("GetBySummonerName2")]
        public async Task<IActionResult> GetBySummonerName2(Dtos.GetBySummonerName getBySummoner)
        {
            try
            {
                Region regionEnum = Region.Get(getBySummoner.Region);
                var riotApi = RiotApi.NewInstance(riotApiKey);
                var result = await riotApi.SummonerV4.GetBySummonerNameAsync(regionEnum, getBySummoner.SummonerName);
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("GetByAccountId/{name}")]
        public async Task<IActionResult> GetByAccountId(string name)
        {
            try
            {
                var riotApi = RiotApi.NewInstance(riotApiKey);
                var result = await riotApi.SummonerV4.GetByAccountIdAsync(Region.NA, name);
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}