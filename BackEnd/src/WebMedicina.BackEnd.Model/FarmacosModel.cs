﻿using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

public partial class FarmacosModel : BaseModel
{
    public int IdFarmaco { get; set; }

    public string Nombre { get; set; } = null!;

}
