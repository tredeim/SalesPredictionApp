public class SalesCalculator : ISalesCalculator
{
    public double CalculateADS(int productId)
    {
        IProductRepository productRepository = new ProductRepository();
        var productSales = productRepository.GetProductSales(productId);
        if (productSales == null)
            return double.NaN;

        var filteredSales = productSales.Where(s => s.Stock > 0);
        if (!filteredSales.Any())
            return 0;

        return filteredSales.Average(s => s.Sales);
    }

    public double CalculateSalesPrediction(int productId, int days)
    {
        IProductRepository productRepository = new ProductRepository();
        var productSeasonCoefs = productRepository.GetProductSeasonCoefs(productId);
        if (productSeasonCoefs == null || days <= 0)
            return double.NaN;

        var ads = CalculateADS(productId);
        if (ads == double.NaN)
            return double.NaN;

        var futureCoef = GetFutureSeasonCoef(productId, days);
        if (futureCoef == double.NaN)
            return double.NaN;

        return ads * days * futureCoef;
    }

    public double CalculateDemand(int productId, int days)
    {
        IProductRepository productRepository = new ProductRepository();
        var productSales = productRepository.GetProductSales(productId);
        if (productSales == null || days <= 0)
            return double.NaN;

        var prediction = CalculateSalesPrediction(productId, days);
        if (prediction == double.NaN)
            return double.NaN;

        var currentStock = productSales.OrderByDescending(s => s.Date).FirstOrDefault()?.Stock ?? 0 - productSales.OrderByDescending(s => s.Date).FirstOrDefault()?.Sales ?? 0; ;

        return prediction - currentStock;
    }

    private double GetFutureSeasonCoef(int productId, int days)
    {
        IProductRepository productRepository = new ProductRepository();
        var productSeasonCoefs = productRepository.GetProductSeasonCoefs(productId);
        if (productSeasonCoefs == null || days <= 0)
            return double.NaN;

        var futureDate = DateTime.Now.AddDays(days);
        var futureMonth = futureDate.Month;

        var futureCoef = productSeasonCoefs.FirstOrDefault(c => c.Month == futureMonth);

        // If no coefficient found for the future month, assume no seasonality effect.
        if (futureCoef == null)
            return 1; 

        return futureCoef.Coef;
    }
}