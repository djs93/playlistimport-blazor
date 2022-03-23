using CsvHelper.Configuration;
using playlistimport_blazor.Data.Converters;

namespace playlistimport_blazor.Data;

public sealed class SongMap : ClassMap<Song>
{
    public SongMap()
    {
        Map(m => m.Name).Name("Name", "Track Name");
        Map(m => m.Artist).Name("Artist", "Artist Name(s)");
        Map(m => m.Composer).Optional();
        Map(m => m.Genre).Optional();
        Map(m => m.Year).Name("Year", "Album Release Date").TypeConverter<CustomDateYearConverter>();
        Map(m => m.Plays).TypeConverter<CustomIntConverter>().Optional();
        Map(m => m.Popularity).TypeConverter<CustomIntConverter>().Optional();
    }
}