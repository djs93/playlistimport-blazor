using System.Globalization;
using CsvHelper;

namespace playlistimport_blazor.Utilities;

public class CsvWrite
{
    public static void WriteListToCsv<T>(List<T> list, string filepath)
    {
        using (var writer = new StreamWriter(filepath))
        using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csvWriter.WriteRecords(list);
        }
        Console.WriteLine("Done Writing");
    }
}