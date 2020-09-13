using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using ApiForRiot.Dtos;
using Microsoft.AspNetCore.Mvc;
using MingweiSamuel.Camille;
using MingweiSamuel.Camille.Enums;
using MingweiSamuel.Camille.MatchV4;
using riotApi.Configs;

namespace ApiForRiot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        public string riotApiKey = RiotApiConfiguration.RiotApiKey;



        [HttpGet("GetSummonerMatch/{name}/{region}/{matchesNumber}")]
        public async Task<IActionResult> GetSummonerMatch(string name, string region, int matchesNumber)
        {
            try
            {
                Region regionEnum = Region.Get(region);
                var riotApi = RiotApi.NewInstance(riotApiKey);
                var summonerData = await riotApi.SummonerV4.GetBySummonerNameAsync(regionEnum, name);
                Matchlist result = riotApi.MatchV4.GetMatchlist(regionEnum, summonerData.AccountId);
                MatchReference[] matches = result.Matches.Take(matchesNumber).ToArray();
                ArrayList machesToReturn = new ArrayList();
                foreach (var match in matches)
                {
                    MatchOne matchToList = new MatchOne();
                    matchToList.GameId = match.GameId;
                    matchToList.Lane = match.Lane;
                    matchToList.PlatformId = match.PlatformId;
                    matchToList.Queue = match.Queue;
                    matchToList.Role = match.Role;
                    matchToList.Season = match.Season;
                    matchToList.Timestamp = match.Timestamp;
                    matchToList.Champion = match.Champion;
                    matchToList.matchInfo = await GetMatchInfoFromApi(match.GameId, region);

                    Champion champion = (Champion)match.Champion;
                    var championName = ChampionUtils.Name(champion);

                    

                    matchToList.championName = championName;

                    var matchData = riotApi.MatchV4.GetMatch(regionEnum, match.GameId);

                    matchToList.gameDuration = matchData.GameDuration;
                    matchToList.gameMode = matchData.GameMode;

                    var parcipientId = 0;
                    foreach (var participant in matchData.ParticipantIdentities)
                    {
                        if (participant.Player.SummonerName == name)
                        {
                            parcipientId = participant.ParticipantId;
                        }
                    }
                    var teamId = 0;
                    foreach (var participants in matchData.Participants)
                    {
                        if (participants.ParticipantId == parcipientId)
                        {
                            teamId = participants.TeamId;
                        }
                    }
                    var winTeamOne = false;

                    if (matchData.Teams[0].TeamId == teamId)
                    {
                        if (matchData.Teams[0].Win == "Win")
                        {
                            winTeamOne = true;
                        }
                    }
                    else
                    if (matchData.Teams[1].TeamId == teamId)
                    {
                        if (matchData.Teams[1].Win == "Win")
                        {
                            winTeamOne = true;
                        }
                    }
                    matchToList.win = winTeamOne;
                    machesToReturn.Add(matchToList);
                }
                return Ok(machesToReturn.ToArray());
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e);
            }
        }
        private async Task<Match> GetMatchInfoFromApi(long matchId, string region)
        {
                Region regionEnum = Region.Get(region);
                var riotApi = RiotApi.NewInstance(riotApiKey);
                var matchData = await riotApi.MatchV4.GetMatchAsync(regionEnum, matchId);
                return matchData;
        }

        [HttpGet("GetMatchInfo/{userName}/{region}/{matchId}")]
        public async Task<IActionResult> GetMatchInfo(string userName, string region,int matchId)
        {
            try
            {
                Console.WriteLine("UserName:"+userName+" Region: "+region+" matchId:"+matchId);
                return Ok(await GetSummonerMatch(userName, region, matchId));
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}