using System.Globalization;
using System.Text;

GenerateSalesHistory("../../../../DataFiles/salesData.txt", 10, 30);

GenerateSeasonalityCoefficients("../../../../DataFiles/seasonCoefs.txt", 10);

static void GenerateSalesHistory(string filePath, int numberOfProducts, int numberOfDays)
{
    Random random = new Random();
    StringBuilder sb = new StringBuilder();
    sb.AppendLine("id, date, sales, stock");
    for (int i = 0; i < numberOfDays; i++)
    {
        DateTime currentDate = DateTime.Today.AddDays(-i);
        foreach (var productId in Enumerable.Range(1, numberOfProducts))
        {
            int sales = random.Next(20);
            int stock = random.Next(100);
            sb.AppendLine($"{productId}, {currentDate.ToString("yyyy-MM-dd")}, {sales}, {stock}");
        }
    }
   
    File.WriteAllText(filePath, sb.ToString());
}

static void GenerateSeasonalityCoefficients(string filePath, int numberOfProducts)
{
    var random = new Random();
    var sb = new StringBuilder();
    sb.AppendLine("id, month, coef");
    for (int productId = 1; productId <= numberOfProducts; productId++)
    {
        for (int month = 1; month <= 12; month++)
        {
            var coefficient = Math.Round(random.NextDouble() * 3, 2);
            sb.AppendLine($"{productId}, {month}, {coefficient.ToString(new NumberFormatInfo { NumberDecimalSeparator = "." })}");
        }
    }
  
    File.WriteAllText(filePath, sb.ToString());
}