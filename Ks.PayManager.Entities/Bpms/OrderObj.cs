using System.Collections.Generic;

namespace Ks.PayManager.Entities.Bpms
{
    public class OrderObj
    {
        public string TipoCliente { get; set; }

        public decimal MontoOrden { get; set; }

        public string Destino { get; set; }

        public string OrderId { get; set; }

        public string NombreCliente { get; set; }

        public string ApellidoCliente { get; set; }

        public string Correo { get; set; }

        public string Pais { get; set; }

        public string Departamento { get; set; }

        public string Ciudad { get; set; }

        public string Barrio { get; set; }

        public string Direccion { get; set; }

        public string CodigoCiudad { get; set; }

        public string EstadoOrden { get; set; }

        public List<Product> Productos { get; set; }

        public string IdentificadorSolicitud { get; set; }
    }
}
