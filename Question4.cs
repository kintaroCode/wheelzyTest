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
   var orders = dbContext.order.Where( x => x.
}
