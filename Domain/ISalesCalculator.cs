public interface ISalesCalculator
{
    double CalculateADS(int prooductId);
    double CalculateSalesPrediction(int prooductId, int days);
    double CalculateDemand(int produvtId, int days);
}