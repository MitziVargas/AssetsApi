using System;
using System.Collections.Generic;

#nullable disable

namespace AssetsApi.Models
{
    public partial class Activo
    {
        public string Nombre { get; set; }
        public string Area { get; set; }
        public decimal Costo { get; set; }
        public int Codigo { get; set; }
    }
}
