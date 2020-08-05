using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MingweiSamuel.Camille;
using MingweiSamuel.Camille.Enums;
using riotApi.Configs;

namespace riotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummonerBaseController : ControllerBase
    {
        public string riotApiKey = RiotApiConfiguration.RiotApiKey;
 
        [HttpGet("GetBySummonerName/{name}/{region}")]
        public async Task<IActionResult> GetBySummonerName(string name, string region)
        {
            Region regionEnum = Region.Get(region);
            var riotApi = RiotApi.NewInstance(riotApiKey);
            var dupa = await riotApi.SummonerV4.GetBySummonerNameAsync(regionEnum, name);
            return Ok(dupa);
        }

        [HttpGet("GetBySummonerName2")]    
        public async Task<IActionResult> GetBySummonerName2(Dtos.GetBySummonerName getBySummoner)
        {
            Region regionEnum = Region.Get(getBySummoner.Region);
            var riotApi = RiotApi.NewInstance(riotApiKey);
            var dupa = await riotApi.SummonerV4.GetBySummonerNameAsync(regionEnum, getBySummoner.SummonerName);
            return Ok(dupa);
        }

        [HttpGet("GetByAccountId/{name}")]
        public async Task<IActionResult> GetByAccountId(string name)
        {
            var riotApi = RiotApi.NewInstance(riotApiKey);
            var dupa = riotApi.SummonerV4.GetByAccountId(Region.NA, name);
            return Ok(dupa);
        }
    }
}