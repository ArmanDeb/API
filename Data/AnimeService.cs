using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace myApp.Data // Ensure this namespace matches your project structure
{
    public class AnimeService
    {
        private readonly HttpClient _httpClient;

        public AnimeService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<Anime>> GetAnimeListAsync()
        {
            // Endpoint for top anime list in Jikan API v4
            string apiUrl = "https://api.jikan.moe/v4/top/anime";

            try
            {
                // Fetch data from Jikan API
                var animeResponse = await _httpClient.GetFromJsonAsync<JikanApiResponse>(apiUrl);

                // Extract and return the anime list
                return animeResponse?.Top ?? new List<Anime>();
            }
            catch (Exception ex)
            {
                // Handle any errors
                Console.WriteLine($"Error fetching anime list: {ex.Message}");
                return new List<Anime>();
            }
        }
    }

    public class JikanApiResponse
    {
        public List<Anime> Top { get; set; }
    }

    public class Anime
    {
        public string Title { get; set; }
        public string ImageURL { get; set; }
        // Add other properties you want to display
    }
}
