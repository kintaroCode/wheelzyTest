

using(var context = new wheelzyTest.Modelos.WheelzyDbsContext())
{
    var data = context.StatusLogs.Select(c =>
        new
        {            
            CarId = c.Car.CarId,
            Year = c.Car.Year,
            Plate = c.Car.Plate,
            LocationId = c.Car.LocationId,
            Make = c.Car.Make,
            Model = c.Car.Model,
            Submodel = c.Car.Submodel,
            BuyerName = c.Buyer.Name,
            Amount = c.Buyer.Amounts.OrderByDescending(a => a.BuyerId).FirstOrDefault(),
            c.Status,
            c.StatusDate,
        }).ToList();   

    foreach (var element in data)
    {
        Console.WriteLine($"""
            Card ID: {element.CarId},
            Year: {element.Year}, 
            Plate: {element.Plate}  
            Location: {element.LocationId},
            Make: {element.Make}, 
            Model: {element.Model},
            Submodel: {element.Submodel}, 
            BuyerName: {element.BuyerName}, 
            Amount: {element.Amount}, 
            Status: {element.Status}, 
            StatusDate: {element.StatusDate}           
            """);
    }
}

