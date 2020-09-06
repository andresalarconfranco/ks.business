using System;
using System.Collections.Generic;
using System.Text;

namespace Ks.Entities.Orders
{
    public class ItemsInfo
    {
        public int ItemId { get; set; }
        public string IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string NumeroParte { get; set; }//Repetir código
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
