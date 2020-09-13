using MingweiSamuel.Camille.Enums;
using MingweiSamuel.Camille.MatchV4;

namespace ApiForRiot.Dtos
{
    public class MatchOne : MatchReference
    {
        public bool win { get; set; }
        public long gameDuration  { get; set; }
        public string gameMode { get; set; }
        public string championName { get; set; }
        public Match matchInfo {get; set;}
    }
}