using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace playlistimport_blazor.Data.Converters;

public class CustomIntConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (text != "")
        {
            return int.Parse(text);
        }
        else
        {
            return 0;
        }
    }
}