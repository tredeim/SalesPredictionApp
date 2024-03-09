using System.Globalization;
using System.Linq;

public static class DataReader
{
    static string separator = ".";
    public static Dictionary<int, List<Sale>> ReadSalesData(string path)
    {
        var salesData = new Dictionary<int, List<Sale>>();

        try
        {
            using (var reader = new StreamReader(path))
            {
                reader.ReadLine();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var data = line.Split(", ");
                    var id = int.Parse(data[0]);
                    if (!salesData.ContainsKey(id))
                    {
                        salesData[id] = new List<Sale>();
                    }
                    salesData[id].Add(new Sale
                    {
                        ProductId = id,
                        Date = DateTime.Parse(data[1]),
                        Sales = int.Parse(data[2]),
                        Stock = int.Parse(data[3])
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading sales data: {ex.Message}");
        }

        return salesData;
    }

    public static Dictionary<int, List<SeasonCoef>> ReadSeasonCoefs(string path)
    {
        var seasonCoefs = new Dictionary<int, List<SeasonCoef>>();

        try
        {
            using (var reader = new StreamReader(path))
            {
                reader.ReadLine();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var data = line.Split(", ");
                    var id = int.Parse(data[0]);
                    if (!seasonCoefs.ContainsKey(id))
                    {
                        seasonCoefs[id] = new List<SeasonCoef>();
                    }
                    seasonCoefs[id].Add(new SeasonCoef
                    {
                        ProductId = id,
                        Month = int.Parse(data[1]),
                        Coef = double.Parse(data[2], new NumberFormatInfo { NumberDecimalSeparator = separator })
                    });
                }
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading seasonality coefficients: {ex.Message}");
        }

        return seasonCoefs;
    }
}