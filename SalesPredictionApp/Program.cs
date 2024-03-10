ISalesCalculator salesCalculator = new SalesCalculator();

if (args.Length < 2)
{
    Console.WriteLine($"ads <product ID>: Calculates product ADS {Environment.NewLine}" +
                  $"prediction <product ID> <number of days>: Calculates product sales prediction {Environment.NewLine}" +
                  $"demand <product ID> <amount of days>: Calculates product demand for purchase");
    return;
}

string command = args[0];
if (!int.TryParse(args[1], out int id))
{
    Console.WriteLine("Product ID must be an integer!");
    return;
}

int days = 0;
if (args.Length > 2 && !int.TryParse(args[2], out days))
{
    Console.WriteLine("Number of days must be an integer!");
    return;
}

switch (command.ToLower())
{
    case "ads":
        var ads = salesCalculator.CalculateADS(id);
        if (ads == double.NaN)
        {
            Console.WriteLine("Such ID does not exist in salesData.txt");
            return;
        }

        Console.WriteLine($"ADS for product {id}: {ads}");
        break;
    case "prediction":
        
        if (days <= 0)
        {
            Console.WriteLine("Days should be greater than 0 for prediction.");
            return;
        }
        var prediction = salesCalculator.CalculateSalesPrediction(id, days);
        if (prediction == double.NaN)
        {
            Console.WriteLine("Such ID does not exist in seasonCoefs.txt");
            return;
        }

        Console.WriteLine($"Sales prediction for product {id} in {days} days: {prediction}");
        break;
    case "demand":
        if (days <= 0)
        {
            Console.WriteLine("Days should be greater than 0 for demand calculation.");
            return;
        }
        var demand = salesCalculator.CalculateDemand(id, days);
        if (demand == double.NaN)
        {
            Console.WriteLine("Such ID does not exist in seasonCoefs.txt");
            return;
        }

        Console.WriteLine($"Demand for product {id} in {days} days: {demand}");
        break;
    default:
        Console.WriteLine("Invalid command.");
        break;
}