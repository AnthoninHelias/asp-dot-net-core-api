using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Carte
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Rareté { get; set; } = null!;
}
