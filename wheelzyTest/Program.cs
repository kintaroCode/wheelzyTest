

// pregunta numero 1
using System.Text.RegularExpressions;

using (var context = new wheelzyTest.Modelos.WheelzyDbsContext())
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
            Amount = c.Buyer.Amounts.FirstOrDefault(a => a.LocationId == c.Car.LocationId),
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
            Amount: {element.Amount?.Amount1}, 
            Status: {element.Status}, 
            StatusDate: {element.StatusDate}           
            """);
    }
}

//pregunta numero 5
int SearchFile(string Path)
{
    var files = Directory.GetFiles(Path, "*.cs", SearchOption.AllDirectories);
    int counter = 0;
    foreach (var file in files)
    {
        Console.WriteLine(file);
        string[] lineas = File.ReadAllLines(file);
        
        for (int i = 0; i < lineas.Length; i++)
        {
            if (lineas[i].Contains("async"))
            {
                lineas[i] = Regex.Replace(lineas[i], @"(\b\w+)(?=\s*\()", "$1Async");
                counter++;
            }

            if (lineas[i].Contains("Vm"))
                lineas[i] = Regex.Replace(lineas[i], "Vm", "VM");
            
            if (lineas[i].Contains("Vms"))
                lineas[i] = Regex.Replace(lineas[i], "Vms", "VMS");

            if (lineas[i].Contains("Dto"))
                lineas[i] = Regex.Replace(lineas[i], "Dto", "DTO");
            
            if (lineas[i].Contains("Dtos"))
                lineas[i] = Regex.Replace(lineas[i], "Dtos", "DTOs");
              
        }       
        File.WriteAllLines(file, lineas);
    }

    return counter;
}

//agregue una ubicacion puedes cambiarla por la que desees y que tenga archivos .cs
Console.WriteLine(SearchFile("C:\\Users\\User\\Desktop\\isrrael\\imagenes para proyectos"));

