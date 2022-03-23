using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace playlistimport_blazor.Data.Converters;

//converting for year
public class CustomDateYearConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (text != "")
        {
            if (text.Contains('-'))
            {
                var split = text.Split('-');
                var year = int.Parse(split[0]);
                var month = int.Parse(split[1]);
            
                var date = new DateOnly(year, month, 1);
                return date;
            }
            else
            {
                var year = int.Parse(text);
            
                var date = new DateOnly(year, 1, 1);
                return date;
            }
        }
        else
        {
            var date = new DateOnly(1, 1, 1);
            return date;
        }
    }
}