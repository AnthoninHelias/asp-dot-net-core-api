using System;
using System.Collections.Generic;

namespace API.DTO;

public partial class BanlistDTO
{
    public int Id { get; set; }

    public int Nom { get; set; }

    public short Limitée { get; set; }

    public sbyte Interdite { get; set; }
}
