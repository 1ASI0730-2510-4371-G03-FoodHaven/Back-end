    using System.ComponentModel.DataAnnotations.Schema;
    using food_heaven_backend.Shared.Domain.Model.Entities;

    namespace food_heaven_backend.FoodCatalogContext.Domain.Models.Entities;

    [Table("comida")]
    public class Comida : BaseEntity
    {
        [Column("id_comida")]
        public new int Id { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;
        
        [Column("descripcion")]
        public string Descripcion { get; set; }= string.Empty;
        
        [Column("calorias")]
        public int Calorias { get; set; }
        
        
        [ForeignKey(nameof(Id_TipoComida))] 
        public TipoComida TipoComida { get; set; } = null!; // navegación opcional para después
        
        [Column("id_tipo_comida")]
        public int Id_TipoComida { get; set; }
        
        
        //FK
        [Column("id_proveedor")]
        public int Id_Proveedor { get; set; }
        
        [ForeignKey("Id_Proveedor")]
        public Proveedor Proveedor { get; set; } = null!; // <- esto es lo que necesitas
        
        [Column("es_especial")]
        public bool EsEspecial { get; set; }


        public Comida(string nombre, string descripcion, int calorias, int idTipocomida, int idProveedor, bool esEspecial)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Calorias = calorias;
            Id_TipoComida = idTipocomida;
            Id_Proveedor = idProveedor;
            EsEspecial = esEspecial;


        }

        public Comida()
        {
            
        }

    }


