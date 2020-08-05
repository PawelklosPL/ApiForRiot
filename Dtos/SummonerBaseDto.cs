using System.ComponentModel.DataAnnotations;

namespace riotApi.Dtos
{
    public static class SummonerBaseDto
    {

    }
    public class GetBySummonerName
    {
        [Required]
        public string SummonerName { get; set; }
        [Required]
        public string Region { get; set; }
    }
}