using CommandLine;
using System;
using System.Collections.Generic;

namespace VoteCount
{
    public class Options
    {
        [Option('i', "input file", Required = false, Default = "resources/votes.csv", HelpText = "File to read from.")]
        public string Input { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                          .WithParsed(Run)
                          .WithNotParsed(Errors);
        }

        public static void Run(Options options)
        {
            throw new NotImplementedException();
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
