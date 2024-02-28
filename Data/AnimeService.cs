using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public class AnimeService
    {
        private readonly HttpClient _httpClient;

        public AnimeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Assuming an endpoint URL for listing anime
        private string listAnimeEndpoint = "https://api.jikan.moe/v4/top/anime";
        // Endpoint for searching anime
        private string searchAnimeEndpoint = "https://api.jikan.moe/v4/anime";

        public async Task<List<AnimeData>> GetAnimeListAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<AnimeResponse>(listAnimeEndpoint);
                return response?.Data ?? new List<AnimeData>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching anime list: {ex.Message}");
                return new List<AnimeData>();
            }
        }

        // New search method
        public async Task<List<AnimeData>> SearchAnimeAsync(string query)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<AnimeResponse>($"{searchAnimeEndpoint}?q={Uri.EscapeDataString(query)}");
                return response?.Data ?? new List<AnimeData>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching for anime: {ex.Message}");
                return new List<AnimeData>();
            }
        }
    }

    public class AnimeResponse
    {
        public List<AnimeData> Data { get; set; }
    }

    public class AnimeData
    {
        public int Mal_id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; } // Ensure you have a Title property as it's crucial for search results.
        public AnimeImages Images { get; set; }
        // Other properties as needed
    }

    public class AnimeImages
    {
        public AnimeImageType Jpg { get; set; }
        // Include Webp if needed
    }

    public class AnimeImageType
    {
        public string Image_url { get; set; }
        public string Small_image_url { get; set; }
        public string Large_image_url { get; set; }
    }
}
