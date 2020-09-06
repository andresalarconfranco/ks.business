using System;
using System.Collections.Generic;
using System.Text;

namespace Ks.PayManager.Entities.Bpms
{
    public class ProcesoDetalle
    {
        public string UserName { get; set; }

        public string Pws { get; set; }

        public string NombreProceso { get; set; }

        public string Version { get; set; }

        public string NombreTarea { get; set; }

        public string IdTarea { get; set; }

        public OrderObj objectordenInput { get; set; }
    }
}
