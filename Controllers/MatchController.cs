using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MingweiSamuel.Camille;
using MingweiSamuel.Camille.Enums;
using riotApi.Configs;

namespace ApiForRiot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        public string riotApiKey = RiotApiConfiguration.RiotApiKey;

        [HttpGet("GetSummonerMatch/{name}/{region}")]
        public async Task<IActionResult> GetSummonerMatch(string name, string region)
        {
            Region regionEnum = Region.Get(region);
            var riotApi = RiotApi.NewInstance(riotApiKey);
            var summonerData = await riotApi.SummonerV4.GetBySummonerNameAsync(regionEnum, name);
            var dupa = await riotApi.MatchV4.GetMatchlistAsync(regionEnum,summonerData.AccountId);
            return Ok(dupa);
        }
    }
}