namespace VoteGenerator.Models
{
    public class Settings
    {
        public Interval Interval { get; set; }
        public Candidate[] Candidates { get; set; }
        public State[] States { get; set; }
    }
}
