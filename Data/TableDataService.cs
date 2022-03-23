namespace playlistimport_blazor.Data;

public class TableDataService
{
    private static List<Song> Songs = new List<Song>();
    
    public Task<List<Song>> GetSongsAsync()
    {
        return Task.FromResult(Songs);
    }

    public Task<List<Song>> SetSongsAsync(List<Song> songs)
    {
        Songs = songs;
        return Task.FromResult(Songs);
    }
}