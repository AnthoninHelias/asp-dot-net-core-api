using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Banlist
{
    public int Id { get; set; }

    public int Nom { get; set; }

    public short Limitée { get; set; }

    public sbyte Interdite { get; set; }
}
