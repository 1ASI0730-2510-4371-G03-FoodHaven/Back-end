using System;
using System.ComponentModel.DataAnnotations.Schema;
using food_heaven_backend.Shared.Domain.Model.Entities;

namespace food_heaven_backend.DetalleEntregas.Domain.Models.Entities
{
    [Table("detalle_entrega")] // nombre real de la tabla
    public class DetalleEntrega : BaseEntity
    {
        [Column("id_entrega")]
        public new int Id { get; set; }

        [Column("id_pedido")]
        public int IdPedido { get; set; }

        [Column("direccion_entrega")]
        public string DireccionEntrega { get; set; }

        [Column("referencia")]
        public string? Referencia { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("hora")]
        public TimeSpan Hora { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        // 🔽 CONSTRUCTOR COMPLETO
        public DetalleEntrega(int idPedido, string direccionEntrega, string? referencia, DateTime fecha, TimeSpan hora, string estado)
        {
            IdPedido = idPedido;
            DireccionEntrega = direccionEntrega;
            Referencia = referencia;
            Fecha = fecha;
            Hora = hora;
            Estado = estado;
        }
    }
}