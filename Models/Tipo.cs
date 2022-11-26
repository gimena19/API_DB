using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_DB.Models;

public partial class Tipo
{
    [JsonIgnore]
    public int IdTipo { get; set; }

    public string? Descripcion { get; set; }
    [JsonIgnore]

    public virtual ICollection<Contacto> Contactos { get; } = new List<Contacto>();
}
