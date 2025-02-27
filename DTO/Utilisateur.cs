using System;
using System.Collections.Generic;

namespace API.DTO;

public partial class UtilisateurDTO
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string MotDePasse { get; set; } = null!;
}
