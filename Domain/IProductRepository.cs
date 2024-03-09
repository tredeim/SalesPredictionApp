public interface IProductRepository
{
    List<Sale> GetProductSales(int productId);
    List<SeasonCoef> GetProductSeasonCoefs(int productId);
}
