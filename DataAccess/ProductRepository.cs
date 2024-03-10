public class ProductRepository : IProductRepository
{
    private const string salesDataFile = "DataFiles/salesData.txt";
    private const string seasonCoefsFile = "DataFiles/seasonCoefs.txt";

    private static readonly Dictionary<int, List<Sale>> Sales;
    private static readonly Dictionary<int, List<SeasonCoef>> SeasonCoefs;
    static ProductRepository()
    {
        Sales = DataReader.ReadSalesData(salesDataFile);
        SeasonCoefs = DataReader.ReadSeasonCoefs(seasonCoefsFile);
    }
    

    public List<Sale>? GetProductSales(int productId)
    {
        if (Sales.ContainsKey(productId))
            return Sales[productId];

        return null;
    }

    public List<SeasonCoef>? GetProductSeasonCoefs(int productId)
    {
        if (SeasonCoefs.ContainsKey(productId)) 
            return SeasonCoefs[productId];
       
        return null;
    }
}