using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Utilisateur
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string MotDePasse { get; set; } = null!;
}
