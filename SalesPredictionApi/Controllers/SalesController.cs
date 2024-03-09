using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace SalesPredictionApi.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        private ISalesCalculator salesCalculator;

        public SalesController()
        {
            salesCalculator = new SalesCalculator();
        }

       
        [HttpGet("ads/{id}")]
        [ProducesResponseType<double>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Results<NotFound, Ok<double>> GetADS(int id)
        {
            var ads = salesCalculator.CalculateADS(id);
            if (ads == double.NaN)
                return TypedResults.NotFound();

            return TypedResults.Ok(ads);
        }

        [HttpGet("prediction/{id}/{days}")]
        public Results<NotFound, Ok<double>> GetSalesPrediction(int id, int days)
        {
            var pred = salesCalculator.CalculateSalesPrediction(id, days);

            if (pred == double.NaN)
                return TypedResults.NotFound();

            return TypedResults.Ok(pred);
        }

        [HttpGet("demand/{id}/{days}")]
        public Results<NotFound, Ok<double>> GetDemand(int id, int days)
        {
            var demand = salesCalculator.CalculateSalesPrediction(id, days);

            if (demand == double.NaN)
                return TypedResults.NotFound();

            return TypedResults.Ok(demand);
        }
    }
}