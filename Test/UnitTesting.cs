using Bunit;
using Xunit;
using MyApp.Services;
using Blazored.LocalStorage;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AnimeServiceTests
{

    public class InMemoryLocalStorageService : ILocalStorageService
{
    private readonly Dictionary<string, object> _storage = new Dictionary<string, object>();

        public event EventHandler<ChangingEventArgs> Changing;
        public event EventHandler<ChangedEventArgs> Changed;

        public ValueTask ClearAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> ContainKeyAsync(string key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask<string?> GetItemAsStringAsync(string key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask<T?> GetItemAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask<string?> KeyAsync(int index, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask<IEnumerable<string>> KeysAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask<int> LengthAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask RemoveItemAsync(string key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask RemoveItemsAsync(IEnumerable<string> keys, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask SetItemAsStringAsync(string key, string data, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask SetItemAsync<T>(string key, T data, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        // Implement the methods of the ILocalStorageService interface
        // ...
    }

    [Fact]
    public async Task Adding_Anime_To_Favorites()
    {
        // Arrange
        var localStorageService = new InMemoryLocalStorageService(); // Using a fake in-memory local storage service.
        var httpClient = new HttpClient(); // No need for a fake HttpClient for this test.
        var animeService = new AnimeService(httpClient, localStorageService);
        
        int testAnimeId = 1; // Example anime ID to test toggling

        // Act
        await animeService.ToggleFavoriteAsync(testAnimeId); // Add
        var favorites = await animeService.GetFavoritesAsync();

        // Assert
        Assert.Contains(testAnimeId, favorites); // Check if the ID was added to favorites
    }

    [Fact]
    public async Task Removing_Anime_From_Favorites()
    {
        // Arrange
        var localStorageService = new InMemoryLocalStorageService(); // Using a fake in-memory local storage service.
        var httpClient = new HttpClient(); // No need for a fake HttpClient for this test.
        var animeService = new AnimeService(httpClient, localStorageService);
        
        int testAnimeId = 1; // Example anime ID to test toggling

        // Adding the anime to favorites initially
        await animeService.ToggleFavoriteAsync(testAnimeId);

        // Act
        await animeService.ToggleFavoriteAsync(testAnimeId); // Remove
        var favorites = await animeService.GetFavoritesAsync();

        // Assert
        Assert.DoesNotContain(testAnimeId, favorites); // Check if the ID was removed from favorites
    }
}