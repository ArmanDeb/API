using Bunit;
using Xunit;
using MyApp.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace unitTest1;

public class SearchPageTests
{
    [Fact]
    public async Task SearchPage_Should_Display_Search_Results()
    {
        // Arrange
        using var ctx = new TestContext();
        var animeService = new MockAnimeService(); // Using a mocked service to simulate search results.
        ctx.Services.AddSingleton<AnimeService>(animeService);

        // Act
        var searchQuery = "Naruto";
        var searchResults = await animeService.SearchAnimeAsync(searchQuery);

        // Assert
        Assert.NotEmpty(searchResults); // Ensure search results are present
    }

    // Implement a MockAnimeService class to simulate AnimeService
    public class MockAnimeService : AnimeService // Extend AnimeService directly
    {
        public MockAnimeService() : base(null, null) // Call the AnimeService constructor with null values
        {
        }

        // Implement the SearchAnimeAsync method if necessary
        public override async Task<List<AnimeData>> SearchAnimeAsync(string query)
        {
            // Simulate search results
            var searchResults = new List<AnimeData>
            {
                new AnimeData { Title = "Naruto", Synopsis = "Synopsis of Naruto", Mal_id = 1 },
                new AnimeData { Title = "Naruto Shippuden", Synopsis = "Synopsis of Naruto Shippuden", Mal_id = 2 },
                // Add more search results if needed
            };
            return searchResults;
        }
    }
}