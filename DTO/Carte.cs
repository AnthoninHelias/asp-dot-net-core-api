using System;
using System.Collections.Generic;

namespace API.DTO;

public partial class CarteDTO
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Rareté { get; set; } = null!;
}
