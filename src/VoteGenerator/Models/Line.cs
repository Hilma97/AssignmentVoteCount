using System;
using System.Globalization;

namespace VoteGenerator.Models
{
    public class Line
    {
        public static Line Parse(string line)
        {
            var data = line.Split(";");
            return new Line
            {
                Time = DateTime.Parse(data[0], CultureInfo.InvariantCulture),
                State = data[1],
                Votes = int.Parse(data[2]),
                Candidate = data[3]
            };       
        }

        public DateTime Time { get; set; }
        public string State { get; set; }
        public int Votes { get; set; }
        public string Candidate { get; set; }

        public override string ToString()
        {
            return $"{Time:u};{State};{Votes};{Candidate}";
        }
    }
}
