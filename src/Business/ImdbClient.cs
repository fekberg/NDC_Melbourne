using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ImdbClient
    {
        private static readonly string API_KEY = "36e2efe6";
        private static readonly string DATA_ENDPOINT = $"http://www.omdbapi.com/?apikey={API_KEY}";

        private async Task<SearchResult> FindInternalAsync(string search, int page)
        {
            using var client = new HttpClient();

            var result = await client.GetAsync($"{DATA_ENDPOINT}&s={search}&page={page}");

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            var searchResult = JsonConvert.DeserializeObject<SearchResult>(content);

            return searchResult;
        }

        public async Task<IEnumerable<Movie>> FindAsync(string search)
        {
            var result = await FindInternalAsync(search, 1);

            return result.Search;
        }

        public async IAsyncEnumerable<IEnumerable<Movie>> FindAllAsync(string search)
        {
            var first = await FindInternalAsync(search, 1);

            yield return first.Search;

            if (first.Pages <= 1) yield break;

            for(int i = 2; i <= first.Pages; i++)
            {
                await Task.Delay(2000);

                var result = await FindInternalAsync(search, i);

                yield return result.Search;
            }
        }

        public async Task<Movie> DetailsAsync(string id)
        {
            using var client = new HttpClient();

            var result = await client.GetAsync($"{DATA_ENDPOINT}&i={id}");

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            var searchResult = JsonConvert.DeserializeObject<Movie>(content);

            return searchResult;
        }
    }
}
