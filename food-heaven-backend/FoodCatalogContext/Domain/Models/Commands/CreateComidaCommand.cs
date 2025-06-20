public class CreateComidaCommand
{
    public string Nombre { get; init; }
    public string Descripcion { get; init; }
    public int Calorias { get; init; }
    public int Id_TipoComida { get; init; }
    public int Id_proveedor { get; init; }
    public bool EsEspecial { get; init; }

    public CreateComidaCommand(string Nombre, string Descripcion, int Calorias, int Id_TipoComida, int Id_proveedor, bool EsEspecial)
    {
        this.Nombre = Nombre;
        this.Descripcion = Descripcion;
        this.Calorias = Calorias;
        this.Id_TipoComida = Id_TipoComida;
        this.Id_proveedor = Id_proveedor;
        this.EsEspecial = EsEspecial;
    }
}