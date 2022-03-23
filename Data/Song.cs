namespace playlistimport_blazor.Data;

public class Song
{
    public string Name { get; set; }
    public string Artist { get; set; }
    public string Composer { get; set; }
    public string Genre { get; set; }
    public DateOnly Year { get; set; }
    public int Plays { get; set; }
    public int Popularity { get; set; }

    public override bool Equals(object? other)
    {
        return Equals(other as Song);
    }

    //We'll consider songs "equal" if the name, artist, and year are the same
    protected bool Equals(Song? other)
    {
        if (other == null) return false;
        return Name == other.Name && Artist == other.Artist && Year.Equals(other.Year);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Artist, Year);
    }
}