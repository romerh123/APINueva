using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIWEBR.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? CodigoBarra { get; set; }

    public string? Descripcion { get; set; }

    public string? Marca { get; set; }

    public int? Cantidad { get; set; }

    public int? IdCategoria { get; set; }

    public int? Precio { get; set; }
    [JsonIgnore]
    public virtual Categoria? oCategoria { get; set; }
}
