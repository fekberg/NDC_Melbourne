using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Business
{
#nullable enable
    public class Movie
    {
        public string ImdbID { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string? Genre { get; set; }
        public string Runtime { get; set; } = string.Empty;
        public string Released { get; set; } = string.Empty;
        public string Poster { get; set; } = string.Empty;

        public int TotalMinutes => Convert.ToInt32(Runtime.Split(' ')[0]);

        public string PrettyGenre => GeneratePrettyGenre();

        private string GeneratePrettyGenre()
        {
            if (Genre == null) return string.Empty;

            var genres = Genre.Split(','); // Adventure, Crime, Action

            if(genres.Length >= 2)
            {
                return string.Join(",", genres.Take(2)) + ", ...";

                    // Adventure, Crime, ...
            }

            return Genre;
        }

        [return: NotNull]
        public int? Test([DisallowNull]string name)
        {
            var x = name.Length;

            return null;
        }
    }
}
