using System;
using System.Collections.Generic;
using System.Text;

namespace Ks.Entities.Orders
{
    public class OrderInfo
    {
        public string TipoCliente { get; set; }
        public decimal MontoOrden{ get; set; } //Ajustar
        public string Correo { get; set; }
        public string Destino { get; set; }
        public string OrderId { get; set; } //Akistar Lógica
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string Pais { get; set; }
        public string Departamento { get; set; }
        public string Ciudad { get; set; }
        public string Barrio { get; set; }
        public string Direccion { get; set; }
        public string CodigoCiudad { get; set; }
        public string EstadoOrden { get; set; }
        public string ProveedorMercancia { get; set; }
        public List<ItemsInfo> Productos { get; set; }
    }
}
