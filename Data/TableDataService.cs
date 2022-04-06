namespace playlistimport_blazor.Data;

public class TableDataService
{
    private static List<Song> _songs = new List<Song>();
    private static List<string> _genres = new List<string>();
    
    public static Task<List<Song>> GetSongsAsync()
    {
        return Task.FromResult(_songs);
    }
    
    public static Task<List<string>> GetGenresAsync()
    {
        return Task.FromResult(_genres);
    }

    public static Task<List<Song>> SetSongsAsync(List<Song> songs)
    {
        _songs = songs;
        _genres = _songs.Select(x => x.Genre).Distinct().ToList();
        return Task.FromResult(_songs);
    }
}