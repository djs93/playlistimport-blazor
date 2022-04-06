using System.Text;

namespace playlistimport_blazor.Data;

public class CustomQueries
{
    public enum AvailableQueries
    {
        YearQuery,
        ArtistQuery,
        GenreQuery,
        TopQuery
    }

    private static readonly Dictionary<AvailableQueries, string> QueryNames = new Dictionary<AvailableQueries, string>()
    {
        {AvailableQueries.YearQuery, "Year"},
        {AvailableQueries.ArtistQuery, "Artist"},
        {AvailableQueries.GenreQuery, "Genre"},
        {AvailableQueries.TopQuery, "Top Songs from query"}
    };
    
    private static readonly Dictionary<AvailableQueries, Delegate> QueryFunctions = new Dictionary<AvailableQueries, Delegate>()
    {
        {AvailableQueries.YearQuery, SongByYear},
        {AvailableQueries.ArtistQuery, SongByArtist},
        {AvailableQueries.GenreQuery, SongByGenre},
        {AvailableQueries.TopQuery, TopSongs}
    };

    public static string GetAvailableQueries()
    {
        StringBuilder result = new StringBuilder();
        
        foreach ((AvailableQueries key, string? value) in QueryNames)
        {
            result.Append($"\t{(int)key}:{value}\n");
        }

        return result.ToString();
    }
    
    public static List<Song> SongByYear(List<Song> songs, FilterData data)
    {
        //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
        IEnumerable<Song> songQuery =
            from song in songs
            orderby song.Plays descending 
            where song.Year.Year == data.Year
            select song;

        var songQueryResults = songQuery.ToList();

        return songQueryResults;
    }

    
    public static List<Song> SongByArtist(List<Song> songs, FilterData data)
    {
        IEnumerable<Song> songQuery =
            from song in songs
            orderby song.Plays descending
            where song.Artist.ToLower().Contains(data.Artist.ToLower())
            select song;

        var songQueryResults = songQuery.ToList();

        return songQueryResults;
    }


    public static List<Song> SongByGenre(List<Song> songs, FilterData data)
    {
        IEnumerable<Song> songQuery =
            from song in songs
            orderby song.Plays descending
            where song.Genre.ToLower().Equals(data.Genre.ToLower())
            select song;

        var songQueryResults = songQuery.ToList();

        return songQueryResults;
    }
    
    
    public static List<Song> TopSongs(List<Song> songs, FilterData data)
    {
        IEnumerable<Song> songQuery =
            from song in songs
            orderby song.Plays descending
            select song;

        var songQueryResults = songQuery.Take(data.NumberOfSongs).ToList();

        return songQueryResults;
    }
    

    public static List<Song>? RunSongQueries(List<AvailableQueries> queryTypes, List<Song>? songsToQuery, FilterData data)
    {
        return queryTypes.Aggregate(songsToQuery, (current, queryType) => RunQuery(queryType, current, data));
    }

    private static List<Song> RunQuery(AvailableQueries queryType, List<Song>? songsToQuery, FilterData data)
    {
        return (List<Song>)QueryFunctions[queryType].DynamicInvoke(songsToQuery, data)!;
    }
}