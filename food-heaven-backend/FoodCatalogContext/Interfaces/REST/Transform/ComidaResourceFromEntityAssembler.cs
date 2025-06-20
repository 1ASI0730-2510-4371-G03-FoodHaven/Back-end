using food_heaven_backend.FoodCatalogContext.Domain.Models.Entities;
using food_heaven_backend.FoodCatalogContext.Interfaces.REST.Resources;

namespace food_heaven_backend.FoodCatalogContext.Interfaces.REST.Transform;

public static class ComidaResourceFromEntityAssembler
{
    public static ComidaResource ToResourceFromEntity(Comida comida)
    {
        return new ComidaResource
        (
            comida.Id,
            comida.Nombre,
            comida.Descripcion,
            comida.Calorias,
            comida.Id_TipoComida,
            comida.Id_Proveedor,
            comida.EsEspecial
        );
    }
}