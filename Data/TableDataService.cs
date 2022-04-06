namespace playlistimport_blazor.Data;

public class TableDataService
{
    private static List<Song> _songs = new List<Song>();
    private static List<Song>? _filteredSongs;
    private static List<string> _genres = new List<string>();
    private static readonly Song NoResultsSong = new Song()
    {
        Name = "No results found",
        Artist = "",
        Genre = "",
        Year = new DateOnly(1,1,1)
    };
    
    public static Task<List<Song>> GetSongsAsync()
    {
        if (_filteredSongs == null)
        {
            return Task.FromResult(_songs);
        }

        if (_filteredSongs.Count == 0)
        {
            return Task.FromResult(new List<Song>() { NoResultsSong });
        }
        return Task.FromResult(_filteredSongs);
    }
    
    public static Task<List<string>> GetGenresAsync()
    {
        return Task.FromResult(_genres);
    }

    public static Task<List<Song>> SetSongsAsync(List<Song> songs)
    {
        _songs = songs;
        _genres = _songs.Select(x => x.Genre).Distinct().ToList();
        _genres.Sort();
        _genres.Insert(0, "");
        return Task.FromResult(_songs);
    }
    public static Task<List<Song>> SetFilteredSongsAsync(List<Song>? songs)
    {
        _filteredSongs = songs;
        _genres = _songs.Select(x => x.Genre).Distinct().ToList();
        _genres.Sort();
        _genres.Insert(0, "");
        return Task.FromResult(_songs);
    }
    
    public static Task<List<Song>> ClearFiltersAsync()
    {
        _filteredSongs = null;
        _genres = _songs.Select(x => x.Genre).Distinct().ToList();
        _genres.Sort();
        _genres.Insert(0, "");
        return Task.FromResult(_songs);
    }
    
    public static Task<List<Song>> ApplyFiltersAsync(FilterData data)
    {
        var filteredSongs = _songs;
        if (string.IsNullOrWhiteSpace(data.Artist) && string.IsNullOrWhiteSpace(data.Genre) && data.Year < 0 && data.NumberOfSongs == 0)
        {
            ClearFiltersAsync();
            return Task.FromResult(_songs);
        }
        
        if (!string.IsNullOrWhiteSpace(data.Genre))
        {
            filteredSongs = filteredSongs.Where(x => x.Genre.Equals(data.Genre)).ToList();
            Console.WriteLine("Filtered by genre: " + data.Genre);
        }
        if (!string.IsNullOrWhiteSpace(data.Artist))
        {
            filteredSongs = filteredSongs.Where(x => x.Artist.ToLower().Contains(data.Artist.ToLower())).ToList();
            Console.WriteLine("Filtered by artist: " + data.Artist);
        }
        if (data.Year > 0)
        {
            filteredSongs = filteredSongs.Where(x => x.Year.Year == data.Year).ToList();
            Console.WriteLine("Filtered by year: " + data.Year);
        }
        if (data.NumberOfSongs > 0)
        {
            filteredSongs = filteredSongs.Take(data.NumberOfSongs).ToList();
            Console.WriteLine("Filtered by number of songs: " + data.NumberOfSongs);
        }
        _filteredSongs = filteredSongs;
        return Task.FromResult(filteredSongs);
    }
}