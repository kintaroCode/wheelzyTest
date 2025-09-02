//Wheelzy Test

//Pregunta 3 .- mejorar --->  
public void UpdateCustomersBalanceByInvoices(List<Invoice> invoices)
{
  foreach (var invoice in invoices)
  {
    var customer = dbContext.Customers.SingleOrDefault(invoice.CustomerId.Value);
    customer.Balance -= invoice.Total;
    dbContext.SaveChanges();
  }
}

//¿qué hace el método ?
// a.- actualiza el balance en cuenta de los clientes. 

//¿qué datos necesita?
// a.- una lista de facturas con id de clientes. 
// b.- buscar los clientes a BD.
// c.- guardar el nuevo valor.

//¿problema principal?
//esta todo dentro de un foreach

//a -> sacaria la llamda de "var customer = dbContext.Customers.SingleOrDefault(invoice.CustomerId.Value)" 
//y "dbContext.SaveChanges" fuera del foreach para no llamar dos veces en cada  ocasiona a base de datos ;
//b.-> haria solo un savechages();
//c.-> No es necesario llamar a todos los clientes de la base, solo lo involucrados en las facturas. 

public void UpdateCustomersBalanceByInvoices(List<Invoice> invoices)
{
  var listInvoiceIds = invoices.select(x.CustomerId.HasValue).distinc.toList()
  
  var customer = dbContext.Customers.where(c => listInvoiceIds.Contains(c.CustomerId));
  
  foreach (var invoice in invoices)
  {
    if(customers.TryGetValue(invoice.CustomerId.Value, out var customer)){      
      customer.Balance -= invoice.Total;
    }
  } 

   dbContext.SaveChanges();
}

