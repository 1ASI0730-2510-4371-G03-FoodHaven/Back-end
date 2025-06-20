namespace food_heaven_backend.FoodCatalogContext.Interfaces.REST.Resources;

public record ComidaResource(int Id, string nombre, string descripcion, int calorias, int idTipocomida, int idProveedor, bool esEspecial)
{
    
}