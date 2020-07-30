using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class SearchResult
    {
#nullable disable
        public IEnumerable<Movie> Search { get; set; }
        public int TotalResults { get; set; }

        public int Pages => TotalResults / 10;
    }
}
