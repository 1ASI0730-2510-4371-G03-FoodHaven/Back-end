namespace food_heaven_backend.FoodCatalogContext.Domain.Models.Commands;

public class UpdateComidaCommand
{
    public int Id { get; init; }
    public string Nombre { get; }
    public string Descripcion { get; }
    public int Calorias { get; }
    public int Id_TipoComida { get; }
    public int Id_proveedor { get;  }
    public bool EsEspecial { get; }

    public UpdateComidaCommand(int Id, string Nombre, string Descripcion, int Calorias, int Id_TipoComida, int Id_proveedor, bool EsEspecial)
    {
        this.Id = Id;
        this.Nombre = Nombre;
        this.Descripcion = Descripcion;
        this.Calorias = Calorias;
        this.Id_TipoComida = Id_TipoComida;
        this.Id_proveedor = Id_proveedor;
        this.EsEspecial = EsEspecial;
    }

}