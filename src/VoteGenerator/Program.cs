using CommandLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using VoteGenerator.Models;

namespace VoteGenerator
{
    public class Options
    {
        [Option('o', "output file", Required = true, HelpText = "File to output to.")]
        public string Output { get; set; }

        [Option('r', "final vote ratio", Required = false, Default = 0.513, HelpText = "Final vote ratio.")]
        public double Ratio { get; set; }

        [Option('c', "final vote count", Required = false, Default = 155506056, HelpText = "Final votes cast.")]
        public int Count { get; set; }

        [Option('u', "chunk size", Required = false, Default = 20000, HelpText = "Chunk size to use.")]
        public int ChunkSize { get; set; }
    }

    public class Program
    {
        private static readonly Random _randomizer = new Random();

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                          .WithParsed(Run)
                          .WithNotParsed(Errors);
        }

        public static void Run(Options options)
        {
            var settings = GetSettings();
            var remaining = options.Count;
            var candidates = settings.Candidates;
            var lines = new List<Line>();
            
            while(remaining > 0)
            {
                var chunk = Math.Min(GetChunkSize(options.ChunkSize), remaining);
                var date = GetDate(settings.Interval);
                var state = GetState(settings);
                
                var votes = (int)Math.Ceiling(chunk * options.Ratio);
                lines.Add(GetLine(date, state, votes, candidates[0]));

                votes = (int)Math.Floor(chunk * (1 - options.Ratio));
                lines.Add(GetLine(date, state, votes, candidates[1]));

                remaining -= chunk;
            }

            using (var file = File.OpenWrite(options.Output))
            using (var writer = new StreamWriter(file))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line.ToString());
                }
            }
        }

        private static Line GetLine(DateTime date, State state, int votes, Candidate candidate)
        {
            return new Line
            {
                Time = date,
                State = state.Abbreviation,
                Candidate = candidate.Party,
                Votes = votes
            };
        }

        private static DateTime GetDate(Interval interval)
        {
            var buffer = new byte[8];

            var min = interval.From.Ticks;
            var max = interval.To.Ticks;

            _randomizer.NextBytes(buffer);
            
            long temp = BitConverter.ToInt64(buffer, 0);

            return new DateTime(Math.Abs(temp % (max - min)) + min);
        }

        private static int GetChunkSize(int chunkSize)
        {
            return _randomizer.Next(1, chunkSize);
        }

        private static State GetState(Settings settings)
        {
            return settings.States[_randomizer.Next(0, settings.States.Length)];
        }

        private static Settings GetSettings()
        {
            try
            {
                var data = File.ReadAllText("resources/data.json");
                var settings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                };

                return JsonConvert.DeserializeObject<Settings>(data, settings);
            }
            catch (IOException)
            {
                return new Settings();
            }
        }

        private static void Errors(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error.ToString());
            }
        }
    }
}
