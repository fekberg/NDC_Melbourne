using Business;
using System;

namespace WorkingWithPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            var movie = new Movie  { Genre = "Action", Year = "2020" };
            var movie1 = new Movie { Genre = "Comedy", Year = "2019" };
            var movie2 = new Movie { Genre = "Romance", Year = "2020" };
            var movie3 = new Movie { Genre = "Adventure", Year = "2018" };

            var tv = new TV { Title = "Test" };
        }

        static string GetTitle(OMDBEntity show)
        {
            var result = show switch
            {
                ("Harry Potter", _) => "I dont care about the genre",
                Movie { Year : "2019" } m => m.TotalMinutes.ToString(),
                Movie m => m.Title,
                TV t => t.Title,
                _ => throw new NotImplementedException()
            };

            var result2 = (show.Title, 100) switch
            {
                ("Harry Potter", 0) => "",
                ("Harry Potter", var x) => $"Harry says {x}",
            };

            return result;
        }
    }
}
