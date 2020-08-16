using MingweiSamuel.Camille.MatchV4;

namespace ApiForRiot.Dtos
{
    public class MatchOne : MatchReference
    {
        public bool Win { get; set; }
        public long gameDuration  { get; set; }
        public string gameMode { get; set; }
    }
}