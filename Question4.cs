//Wheelzy Test

//Question 4
//¿datos de entrada?
//  a.- fecha de inicio y fin a modo de filtro (opcionales) periodos a tomar en cuenta 
// b.- lista de clientes propietario de las ordenes 
// c.- lista de statusIs para diferenciar los tipos de ordenes 
// d.- isActive posiblemente una flag que me diga ademas del statusIs que la orden ya no se toma en cuenta. 
// e.- todos los parametros pueden venir null ya que las listas tambien pueden pasar null.

//¿que hace el metodo?
// entrega una lista filtrada segun los parametros de entrada

//restricciones = usar EF . 
public async Task<List<OrderDTO>> GetOrders(DateTime? dateFrom, DateTime?
                                            dateTo, List<int> customerIds, 
                                            List<int> statusIds, bool? isActive)
{
// your implementation
// iria de los mas general a lo mas especifico CurstgomerIds, statusIds, IsActive, datefrom, DateTo
   var orders = dbContext.order;
    if(customerIds is not null)
      orders = orders.where(x=> customerIds.contains(x.customerId))

    if(statusIds is not null)
      orders = orders.where(x=> statusIds.contains(x.statusIds))

    if(isActive == true)
      orders =orders.where(x => x.isActive == isActive)
      
    if(dateFrom is not null)
      orders = orders.Where(x => x.date >= dateFrom)

    if(dateTo is not null)
       orders = orders.Where(x => x.date <= dateTo)
      
  // retornar en OrderDTO que puede ser objeto predefinido
      return await orders.select(x => new orderDTO{
           OrderId = x.OrderId,
           Date = x.Date,
           CustomerId = x.CustomerId,
           StatusId = x.StatusId,
           IsActive = x.IsActive
      }
  
}
