namespace playlistimport_blazor.Utilities;

public static class ListOperations
{
    public static List<T> RemoveDuplicates<T>(List<T> list)
    {
        var distinctItems = list.Distinct().ToList();
        return distinctItems;
    }
}